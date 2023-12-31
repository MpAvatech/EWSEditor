﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;
using EWSEditor.Forms.Controls;
using EWSEditor.Logging;
using EWSEditor.Common;

namespace EWSEditor.Forms
{
    public partial class MessageForm : Form
    {
        private bool _IsExistingEmail = false;
        private bool _CanEdit = false;
        private bool _CanSend = false;
        private bool _CanReply = false;

        private ExchangeService _CurrentService = null;
        private EmailMessage _EmailMessage = null;
        //private EditMessageType _EditMessageType;
        private EmailMessage _ResponseMessage = null;
 
 
        public enum EditMessageType
        {
            IsExistingDraft,
            IsExistingSent,
            IsNew,
            IsResponding
        }

        public MessageForm()
        {
            InitializeComponent();
        }

        //public MessageForm(EwsCaller oEwsCaller)
        //{
        //    InitializeComponent();
        //    _EwsCaller = oEwsCaller;

        //    _IsExistingEmail = false;
        //    _CanEdit = true;
        //    _CanSend = true;
        //    _CanReply = true;
             
        //    ClearForm();
        //}

        // Existing Message
        public MessageForm(ExchangeService CurrentService, ItemId oItemId)
        {
            InitializeComponent();

            _CurrentService = CurrentService;

            _EmailMessage = LoadEmailMessageForEdit(_CurrentService, oItemId);
            _IsExistingEmail = true;

            _CanEdit = false;
            _CanSend = false;
            _CanReply = true;
            if (_EmailMessage.IsNew)  // is stored?
            {
                // _EditMessageType = EditMessageType.IsNew;
                _CanEdit = true;
                _CanSend = true;
                _CanReply = false;
            }


            //if (_EmailMessage.IsSubmitted)  // is stored?
            //{
            //    //_EditMessageType = EditMessageType.IsNew;
            //    _CanEdit = false;
            //    _CanSend = false;
            //    _CanReply = true;
            //}


            SetFormFromMessage(_EmailMessage, _CanEdit, _CanSend, _CanReply);
        }

        // New Message
        public MessageForm(ExchangeService CurrentService, FolderId oFolderId)
        {
            InitializeComponent();

            _CurrentService = CurrentService;
            _EmailMessage = new EmailMessage(_CurrentService);
            //_EmailMessage = LoadEmailMessageForEdit(oEwsCaller, oItemId);
            _IsExistingEmail = false;
           
 
            if (_EmailMessage.IsNew)  // is stored?
            {
                // _EditMessageType = EditMessageType.IsNew;
                _CanEdit = true;
                _CanSend = true;
                _CanReply = false;
            }
 
            SetFormFromMessage(_EmailMessage, _CanEdit, _CanSend, _CanReply);
        }
        public MessageForm(ExchangeService CurrentService, ref EmailMessage oEmailMessage)
        {
            InitializeComponent();
            _CurrentService = CurrentService;
            _EmailMessage = oEmailMessage;
            _IsExistingEmail = true;

            _CanEdit = false;
            _CanSend = false;
            _CanReply = true;
            if (_EmailMessage.IsNew)  // is stored?
            {
               // _EditMessageType = EditMessageType.IsNew;
                _CanEdit = true;
                _CanSend = true;
                _CanReply = false;
            }

            
             
            //if (_EmailMessage.IsSubmitted)  // is sent?
            //{
            //    //_EditMessageType = EditMessageType.IsNew;
            //    _CanEdit = false;
            //    _CanSend = false;
            //    _CanReply = true;
            //}


            SetFormFromMessage(oEmailMessage, _CanEdit, _CanSend, _CanReply);
        }

        private EmailMessage LoadEmailMessageForEdit(ExchangeService CurrentService, ItemId oItemId)
        {
 
            ItemView oView = new ItemView(9999);
            PropertySet oPropertySet = new PropertySet(BasePropertySet.FirstClassProperties);      
            //oPropertySet.Add(ItemSchema.ExtendedProperties);
            oPropertySet.Add(EmailMessageSchema.ExtendedProperties);
            EmailMessage oEmailMessage = EmailMessage.Bind(CurrentService, oItemId);
             
 
           
            return oEmailMessage;
        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_ResponseMessage == null)
            {
                SendMessage(true);

                this.Close();
            }
        }

