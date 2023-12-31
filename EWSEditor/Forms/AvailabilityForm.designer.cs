﻿namespace EWSEditor.Forms
{
    partial class AvailabilityForm
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.StartTimeWindowLabel = new System.Windows.Forms.Label();
            this.EndWindowDate = new System.Windows.Forms.DateTimePicker();
            this.StartWindowDate = new System.Windows.Forms.DateTimePicker();
            this.EndTimeWindowLabel = new System.Windows.Forms.Label();
            this.TimeWindowGroup = new System.Windows.Forms.GroupBox();
            this.CurrentMeetingLabel = new System.Windows.Forms.Label();
            this.MeetingDurationText = new System.Windows.Forms.TextBox();
            this.CurrentMeetingCheck = new System.Windows.Forms.CheckBox();
            this.lblMeetingDuration = new System.Windows.Forms.Label();
            this.CurrentMeetingDate = new System.Windows.Forms.DateTimePicker();
            this.DetailWindowGroup = new System.Windows.Forms.GroupBox();
            this.StartDetailLabel = new System.Windows.Forms.Label();
            this.EndDetailDate = new System.Windows.Forms.DateTimePicker();
            this.EndDetailLabel = new System.Windows.Forms.Label();
            this.StartDetailDate = new System.Windows.Forms.DateTimePicker();
            this.AttendeeAvailabilityGroup = new System.Windows.Forms.GroupBox();
            this.SuggestionsList = new System.Windows.Forms.ListView();
            this.DateColumn = new System.Windows.Forms.ColumnHeader();
            this.DateQualityColumn = new System.Windows.Forms.ColumnHeader();
            this.TimeColumn = new System.Windows.Forms.ColumnHeader();
            this.TimeQualityColumn = new System.Windows.Forms.ColumnHeader();
            this.ConflictsColumn = new System.Windows.Forms.ColumnHeader();
            this.IsWorkTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.AttendeeAvailabilityList = new System.Windows.Forms.ListView();
            this.ViewTypeColumn = new System.Windows.Forms.ColumnHeader();
            this.WorkingHoursColumn = new System.Windows.Forms.ColumnHeader();
            this.MergedFreeBusyStatusColumn = new System.Windows.Forms.ColumnHeader();
            this.CalEventsList = new System.Windows.Forms.ListView();
            this.FreeBusyStatusColumn = new System.Windows.Forms.ColumnHeader();
            this.StartTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.EndTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.SubjectColumn = new System.Windows.Forms.ColumnHeader();
            this.LocationColumn = new System.Windows.Forms.ColumnHeader();
            this.IsExceptionColumn = new System.Windows.Forms.ColumnHeader();
            this.IsMeetingColumn = new System.Windows.Forms.ColumnHeader();
            this.IsPrivateColumn = new System.Windows.Forms.ColumnHeader();
            this.IsRecurringColumn = new System.Windows.Forms.ColumnHeader();
            this.IsReminderSetColumn = new System.Windows.Forms.ColumnHeader();
            this.StoreIdColumn = new System.Windows.Forms.ColumnHeader();
            this.AddAttendeeButton = new System.Windows.Forms.Button();
            this.SuggestionsGroup = new System.Windows.Forms.GroupBox();
            this.GoodSuggestThresholdText = new System.Windows.Forms.TextBox();
            this.lblGoodSuggestThreshold = new System.Windows.Forms.Label();
            this.MaxNonWorkSuggestText = new System.Windows.Forms.TextBox();
            this.TempMinSuggestQualCombo = new System.Windows.Forms.ComboBox();
            this.MaxSuggestPerDayText = new System.Windows.Forms.TextBox();
            this.lblMaxNonWorkSuggest = new System.Windows.Forms.Label();
            this.lblMaxSuggestPerDay = new System.Windows.Forms.Label();
            this.lblMinSuggestQual = new System.Windows.Forms.Label();
            this.TempRequestFBViewCombo = new System.Windows.Forms.ComboBox();
            this.lblRequestFBView = new System.Windows.Forms.Label();
            this.MergeFBIntervalText = new System.Windows.Forms.TextBox();
            this.lblMergeFBInterval = new System.Windows.Forms.Label();
            this.TempAvailDataCombo = new EWSEditor.Forms.Controls.EnumComboBox<Microsoft.Exchange.WebServices.Data.AvailabilityData>();
            this.label1 = new System.Windows.Forms.Label();
            this.GetAvailabilityButton = new System.Windows.Forms.Button();
            this.AvailabilityDataGroup = new System.Windows.Forms.GroupBox();
            this.GlobalObjectIdText = new System.Windows.Forms.TextBox();
            this.GlobalObjectIdLabel = new System.Windows.Forms.Label();
            this.RemoveAttendeeButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AttendeeList = new System.Windows.Forms.ListView();
            this.SmtpColumn = new System.Windows.Forms.ColumnHeader();
            this.TypeColumn = new System.Windows.Forms.ColumnHeader();
            this.ExcludeConflictsColumn = new System.Windows.Forms.ColumnHeader();
            this.TimeWindowGroup.SuspendLayout();
            this.DetailWindowGroup.SuspendLayout();
            this.AttendeeAvailabilityGroup.SuspendLayout();
            this.SuggestionsGroup.SuspendLayout();
            this.AvailabilityDataGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.CloseButton.Location = new System.Drawing.Point(834, 720);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 28;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // StartTimeWindowLabel
            // 
            this.StartTimeWindowLabel.Location = new System.Drawing.Point(9, 19);
            this.StartTimeWindowLabel.Name = "StartTimeWindowLabel";
            this.StartTimeWindowLabel.Size = new System.Drawing.Size(58, 16);
            this.StartTimeWindowLabel.TabIndex = 41;
            this.StartTimeWindowLabel.Text = "Start Time:";
            // 
            // EndWindowDate
            // 
            this.EndWindowDate.CustomFormat = "MMM dd, yyyy - hh:mm tt";
            this.EndWindowDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndWindowDate.Location = new System.Drawing.Point(355, 16);
            this.EndWindowDate.Name = "EndWindowDate";
            this.EndWindowDate.Size = new System.Drawing.Size(162, 20);
            this.EndWindowDate.TabIndex = 40;
            // 
            // StartWindowDate
            // 
            this.StartWindowDate.CustomFormat = "MMM dd, yyyy - hh:mm tt";
            this.StartWindowDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartWindowDate.Location = new System.Drawing.Point(89, 17);
            this.StartWindowDate.Name = "StartWindowDate";
            this.StartWindowDate.Size = new System.Drawing.Size(162, 20);
            this.StartWindowDate.TabIndex = 39;
            // 
            // EndTimeWindowLabel
            // 
            this.EndTimeWindowLabel.Location = new System.Drawing.Point(257, 18);
            this.EndTimeWindowLabel.Name = "EndTimeWindowLabel";
            this.EndTimeWindowLabel.Size = new System.Drawing.Size(58, 16);
            this.EndTimeWindowLabel.TabIndex = 42;
            this.EndTimeWindowLabel.Text = "End Time:";
            // 
            // TimeWindowGroup
            // 
            this.TimeWindowGroup.Controls.Add(this.StartTimeWindowLabel);
            this.TimeWindowGroup.Controls.Add(this.CurrentMeetingLabel);
            this.TimeWindowGroup.Controls.Add(this.MeetingDurationText);
            this.TimeWindowGroup.Controls.Add(this.CurrentMeetingCheck);
            this.TimeWindowGroup.Controls.Add(this.EndWindowDate);
            this.TimeWindowGroup.Controls.Add(this.lblMeetingDuration);
            this.TimeWindowGroup.Controls.Add(this.CurrentMeetingDate);
            this.TimeWindowGroup.Controls.Add(this.EndTimeWindowLabel);
            this.TimeWindowGroup.Controls.Add(this.StartWindowDate);
            this.TimeWindowGroup.Location = new System.Drawing.Point(313, 12);
            this.TimeWindowGroup.Name = "TimeWindowGroup";
            this.TimeWindowGroup.Size = new System.Drawing.Size(600, 107);
            this.TimeWindowGroup.TabIndex = 40;
            this.TimeWindowGroup.TabStop = false;
            this.TimeWindowGroup.Text = "Availability Time Window and Current Meeting Info";
            // 
            // CurrentMeetingLabel
            // 
            this.CurrentMeetingLabel.Enabled = false;
            this.CurrentMeetingLabel.Location = new System.Drawing.Point(9, 72);
            this.CurrentMeetingLabel.Name = "CurrentMeetingLabel";
            this.CurrentMeetingLabel.Size = new System.Drawing.Size(61, 16);
            this.CurrentMeetingLabel.TabIndex = 110;
            this.CurrentMeetingLabel.Text = "Meet Time:";
            // 
            // MeetingDurationText
            // 
            this.MeetingDurationText.Location = new System.Drawing.Point(355, 70);
            this.MeetingDurationText.Name = "MeetingDurationText";
            this.MeetingDurationText.Size = new System.Drawing.Size(42, 20);
            this.MeetingDurationText.TabIndex = 106;
            // 
            // CurrentMeetingCheck
            // 
            this.CurrentMeetingCheck.Location = new System.Drawing.Point(6, 47);
            this.CurrentMeetingCheck.Name = "CurrentMeetingCheck";
            this.CurrentMeetingCheck.Size = new System.Drawing.Size(168, 17);
            this.CurrentMeetingCheck.TabIndex = 108;
            this.CurrentMeetingCheck.Text = "Specify a current meeting time";
            this.CurrentMeetingCheck.UseVisualStyleBackColor = true;
            // 
            // lblMeetingDuration
            // 
            this.lblMeetingDuration.Location = new System.Drawing.Point(257, 71);
            this.lblMeetingDuration.Name = "lblMeetingDuration";
            this.lblMeetingDuration.Size = new System.Drawing.Size(94, 19);
            this.lblMeetingDuration.TabIndex = 100;
            this.lblMeetingDuration.Text = "Meeting Duration:";
            // 
            // CurrentMeetingDate
            // 
            this.CurrentMeetingDate.CustomFormat = "MMM dd, yyyy - hh:mm tt";
            this.CurrentMeetingDate.Enabled = false;
            this.CurrentMeetingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CurrentMeetingDate.Location = new System.Drawing.Point(89, 70);
            this.CurrentMeetingDate.Name = "CurrentMeetingDate";
            this.CurrentMeetingDate.Size = new System.Drawing.Size(162, 20);
            this.CurrentMeetingDate.TabIndex = 109;
            // 
            // DetailWindowGroup
            // 
            this.DetailWindowGroup.Controls.Add(this.StartDetailLabel);
            this.DetailWindowGroup.Controls.Add(this.EndDetailDate);
            this.DetailWindowGroup.Controls.Add(this.EndDetailLabel);
            this.DetailWindowGroup.Controls.Add(this.StartDetailDate);
            this.DetailWindowGroup.Location = new System.Drawing.Point(6, 19);
            this.DetailWindowGroup.Name = "DetailWindowGroup";
            this.DetailWindowGroup.Size = new System.Drawing.Size(258, 80);
            this.DetailWindowGroup.TabIndex = 48;
            this.DetailWindowGroup.TabStop = false;
            this.DetailWindowGroup.Text = "Detailed Suggestions Time Window";
            // 
            // StartDetailLabel
            // 
            this.StartDetailLabel.Location = new System.Drawing.Point(16, 26);
            this.StartDetailLabel.Name = "StartDetailLabel";
            this.StartDetailLabel.Size = new System.Drawing.Size(58, 16);
            this.StartDetailLabel.TabIndex = 41;
            this.StartDetailLabel.Text = "Start Time:";
            // 
            // EndDetailDate
            // 
            this.EndDetailDate.CustomFormat = "MMM dd, yyyy - hh:mm tt";
            this.EndDetailDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDetailDate.Location = new System.Drawing.Point(86, 48);
            this.EndDetailDate.Name = "EndDetailDate";
            this.EndDetailDate.Size = new System.Drawing.Size(162, 20);
            this.EndDetailDate.TabIndex = 40;
            // 
            // EndDetailLabel
            // 
            this.EndDetailLabel.Location = new System.Drawing.Point(16, 52);
            this.EndDetailLabel.Name = "EndDetailLabel";
            this.EndDetailLabel.Size = new System.Drawing.Size(58, 16);
            this.EndDetailLabel.TabIndex = 42;
            this.EndDetailLabel.Text = "End Time:";
            // 
            // StartDetailDate
            // 
            this.StartDetailDate.CustomFormat = "MMM dd, yyyy - hh:mm tt";
            this.StartDetailDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDetailDate.Location = new System.Drawing.Point(86, 22);
            this.StartDetailDate.Name = "StartDetailDate";
            this.StartDetailDate.Size = new System.Drawing.Size(162, 20);
            this.StartDetailDate.TabIndex = 39;
            // 
            // AttendeeAvailabilityGroup
            // 
            this.AttendeeAvailabilityGroup.Controls.Add(this.SuggestionsList);
            this.AttendeeAvailabilityGroup.Controls.Add(this.AttendeeAvailabilityList);
            this.AttendeeAvailabilityGroup.Controls.Add(this.CalEventsList);
            this.AttendeeAvailabilityGroup.Location = new System.Drawing.Point(5, 352);
            this.AttendeeAvailabilityGroup.Name = "AttendeeAvailabilityGroup";
            this.AttendeeAvailabilityGroup.Size = new System.Drawing.Size(908, 359);
            this.AttendeeAvailabilityGroup.TabIndex = 49;
            this.AttendeeAvailabilityGroup.TabStop = false;
            this.AttendeeAvailabilityGroup.Text = "Attendees and Availability";
            // 
            // SuggestionsList
            // 
            this.SuggestionsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DateColumn,
            this.DateQualityColumn,
            this.TimeColumn,
            this.TimeQualityColumn,
            this.ConflictsColumn,
            this.IsWorkTimeColumn});
            this.SuggestionsList.Location = new System.Drawing.Point(13, 203);
            this.SuggestionsList.Name = "SuggestionsList";
            this.SuggestionsList.Size = new System.Drawing.Size(884, 140);
            this.SuggestionsList.TabIndex = 33;
            this.SuggestionsList.UseCompatibleStateImageBehavior = false;
            this.SuggestionsList.View = System.Windows.Forms.View.Details;
            // 
            // DateColumn
            // 
            this.DateColumn.Text = "Date";
            this.DateColumn.Width = 91;
            // 
            // DateQualityColumn
            // 
            this.DateQualityColumn.Text = "DateQuality";
            this.DateQualityColumn.Width = 101;
            // 
            // TimeColumn
            // 
            this.TimeColumn.Text = "Time";
            this.TimeColumn.Width = 70;
            // 
            // TimeQualityColumn
            // 
            this.TimeQualityColumn.Text = "TimeQuality";
            this.TimeQualityColumn.Width = 112;
            // 
            // ConflictsColumn
            // 
            this.ConflictsColumn.Text = "Conflicts";
            this.ConflictsColumn.Width = 72;
            // 
            // IsWorkTimeColumn
            // 
            this.IsWorkTimeColumn.Text = "IsWorkTime";
            this.IsWorkTimeColumn.Width = 74;
            // 
            // AttendeeAvailabilityList
            // 
            this.AttendeeAvailabilityList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ViewTypeColumn,
            this.WorkingHoursColumn,
            this.MergedFreeBusyStatusColumn});
            this.AttendeeAvailabilityList.FullRowSelect = true;
            this.AttendeeAvailabilityList.Location = new System.Drawing.Point(13, 19);
            this.AttendeeAvailabilityList.MultiSelect = false;
            this.AttendeeAvailabilityList.Name = "AttendeeAvailabilityList";
            this.AttendeeAvailabilityList.Size = new System.Drawing.Size(884, 72);
            this.AttendeeAvailabilityList.TabIndex = 32;
            this.AttendeeAvailabilityList.UseCompatibleStateImageBehavior = false;
            this.AttendeeAvailabilityList.View = System.Windows.Forms.View.Details;
            // 
            // ViewTypeColumn
            // 
            this.ViewTypeColumn.Text = "ViewType";
            this.ViewTypeColumn.Width = 90;
            // 
            // WorkingHoursColumn
            // 
            this.WorkingHoursColumn.Text = "WorkingHours";
            this.WorkingHoursColumn.Width = 90;
            // 
            // MergedFreeBusyStatusColumn
            // 
            this.MergedFreeBusyStatusColumn.Text = "MergedFreeBusyStatus";
            this.MergedFreeBusyStatusColumn.Width = 137;
            // 
            // CalEventsList
            // 
            this.CalEventsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FreeBusyStatusColumn,
            this.StartTimeColumn,
            this.EndTimeColumn,
            this.SubjectColumn,
            this.LocationColumn,
            this.IsExceptionColumn,
            this.IsMeetingColumn,
            this.IsPrivateColumn,
            this.IsRecurringColumn,
            this.IsReminderSetColumn,
            this.StoreIdColumn});
            this.CalEventsList.FullRowSelect = true;
            this.CalEventsList.Location = new System.Drawing.Point(13, 97);
            this.CalEventsList.MultiSelect = false;
            this.CalEventsList.Name = "CalEventsList";
            this.CalEventsList.Size = new System.Drawing.Size(884, 100);
            this.CalEventsList.TabIndex = 31;
            this.CalEventsList.UseCompatibleStateImageBehavior = false;
            this.CalEventsList.View = System.Windows.Forms.View.Details;
            // 
            // FreeBusyStatusColumn
            // 
            this.FreeBusyStatusColumn.Text = "FreeBusyStatus";
            this.FreeBusyStatusColumn.Width = 111;
            // 
            // StartTimeColumn
            // 
            this.StartTimeColumn.Text = "StartTime";
            this.StartTimeColumn.Width = 114;
            // 
            // EndTimeColumn
            // 
            this.EndTimeColumn.Text = "EndTime";
            this.EndTimeColumn.Width = 127;
            // 
            // SubjectColumn
            // 
            this.SubjectColumn.Text = "Subject";
            this.SubjectColumn.Width = 195;
            // 
            // LocationColumn
            // 
            this.LocationColumn.Text = "Location";
            this.LocationColumn.Width = 107;
            // 
            // IsExceptionColumn
            // 
            this.IsExceptionColumn.Text = "IsException";
            this.IsExceptionColumn.Width = 69;
            // 
            // IsMeetingColumn
            // 
            this.IsMeetingColumn.Text = "IsMeeting";
            // 
            // IsPrivateColumn
            // 
            this.IsPrivateColumn.Text = "IsPrivate";
            // 
            // IsRecurringColumn
            // 
            this.IsRecurringColumn.Text = "IsRecurring";
            this.IsRecurringColumn.Width = 66;
            // 
            // IsReminderSetColumn
            // 
            this.IsReminderSetColumn.Text = "IsReminderSet";
            this.IsReminderSetColumn.Width = 82;
            // 
            // StoreIdColumn
            // 
            this.StoreIdColumn.Text = "StoreId";
            this.StoreIdColumn.Width = 55;
            // 
            // AddAttendeeButton
            // 
            this.AddAttendeeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddAttendeeButton.Location = new System.Drawing.Point(117, 296);
            this.AddAttendeeButton.Name = "AddAttendeeButton";
            this.AddAttendeeButton.Size = new System.Drawing.Size(90, 23);
            this.AddAttendeeButton.TabIndex = 30;
            this.AddAttendeeButton.Text = "Add Attendee...";
            this.AddAttendeeButton.UseVisualStyleBackColor = true;
            this.AddAttendeeButton.Click += new System.EventHandler(this.AddAttendeeButton_Click);
            // 
            // SuggestionsGroup
            // 
            this.SuggestionsGroup.BackColor = System.Drawing.SystemColors.Control;
            this.SuggestionsGroup.Controls.Add(this.GoodSuggestThresholdText);
            this.SuggestionsGroup.Controls.Add(this.lblGoodSuggestThreshold);
            this.SuggestionsGroup.Controls.Add(this.MaxNonWorkSuggestText);
            this.SuggestionsGroup.Controls.Add(this.TempMinSuggestQualCombo);
            this.SuggestionsGroup.Controls.Add(this.DetailWindowGroup);
            this.SuggestionsGroup.Controls.Add(this.MaxSuggestPerDayText);
            this.SuggestionsGroup.Controls.Add(this.lblMaxNonWorkSuggest);
            this.SuggestionsGroup.Controls.Add(this.lblMaxSuggestPerDay);
            this.SuggestionsGroup.Controls.Add(this.lblMinSuggestQual);
            this.SuggestionsGroup.Location = new System.Drawing.Point(638, 125);
            this.SuggestionsGroup.Name = "SuggestionsGroup";
            this.SuggestionsGroup.Size = new System.Drawing.Size(275, 221);
            this.SuggestionsGroup.TabIndex = 50;
            this.SuggestionsGroup.TabStop = false;
            this.SuggestionsGroup.Text = "Suggestion Options";
            // 
            // GoodSuggestThresholdText
            // 
            this.GoodSuggestThresholdText.Location = new System.Drawing.Point(190, 150);
            this.GoodSuggestThresholdText.Name = "GoodSuggestThresholdText";
            this.GoodSuggestThresholdText.Size = new System.Drawing.Size(42, 20);
            this.GoodSuggestThresholdText.TabIndex = 108;
            // 
            // lblGoodSuggestThreshold
            // 
            this.lblGoodSuggestThreshold.Location = new System.Drawing.Point(6, 150);
            this.lblGoodSuggestThreshold.Name = "lblGoodSuggestThreshold";
            this.lblGoodSuggestThreshold.Size = new System.Drawing.Size(171, 20);
            this.lblGoodSuggestThreshold.TabIndex = 107;
            this.lblGoodSuggestThreshold.Text = "Good Suggestion Thresold (%):";
            // 
            // MaxNonWorkSuggestText
            // 
            this.MaxNonWorkSuggestText.Location = new System.Drawing.Point(190, 101);
            this.MaxNonWorkSuggestText.Name = "MaxNonWorkSuggestText";
            this.MaxNonWorkSuggestText.Size = new System.Drawing.Size(42, 20);
            this.MaxNonWorkSuggestText.TabIndex = 104;
            // 
            // MinSuggestQualDrop
            // 
            this.TempMinSuggestQualCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TempMinSuggestQualCombo.FormattingEnabled = true;
            this.TempMinSuggestQualCombo.Location = new System.Drawing.Point(148, 180);
            this.TempMinSuggestQualCombo.Name = "MinSuggestQualDrop";
            this.TempMinSuggestQualCombo.Size = new System.Drawing.Size(116, 21);
            this.TempMinSuggestQualCombo.TabIndex = 106;
            // 
            // MaxSuggestPerDayText
            // 
            this.MaxSuggestPerDayText.Location = new System.Drawing.Point(190, 124);
            this.MaxSuggestPerDayText.Name = "MaxSuggestPerDayText";
            this.MaxSuggestPerDayText.Size = new System.Drawing.Size(42, 20);
            this.MaxSuggestPerDayText.TabIndex = 105;
            // 
            // lblMaxNonWorkSuggest
            // 
            this.lblMaxNonWorkSuggest.Location = new System.Drawing.Point(6, 102);
            this.lblMaxNonWorkSuggest.Name = "lblMaxNonWorkSuggest";
            this.lblMaxNonWorkSuggest.Size = new System.Drawing.Size(185, 19);
            this.lblMaxNonWorkSuggest.TabIndex = 98;
            this.lblMaxNonWorkSuggest.Text = "Max Non-work Hours Suggestions:";
            // 
            // lblMaxSuggestPerDay
            // 
            this.lblMaxSuggestPerDay.Location = new System.Drawing.Point(6, 127);
            this.lblMaxSuggestPerDay.Name = "lblMaxSuggestPerDay";
            this.lblMaxSuggestPerDay.Size = new System.Drawing.Size(171, 19);
            this.lblMaxSuggestPerDay.TabIndex = 99;
            this.lblMaxSuggestPerDay.Text = "Max Suggestions Per Day:";
            // 
            // lblMinSuggestQual
            // 
            this.lblMinSuggestQual.Location = new System.Drawing.Point(6, 183);
            this.lblMinSuggestQual.Name = "lblMinSuggestQual";
            this.lblMinSuggestQual.Size = new System.Drawing.Size(124, 19);
            this.lblMinSuggestQual.TabIndex = 81;
            this.lblMinSuggestQual.Text = "Min. Suggestion Quality:";
            // 
            // RequestFBViewDrop
            // 
            this.TempRequestFBViewCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TempRequestFBViewCombo.FormattingEnabled = true;
            this.TempRequestFBViewCombo.Location = new System.Drawing.Point(151, 41);
            this.TempRequestFBViewCombo.Name = "RequestFBViewDrop";
            this.TempRequestFBViewCombo.Size = new System.Drawing.Size(156, 21);
            this.TempRequestFBViewCombo.TabIndex = 83;
            // 
            // lblRequestFBView
            // 
            this.lblRequestFBView.Location = new System.Drawing.Point(6, 43);
            this.lblRequestFBView.Name = "lblRequestFBView";
            this.lblRequestFBView.Size = new System.Drawing.Size(141, 19);
            this.lblRequestFBView.TabIndex = 82;
            this.lblRequestFBView.Text = "Requested Free/Busy View:";
            // 
            // MergeFBIntervalText
            // 
            this.MergeFBIntervalText.Location = new System.Drawing.Point(151, 71);
            this.MergeFBIntervalText.Name = "MergeFBIntervalText";
            this.MergeFBIntervalText.Size = new System.Drawing.Size(42, 20);
            this.MergeFBIntervalText.TabIndex = 107;
            // 
            // lblMergeFBInterval
            // 
            this.lblMergeFBInterval.Location = new System.Drawing.Point(6, 72);
            this.lblMergeFBInterval.Name = "lblMergeFBInterval";
            this.lblMergeFBInterval.Size = new System.Drawing.Size(138, 19);
            this.lblMergeFBInterval.TabIndex = 101;
            this.lblMergeFBInterval.Text = "Merged Free/Busy Interval:";
            // 
            // AvailDataDrop
            // 
            this.TempAvailDataCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TempAvailDataCombo.FormattingEnabled = true;
            this.TempAvailDataCombo.Location = new System.Drawing.Point(151, 14);
            this.TempAvailDataCombo.Name = "AvailDataDrop";
            this.TempAvailDataCombo.Size = new System.Drawing.Size(156, 21);
            this.TempAvailDataCombo.TabIndex = 85;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 19);
            this.label1.TabIndex = 84;
            this.label1.Text = "Requested Availability Data:";
            // 
            // GetAvailabilityButton
            // 
            this.GetAvailabilityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GetAvailabilityButton.Location = new System.Drawing.Point(708, 720);
            this.GetAvailabilityButton.Name = "GetAvailabilityButton";
            this.GetAvailabilityButton.Size = new System.Drawing.Size(120, 23);
            this.GetAvailabilityButton.TabIndex = 86;
            this.GetAvailabilityButton.Text = "Get User Availability";
            this.GetAvailabilityButton.UseVisualStyleBackColor = true;
            this.GetAvailabilityButton.Click += new System.EventHandler(this.GetAvailabilityButton_Click);
            // 
            // AvailabilityDataGroup
            // 
            this.AvailabilityDataGroup.Controls.Add(this.TempAvailDataCombo);
            this.AvailabilityDataGroup.Controls.Add(this.GlobalObjectIdText);
            this.AvailabilityDataGroup.Controls.Add(this.label1);
            this.AvailabilityDataGroup.Controls.Add(this.GlobalObjectIdLabel);
            this.AvailabilityDataGroup.Controls.Add(this.lblRequestFBView);
            this.AvailabilityDataGroup.Controls.Add(this.TempRequestFBViewCombo);
            this.AvailabilityDataGroup.Controls.Add(this.lblMergeFBInterval);
            this.AvailabilityDataGroup.Controls.Add(this.MergeFBIntervalText);
            this.AvailabilityDataGroup.Location = new System.Drawing.Point(313, 125);
            this.AvailabilityDataGroup.Name = "AvailabilityDataGroup";
            this.AvailabilityDataGroup.Size = new System.Drawing.Size(319, 221);
            this.AvailabilityDataGroup.TabIndex = 111;
            this.AvailabilityDataGroup.TabStop = false;
            this.AvailabilityDataGroup.Text = "Availability Data Options";
            // 
            // GlobalObjectIdText
            // 
            this.GlobalObjectIdText.Location = new System.Drawing.Point(151, 99);
            this.GlobalObjectIdText.Name = "GlobalObjectIdText";
            this.GlobalObjectIdText.Size = new System.Drawing.Size(156, 20);
            this.GlobalObjectIdText.TabIndex = 113;
            // 
            // GlobalObjectIdLabel
            // 
            this.GlobalObjectIdLabel.Location = new System.Drawing.Point(6, 102);
            this.GlobalObjectIdLabel.Name = "GlobalObjectIdLabel";
            this.GlobalObjectIdLabel.Size = new System.Drawing.Size(138, 19);
            this.GlobalObjectIdLabel.TabIndex = 112;
            this.GlobalObjectIdLabel.Text = "Global Object ID:";
            // 
            // RemoveAttendeeButton
            // 
            this.RemoveAttendeeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveAttendeeButton.Location = new System.Drawing.Point(211, 296);
            this.RemoveAttendeeButton.Name = "RemoveAttendeeButton";
            this.RemoveAttendeeButton.Size = new System.Drawing.Size(88, 23);
            this.RemoveAttendeeButton.TabIndex = 34;
            this.RemoveAttendeeButton.Text = "Remove";
            this.RemoveAttendeeButton.UseVisualStyleBackColor = true;
            this.RemoveAttendeeButton.Click += new System.EventHandler(this.RemoveAttendeeButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RemoveAttendeeButton);
            this.groupBox2.Controls.Add(this.AttendeeList);
            this.groupBox2.Controls.Add(this.AddAttendeeButton);
            this.groupBox2.Location = new System.Drawing.Point(5, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 334);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attendees";
            // 
            // AttendeeList
            // 
            this.AttendeeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SmtpColumn,
            this.TypeColumn,
            this.ExcludeConflictsColumn});
            this.AttendeeList.Dock = System.Windows.Forms.DockStyle.Top;
            this.AttendeeList.FullRowSelect = true;
            this.AttendeeList.HideSelection = false;
            this.AttendeeList.Location = new System.Drawing.Point(3, 16);
            this.AttendeeList.MultiSelect = false;
            this.AttendeeList.Name = "AttendeeList";
            this.AttendeeList.Size = new System.Drawing.Size(296, 274);
            this.AttendeeList.TabIndex = 1;
            this.AttendeeList.UseCompatibleStateImageBehavior = false;
            this.AttendeeList.View = System.Windows.Forms.View.Details;
            this.AttendeeList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.AttendeeList_ItemSelectionChanged);
            // 
            // SmtpColumn
            // 
            this.SmtpColumn.Text = "SMTP Address";
            this.SmtpColumn.Width = 83;
            // 
            // TypeColumn
            // 
            this.TypeColumn.Text = "Attendee Type";
            this.TypeColumn.Width = 111;
            // 
            // ExcludeConflictsColumn
            // 
            this.ExcludeConflictsColumn.Text = "Exclude Conflicts";
            this.ExcludeConflictsColumn.Width = 94;
            // 
            // AvailabilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(920, 755);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.AttendeeAvailabilityGroup);
            this.Controls.Add(this.AvailabilityDataGroup);
            this.Controls.Add(this.GetAvailabilityButton);
            this.Controls.Add(this.TimeWindowGroup);
            this.Controls.Add(this.SuggestionsGroup);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AvailabilityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Availability";
            this.Load += new System.EventHandler(this.AvailabilityDialog_Load);
            this.TimeWindowGroup.ResumeLayout(false);
            this.TimeWindowGroup.PerformLayout();
            this.DetailWindowGroup.ResumeLayout(false);
            this.AttendeeAvailabilityGroup.ResumeLayout(false);
            this.SuggestionsGroup.ResumeLayout(false);
            this.SuggestionsGroup.PerformLayout();
            this.AvailabilityDataGroup.ResumeLayout(false);
            this.AvailabilityDataGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label StartTimeWindowLabel;
        private System.Windows.Forms.DateTimePicker EndWindowDate;
        private System.Windows.Forms.DateTimePicker StartWindowDate;
        private System.Windows.Forms.Label EndTimeWindowLabel;
        private System.Windows.Forms.GroupBox TimeWindowGroup;
        private System.Windows.Forms.GroupBox DetailWindowGroup;
        private System.Windows.Forms.Label StartDetailLabel;
        private System.Windows.Forms.DateTimePicker EndDetailDate;
        private System.Windows.Forms.Label EndDetailLabel;
        private System.Windows.Forms.DateTimePicker StartDetailDate;
        private System.Windows.Forms.GroupBox AttendeeAvailabilityGroup;
        private System.Windows.Forms.GroupBox SuggestionsGroup;
        private System.Windows.Forms.Button AddAttendeeButton;
        private System.Windows.Forms.ComboBox TempRequestFBViewCombo;
        private System.Windows.Forms.Label lblRequestFBView;
        private System.Windows.Forms.Label lblMinSuggestQual;
        private System.Windows.Forms.TextBox MergeFBIntervalText;
        private System.Windows.Forms.TextBox MeetingDurationText;
        private System.Windows.Forms.TextBox MaxSuggestPerDayText;
        private System.Windows.Forms.TextBox MaxNonWorkSuggestText;
        private System.Windows.Forms.Label lblMergeFBInterval;
        private System.Windows.Forms.Label lblMeetingDuration;
        private System.Windows.Forms.Label lblMaxSuggestPerDay;
        private System.Windows.Forms.Label lblMaxNonWorkSuggest;
        private System.Windows.Forms.ComboBox TempAvailDataCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GoodSuggestThresholdText;
        private System.Windows.Forms.Label lblGoodSuggestThreshold;
        private System.Windows.Forms.ComboBox TempMinSuggestQualCombo;
        private System.Windows.Forms.Button GetAvailabilityButton;
        private System.Windows.Forms.Label CurrentMeetingLabel;
        private System.Windows.Forms.CheckBox CurrentMeetingCheck;
        private System.Windows.Forms.DateTimePicker CurrentMeetingDate;
        private System.Windows.Forms.GroupBox AvailabilityDataGroup;
        private System.Windows.Forms.ListView CalEventsList;
        private System.Windows.Forms.TextBox GlobalObjectIdText;
        private System.Windows.Forms.Label GlobalObjectIdLabel;
        private System.Windows.Forms.ColumnHeader FreeBusyStatusColumn;
        private System.Windows.Forms.ColumnHeader StartTimeColumn;
        private System.Windows.Forms.ColumnHeader EndTimeColumn;
        private System.Windows.Forms.ListView AttendeeAvailabilityList;
        private System.Windows.Forms.ColumnHeader ViewTypeColumn;
        private System.Windows.Forms.ColumnHeader WorkingHoursColumn;
        private System.Windows.Forms.ColumnHeader MergedFreeBusyStatusColumn;
        private System.Windows.Forms.ColumnHeader SubjectColumn;
        private System.Windows.Forms.ColumnHeader LocationColumn;
        private System.Windows.Forms.ColumnHeader IsExceptionColumn;
        private System.Windows.Forms.ColumnHeader IsMeetingColumn;
        private System.Windows.Forms.ColumnHeader IsPrivateColumn;
        private System.Windows.Forms.ColumnHeader IsRecurringColumn;
        private System.Windows.Forms.ColumnHeader IsReminderSetColumn;
        private System.Windows.Forms.ColumnHeader StoreIdColumn;
        private System.Windows.Forms.Button RemoveAttendeeButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView AttendeeList;
        private System.Windows.Forms.ColumnHeader SmtpColumn;
        private System.Windows.Forms.ColumnHeader TypeColumn;
        private System.Windows.Forms.ColumnHeader ExcludeConflictsColumn;
        private System.Windows.Forms.ListView SuggestionsList;
        private System.Windows.Forms.ColumnHeader DateColumn;
        private System.Windows.Forms.ColumnHeader DateQualityColumn;
        private System.Windows.Forms.ColumnHeader TimeColumn;
        private System.Windows.Forms.ColumnHeader TimeQualityColumn;
        private System.Windows.Forms.ColumnHeader ConflictsColumn;
        private System.Windows.Forms.ColumnHeader IsWorkTimeColumn;
    }
}