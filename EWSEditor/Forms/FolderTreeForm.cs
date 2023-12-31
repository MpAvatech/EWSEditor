using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EWSEditor.Common.Extensions;
using EWSEditor.Common.ServiceProfiles;
using EWSEditor.Logging;
using EWSEditor.PropertyInformation;
using EWSEditor.Resources;
using EWSEditor.Settings;
using Microsoft.Exchange.WebServices.Data;
using EWSEditor.Exchange;

namespace EWSEditor.Forms
{
    public partial class FolderTreeForm : BrowserForm
    {
        private const int ExchangeServiceImageIndex = 0;
        private const int FolderImageIndex = 1;
        private const int CalendarFolderImageIndex = 2;
        private const int ContactFolderImageIndex = 3;
        private const int TaskFolderImageIndex = 4;

        // EWSEditor:5554 Add a timer to the BeforeExpand event of the tree view to give the
        // NodeMouseDoubleClick event time to fire.  If it does, then don't expand.
        private const int DeferredTreeViewActionInterval = 100;
        private Timer deferredTreeViewActionTimer = null;
        private TreeViewAction? deferredTreeViewAction = null;

        private PropertySet folderNodePropertySet = new PropertySet(
            BasePropertySet.IdOnly,
            new PropertyDefinitionBase[] { FolderSchema.DisplayName, FolderSchema.ChildFolderCount });

        // MenuItems to add to the File menu
        private System.Windows.Forms.ToolStripMenuItem newExchangeServiceMenu = new ToolStripMenuItem();
        private System.Windows.Forms.ToolStripMenuItem openDefaultExchangeServiceMenu = new ToolStripMenuItem();
        private System.Windows.Forms.ToolStripMenuItem closeExchangeServiceMenu = new ToolStripMenuItem();
        private System.Windows.Forms.ToolStripSeparator fileSplitMenu1 = new ToolStripSeparator();
        private System.Windows.Forms.ToolStripMenuItem openProfileMenu = new ToolStripMenuItem();
        private System.Windows.Forms.ToolStripMenuItem saveProfileMenu = new ToolStripMenuItem();
        private System.Windows.Forms.ToolStripSeparator fileSplitMenu2 = new ToolStripSeparator();

        #region Constructors

        public FolderTreeForm()
        {
            InitializeComponent();
            this.InitializeFileMenu();
        }