        private bool SendMessage(bool bSaveCopy)
        {
            bool bRet = false;
             
            //if (_ResponseMessage == null)
           // {
               bRet = SetMessageFromForm(ref _EmailMessage);
               if (_ResponseMessage == null)
               {
                    try
                    {
                        if (bSaveCopy)
                            _EmailMessage.SendAndSaveCopy();
                        else
                            _EmailMessage.Send();
                        bRet = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Sending Message");
                        bRet = false;
                    }
                }
            //}
            //else
            //    try
            //    {
            //        if (bSaveCopy)
            //            _ResponseMessage.SendAndSaveCopy();
            //        else
            //            _ResponseMessage.Send();
            //        bRet = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Error Sending Response Message");
            //        bRet = false;
            //    }
            return bRet;
        }

        //private bool SendReplyMessage(ref EmailMessage oEmailMessage, bool bReplyAll)
        //{
        //    bool bRet = false;
        //    try
        //    {
        //        ResponseMessage oResponseMessage = oEmailMessage.CreateReply(bReplyAll);
        //        if (SetMessageFromForm(ref oEmailMessage))
        //        {
        //            oEmailMessage.SendAndSaveCopy();
        //        }
        //        btnSave.Enabled = false;
        //        btnSend.Enabled = false;
        //        //_EditMessageType = EditMessageType.IsExistingSent;
        //        bRet = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error Sending Reply Message");
        //    }

        //    return bRet;
        //}

        //private bool MakeForwardedMessage(ref EmailMessage oEmailMessage)
        //{
        //    bool bRet = false;
        //    MessageResponseForm oMessageResponseForm = null;
        //    oMessageResponseForm = new MessageResponseForm(ref oEmailMessage, ResponseMessageType.Forward);
        //    oMessageResponseForm.ShowDialog();
        //    oMessageResponseForm = null;

        //    //SetFormFromResponseMessage(_ResponseMessage);
 
        //    return bRet;
        //}

        //private bool MakeReplyMessage(ref EmailMessage oEmailMessage, bool bReplyAll)
        //{
        //    bool bRet = false;
            
        //    MessageResponseForm oMessageResponseForm = null;
         
        //    if (bReplyAll == true)
        //        oMessageResponseForm = new MessageResponseForm(ref oEmailMessage, ResponseMessageType.ReplyAll);
        //    else
        //        oMessageResponseForm = new MessageResponseForm(ref oEmailMessage, ResponseMessageType.Reply);
        //    oMessageResponseForm.ShowDialog();
        //    oMessageResponseForm = null;
        //    //SetFormFromResponseMessage(_ResponseMessage);
        //    return bRet;
        //}

 

        private bool SetMessageFromForm(ref EmailMessage oEmailMessage)
        {
            bool bRet = true;
            

            bRet = SetRecipientsFromString(ref oEmailMessage, "To", txtTo.Text.Trim());
            if (bRet) bRet = SetRecipientsFromString(ref oEmailMessage, "CC", txtCC.Text.Trim());
            if (bRet) bRet = SetRecipientsFromString(ref oEmailMessage, "BCC", txtBCC.Text.Trim());
             
            //SetRecipientsFromString(ref oEmailMessage, ref oEmailMessage.ToRecipients, txtCC.Text.Trim());

            if (bRet)
            {
                try
                {
                    oEmailMessage.Subject = txtSubject.Text.Trim();
                     
 
                    if (cmboMessageType.Text == "HTML")
                    {
                        //oEmailMessage.Body.BodyType = BodyType.HTML;
                        oEmailMessage.Body = new MessageBody(BodyType.HTML, txtBody.Text);
                    }
                    else
                    {
                        oEmailMessage.Body = new MessageBody(BodyType.Text, txtBody.Text);
                    }
                    //oEmailMessage.Body.Text = txtBody.Text;
                    oEmailMessage.IsReadReceiptRequested = chkReadReceipt.Checked;
                    oEmailMessage.IsDeliveryReceiptRequested = chkDeliveryReceipt.Checked;
                }
 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error setting properties on message");
                    bRet = false;
                }
            }

