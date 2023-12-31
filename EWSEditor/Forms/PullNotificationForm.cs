﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;

using EWSEditor.Resources;

namespace EWSEditor.Forms
{
    public partial class PullNotificationForm : CountedForm
    {
        private PullSubscription CurrentSubscription = null;
        private FolderId CurrentFolderId = null;

        public PullNotificationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show the PullNotificationForm non-modal
        /// </summary>
        /// <param name="service">ExchangeService to use when making calls.</param>
        public static void Show(ExchangeService service)
        {
            Show(DisplayStrings.TITLE_NOTIFICATIONS,
                null,
                service);
        }

        /// <summary>
        /// Show the form and default to the given folder.
        /// </summary>
        /// <param name="caption">Form caption to display</param>
        /// <param name="service">ExchangeService to use when making calls.</param>
        /// <param name="folder">Folder to display events from by default</param>
        public static void Show(string caption,
            FolderId folderId,
            ExchangeService service)
        {
            PullNotificationForm diag = new PullNotificationForm();

            // Only try to populate CurrentFolderId if we were passed a value
            if (folderId != null)
            {
                diag.SetAndDisplayFolderId(folderId);
            }
            diag.Text = caption.Length == 0 ? "''" : caption;
            diag.CurrentService = service;

            diag.Show();
        }

        /// <summary>
        /// Gather form input and create PullSubscription
        /// </summary>
        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            //const int DEFAULT_NOTIFICATION_TIMEOUT = 5;

            // Convert the check box settings into an array of EventTypes
            List<EventType> eventTypes = new List<EventType>();

            if (this.chkCopiedEvent.Checked)
            {
                eventTypes.Add(EventType.Copied);
            }

            if (this.chkCreatedEvent.Checked)
            {
                eventTypes.Add(EventType.Created);
            }

            if (this.chkDeletedEvent.Checked)
            {
                eventTypes.Add(EventType.Deleted);
            }

            if (this.chkModifiedEvent.Checked)
            {
                eventTypes.Add(EventType.Modified);
            }

            if (this.chkMovedEvent.Checked)
            {
                eventTypes.Add(EventType.Moved);
            }

            if (this.chkNewMailEvent.Checked)
            {
                eventTypes.Add(EventType.NewMail);
            }

            int TimeOutMinutes = Convert.ToInt32(numMinutes.Value);

            if (chkAllFoldes.Checked == false)
            {
                // Create the subscription based on the form settings
                this.CurrentSubscription = this.CurrentService.SubscribeToPullNotifications(
                                                                        new FolderId[] { this.CurrentFolderId },
                                                                        TimeOutMinutes,
                                                                        string.Empty,
                                                                        eventTypes.ToArray());
            }
            else
            {
                // Create the subscription based on the form settings
                this.CurrentSubscription = this.CurrentService.SubscribeToPullNotificationsOnAllFolders(
                                                                        TimeOutMinutes,
                                                                        string.Empty,
                                                                        eventTypes.ToArray());
            }
            // Enable/Disable form controls for an active subscription
            this.btnUnsubscribe.Enabled = true;
            this.btnSubscribe.Enabled = false;
            this.btnGetEvents.Enabled = true;
        }

        /// <summary>
        /// Call Unsubscribe() and reset the form.
        /// </summary>
        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            this.CurrentSubscription.Unsubscribe();
            this.CurrentSubscription = null;

            // Enable/Disable form controls for no active subscription
            this.btnUnsubscribe.Enabled = false;
            this.btnSubscribe.Enabled = true;
            this.btnGetEvents.Enabled = false;