        /// <summary>
        /// Initializes a new instance of the FolderTreeForm class and loads the 
        /// tree view based on the profile information passed in from the command line.
        /// </summary>
        /// <param name="profilePath">Path to the profile to load</param>
        public FolderTreeForm(string profilePath)
        {
            InitializeComponent();
            this.InitializeFileMenu();

            // Make sure the file exists
            if (!System.IO.File.Exists(profilePath))
            {
                DebugLog.WriteVerbose(string.Format("Leave: Profile path, {0}, does not exist", profilePath));
                return;
            }

            // Load the file into a ServiceProfile
            ServiceProfile profile = new ServiceProfile(profilePath, true, true);
            foreach (ServiceProfileItem item in profile.Items)
            {
                TreeNode serviceNode = this.AddServiceToTreeView(item.Service, false);

                foreach (FolderId folder in item.RootFolderIds)
                {
                    try
                    {
                        this.AddRootFolderToTreeView(item.Service, folder, serviceNode);
                    }
                    catch (Exception ex)
                    {
                        // Keep going through the loop after showing exception
                        ErrorDialog.ShowError(ex);
                        DebugLog.WriteVerbose("Handled exception when adding root folders to service.", ex);
                    }
                }
            }

            // Ensure that the root node is selected
            if (this.FolderTreeView.Nodes.Count > 0)
            {
                this.FolderTreeView.SelectedNode = this.FolderTreeView.Nodes[0];
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the current ExchangeService selected in the TreeView.  When
        /// CurrentService is set to an ExchangeService enable certain controls and
        /// when set to null, disable the controls.
        /// </summary>
        public new ExchangeService CurrentService
        {
            get
            {
                return base.CurrentService;
            }

            set
            {
                base.CurrentService = value;

                // Is there a CurrentService set?
                bool isCurrentService = base.CurrentService != null;

                // Clear the property details grid if there is no service
                if (!isCurrentService)
                {
                    FolderPropertyDetailsGrid.Clear();
                }

                // Enable the following controls when connected...
                this.saveProfileMenu.Enabled = isCurrentService;
                this.closeExchangeServiceMenu.Enabled = isCurrentService;
                
                this.openDefaultExchangeServiceMenu.Enabled = !isCurrentService;
                this.openProfileMenu.Enabled = !isCurrentService;
                this.newExchangeServiceMenu.Enabled = !isCurrentService;

                // Actions Menu
                this.mnuOpenItemById.Enabled = isCurrentService;
                this.mnuOpenFolderById.Enabled = isCurrentService;
                this.mnuResolveName.Enabled = isCurrentService;
                this.mnuFindAppointments.Enabled = isCurrentService;

                // View Menu
                this.mnuRefresh.Enabled = isCurrentService;
                this.mnuViewResetPropertySet.Enabled = isCurrentService;
                this.mnuViewConfigPropertySet.Enabled = isCurrentService;

                // Tools Menu
                this.mnuSynchronization.Enabled = isCurrentService;
                this.mnuNotification.Enabled = isCurrentService;
                this.mnuStreamingNotification.Enabled = isCurrentService && CurrentService.RequestedServerVersion.CompareTo(ExchangeVersion.Exchange2010_SP1) >=0;  // Only enable streaming notifications for 2k10 SP1+
                this.mnuDisplayDelegates.Enabled = isCurrentService;
                this.UserAvailabilityMenuItem.Enabled = isCurrentService;
                this.UserOofSettingsMenuItem.Enabled = isCurrentService;
                this.ConvertIdMenuItem.Enabled = isCurrentService;
            }
        }

        /// <summary>
        /// Gets the Timer used to defer TreeViewActions and initializes
        /// it if necessary.
        /// </summary>
        private Timer DeferredTreeViewActionTimer
        {
            get
            {
                if (this.deferredTreeViewActionTimer == null)
                {
                    this.deferredTreeViewActionTimer = new Timer();
                    this.DeferredTreeViewActionTimer.Tick += new EventHandler(this.DoDeferredTreeViewAction);
                    this.DeferredTreeViewActionTimer.Interval = DeferredTreeViewActionInterval;
                }

                return this.deferredTreeViewActionTimer;
            }
        }

        #region Private Static Methods

        private static TreeNode GetRootOfNode(TreeNode node)
        {
            if (node == null)
            {
                DebugLog.WriteVerbose("Leave: node is null");
                return null;
            }

            if (node.Parent != null)
            {
                return GetRootOfNode(node.Parent);
            }
            else
            {
                DebugLog.WriteVerbose("Leave: node.Parent is null");
                return node;
            }
        }

        private static Folder GetFolderFromNode(TreeNode node)
        {
            if ((node != null) && (node.Tag is RootFolderNodeTag))
            {
                return ((RootFolderNodeTag)node.Tag).FolderObject;
            }
            else if ((node != null) && (node.Tag is Folder))
            {
                return (Folder)node.Tag;
            }
            else
            {
                return null;
            }
        }

        private static void GetFolderNodeText(Folder folder, FolderId origFolderId, out string nodeText, out string nodeToolTip)
        {
            // Initialize output
            nodeText = string.Empty;
            nodeToolTip = string.Empty;

            // HACK? If we see the FolderName is MsgFolderRoot we know for sure we are doing the right
            // thing to return 'MsgFolderRoot' as the folder name.  However, if we opened this folder
            // by FolderId and just have an empty DisplayName then we are really just assuming this the
            // root folder.
            if (folder.Id.FolderName.HasValue && folder.Id.FolderName.Value == WellKnownFolderName.MsgFolderRoot)
            {
                nodeText = System.Enum.GetName(typeof(WellKnownFolderName), WellKnownFolderName.MsgFolderRoot);
            }
            else if (folder.DisplayName == null || folder.DisplayName.Length == 0)
            {
                nodeText = System.Enum.GetName(typeof(WellKnownFolderName), WellKnownFolderName.MsgFolderRoot);
            }
            else
            {
                nodeText = folder.DisplayName;
            }

            // If origFolderId is NULL then we're done, there's no root folder
            // naming to do
            if (origFolderId == null)
            {
                return;
            }

            // Root Folder Naming - mstehle 7/28/2009
            // Depending on how the user created the FolderId, we want to indicate in
            // the tree view if we can that the folder may be in another mailbox.  If
            // the FolderId is specified by WellKnownName with a MailboxAddress it is
            // easy to call out the mailbox.  If the FolderId is specified by id then
            // we can't tell if this folder is in the act as account's mailbox or not.
            //
            // Folder by Name with Mailbox = "FolderName [MailboxAddress]"
            // Folder by Name without Mailbox = "FolderName"
            // Folder by Id = "FolderName*"
            if (origFolderId.Mailbox != null && origFolderId.FolderName.HasValue)
            {
                nodeText = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "{0} [{1}]",
                    nodeText,
                    origFolderId.Mailbox.Address);

                nodeToolTip = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "WellKnownFolder '{0}' in the '{1}' mailbox.",
                    System.Enum.GetName(typeof(WellKnownFolderName), origFolderId.FolderName.Value),
                    origFolderId.Mailbox.Address);
            }
            else if (origFolderId.Mailbox == null && origFolderId.FolderName.HasValue)
            {
                nodeToolTip = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "WellKnownFolder '{0}' in the '{1}' mailbox.",
                    System.Enum.GetName(typeof(WellKnownFolderName), origFolderId.FolderName.Value),
                    folder.Service.GetActAsAccountName());
            }
            else if (!origFolderId.FolderName.HasValue)
            {
                nodeText = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "{0}*",
                    nodeText);

