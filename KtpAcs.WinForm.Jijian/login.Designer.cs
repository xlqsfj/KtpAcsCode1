﻿namespace KtpAcs.WinForm.Jijian
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.FormErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.picClose = new DevExpress.XtraEditors.PictureEdit();
            this.btn_send = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.LoginBtn = new DevExpress.XtraEditors.SimpleButton();
            this.PasswordTxt = new DevExpress.XtraEditors.TextEdit();
            this.UserNameTxt = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.FormErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNameTxt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // FormErrorProvider
            // 
            this.FormErrorProvider.ContainerControl = this;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.picClose);
            this.panelControl1.Controls.Add(this.btn_send);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.LoginBtn);
            this.panelControl1.Controls.Add(this.PasswordTxt);
            this.panelControl1.Controls.Add(this.UserNameTxt);
            this.panelControl1.Location = new System.Drawing.Point(-1, 1);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(554, 412);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseDown);
            this.panelControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseMove);
            this.panelControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseUp);
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.EditValue = global::KtpAcs.WinForm.Jijian.Properties.Resources.img_gi;
            this.picClose.Location = new System.Drawing.Point(509, 5);
            this.picClose.Name = "picClose";
            this.picClose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picClose.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picClose.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picClose.Size = new System.Drawing.Size(40, 34);
            this.picClose.TabIndex = 21;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // btn_send
            // 
            this.btn_send.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(248)))));
            this.btn_send.Appearance.Options.UseBackColor = true;
            this.btn_send.Location = new System.Drawing.Point(458, 183);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(92, 23);
            this.btn_send.TabIndex = 20;
            this.btn_send.Text = "验证码";
            this.btn_send.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(300, 164);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "验证码";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(300, 92);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 19;
            this.labelControl1.Text = "手机号";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::KtpAcs.WinForm.Jijian.Properties.Resources.img_login1;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(265, 412);
            this.pictureEdit1.TabIndex = 17;
            this.pictureEdit1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureEdit1_MouseDown);
            this.pictureEdit1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureEdit1_MouseMove);
            this.pictureEdit1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureEdit1_MouseUp);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(248)))));
            this.LoginBtn.Appearance.ForeColor = System.Drawing.Color.Red;
            this.LoginBtn.Appearance.Options.UseBackColor = true;
            this.LoginBtn.Appearance.Options.UseForeColor = true;
            this.LoginBtn.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(248)))));
            this.LoginBtn.AppearanceDisabled.Options.UseBackColor = true;
            this.LoginBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LoginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginBtn.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.LoginBtn.Location = new System.Drawing.Point(314, 284);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(201, 34);
            this.LoginBtn.TabIndex = 16;
            this.LoginBtn.Text = "登 录";
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.EditValue = "";
            this.PasswordTxt.Location = new System.Drawing.Point(300, 184);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.Size = new System.Drawing.Size(152, 20);
            this.PasswordTxt.TabIndex = 15;
            this.PasswordTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordTxt_KeyDown);
            // 
            // UserNameTxt
            // 
            this.UserNameTxt.EditValue = "";
            this.UserNameTxt.Location = new System.Drawing.Point(300, 112);
            this.UserNameTxt.Name = "UserNameTxt";
            this.UserNameTxt.Size = new System.Drawing.Size(246, 20);
            this.UserNameTxt.TabIndex = 14;
            // 
            // Login
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 412);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = global::KtpAcs.WinForm.Jijian.Properties.Resources.ktp_logo;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Login_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.FormErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNameTxt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider FormErrorProvider;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit picClose;
        private DevExpress.XtraEditors.SimpleButton btn_send;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton LoginBtn;
        private DevExpress.XtraEditors.TextEdit PasswordTxt;
        private DevExpress.XtraEditors.TextEdit UserNameTxt;
    }
}

