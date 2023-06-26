namespace EWSEditor.CustomForm
{
    partial class EWSTest
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblExchangeServiceURLTextDesc = new System.Windows.Forms.Label();
            this.ExchangeServiceURLText = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.lblExImp = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblDomain = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox_SharedCalendarUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblExchangeServiceURLTextDesc);
            this.panel1.Controls.Add(this.ExchangeServiceURLText);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 55);
            this.panel1.TabIndex = 1;
            // 
            // lblExchangeServiceURLTextDesc
            // 
            this.lblExchangeServiceURLTextDesc.AutoSize = true;
            this.lblExchangeServiceURLTextDesc.Enabled = false;
            this.lblExchangeServiceURLTextDesc.Location = new System.Drawing.Point(8, 9);
            this.lblExchangeServiceURLTextDesc.Name = "lblExchangeServiceURLTextDesc";
            this.lblExchangeServiceURLTextDesc.Size = new System.Drawing.Size(281, 13);
            this.lblExchangeServiceURLTextDesc.TabIndex = 6;
            this.lblExchangeServiceURLTextDesc.Text = "Example: https://mail.contoso.com/EWS/Exchange.asmx";
            // 
            // ExchangeServiceURLText
            // 
            this.ExchangeServiceURLText.Location = new System.Drawing.Point(11, 25);
            this.ExchangeServiceURLText.Name = "ExchangeServiceURLText";
            this.ExchangeServiceURLText.Size = new System.Drawing.Size(386, 20);
            this.ExchangeServiceURLText.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtDomain);
            this.panel2.Controls.Add(this.lblExImp);
            this.panel2.Controls.Add(this.lblPassword);
            this.panel2.Controls.Add(this.lblDomain);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.txtUserName);
            this.panel2.Controls.Add(this.lblUserName);
            this.panel2.Location = new System.Drawing.Point(12, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(408, 92);
            this.panel2.TabIndex = 2;
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(89, 59);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(308, 20);
            this.txtDomain.TabIndex = 7;
            // 
            // lblExImp
            // 
            this.lblExImp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExImp.AutoSize = true;
            this.lblExImp.Location = new System.Drawing.Point(23, -89);
            this.lblExImp.Name = "lblExImp";
            this.lblExImp.Size = new System.Drawing.Size(0, 13);
            this.lblExImp.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 41);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(8, 66);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(46, 13);
            this.lblDomain.TabIndex = 6;
            this.lblDomain.Text = "Domain:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(89, 34);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(308, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(89, 8);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(308, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(8, 15);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "User Name:";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.textBox_SharedCalendarUser);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(12, 171);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(408, 255);
            this.panel5.TabIndex = 12;
            // 
            // textBox_SharedCalendarUser
            // 
            this.textBox_SharedCalendarUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SharedCalendarUser.Location = new System.Drawing.Point(13, 29);
            this.textBox_SharedCalendarUser.Multiline = true;
            this.textBox_SharedCalendarUser.Name = "textBox_SharedCalendarUser";
            this.textBox_SharedCalendarUser.Size = new System.Drawing.Size(388, 210);
            this.textBox_SharedCalendarUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "List of Email Adress";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(408, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EWSTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 467);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EWSTest";
            this.Text = "EWSTest";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblExchangeServiceURLTextDesc;
        private System.Windows.Forms.TextBox ExchangeServiceURLText;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label lblExImp;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox_SharedCalendarUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}