                nodeToolTip = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "Folder '{0}' was added by Id and could reside in another mailbox.",
                    nodeText);
            }
        }

        #endregion

        private void InitializeFileMenu()
        {
            this.newExchangeServiceMenu.Name = "mnuNewBinding";
            this.newExchangeServiceMenu.Size = new System.Drawing.Size(237, 22);
            this.newExchangeServiceMenu.Text = "New Exchange Service...";
            this.newExchangeServiceMenu.Click += new System.EventHandler(this.NewExchangeServiceMenu_Click);

            this.openDefaultExchangeServiceMenu.Name = "mnuOpenDefaultBinding";
            this.openDefaultExchangeServiceMenu.Size = new System.Drawing.Size(237, 22);
            this.openDefaultExchangeServiceMenu.Text = "Open Default Exchange Service";
            this.openDefaultExchangeServiceMenu.Click += new System.EventHandler(this.OpenDefaultExchangeServiceMenu_Click);

            this.closeExchangeServiceMenu.Name = "mnuCloseBinding";
            this.closeExchangeServiceMenu.Size = new System.Drawing.Size(237, 22);
            this.closeExchangeServiceMenu.Text = "Close Exchange Service";
            this.closeExchangeServiceMenu.Click += new System.EventHandler(this.CloseExchangeServiceButton_Click);

            this.openProfileMenu.Name = "mnuOpenProfile";
            this.openProfileMenu.Size = new System.Drawing.Size(237, 22);
            this.openProfileMenu.Text = "Open Services Profile...";
            this.openProfileMenu.Click += new System.EventHandler(this.OpenProfileMenu_Click);

            this.saveProfileMenu.Name = "mnuSaveProfile";
            this.saveProfileMenu.Size = new System.Drawing.Size(237, 22);
            this.saveProfileMenu.Text = "Save Services Profile...";
            this.saveProfileMenu.Click += new System.EventHandler(this.SaveProfileMenu_Click);

            int exit = this.mnuFile.DropDownItems.IndexOfKey(this.mnuExit.Name);
            this.mnuFile.DropDownItems.Insert(exit, this.fileSplitMenu2);
            this.mnuFile.DropDownItems.Insert(exit, this.saveProfileMenu);
            this.mnuFile.DropDownItems.Insert(exit, this.openProfileMenu);
            this.mnuFile.DropDownItems.Insert(exit, this.fileSplitMenu1);
            this.mnuFile.DropDownItems.Insert(exit, this.closeExchangeServiceMenu);
            this.mnuFile.DropDownItems.Insert(exit, this.openDefaultExchangeServiceMenu);
            this.mnuFile.DropDownItems.Insert(exit, this.newExchangeServiceMenu);
        }

        private void FolderTreeForm_Load(object sender, EventArgs e)
        {
            this.CurrentService = null;

            this.Text = string.Empty;
            this.mnuRefresh.Click += new EventHandler(this.RefreshMenu_Click);

            this.DefaultDetailPropertySet.BasePropertySet = BasePropertySet.FirstClassProperties;
        }

        #region File Menu

        /// <summary>
        /// Add a new service binding to the tree view
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void NewExchangeServiceMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ExchangeService service = null;
                DialogResult result = ServiceDialog.ShowDialog(ref service);

                if (result != DialogResult.OK)
                {
                    DebugLog.WriteVerbose(string.Format("Leave: ServiceDialog returned {0}", result));
                    return;
                }

                if (service == null)
                {
                    DebugLog.WriteVerbose(string.Format("Leave: ExchangeService is null"));
                    return;
                }

                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                TreeNode serviceNode = this.AddServiceToTreeView(service);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// Close the selected ExchangeService and remove it from the tree view
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void CloseExchangeServiceButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                if (this.FolderTreeView.SelectedNode == null)
                {
                    DebugLog.WriteVerbose("Leave: this.FolderTreeView.SelectedNode is null.");
                    return;
                }

                TreeNode rootNode = GetRootOfNode(FolderTreeView.SelectedNode);
                if (rootNode == null)
                {
                    DebugLog.WriteVerbose("Leave: Root node is null.");
                    return;
                }

                // Select the next sibling ExchangeService if one is available
                if (rootNode.NextNode != null)
                {
                    this.FolderTreeView.SelectedNode = rootNode.NextNode;
                }
                else 
                {
                    this.CurrentService = null;
                }

                rootNode.Remove();
                this.FolderPropertyDetailsGrid.Clear();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// Load the ExchangeServices and root folders to a ServiceProfile persist
        /// it to a file.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void SaveProfileMenu_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK) 
            {
                DebugLog.WriteVerbose(string.Format("Leave: SaveFileDialog result was {0}.", result));
                return; 
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                ServiceProfile profile = new ServiceProfile();
                TreeNode serviceNode = FolderTreeView.TopNode;
                while (serviceNode != null)
                {
                    ExchangeService service = (ExchangeService)serviceNode.Tag;

                    // Each Service in a ServiceProfile can have multiple root folders
                    List<FolderId> rootFolderIds = new List<FolderId>();
                    foreach (TreeNode rootFolderNode in serviceNode.Nodes)
                    {
                        if (rootFolderNode.Tag is RootFolderNodeTag)
                        {
                            rootFolderIds.Add(((RootFolderNodeTag)rootFolderNode.Tag).OriginalFolderId);
                        }
                    }

                    profile.AddServiceToProfile(service, rootFolderIds.ToArray());

                    // Go to the next ExchangeService node
                    serviceNode = serviceNode.NextNode;
                }

                profile.SaveProfile(saveFileDialog.FileName);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Open a profile to load the ExchangeServices and root folders 
        /// into the FolderTreeView
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OpenProfileMenu_Click(object sender, EventArgs e)
        {
            try
            {
                // Warn the user if there is at least one ExchangeServices listed
                if (FolderTreeView.Nodes.Count > 1)
                {
                    DialogResult msgResult = MessageBox.Show(
                        "Opening a Service Profile will close all current ExchangeServices.  Do you want to continue?",
                        "EWS Editor - Open Profile",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);

                    if (msgResult == DialogResult.Yes)
                    {
                        this.CurrentService = null;
                        this.FolderTreeView.Nodes.Clear();
                        this.FolderPropertyDetailsGrid.Clear();
                    }
                    else
                    {
                        DebugLog.WriteVerbose("Leave: Don't open ServiceProfile and close existing ExchangeServices.");
                        return;
                    }
                }

                // Get the file path to open, if the user cancels bail out
                DialogResult fileResult = this.openFileDialog.ShowDialog();
                if (fileResult != DialogResult.OK) 
                {
                    DebugLog.WriteVerbose(string.Format("Leave: OpenFileDialog result was {0}.", fileResult));
                    return; 
                }

                if (!System.IO.File.Exists(this.openFileDialog.FileName))
                {
                    DebugLog.WriteVerbose(string.Format("Leave: OpenFileDialog file name, {0}, does not exist.", this.openFileDialog.FileName));
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ServiceProfile profile = new ServiceProfile(this.openFileDialog.FileName, true, true);
                foreach (ServiceProfileItem item in profile.Items)
                {
                    TreeNode serviceNode = this.AddServiceToTreeView(item.Service, false);
                    foreach (FolderId folder in item.RootFolderIds)
                    {
                        try
                        {
                            this.AddRootFolderToTreeView(item.Service, folder, serviceNode);
                        }
                        catch (Exception ex)
                        {
                            ApplicationException appEx = new ApplicationException(
                                string.Format(
                                    System.Globalization.CultureInfo.CurrentCulture, 
                                    "Error adding a root folder to tree view. Service: {0} Folder: {1}", 
                                    serviceNode.Text, 
                                    folder.ToString()),
                                ex);
                            ErrorDialog.ShowError(appEx);

                            DebugLog.WriteVerbose("Handled exception when adding root folders to service.", ex);
                        }
                    }
                }

                // Select the first node
                this.FolderTreeView.SelectedNode = this.FolderTreeView.TopNode;
            }
            catch
            {
                // If we fail anywhere in here clear the tree view and bail out
                this.CurrentService = null;
                this.FolderTreeView.Nodes.Clear();
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Open an ExchangeService with no input
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OpenDefaultExchangeServiceMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EwsProxyFactory.InitializeWithDefaults();
                ExchangeService service = EwsProxyFactory.CreateExchangeService();

                TreeNode newNode = this.AddServiceToTreeView(service);
                this.FolderTreeView.SelectedNode = newNode;
                this.CurrentService = service;
            }
            catch
            {
                // If we fail anywhere in here clear the tree view and bail out
                FolderTreeView.Nodes.Clear();
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region TreeView Events

        private void FolderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
             
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Set the server version and update the status bar...
                this.BindSelectedNode();


                // Specific folder type configurations
                Folder oFolder = (GetFolderFromNode(e.Node));
                if (oFolder != null)
                {
                    // Calendar folders get the calendar view folder
                    if (oFolder.FolderClass == "IPF.Appointment")
                    {
                        mnuFolderCalendarView.Visible = true;
                        toolStripMenuItem14.Visible = true;
                    }
                    else
                    {
                        mnuFolderCalendarView.Visible = false;
                        toolStripMenuItem14.Visible = false;
                    }
                    oFolder = null;
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FolderTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.FolderTreeView.SelectedNode != e.Node)
                {
                    this.FolderTreeView.SelectedNode = e.Node;
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// If the user double clicks a node display the 'regular' folder 
        /// contents, *NOT* to expand/collapse the TreeViewNode.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void FolderTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Folder folder = GetFolderFromNode(e.Node);
            if (folder != null)
            {
                DebugLog.WriteVerbose(string.Format("Double click on '{0}' node, cancelling deferred actions and showing contents", e.Node.Text));

                // Cancel any deferred actions
                this.deferredTreeViewAction = null;

                FolderContentForm.Show(
                    string.Format(DisplayStrings.TITLE_CONTENTS_FOLDER, folder.DisplayName),
                    folder,
                    ItemTraversal.Shallow,
                    this.CurrentService, 
                    this);
            }
        }

        /// <summary>
        /// Double clicking a folder tree node should display the contents table,
        /// *NOT* expand the node.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">TreeViewCancelEventArgs show us which node is attempting to expand.</param>
        private void FolderTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Only defer actions for nodes representing a folder.
            if (GetFolderFromNode(e.Node) != null)
            {
                this.DeferTreeViewAction(e);
                DebugLog.WriteVerbose(string.Format("TreeViewAction, {0}, {1} cancelled and deferred.", e.Action, e.Cancel ? "was" : "was not"));
            }


            //Folder oFolder = (GetFolderFromNode(e.Node));
            //if (oFolder != null)
            //{
            //    if (oFolder.FolderClass == "IPF.Appointment")
            //        mnuFolderCalendarView.Visible = true;
            //    else
            //        mnuFolderCalendarView.Visible = false;
            //    oFolder = null;
            //}
 
        }

        /// <summary>
        /// Double clicking a folder tree node should display the contents table,
        /// *NOT* collapse the node.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">TreeViewCancelEventArgs show us which node is attempting to expand.</param>
        private void FolderTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            // Only defer actions for nodes representing a folder.
            if (GetFolderFromNode(e.Node) != null)
            {
                this.DeferTreeViewAction(e);
                DebugLog.WriteVerbose(string.Format("TreeViewAction, {0}, {1} cancelled and deferred.", e.Action, e.Cancel ? "was" : "was not"));
            }
        }

        #endregion

        #region View Menu

        /// <summary>
        /// Rebind to the selected node in the TreeView
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void RefreshMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                this.BindSelectedNode();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        #endregion

        #region Right Click Context Menu


        private void AddExtendedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);

            ExtendedPropertyDefinition propDef = null;
            if (ExtendedPropertyDialog.ShowDialog(ref propDef) == DialogResult.OK)
            {
                folder.SetExtendedProperty(propDef, "Blah");
                folder.Update();
            }
        }

        private void EditExchangeServiceMenu_Click(object sender, EventArgs e)
        {
            ExchangeService service = this.CurrentService;
            if (ServiceDialog.ShowDialog(ref service) == DialogResult.OK)
            {
                if (!service.IsEqual(this.CurrentService))
                {
                    // TODO: What to do if the ExchangeService was changed?
                }
            }
        }

        /// <summary>
        /// ExchangeService node context menu option to add a
        /// root folder to the view.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void AddRootFolderMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                FolderId folderId = null;
                if (FolderIdDialog.ShowDialog(ref folderId) == DialogResult.OK)
                {
                    this.AddRootFolderToTreeView(folderId, FolderTreeView.SelectedNode);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Open the FolderContentsForm for the contents of the
        /// selected folder.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OpenContentsMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                FolderContentForm.Show(
                    string.Format(DisplayStrings.TITLE_CONTENTS_FOLDER, folder.DisplayName),
                    folder, 
                    ItemTraversal.Shallow, 
                    this.CurrentService, 
                    this);
            }
        }

        /// <summary>
        /// Open the FolderContentsForm for the associated contents 
        /// table for the selected folder.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OpenAssocContentsMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                FolderContentForm.Show(
                    string.Format(DisplayStrings.TITLE_CONTENTS_ASSOC, folder.DisplayName),
                    folder, 
                    ItemTraversal.Associated, 
                    this.CurrentService, 
                    this);
            }
        }

        /// <summary>
        /// Open the FolderContensForm for the soft deleted contents
        /// table for the selected folder
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OpenSoftDelItemsMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                FolderContentForm.Show(
                    string.Format(DisplayStrings.TITLE_CONTENTS_SOFT, folder.DisplayName),
                    folder, 
                    ItemTraversal.SoftDeleted, 
                    this.CurrentService, 
                    this);
            }
        }

        /// <summary>
        /// Open Calendar series view.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void MnuFolderCalendarView_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                if (folder.FolderClass == "IPF.Appointment")
                {
                     
                    CalendarMonthView oForm = new CalendarMonthView(this, this.CurrentService, folder.Id);
                    oForm.ShowDialog();
                    oForm = null;
                }

 
            }

            //if (this.currentFolder.FolderClass == "IPF.Appointment")
            //    MnuFolderCalendarView.Enabled = true;
            //else
            //    mnuFolderCalendarView.Enabled = false;
 
        }

        /// <summary>
        /// Open the PermissionsDialog for the selected folder.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void PermissionsMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                PermissionsDialog.Show(this.CurrentService, folder.Id);
            }
        }

        private void DumpMimeContentsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
                FormsUtil.DumpMimeContents(this.CurrentService, folder);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DumpXmlContentsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
                FormsUtil.DumpXmlContents(this.CurrentService, folder);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Create a new folder underneath the currently selected folder.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void CreateSubFolderMenu_Click(object sender, EventArgs e)
        {
            Folder parentFolder = GetFolderFromNode(this.FolderTreeView.SelectedNode);
            if (parentFolder != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    Folder folder = new Folder(this.CurrentService);
                    folder.DisplayName = "NewFolder";
                    folder.Save(parentFolder.Id);

                    // Load the current PropertySet for this new folder
                    folder.Load(this.CurrentDetailPropertySet);

                    TreeNode newNode = this.AddFolderToTreeView(folder, this.FolderTreeView.SelectedNode);
                    this.FolderTreeView.SelectedNode = newNode;

                    // Refresh the tree view node
                    this.BindSelectedNode();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void DeleteFolderMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                ToolStripItem item = sender as ToolStripItem;
                if (item != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        // Delete the folder in the manner selected in the context menu
                        if (item.Name == this.DeleteHardMenu.Name)
                        {
                            folder.Delete(DeleteMode.HardDelete);
                        }
                        else if (item.Name == this.DeleteSoftMenu.Name)
                        {
                            folder.Delete(DeleteMode.SoftDelete);
                        }
                        else if (item.Name == this.DeleteMoveMenu.Name)
                        {
                            folder.Delete(DeleteMode.MoveToDeletedItems);
                        }

                        // Remove deleted node from tree view
                        TreeNode deletedNode = this.FolderTreeView.SelectedNode;
                        this.FolderTreeView.SelectedNode = deletedNode.Parent;
                        deletedNode.Remove();

                        this.BindSelectedNode();
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Display a dialog for the user to identify a target folder
        /// for the move and peform the move, if successful remove 
        /// the tree view node.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void MoveFolderMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(this.FolderTreeView.SelectedNode);
            if (folder == null)
            {
                DebugLog.WriteVerbose("Could not get Folder from this.FolderTreeView.SelectedNode");
                return;
            }

            FolderId targetFolderId = null;
            if (FolderIdDialog.ShowDialog(ref targetFolderId) == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    folder.Move(targetFolderId);

                    // Remove moved node from tree view
                    TreeNode movedNode = this.FolderTreeView.SelectedNode;
                    this.FolderTreeView.SelectedNode = movedNode.Parent;
                    movedNode.Remove();

                    this.BindSelectedNode();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Display a dialog for the user to identify a target folder
        /// to copy the selected folder to and perform the copy.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void CopyFolderMenu_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(this.FolderTreeView.SelectedNode);
            if (folder == null)
            {
                DebugLog.WriteVerbose("Could not get Folder from this.FolderTreeView.SelectedNode");
                return;
            }

            FolderId targetFolderId = null;
            if (FolderIdDialog.ShowDialog(ref targetFolderId) == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    folder.Copy(targetFolderId);

                    this.BindSelectedNode();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region PropertyDetailsGrid Events

        private void FolderPropertyDetailsGrid_PropertyChanged(object sender, System.EventArgs e)
        {
            this.BindSelectedNode();
        }

        #endregion

        #region Private Methods

        private TreeNode AddServiceToTreeView(ExchangeService service)
        {
            return this.AddServiceToTreeView(service, true);
        }

        private TreeNode AddServiceToTreeView(ExchangeService service, bool offerRootFolder)
        {
            TreeNode serviceRootNode = null;

            // Create a root node for the ExchangeService
            serviceRootNode = FolderTreeView.Nodes.Add(PropertyInterpretation.GetPropertyValue(service));
            serviceRootNode.Tag = service;

            // ExchangeService ToolTip - mstehle 7/29/2009
            // ToolTips are used to give the user a literal definition of
            // the ExchangeService's configuration as far as what CAS server
            // the service will talk to and what account will be impersonated.
            if (service.ImpersonatedUserId != null)
            {
                // With Impersonation = "ServiceAccount contacting HostName as ActAsAccount"
                serviceRootNode.ToolTipText = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture, 
                    "Service account '{0}' is contacting '{1}' as account '{2}'.",
                    service.GetServiceAccountName(),
                    service.Url.Host,
                    service.ImpersonatedUserId.Id);
            }
            else
            {
                // Without Impersonation = "ServiceAccount contacting HostName"
                serviceRootNode.ToolTipText = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture, 
                    "Service account '{0}' is contacting '{1}'.",
                    service.GetServiceAccountName(),
                    service.Url.Host);
            }

            // Set the node image, don't show a different image when selected
            serviceRootNode.ImageIndex = ExchangeServiceImageIndex;
            serviceRootNode.SelectedImageIndex = serviceRootNode.ImageIndex;

            serviceRootNode.ContextMenuStrip = this.mnuServiceContext;

            if (offerRootFolder)
            {
                DialogResult res = MessageBox.Show(
                    "Do you want to automatically add the mailbox root to the tree view?",
                    "EWSEditor",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    this.AddRootFolderToTreeView(
                        service,
                        new FolderId(WellKnownFolderName.Root),
                        serviceRootNode);
                }
            }

            return serviceRootNode;
        }

        private TreeNode AddRootFolderToTreeView(FolderId folderId, TreeNode parent)
        {
            return this.AddRootFolderToTreeView(this.CurrentService, folderId, parent);
        }

        private TreeNode AddRootFolderToTreeView(ExchangeService service, FolderId folderId, TreeNode parent)
        {
            Folder folder = Folder.Bind(
                service,
                folderId,
                this.folderNodePropertySet);

            string nodeText = string.Empty;
            string nodeToolTip = string.Empty;

            GetFolderNodeText(folder, folderId, out nodeText, out nodeToolTip);

            return this.AddFolderToTreeView(folderId, folder, parent, nodeText, nodeToolTip);
        }

        private TreeNode AddFolderToTreeView(Folder folder, TreeNode parent)
        {
            return this.AddFolderToTreeView(folder, parent, string.Empty, string.Empty);
        }

        private TreeNode AddFolderToTreeView(Folder folder, TreeNode parent, string nodeText, string nodeToolTip)
        {
            return this.AddFolderToTreeView(null, folder, parent, nodeText, nodeToolTip);
        }

        private TreeNode AddFolderToTreeView(FolderId origId, Folder folder, TreeNode parentNode, string nodeText, string nodeToolTip)
        {
            TreeNode newNode = null;

            // If there is no suggested node text either then give it a default name of the folder.
            if (nodeText != null && nodeText.Length > 0)
            {
                newNode = parentNode.Nodes.Add(nodeText);
            }
            else
            {
                nodeText = string.Empty;
                nodeToolTip = string.Empty;

                GetFolderNodeText(folder, origId, out nodeText, out nodeToolTip);
                newNode = parentNode.Nodes.Add(nodeText);
                newNode.ToolTipText = nodeToolTip;
            }

            newNode.ContextMenuStrip = this.cmsFolderMenu;
            
            // RootFolderNodeTag - 10/26/2009 mstehle
            // Only add the RootFolderNodeTag to root folders underneath an
            // ExchangeService node.
            if (parentNode.Tag is ExchangeService)
            {
                RootFolderNodeTag tag = new RootFolderNodeTag();
                tag.FolderObject = folder;
                if (origId != null)
                {
                    tag.OriginalFolderId = origId;
                }
                else
                {
                    tag.OriginalFolderId = folder.Id;
                }

                newNode.Tag = tag;
            }
            else
            {
                newNode.Tag = folder;
            }

            // Only set a tool tip if we explicitly define one
            if (nodeToolTip != null && nodeToolTip.Length > 0)
            {
                newNode.ToolTipText = nodeToolTip;
            }

            //// TODO: At some point it might be nice to give folders specific icons
            //// based on folder class.
            ////if (folder.FolderClass == "IPF.Appointment")
            ////{
            ////    newNode.ImageIndex = IMAGE_CALENDAR;
            ////}
            ////else if (folder.FolderClass == "IPF.Contact")
            ////{
            ////    newNode.ImageIndex = IMAGE_CONTACT;
            ////}
            ////else if (folder.FolderClass == "IPF.Task")
            ////{
            ////    newNode.ImageIndex = IMAGE_TASK;
            ////}

            // The node image shouldn't change when selected
            newNode.SelectedImageIndex = newNode.ImageIndex;

            if (folder.ChildFolderCount > 0)
            {
                newNode.Nodes.Add("[PLACEHOLDER]");
            }

            // If the new node is a root folder node and the parent
            // node is not yet expanded, expand it so the user can
            // see it.
            if (newNode.Tag is RootFolderNodeTag && 
                !newNode.Parent.IsExpanded)
            {
                newNode.Parent.Expand();
            }

            return newNode;
        }

        /// <summary>
        /// If a folder in the tree view is selected, display
        /// its properties in the property grid.
        /// </summary>
        private void BindSelectedNode()
        {
            // If no node is selected, bail out
            if (this.FolderTreeView.SelectedNode == null) 
            {
                DebugLog.WriteVerbose("Leave: this.FolderTreeView.SelectedNode is null");
                return; 
            }

            if (GetFolderFromNode(this.FolderTreeView.SelectedNode) != null)
            {
                Folder folder = GetFolderFromNode(this.FolderTreeView.SelectedNode);

                // If this folder is a root folder node then we can get an
                // original FolderId which we'll pass to GetFolderNodeText()
                FolderId originalFolderId = null;
                if (FolderTreeView.SelectedNode.Tag is RootFolderNodeTag)
                {
                    originalFolderId = ((RootFolderNodeTag)FolderTreeView.SelectedNode.Tag).OriginalFolderId;
                }

                // Ensure that the form is setup properly
                this.mnuViewConfigPropertySet.Enabled = true;
                this.CurrentService = folder.Service;

                // Reload folder with the current property set
                EWSEditor.Forms.FormsUtil.PerformRetryableLoad(folder as ServiceObject, this.CurrentDetailPropertySet);

                // If this folder didn't have subfolders before, put a placeholder folder there to be
                // expanded later
                if (this.FolderTreeView.SelectedNode.Nodes.Count == 0 && folder.ChildFolderCount > 0)
                {
                    this.FolderTreeView.SelectedNode.Nodes.Add("[PLACEHOLDER]");
                    this.FolderTreeView.SelectedNode.Collapse();
                }

                // Refresh the folder name in the tree view
                string nodeText = string.Empty;
                string nodeToolTip = string.Empty;

                GetFolderNodeText(
                    folder,
                    originalFolderId, 
                    out nodeText, 
                    out nodeToolTip);

                this.FolderTreeView.SelectedNode.Text = nodeText;
                this.FolderTreeView.SelectedNode.ToolTipText = nodeToolTip;

                // Reload the property grid
                this.FolderPropertyDetailsGrid.LoadObject(folder);
            }
            else if (FolderTreeView.SelectedNode.Tag is ExchangeService)
            {
                this.CurrentService = FolderTreeView.SelectedNode.Tag as ExchangeService;
                this.FolderPropertyDetailsGrid.LoadObject(this.CurrentService);
            }
        }

        #endregion
        
        /// <summary>
        /// Given the event timing we can't tell the difference between a 
        /// TreeViewAction.Collapse or TreeViewAction.Expand coming from a double 
        /// click on a node and an expansion change coming from clicking the '+' 
        /// without deferring the Expand and Collapse actions until after
        /// FolderTreeView_NodeMouseDoubleClick has a chance to fire.
        /// </summary>
        /// <param name="e">TreeViewCancelEventArgs used to cancel and defer the TreeViewAction</param>
        private void DeferTreeViewAction(TreeViewCancelEventArgs e)
        {
            if (this.deferredTreeViewAction != null)
            {
                DebugLog.WriteVerbose(string.Format("Leave: {0}, is already deferred will skip deferring passed action, {1}", this.deferredTreeViewAction, e.Action));
                return;
            }

            // If the action is already pending then we don't need to do anything
            if (this.deferredTreeViewAction != e.Action)
            {
                switch (e.Action)
                {
                    case TreeViewAction.Collapse:
                    case TreeViewAction.Expand:
                        // Cancel the action
                        e.Cancel = true;
                        this.deferredTreeViewAction = e.Action;
                        this.DeferredTreeViewActionTimer.Start();
                        DebugLog.WriteVerbose(string.Format("Deferred {0} on node, '{1}'", this.deferredTreeViewAction, e.Node.Text));
                        break;
                    default:
                        DebugLog.WriteVerbose("Leave: TreeViewAction not deferrable");
                        break;
                }
            }
        }

        private void DoDeferredTreeViewAction(object sender, EventArgs e)
        {
            DebugLog.WriteVerbose(string.Format("Enter: this.deferredTreeViewAction = {0}", this.deferredTreeViewAction));

            try
            {
                // Only expand/collapse the TreeViewNode if the user *didn't* 
                // double click the node
                if (!this.deferredTreeViewAction.HasValue)
                {
                    DebugLog.WriteVerbose("Leave: There is no deferred action");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                switch (this.deferredTreeViewAction.Value)
                {
                    case TreeViewAction.Collapse:
                        this.FolderTreeView.SelectedNode.Collapse();
                        break;
                    case TreeViewAction.Expand:
                        this.ExpandSelectedTreeNode();
                        break;
                    default:
                        // We shouldn't get here, but don't fail if we do
                        DebugLog.WriteVerbose(string.Format("Unexpected this.deferredTreeViewAction.Value, {0}", this.deferredTreeViewAction.Value));
                        break;
                }
            }
            finally
            {
                // Reset deferred actions
                this.deferredTreeViewAction = null;
                this.DeferredTreeViewActionTimer.Stop();
                
                this.Cursor = Cursors.Default;
            }

            DebugLog.WriteVerbose("Leave");
        }

        private void ExpandSelectedTreeNode()
        {
            TreeNode node = this.FolderTreeView.SelectedNode;
            if (node == null)
            {
                DebugLog.WriteVerbose("Leave: this.FolderTreeView.SelectedNode is null");
                return;
            }
            
            Folder parentFolder = GetFolderFromNode(node);
            if (parentFolder == null)
            {
                DebugLog.WriteVerbose(string.Format("Leave: No folder for this.FolderTreeView.SelectedNode, '{0}'", this.FolderTreeView.SelectedNode.Text));
                return;
            }

            node.Nodes.Clear();

            // If the parent folder has no childern bail out
            if (parentFolder.ChildFolderCount == 0)
            {
                DebugLog.WriteVerbose(String.Format("Leave: Folder for this.FolderTreeView.SelectedNode, '{0}', has no children", this.FolderTreeView.SelectedNode.Text));
                return; 
            }

            // Find sub-folders of the parent folder
            int viewSize = GlobalSettings.FindFolderViewSize;
            bool finished = false;
            List<Folder> subFolders = new List<Folder>();
            while (!finished)
            {
                FolderView view = new FolderView(viewSize, subFolders.Count);
                FindFoldersResults results = parentFolder.FindFolders(view);
                subFolders.AddRange(results.Folders);
                finished = !results.MoreAvailable;
            }

            // Add the folders to the tree view
            foreach (Folder folder in subFolders)
            {
                this.AddFolderToTreeView(folder, node);
            }

            // Expand the selected node to show the children
            if (!node.IsExpanded)
            {
                node.Expand();
            }
        }

        /// <summary>
        /// This simple structure is used to store the Folder object itself 
        /// as well as the FolderId used to bind to it originally.
        /// </summary>
        private struct RootFolderNodeTag
        {
            internal Folder FolderObject;
            internal FolderId OriginalFolderId;
        }

        private void mnuOpenStreamingNotifications_Click(object sender, EventArgs e)
        {
            Folder folder = GetFolderFromNode(FolderTreeView.SelectedNode);
            if (folder != null)
            {
                StreamingNotificationForm.Show(
                    string.Format(DisplayStrings.TITLE_CONTENTS_FOLDER, folder.DisplayName),
                    folder.Id);
            }
        }

        private void FolderPropertyDetailsGrid_Load(object sender, EventArgs e)
        {

        }
    }
}
