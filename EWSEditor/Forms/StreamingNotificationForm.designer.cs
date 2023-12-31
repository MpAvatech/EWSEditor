﻿namespace EWSEditor.Forms
{
    partial class StreamingNotificationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpSynchronize = new System.Windows.Forms.GroupBox();
            this.chkAllFoldes = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numSubs = new System.Windows.Forms.NumericUpDown();
            this.numThreads = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SubscriptionLifetime = new System.Windows.Forms.NumericUpDown();
            this.btnGetFolderId = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSerialize = new System.Windows.Forms.CheckBox();
            this.chkFreeBusyChangedEvent = new System.Windows.Forms.CheckBox();
            this.chkNewMailEvent = new System.Windows.Forms.CheckBox();
            this.chkMovedEvent = new System.Windows.Forms.CheckBox();
            this.chkModifiedEvent = new System.Windows.Forms.CheckBox();
            this.chkDeletedEvent = new System.Windows.Forms.CheckBox();
            this.chkCreatedEvent = new System.Windows.Forms.CheckBox();
            this.chkCopiedEvent = new System.Windows.Forms.CheckBox();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.btnUnsubscribe = new System.Windows.Forms.Button();
            this.txtFolderId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEventsHeader = new System.Windows.Forms.Label();
            this.lstEvents = new System.Windows.Forms.ListView();
            this.colEventClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEventType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colParentFolderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOldParentFolderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjectId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOldObjectId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOldFolderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnreadCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ConnectionCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.EventCount = new System.Windows.Forms.Label();
            this.btnClearEvents = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SubscriptionCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ThreadCount = new System.Windows.Forms.Label();
            this.grpSynchronize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSubs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriptionLifetime)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSynchronize
            // 
            this.grpSynchronize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSynchronize.Controls.Add(this.chkAllFoldes);
            this.grpSynchronize.Controls.Add(this.label5);
            this.grpSynchronize.Controls.Add(this.label4);
            this.grpSynchronize.Controls.Add(this.numSubs);
            this.grpSynchronize.Controls.Add(this.numThreads);
            this.grpSynchronize.Controls.Add(this.label3);
            this.grpSynchronize.Controls.Add(this.SubscriptionLifetime);
            this.grpSynchronize.Controls.Add(this.btnGetFolderId);
            this.grpSynchronize.Controls.Add(this.label1);
            this.grpSynchronize.Controls.Add(this.chkSerialize);
            this.grpSynchronize.Controls.Add(this.chkFreeBusyChangedEvent);
            this.grpSynchronize.Controls.Add(this.chkNewMailEvent);
            this.grpSynchronize.Controls.Add(this.chkMovedEvent);
            this.grpSynchronize.Controls.Add(this.chkModifiedEvent);
            this.grpSynchronize.Controls.Add(this.chkDeletedEvent);
            this.grpSynchronize.Controls.Add(this.chkCreatedEvent);
            this.grpSynchronize.Controls.Add(this.chkCopiedEvent);
            this.grpSynchronize.Controls.Add(this.btnSubscribe);
            this.grpSynchronize.Controls.Add(this.btnUnsubscribe);
            this.grpSynchronize.Controls.Add(this.txtFolderId);
            this.grpSynchronize.Controls.Add(this.label2);
            this.grpSynchronize.Location = new System.Drawing.Point(12, 12);
            this.grpSynchronize.Name = "grpSynchronize";
            this.grpSynchronize.Size = new System.Drawing.Size(997, 127);
            this.grpSynchronize.TabIndex = 7;
            this.grpSynchronize.TabStop = false;
            this.grpSynchronize.Text = "Notification Settings...";
            this.grpSynchronize.Enter += new System.EventHandler(this.grpSynchronize_Enter);
            // 
            // chkAllFoldes
            // 
            this.chkAllFoldes.AutoSize = true;
            this.chkAllFoldes.Checked = true;
            this.chkAllFoldes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllFoldes.Location = new System.Drawing.Point(9, 19);
            this.chkAllFoldes.Name = "chkAllFoldes";
            this.chkAllFoldes.Size = new System.Drawing.Size(136, 17);
            this.chkAllFoldes.TabIndex = 26;
            this.chkAllFoldes.Text = "Subscribe to All Folders";
            this.chkAllFoldes.UseVisualStyleBackColor = true;
            this.chkAllFoldes.CheckedChanged += new System.EventHandler(this.chkAllFoldes_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(329, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Subs/Thread";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Number of Threads";
            // 
            // numSubs
            // 
            this.numSubs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numSubs.Location = new System.Drawing.Point(405, 97);
            this.numSubs.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSubs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSubs.Name = "numSubs";
            this.numSubs.Size = new System.Drawing.Size(50, 20);
            this.numSubs.TabIndex = 24;
            this.numSubs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numThreads
            // 
            this.numThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numThreads.Location = new System.Drawing.Point(272, 97);
            this.numThreads.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThreads.Name = "numThreads";
            this.numThreads.Size = new System.Drawing.Size(50, 20);
            this.numThreads.TabIndex = 24;
            this.numThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Connection Lifetime";
            // 
            // SubscriptionLifetime
            // 
            this.SubscriptionLifetime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubscriptionLifetime.Location = new System.Drawing.Point(570, 97);
            this.SubscriptionLifetime.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.SubscriptionLifetime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SubscriptionLifetime.Name = "SubscriptionLifetime";
            this.SubscriptionLifetime.Size = new System.Drawing.Size(50, 20);
            this.SubscriptionLifetime.TabIndex = 24;
            this.SubscriptionLifetime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // btnGetFolderId
            // 
            this.btnGetFolderId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetFolderId.Location = new System.Drawing.Point(957, 39);
            this.btnGetFolderId.Name = "btnGetFolderId";
            this.btnGetFolderId.Size = new System.Drawing.Size(25, 23);
            this.btnGetFolderId.TabIndex = 23;
            this.btnGetFolderId.Text = "...";
            this.btnGetFolderId.UseVisualStyleBackColor = true;
            this.btnGetFolderId.Click += new System.EventHandler(this.btnGetFolderId_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 22;
            this.label1.Text = "Events Types:";
            // 
            // chkSerialize
            // 
            this.chkSerialize.AutoSize = true;
            this.chkSerialize.Checked = true;
            this.chkSerialize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSerialize.Location = new System.Drawing.Point(13, 100);
            this.chkSerialize.Name = "chkSerialize";
            this.chkSerialize.Size = new System.Drawing.Size(127, 17);
            this.chkSerialize.TabIndex = 21;
            this.chkSerialize.Text = "Serialize Connections";
            this.chkSerialize.UseVisualStyleBackColor = true;
            // 
            // chkFreeBusyChangedEvent
            // 
            this.chkFreeBusyChangedEvent.AutoSize = true;
            this.chkFreeBusyChangedEvent.Checked = true;
            this.chkFreeBusyChangedEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFreeBusyChangedEvent.Location = new System.Drawing.Point(688, 75);
            this.chkFreeBusyChangedEvent.Name = "chkFreeBusyChangedEvent";
            this.chkFreeBusyChangedEvent.Size = new System.Drawing.Size(141, 17);
            this.chkFreeBusyChangedEvent.TabIndex = 21;
            this.chkFreeBusyChangedEvent.Text = "FreeBusyChangedEvent";
            this.chkFreeBusyChangedEvent.UseVisualStyleBackColor = true;
            // 
            // chkNewMailEvent
            // 
            this.chkNewMailEvent.AutoSize = true;
            this.chkNewMailEvent.Checked = true;
            this.chkNewMailEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNewMailEvent.Location = new System.Drawing.Point(587, 74);
            this.chkNewMailEvent.Name = "chkNewMailEvent";
            this.chkNewMailEvent.Size = new System.Drawing.Size(95, 17);
            this.chkNewMailEvent.TabIndex = 21;
            this.chkNewMailEvent.Text = "NewMailEvent";
            this.chkNewMailEvent.UseVisualStyleBackColor = true;
            // 
            // chkMovedEvent
            // 
            this.chkMovedEvent.AutoSize = true;
            this.chkMovedEvent.Checked = true;
            this.chkMovedEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMovedEvent.Location = new System.Drawing.Point(297, 75);
            this.chkMovedEvent.Name = "chkMovedEvent";
            this.chkMovedEvent.Size = new System.Drawing.Size(87, 17);
            this.chkMovedEvent.TabIndex = 20;
            this.chkMovedEvent.Text = "MovedEvent";
            this.chkMovedEvent.UseVisualStyleBackColor = true;
            // 
            // chkModifiedEvent
            // 
            this.chkModifiedEvent.AutoSize = true;
            this.chkModifiedEvent.Checked = true;
            this.chkModifiedEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkModifiedEvent.Location = new System.Drawing.Point(487, 74);
            this.chkModifiedEvent.Name = "chkModifiedEvent";
            this.chkModifiedEvent.Size = new System.Drawing.Size(94, 17);
            this.chkModifiedEvent.TabIndex = 19;
            this.chkModifiedEvent.Text = "ModifiedEvent";
            this.chkModifiedEvent.UseVisualStyleBackColor = true;
            // 
            // chkDeletedEvent
            // 
            this.chkDeletedEvent.AutoSize = true;
            this.chkDeletedEvent.Checked = true;
            this.chkDeletedEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeletedEvent.Location = new System.Drawing.Point(200, 75);
            this.chkDeletedEvent.Name = "chkDeletedEvent";
            this.chkDeletedEvent.Size = new System.Drawing.Size(91, 17);
            this.chkDeletedEvent.TabIndex = 18;
            this.chkDeletedEvent.Text = "DeletedEvent";
            this.chkDeletedEvent.UseVisualStyleBackColor = true;
            // 
            // chkCreatedEvent
            // 
            this.chkCreatedEvent.AutoSize = true;
            this.chkCreatedEvent.Checked = true;
            this.chkCreatedEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreatedEvent.Location = new System.Drawing.Point(390, 74);
            this.chkCreatedEvent.Name = "chkCreatedEvent";
            this.chkCreatedEvent.Size = new System.Drawing.Size(91, 17);
            this.chkCreatedEvent.TabIndex = 17;
            this.chkCreatedEvent.Text = "CreatedEvent";
            this.chkCreatedEvent.UseVisualStyleBackColor = true;
            // 
            // chkCopiedEvent
            // 
            this.chkCopiedEvent.AutoSize = true;
            this.chkCopiedEvent.Checked = true;
            this.chkCopiedEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopiedEvent.Location = new System.Drawing.Point(104, 75);
            this.chkCopiedEvent.Name = "chkCopiedEvent";
            this.chkCopiedEvent.Size = new System.Drawing.Size(87, 17);
            this.chkCopiedEvent.TabIndex = 16;
            this.chkCopiedEvent.Text = "CopiedEvent";
            this.chkCopiedEvent.UseVisualStyleBackColor = true;
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubscribe.Location = new System.Drawing.Point(830, 98);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnSubscribe.TabIndex = 15;
            this.btnSubscribe.Text = "Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // btnUnsubscribe
            // 
            this.btnUnsubscribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnsubscribe.Enabled = false;
            this.btnUnsubscribe.Location = new System.Drawing.Point(911, 98);
            this.btnUnsubscribe.Name = "btnUnsubscribe";
            this.btnUnsubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnUnsubscribe.TabIndex = 14;
            this.btnUnsubscribe.Text = "Unsubscribe";
            this.btnUnsubscribe.UseVisualStyleBackColor = true;
            this.btnUnsubscribe.Click += new System.EventHandler(this.btnUnsubscribe_Click);
            // 
            // txtFolderId
            // 
            this.txtFolderId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderId.Location = new System.Drawing.Point(101, 42);
            this.txtFolderId.Name = "txtFolderId";
            this.txtFolderId.ReadOnly = true;
            this.txtFolderId.Size = new System.Drawing.Size(850, 20);
            this.txtFolderId.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Folder Id:";
            // 
            // lblEventsHeader
            // 
            this.lblEventsHeader.Location = new System.Drawing.Point(18, 141);
            this.lblEventsHeader.Name = "lblEventsHeader";
            this.lblEventsHeader.Size = new System.Drawing.Size(716, 23);
            this.lblEventsHeader.TabIndex = 15;
            this.lblEventsHeader.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lstEvents
            // 
            this.lstEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEventClass,
            this.colTID,
            this.colTimestamp,
            this.colEventType,
            this.colParentFolderId,
            this.colOldParentFolderId,
            this.colObjectId,
            this.colOldObjectId,
            this.colFolderId,
            this.colOldFolderId,
            this.colUnreadCount});
            this.lstEvents.FullRowSelect = true;
            this.lstEvents.Location = new System.Drawing.Point(12, 167);
            this.lstEvents.MultiSelect = false;
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.ShowItemToolTips = true;
            this.lstEvents.Size = new System.Drawing.Size(997, 407);
            this.lstEvents.TabIndex = 14;
            this.lstEvents.UseCompatibleStateImageBehavior = false;
            this.lstEvents.View = System.Windows.Forms.View.Details;
            this.lstEvents.SelectedIndexChanged += new System.EventHandler(this.lstEvents_SelectedIndexChanged);
            this.lstEvents.DoubleClick += new System.EventHandler(this.lstEvents_DoubleClick);
            // 
            // colEventClass
            // 
            this.colEventClass.Text = "EventClass";
            this.colEventClass.Width = 93;
            // 
            // colTID
            // 
            this.colTID.Text = "Thread";
            // 
            // colTimestamp
            // 
            this.colTimestamp.Text = "Timestamp";
            this.colTimestamp.Width = 66;
            // 
            // colEventType
            // 
            this.colEventType.Text = "EventType";
            this.colEventType.Width = 72;
            // 
            // colParentFolderId
            // 
            this.colParentFolderId.Text = "ParentFolderId";
            this.colParentFolderId.Width = 100;
            // 
            // colOldParentFolderId
            // 
            this.colOldParentFolderId.Text = "OldParentFolderId";
            this.colOldParentFolderId.Width = 100;
            // 
            // colObjectId
            // 
            this.colObjectId.Text = "Item.ItemId";
            this.colObjectId.Width = 81;
            // 
            // colOldObjectId
            // 
            this.colOldObjectId.Text = "Item.OldItemId";
            this.colOldObjectId.Width = 89;
            // 
            // colFolderId
            // 
            this.colFolderId.Text = "FolderEvent.FolderId";
            this.colFolderId.Width = 112;
            // 
            // colOldFolderId
            // 
            this.colOldFolderId.Text = "FolderEvent.OldFolderId";
            this.colOldFolderId.Width = 124;
            // 
            // colUnreadCount
            // 
            this.colUnreadCount.Text = "FolderEvent.UnreadCount";
            this.colUnreadCount.Width = 99;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(934, 580);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Connections: ";
            // 
            // ConnectionCount
            // 
            this.ConnectionCount.AutoSize = true;
            this.ConnectionCount.Location = new System.Drawing.Point(232, 148);
            this.ConnectionCount.Name = "ConnectionCount";
            this.ConnectionCount.Size = new System.Drawing.Size(13, 13);
            this.ConnectionCount.TabIndex = 18;
            this.ConnectionCount.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(920, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Events:";
            // 
            // EventCount
            // 
            this.EventCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EventCount.AutoSize = true;
            this.EventCount.Location = new System.Drawing.Point(993, 148);
            this.EventCount.Name = "EventCount";
            this.EventCount.Size = new System.Drawing.Size(13, 13);
            this.EventCount.TabIndex = 18;
            this.EventCount.Text = "0";
            // 
            // btnClearEvents
            // 
            this.btnClearEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearEvents.Location = new System.Drawing.Point(12, 580);
            this.btnClearEvents.Name = "btnClearEvents";
            this.btnClearEvents.Size = new System.Drawing.Size(75, 23);
            this.btnClearEvents.TabIndex = 19;
            this.btnClearEvents.Text = "Clear Events";
            this.btnClearEvents.UseVisualStyleBackColor = true;
            this.btnClearEvents.Click += new System.EventHandler(this.btnClearEvents_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(328, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Subscriptions:";
            // 
            // SubscriptionCount
            // 
            this.SubscriptionCount.AutoSize = true;
            this.SubscriptionCount.Location = new System.Drawing.Point(414, 148);
            this.SubscriptionCount.Name = "SubscriptionCount";
            this.SubscriptionCount.Size = new System.Drawing.Size(13, 13);
            this.SubscriptionCount.TabIndex = 18;
            this.SubscriptionCount.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Threads:";
            this.label9.DoubleClick += new System.EventHandler(this.ThreadCountLabel_DoubleClick);
            // 
            // ThreadCount
            // 
            this.ThreadCount.AutoSize = true;
            this.ThreadCount.Location = new System.Drawing.Point(84, 148);
            this.ThreadCount.Name = "ThreadCount";
            this.ThreadCount.Size = new System.Drawing.Size(13, 13);
            this.ThreadCount.TabIndex = 18;
            this.ThreadCount.Text = "0";
            // 
            // StreamingNotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 611);
            this.Controls.Add(this.btnClearEvents);
            this.Controls.Add(this.SubscriptionCount);
            this.Controls.Add(this.EventCount);
            this.Controls.Add(this.ThreadCount);
            this.Controls.Add(this.ConnectionCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblEventsHeader);
            this.Controls.Add(this.lstEvents);
            this.Controls.Add(this.grpSynchronize);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "StreamingNotificationForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StreamingNotificationForm_FormClosing);
            this.Load += new System.EventHandler(this.StreamingNotificationForm_Load);
            this.grpSynchronize.ResumeLayout(false);
            this.grpSynchronize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSubs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriptionLifetime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSynchronize;
        private System.Windows.Forms.TextBox txtFolderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEventsHeader;
        private System.Windows.Forms.ListView lstEvents;
        private System.Windows.Forms.ColumnHeader colEventClass;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button btnUnsubscribe;
        private System.Windows.Forms.CheckBox chkCopiedEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkNewMailEvent;
        private System.Windows.Forms.CheckBox chkMovedEvent;
        private System.Windows.Forms.CheckBox chkModifiedEvent;
        private System.Windows.Forms.CheckBox chkDeletedEvent;
        private System.Windows.Forms.CheckBox chkCreatedEvent;
        private System.Windows.Forms.Button btnGetFolderId;
        private System.Windows.Forms.ColumnHeader colTimestamp;
        private System.Windows.Forms.ColumnHeader colObjectId;
        private System.Windows.Forms.ColumnHeader colOldObjectId;
        private System.Windows.Forms.ColumnHeader colParentFolderId;
        private System.Windows.Forms.ColumnHeader colOldParentFolderId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown SubscriptionLifetime;
        private System.Windows.Forms.ColumnHeader colFolderId;
        private System.Windows.Forms.ColumnHeader colOldFolderId;
        private System.Windows.Forms.ColumnHeader colUnreadCount;
        private System.Windows.Forms.ColumnHeader colEventType;
        private System.Windows.Forms.CheckBox chkFreeBusyChangedEvent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numThreads;
        private System.Windows.Forms.ColumnHeader colTID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numSubs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ConnectionCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label EventCount;
        private System.Windows.Forms.Button btnClearEvents;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label SubscriptionCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label ThreadCount;
        private System.Windows.Forms.CheckBox chkSerialize;
        private System.Windows.Forms.CheckBox chkAllFoldes;
    }
}