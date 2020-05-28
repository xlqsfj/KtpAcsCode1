namespace KtpAcs.WinForm.Jijian.Device
{
    partial class AddDevice
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lab = new DevExpress.XtraEditors.LabelControl();
            this.labName1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_deviceId = new DevExpress.XtraEditors.TextEdit();
            this.txtDeviceIp = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txt_description = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.FormErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.radIsEnter = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.txt_deviceId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeviceIp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_description.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radIsEnter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(114, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "编号(设备号)";
            // 
            // lab
            // 
            this.lab.Location = new System.Drawing.Point(114, 101);
            this.lab.Name = "lab";
            this.lab.Size = new System.Drawing.Size(35, 15);
            this.lab.TabIndex = 0;
            this.lab.Text = "IP地址";
            // 
            // labName1
            // 
            this.labName1.Location = new System.Drawing.Point(112, 216);
            this.labName1.Name = "labName1";
            this.labName1.Size = new System.Drawing.Size(72, 15);
            this.labName1.TabIndex = 0;
            this.labName1.Text = "是否进场方向";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(114, 162);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 15);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "描述";
            // 
            // txt_deviceId
            // 
            this.txt_deviceId.Enabled = false;
            this.txt_deviceId.Location = new System.Drawing.Point(199, 54);
            this.txt_deviceId.Name = "txt_deviceId";
            this.txt_deviceId.Size = new System.Drawing.Size(289, 22);
            this.txt_deviceId.TabIndex = 1;
            // 
            // txtDeviceIp
            // 
            this.txtDeviceIp.Location = new System.Drawing.Point(199, 97);
            this.txtDeviceIp.Name = "txtDeviceIp";
            this.txtDeviceIp.Size = new System.Drawing.Size(289, 22);
            this.txtDeviceIp.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(248)))));
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Location = new System.Drawing.Point(210, 264);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(248)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Location = new System.Drawing.Point(355, 264);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 37);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txt_description
            // 
            this.txt_description.EditValue = "";
            this.txt_description.Location = new System.Drawing.Point(199, 125);
            this.txt_description.Name = "txt_description";
            this.txt_description.Properties.AutoHeight = false;
            this.txt_description.Size = new System.Drawing.Size(289, 72);
            this.txt_description.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(156, 104);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(5, 15);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "*";
            // 
            // FormErrorProvider
            // 
            this.FormErrorProvider.ContainerControl = this;
            // 
            // radIsEnter
            // 
            this.radIsEnter.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.False;
            this.radIsEnter.Location = new System.Drawing.Point(199, 204);
            this.radIsEnter.Name = "radIsEnter";
            this.radIsEnter.Properties.Columns = 2;
            this.radIsEnter.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "进口"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "出口")});
            this.radIsEnter.Size = new System.Drawing.Size(164, 44);
            this.radIsEnter.TabIndex = 357;
            // 
            // AddDevice
            // 
            this.ActiveGlowColor = System.Drawing.Color.White;
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 314);
            this.Controls.Add(this.radIsEnter);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDeviceIp);
            this.Controls.Add(this.txt_deviceId);
            this.Controls.Add(this.labName1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lab);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_description);
            this.Name = "AddDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddDevice";
            ((System.ComponentModel.ISupportInitialize)(this.txt_deviceId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeviceIp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_description.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radIsEnter.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lab;
        private DevExpress.XtraEditors.LabelControl labName1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_deviceId;
        private DevExpress.XtraEditors.TextEdit txtDeviceIp;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txt_description;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider FormErrorProvider;
        private DevExpress.XtraEditors.RadioGroup radIsEnter;
    }
}