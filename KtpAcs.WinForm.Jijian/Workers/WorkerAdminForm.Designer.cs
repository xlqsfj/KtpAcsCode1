﻿namespace KtpAcs.WinForm.Jijian.Workers
{
    partial class WorkerAdminForm
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.tabPageWorkerList = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.xtraTabControl1.Appearance.Options.UseBackColor = true;
            this.xtraTabControl1.Location = new System.Drawing.Point(3, 12);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xTabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(1258, 818);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xTabPage,
            this.tabPageWorkerList});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xTabPage
            // 
            this.xTabPage.Appearance.PageClient.BackColor = System.Drawing.Color.White;
            this.xTabPage.Appearance.PageClient.Options.UseBackColor = true;
            this.xTabPage.Name = "xTabPage";
            this.xTabPage.Size = new System.Drawing.Size(1256, 796);
            this.xTabPage.Text = "人工采集";
            // 
            // tabPageWorkerList
            // 
            this.tabPageWorkerList.Appearance.Header.BackColor = System.Drawing.Color.White;
            this.tabPageWorkerList.Appearance.Header.BackColor2 = System.Drawing.Color.White;
            this.tabPageWorkerList.Appearance.Header.BorderColor = System.Drawing.Color.White;
            this.tabPageWorkerList.Appearance.Header.Options.UseBackColor = true;
            this.tabPageWorkerList.Appearance.Header.Options.UseBorderColor = true;
            this.tabPageWorkerList.Appearance.PageClient.BackColor = System.Drawing.Color.White;
            this.tabPageWorkerList.Appearance.PageClient.Options.UseBackColor = true;
            this.tabPageWorkerList.Name = "tabPageWorkerList";
            this.tabPageWorkerList.Size = new System.Drawing.Size(1256, 796);
            this.tabPageWorkerList.Text = "已入职";
            // 
            // WorkerAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 834);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "WorkerAdminForm";
            this.Text = "WorkerAdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xTabPage;
        private DevExpress.XtraTab.XtraTabPage tabPageWorkerList;
    }
}