            return bRet;

        }

        private bool SetRecipientsFromString(ref EmailMessage oMessage, string sRecipeintLine, string sAddressString)
        {
            bool bRet = false;
 
            char[] arrAddressDelimiter   = {';'};
            string[] sAddresses = sAddressString.Split(arrAddressDelimiter);
            string sSmtpAddress = string.Empty;
            string sRecipientName = string.Empty;
            string sWork = string.Empty;
            char[]  carrEndAddress  = {'>',')'};
            char[]  carrStartAddress  = {'<', ')'};
            char[]  junk = {'<','>','(', ' ', ';'};
            int iEndAddress = 0;
            int iStartAddress = 0;
            string sCurrentAddress = string.Empty;
            try
            {
                foreach (string sAddress in sAddresses)
                {

                    sCurrentAddress = sAddress;

                    iStartAddress = sAddress.IndexOfAny(carrStartAddress);
                    iEndAddress = sAddress.IndexOfAny(carrEndAddress);

                    sSmtpAddress = string.Empty;
                    sRecipientName = string.Empty;
                    if (sAddress.Contains('>') || sAddress.Contains(')'))
                    {
                        sWork = sAddress.Substring(iStartAddress, iEndAddress - iStartAddress);
                        sSmtpAddress = sWork.Trim(junk);

                        sWork = sWork.Substring(0, iStartAddress);
                        sRecipientName = sWork.Trim(junk);
                    }
                    else
                    {
                        sWork = sAddress.Trim(junk);
                        if (sWork.Length != 0)
                            sSmtpAddress = sAddress.Trim(junk);

                    }

                    if (sRecipientName.Length != 0)
                    {
                        if (sSmtpAddress.Length != 0)
                        {
                            switch (sRecipeintLine)
                            {
                                case "To":
                                    oMessage.ToRecipients.Add(new EmailAddress(sRecipientName, sSmtpAddress));
                                    break;
                                case "CC":
                                    oMessage.CcRecipients.Add(new EmailAddress(sRecipientName, sSmtpAddress));
                                    break;
                                case "BCC":
                                    oMessage.BccRecipients.Add(new EmailAddress(sRecipientName, sSmtpAddress));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (sSmtpAddress.Length != 0)
                        {
                            switch (sRecipeintLine)
                            {
                                case "To":
                                    oMessage.ToRecipients.Add(new EmailAddress(sSmtpAddress));
                                    break;
                                case "CC":
                                    oMessage.CcRecipients.Add(new EmailAddress(sSmtpAddress));
                                    break;
                                case "BCC":
                                    oMessage.BccRecipients.Add(new EmailAddress(sSmtpAddress));
                                    break;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n\r\nAddress: " + sCurrentAddress, "Error Parsing Address");
            } 
            
            bRet = true;
 
            return bRet;
 
        }

       

        private void ClearForm()
        {
            txtTo.Text = string.Empty;
            txtBCC.Text = string.Empty;
            txtCC.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtBody.Text = string.Empty;
            chkDeliveryReceipt.Checked = false;
            chkReadReceipt.Checked = false;
            cmboMessageType.Text = string.Empty;
        }
 
        private bool SetFormFromMessage(EmailMessage oEmailMessage, bool  CanEdit, bool  CanSend, bool  CanReply) 
        {
            bool bRet = false;
            int iNothing = 0;
            iNothing = iNothing + 0;
            //_CanEdit =  CanEdit;
            //_CanSend = CanSend;
            //_CanReply = CanReply;

            ClearForm();

            if (_IsExistingEmail == true)
            {


                //message.Attachments.AddFileAttachment("<path to file>");
                foreach (EmailAddress oAddress in oEmailMessage.ToRecipients)
                {
                    txtTo.Text += oAddress.Address + "; ";
                }
                foreach (EmailAddress oAddress in oEmailMessage.BccRecipients)
                {
                    txtBCC.Text += oAddress.Address + "; ";
                }
                foreach (EmailAddress oAddress in oEmailMessage.CcRecipients)
                {
                    txtCC.Text += oAddress.Address + "; ";
                }

                txtSubject.Text = oEmailMessage.Subject;

                if (oEmailMessage.Body.BodyType == BodyType.HTML)
                    cmboMessageType.Text = "HTML";
                else
                    cmboMessageType.Text = "Text";



                txtBody.Text = oEmailMessage.Body.Text;

                try
                {
                    chkDeliveryReceipt.Checked = oEmailMessage.IsDeliveryReceiptRequested;

                }
                catch (Exception ex_IsDeliveryReceiptRequested)
                {

                    chkDeliveryReceipt.Checked = false;
                    { if (ex_IsDeliveryReceiptRequested != null) iNothing = 0; }
                }

                try
                {

                    chkReadReceipt.Checked = oEmailMessage.IsReadReceiptRequested;

                }
                catch (Exception ex_IsReadReceiptRequested)
                {
                    chkReadReceipt.Checked = false;
                    { if (ex_IsReadReceiptRequested != null) iNothing = 0; }
                }

                //oEmailMessage.Sensitivity = Sensitivity.

                //if (oEmailMessage.IsDraft == true)
                btnSend.Enabled = CanSend;


                // if (oEmailMessage.IsNew)
                if (CanEdit == true)
                {
                    // _EditMessageType = EditMessageType.IsNew;
                    btnSave.Enabled = true;
                    chkReadReceipt.Enabled = true;
                    chkDeliveryReceipt.Enabled = true;
                }
                else
                {
                    // _EditMessageType = EditMessageType.IsExistingSent;
                    btnSave.Enabled = false;
                }


                btnReply.Enabled = CanReply;
                btnReplyAll.Enabled = CanReply;
                btnForward.Enabled = CanReply;
                System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
                System.Diagnostics.Debug.WriteLine("Loaded existing email:");
                System.Diagnostics.Debug.WriteLine("    ChangeKey: ", oEmailMessage.ConversationId.ChangeKey);
                System.Diagnostics.Debug.WriteLine("    UniqueId: ", oEmailMessage.ConversationId.UniqueId);
                System.Diagnostics.Debug.WriteLine("    Subject: ", oEmailMessage.Subject);
                System.Diagnostics.Debug.WriteLine("    ConversationTopic: ", oEmailMessage.ConversationTopic);
                System.Diagnostics.Debug.WriteLine("    ConversationIndex: \r\n", StringHelper.HexStringFromByteArray(oEmailMessage.ConversationIndex) + "\r\n");
                System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
            }
            else
            {
                btnSend.Enabled = CanSend; 
                btnSave.Enabled = true;
                chkReadReceipt.Enabled = true;
                chkDeliveryReceipt.Enabled = true;
                cmboMessageType.Text = "Text";
                System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
                System.Diagnostics.Debug.WriteLine("New email started.");
                System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
            }
             
 
            return bRet;
        }




        private bool SaveCurrentMessage()
        {
            bool bRet = false;
            try
            {

 
                if (SetMessageFromForm(ref _EmailMessage))
                {

                    if (_IsExistingEmail == false)
                        _EmailMessage.Save(_EmailMessage.ParentFolderId);
                    else
                        _EmailMessage.Update(ConflictResolutionMode.AutoResolve);
                }
                bRet = true;
            }
            catch (Exception ex)
            {
 
                MessageBox.Show(ex.Message, "Error Saving Message");
            }
            return bRet;
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {

        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            bool bRet = false;

            bRet = MakeResponseMessage(ResponseMessageType.Forward);


 
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            bool bRet = false;

            bRet = MakeResponseMessage(ResponseMessageType.Reply);
 
        }

        private void txtBody_TextChanged(object sender, EventArgs e)
        {

        }

        private bool MakeResponseMessage(ResponseMessageType oResponseMessageType)
        {
            bool bRet = false;
            MessageBody oMessageBody = new MessageBody("----\r\n");
            ResponseMessage oResponseMessage = null;
             
            string sWindowTitle =string.Empty;
            try
            {
                if (oResponseMessageType == ResponseMessageType.Reply)
                {
                    oResponseMessage = _EmailMessage.CreateReply(false);
                    sWindowTitle = "Reply Message";
                }
                if (oResponseMessageType == ResponseMessageType.ReplyAll)
                {
                    oResponseMessage = _EmailMessage.CreateReply(true);
                    sWindowTitle = "Reply All Message";
                }
                if (oResponseMessageType == ResponseMessageType.Forward)
                {
                    oResponseMessage = _EmailMessage.CreateForward();
                    sWindowTitle = "Forward Message";

                }

                oResponseMessage.BodyPrefix = "===========\r\n";
                // Save as drafts AND set as new current message.
                _EmailMessage = oResponseMessage.Save(WellKnownFolderName.Drafts);
                _EmailMessage.Load();

                SetFormFromMessage(_EmailMessage, true, true, false);

                bRet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error creating message");
                bRet = false;
            }
            return bRet;
        }

 
        //private bool SetFormFromResponseMessage(ResponseMessage oEmailMessage)
        //{
        //    bool bRet = false;
        //    ClearForm();
        //    //message.Attachments.AddFileAttachment("<path to file>");
        //    foreach (EmailAddress oAddress in oEmailMessage.ToRecipients)
        //    {
        //        txtTo.Text += oAddress.Address + "; ";
        //    }
        //    foreach (EmailAddress oAddress in oEmailMessage.BccRecipients)
        //    {
        //        txtBCC.Text += oAddress.Address + "; ";
        //    }
        //    foreach (EmailAddress oAddress in oEmailMessage.CcRecipients)
        //    {
        //        txtCC.Text += oAddress.Address + "; ";
        //    }

        //    txtSubject.Text = oEmailMessage.Subject;

        //    if (oEmailMessage.Body.BodyType == BodyType.HTML)
        //        cmboMessageType.Text = "HTML";
        //    else
        //        cmboMessageType.Text = "Text";

        //    txtBody.Text = oEmailMessage.Body.Text;
        //    chkDeliveryReceipt.Checked = oEmailMessage.IsReadReceiptRequested;
        //    chkReadReceipt.Checked = oEmailMessage.IsDeliveryReceiptRequested;
 
        //    //if (oEmailMessage.IsDraft == true)
        //        btnSend.Enabled = true;
        //    //else
        //       // btnSend.Enabled = false;

        //    //if (oEmailMessage.IsNew)
        //    //{
        //    //    _EditMessageType = EditMessageType.IsNew;
        //    //    btnSave.Enabled = true;
        //    //}
        //    //else
        //    //{
        //    //    _EditMessageType = EditMessageType.IsExistingSent;
        //    //    btnSave.Enabled = false;
        //    //}

        //    btnReply.Enabled = false;
        //    btnReplyAll.Enabled = false;
        //    btnForward.Enabled = false;
             
        //    //System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
        //    //System.Diagnostics.Debug.WriteLine("ChangeKey: ", oEmailMessage.ConversationId.ChangeKey);
        //    //System.Diagnostics.Debug.WriteLine("UniqueId: ", oEmailMessage.ConversationId.UniqueId);
        //    //System.Diagnostics.Debug.WriteLine("Subject: ", oEmailMessage.Subject);
        //    //System.Diagnostics.Debug.WriteLine("ConversationTopic: ", oEmailMessage.ConversationTopic);
        //    //System.Diagnostics.Debug.WriteLine("ConversationIndex: \r\n", StringHelper.HexStringFromByteArray(oEmailMessage.ConversationIndex) + "\r\n");
        //    //System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
        //    return bRet;
        //}
         

 

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCurrentMessage();
            this.Close();
        }

        private void btnHeaders_Click(object sender, EventArgs e)
        {
            string sInfo = string.Empty;

            if (_EmailMessage.InternetMessageHeaders != null)
            {
                foreach (InternetMessageHeader oHeader in _EmailMessage.InternetMessageHeaders)
                {
                    sInfo += oHeader.Name + ": " + oHeader.Value + "\r\n";
                }

                ShowTextDocument oForm = new ShowTextDocument();
                oForm.Text = "Message Headers";
                oForm.txtEntry.Text = sInfo;
                oForm.ShowDialog();
                oForm = null;

            }
            //else
            //{
            //    MessageBox.Show("Message has no headers");
            //}
 
        }

        private void btnReplyAll_Click(object sender, EventArgs e)
        {
            //bool bRet = true;
            //bRet = MakeResponseMessage(ResponseMessageType.ReplyAll);
 
        }

        private void btnSimpleForward_Click(object sender, EventArgs e)
        {
            //MessageEasyResponseForm oForm = null;
            //oForm = new MessageEasyResponseForm(ref _EmailMessage, ResponseMessageType.Forward);
            //oForm.ShowDialog();
            //oForm = null;
        }

        private void btnSimpleReply_Click(object sender, EventArgs e)
        {

        }

        //private void btnProperties_Click(object sender, EventArgs e)
        //{
        //    string sInfo = string.Empty;
        //    sInfo = _EwsCaller.GetItemInfo(_EmailMessage.Id, "IPM.Note");
        //    ShowTextDocument oForm = new ShowTextDocument();
        //    oForm.Text = "Item Properties";
        //    oForm.txtEntry.Text = sInfo;
        //    oForm.ShowDialog();
        //    oForm = null;
        //}

        private void chkDeliveryReceipt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {

        }
        

    }
}