            // Reset form elements
            this.lstEvents.Items.Clear();
            this.lblEventsHeader.Text = string.Empty;
        }

        /// <summary>
        /// Call GetEvents to get the new notification events and display them
        /// in the ListView.
        /// </summary>
        private void btnGetEvents_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Guard statement to bail if we haven't subscribed
                if (this.CurrentSubscription == null) { return; }

                GetEventsResults events = this.CurrentSubscription.GetEvents();

                this.lblEventsHeader.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    "{0} events returned at {1}",
                    events.AllEvents.Count,
                    DateTime.Now.ToString());

                this.lstEvents.Items.Clear();

                foreach (ItemEvent itemEvent in events.ItemEvents)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = itemEvent;
                    item.Text = itemEvent.GetType().Name;
                    item.SubItems.Add(itemEvent.TimeStamp.ToString());
                    item.SubItems.Add(itemEvent.EventType.ToString());

                    item.SubItems.Add(
                            itemEvent.ItemId == null ? "" :
                            PropertyInformation.PropertyInterpretation.GetPropertyValue(itemEvent.ItemId));

                    item.SubItems.Add(
                        itemEvent.OldItemId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(itemEvent.OldItemId));

                    item.SubItems.Add(
                        itemEvent.ParentFolderId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(itemEvent.ParentFolderId));

                    item.SubItems.Add(
                        itemEvent.OldParentFolderId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(itemEvent.OldParentFolderId));

                    this.lstEvents.Items.Add(item);
                }

                foreach (FolderEvent folderEvent in events.FolderEvents)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = folderEvent;
                    item.Text = folderEvent.GetType().Name;
                    item.SubItems.Add(folderEvent.TimeStamp.ToString());
                    item.SubItems.Add(folderEvent.EventType.ToString());

                    item.SubItems.Add(
                        folderEvent.FolderId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(folderEvent.FolderId));

                    item.SubItems.Add(
                        folderEvent.OldFolderId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(folderEvent.OldFolderId));

                    item.SubItems.Add(
                        folderEvent.ParentFolderId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(folderEvent.ParentFolderId));

                    item.SubItems.Add(
                        folderEvent.OldParentFolderId == null ? "" :
                        PropertyInformation.PropertyInterpretation.GetPropertyValue(folderEvent.OldParentFolderId));

                    this.lstEvents.Items.Add(item);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Display the GetFolderIdDialog to get a valid FolderId object and
        /// call SetAndDisplayFolderId.
        /// </summary>
        private void btnGetFolderId_Click(object sender, EventArgs e)
        {
            FolderId folderId = null;
            if (Forms.FolderIdDialog.ShowDialog(ref folderId) == DialogResult.OK)
            {
                SetAndDisplayFolderId(folderId);
            }
        }

        /// <summary>
        /// Set the class variable and disaply the proper text for the given
        /// FolderId
        /// </summary>
        /// <param name="folderId"></param>
        private void SetAndDisplayFolderId(FolderId folderId)
        {
            this.txtFolderId.Text = PropertyInformation.TypeValues.FolderIdTypeValue.GetValue(folderId, true);
            this.CurrentFolderId = folderId;
            this.Text = DisplayStrings.TITLE_NOTIFICATIONS;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAllFoldes_CheckedChanged(object sender, EventArgs e)
        {
            SetROchkAllFoldes();
        }

        private void SetROchkAllFoldes()
        {
            bool bChecked = false;
            if (chkAllFoldes.Checked == false)
                bChecked = true;

            txtFolderId.Enabled = bChecked;
            btnGetFolderId.Enabled = bChecked;
        }

        private void grpSynchronize_Enter(object sender, EventArgs e)
        {
             
        }

        private void PullNotificationForm_Load(object sender, EventArgs e)
        {
            SetROchkAllFoldes();
        }

        private void lstEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lstEvents_DoubleClick(object sender, EventArgs e)
        {
            string ParentFolderName = string.Empty;
            string ParentFolderClass = string.Empty;
            string OldParentFolderName = string.Empty;
            string OldParentFolderClass = string.Empty;

            string ItemName = string.Empty;
            string ItemClass = string.Empty;
            string OldItemName = string.Empty;
            string OldItemClass = string.Empty;

            string FolderName = string.Empty;
            string FolderClass = string.Empty;
            string OldFolderName = string.Empty;
            string OldFolderClass = string.Empty;

            Folder oFolder = null;
            //Item oItem = null;

            StringBuilder oSB = new StringBuilder();

            if (lstEvents.SelectedItems.Count > 0)
            {

                ListViewItem oListViewItem = lstEvents.SelectedItems[0];

                if (oListViewItem.Tag.ToString().StartsWith("[") == false)
                {
                     
                     

                    if (lstEvents.SelectedItems[0].Text == "ItemEvent")
                    {
                        ItemEvent oItemEvent = null;

                        oItemEvent = (ItemEvent)oListViewItem.Tag;


                        Item oSomeItem = null;


                        if (oListViewItem.SubItems[3].Text.TrimEnd().Length != 0)
                        {
                            try
                            {

                                oSomeItem = Item.Bind(CurrentService, new ItemId(oItemEvent.ItemId.UniqueId));

                                if (oSomeItem != null)
                                {
                                    ItemName = oSomeItem.Subject;
                                    ItemClass = oSomeItem.ItemClass;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.ToString());
                            }
                        }

                        if (oListViewItem.SubItems[4].Text.TrimEnd().Length != 0)
                        {
                            try
                            {

                                oSomeItem = Item.Bind(CurrentService, new ItemId(oItemEvent.OldItemId.UniqueId));

                                if (oSomeItem != null)
                                {
                                    OldItemName = oSomeItem.Subject;
                                    OldItemClass = oSomeItem.ItemClass;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.ToString());
                            }
                        }

                        if (oListViewItem.SubItems[5].Text.TrimEnd().Length != 0)
                        {
                            try
                            {
                                oFolder = Folder.Bind(CurrentService, new FolderId(oItemEvent.ParentFolderId.UniqueId));
                                if (oFolder  != null)
                                {
                                    ParentFolderName = oFolder .DisplayName;
                                    ParentFolderClass = oFolder.FolderClass;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.ToString());
                            }
                        }
                        if (oListViewItem.SubItems[6].Text.TrimEnd().Length != 0)
                        {
                            try
                            {
                                oFolder  = Folder.Bind(CurrentService, new FolderId(oItemEvent.OldParentFolderId.UniqueId));
                                if (oFolder  != null)
                                {
                                    OldParentFolderName = oFolder.DisplayName;
                                    OldParentFolderClass = oFolder.FolderClass;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.ToString());
                            }
                        }


                         
                        oSB.AppendFormat("EventType:         \r\n    {0}\r\n", lstEvents.SelectedItems[0].Text);
                        oSB.AppendFormat("\r\n");
                        oSB.AppendFormat("TimeStamp:         \r\n    {0}\r\n", lstEvents.SelectedItems[0].SubItems[1].Text);
                        oSB.AppendFormat("\r\n");
                        oSB.AppendFormat("ObjectType:        \r\n    {0}\r\n", lstEvents.SelectedItems[0].SubItems[2].Text);
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("Object:\r\n");
                        if (oItemEvent != null)
                        {
                            if (oItemEvent.ItemId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oItemEvent.ItemId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oItemEvent.ItemId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", ItemName);
                                oSB.AppendFormat("    Class:     {0}\r\n", ItemClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("OldObject:\r\n");
                        if (oItemEvent != null)
                        {
                            if (oItemEvent.OldItemId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oItemEvent.OldItemId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oItemEvent.OldItemId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", OldItemName);
                                oSB.AppendFormat("    Class:     {0}\r\n", OldItemClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("ParentFolder:\r\n");
                        if (oItemEvent != null)
                        {
                            if (oItemEvent.ParentFolderId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oItemEvent.ParentFolderId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oItemEvent.ParentFolderId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", ParentFolderName);
                                oSB.AppendFormat("    Class:     {0}\r\n", ParentFolderClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("OldParentFolder:\r\n");
                        if (oItemEvent != null)
                        {
                            if (oItemEvent.OldParentFolderId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oItemEvent.OldParentFolderId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oItemEvent.OldParentFolderId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", OldParentFolderName);
                                oSB.AppendFormat("    Class:     {0}\r\n", OldParentFolderClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");
                    }

                    if (lstEvents.SelectedItems[0].Text == "FolderEvent")
                    {
                        FolderEvent oFolderEvent = null;
                        oFolderEvent = (FolderEvent)oListViewItem.Tag;


                        Item oSomeItem = null;


                        if (oListViewItem.SubItems[3].Text.TrimEnd().Length != 0)
                        {
                            try
                            {

                                oFolder = Folder.Bind(CurrentService, new FolderId(oFolderEvent.FolderId.UniqueId));

                                if (oFolder != null)
                                {
                                    FolderName = oSomeItem.Subject;
                                    FolderClass = oSomeItem.ItemClass;
                                }
                            }
                            catch (Exception ex)
                            { System.Console.WriteLine(ex.Message); }
                        }

                        if (oListViewItem.SubItems[4].Text.TrimEnd().Length != 0)
                        {
                            try
                            {

                                oFolder = Folder.Bind(CurrentService, new FolderId(oFolderEvent.OldFolderId.UniqueId));

                                if (oFolder != null)
                                {
                                    OldFolderName = oSomeItem.Subject;
                                    OldFolderClass = oSomeItem.ItemClass;
                                }
                            }
                            catch (Exception ex)
                            { System.Console.WriteLine(ex.Message); }
                        }

                        if (oListViewItem.SubItems[5].Text.TrimEnd().Length != 0)
                        {
                            try
                            {
                                oFolder = Folder.Bind(CurrentService, new FolderId(oFolderEvent.ParentFolderId.UniqueId));
                                if (oFolder != null)
                                {
                                    ParentFolderName = oFolder.DisplayName;
                                    ParentFolderClass = oFolder.FolderClass;
                                }
                            }
                            catch (Exception ex)
                            { System.Console.WriteLine(ex.Message); }
                        }
                        if (oListViewItem.SubItems[6].Text.TrimEnd().Length != 0)
                        {
                            try
                            {
                                oFolder = Folder.Bind(CurrentService, new FolderId(oFolderEvent.OldParentFolderId.UniqueId));
                                if (oFolder != null)
                                {
                                    OldParentFolderName = oFolder.DisplayName;
                                    OldParentFolderClass = oFolder.FolderClass;
                                }
                            }
                            catch (Exception ex)
                            { System.Console.WriteLine(ex.Message); }
                        }
 

                         
                        oSB.AppendFormat("EventType:         \r\n    {0}\r\n", lstEvents.SelectedItems[0].Text);
                        oSB.AppendFormat("\r\n");
                        oSB.AppendFormat("TimeStamp:         \r\n    {0}\r\n", lstEvents.SelectedItems[0].SubItems[1].Text);
                        oSB.AppendFormat("\r\n");
                        oSB.AppendFormat("ObjectType:        \r\n    {0}\r\n", lstEvents.SelectedItems[0].SubItems[2].Text);
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("Item:\r\n");
                        if (oFolderEvent != null)
                        {
                            if (oFolderEvent.FolderId!= null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oFolderEvent.FolderId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oFolderEvent.FolderId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", ItemName);
                                oSB.AppendFormat("    Class:     {0}\r\n", ItemClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("OldItem:\r\n");
                        if (oFolderEvent != null)
                        {
                            if (oFolderEvent.OldFolderId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oFolderEvent.OldFolderId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oFolderEvent.OldFolderId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", OldItemName);
                                oSB.AppendFormat("    Class:     {0}\r\n", OldItemClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("ParentFolder:\r\n");
                        if (oFolderEvent != null)
                        {
                            if (oFolderEvent.ParentFolderId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oFolderEvent.ParentFolderId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oFolderEvent.ParentFolderId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", ParentFolderName);
                                oSB.AppendFormat("    Class:     {0}\r\n", ParentFolderClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");

                        oSB.AppendFormat("OldParentFolder:\r\n");
                        if (oFolderEvent != null)
                        {
                            if (oFolderEvent.OldParentFolderId != null)
                            {
                                oSB.AppendFormat("    UniqueId:  {0}\r\n", oFolderEvent.OldParentFolderId.UniqueId.ToString());
                                oSB.AppendFormat("    ChangeKey: {0}\r\n", oFolderEvent.OldParentFolderId.ChangeKey.ToString());
                                oSB.AppendFormat("    Name:      {0}\r\n", OldParentFolderName);
                                oSB.AppendFormat("    Class:     {0}\r\n", OldParentFolderClass);
                            }
                        }
                        oSB.AppendFormat("\r\n");
                    }
                     
                     


                    string sContent = oSB.ToString();

                    ShowTextDocument oForm = new ShowTextDocument();
                    oForm.txtEntry.WordWrap = false;
                    oForm.Text = "Information";
                    oForm.txtEntry.Text = sContent;
                    oForm.ShowDialog();
                }

            }
        }
    }
}
