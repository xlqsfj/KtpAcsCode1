namespace KtpAcs.WinForm.Jijian
{
    partial class PageCon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageCon));
            this.GridPagingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.navPageCount = new System.Windows.Forms.ToolStripLabel();
            this.navRefreshButton = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.navFirstPage = new System.Windows.Forms.ToolStripButton();
            this.navPrePage = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.navPageIndex = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.navNextPage = new System.Windows.Forms.ToolStripButton();
            this.navLastPage = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.navGoPageButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridPagingNavigator)).BeginInit();
            this.GridPagingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridPagingNavigator
            // 
            this.GridPagingNavigator.AddNewItem = null;
            this.GridPagingNavigator.CountItem = this.navPageCount;
            this.GridPagingNavigator.DeleteItem = null;
            this.GridPagingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GridPagingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navRefreshButton,
            this.toolStripSeparator1,
            this.navFirstPage,
            this.navPrePage,
            this.bindingNavigatorSeparator,
            this.navPageIndex,
            this.navPageCount,
            this.bindingNavigatorSeparator1,
            this.navNextPage,
            this.navLastPage,
            this.bindingNavigatorSeparator2,
            this.navGoPageButton});
            this.GridPagingNavigator.Location = new System.Drawing.Point(0, -2);
            this.GridPagingNavigator.MoveFirstItem = this.navFirstPage;
            this.GridPagingNavigator.MoveLastItem = this.navLastPage;
            this.GridPagingNavigator.MoveNextItem = this.navNextPage;
            this.GridPagingNavigator.MovePreviousItem = this.navPrePage;
            this.GridPagingNavigator.Name = "GridPagingNavigator";
            this.GridPagingNavigator.PositionItem = this.navPageIndex;
            this.GridPagingNavigator.Size = new System.Drawing.Size(496, 25);
            this.GridPagingNavigator.TabIndex = 21;
            this.GridPagingNavigator.Text = "GridPagingNavigator";
            // 
            // navPageCount
            // 
            this.navPageCount.Name = "navPageCount";
            this.navPageCount.Size = new System.Drawing.Size(32, 22);
            this.navPageCount.Text = "/ {0}";
            this.navPageCount.ToolTipText = "总项数";
            // 
            // navRefreshButton
            // 
            this.navRefreshButton.Name = "navRefreshButton";
            this.navRefreshButton.Size = new System.Drawing.Size(32, 22);
            this.navRefreshButton.Text = "刷新";
            this.navRefreshButton.Click += new System.EventHandler(this.navRefreshButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // navFirstPage
            // 
            this.navFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("navFirstPage.Image")));
            this.navFirstPage.Name = "navFirstPage";
            this.navFirstPage.RightToLeftAutoMirrorImage = true;
            this.navFirstPage.Size = new System.Drawing.Size(23, 22);
            this.navFirstPage.Text = "移到第一条记录";
            this.navFirstPage.Click += new System.EventHandler(this.navFirstPage_Click);
            // 
            // navPrePage
            // 
            this.navPrePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navPrePage.Image = ((System.Drawing.Image)(resources.GetObject("navPrePage.Image")));
            this.navPrePage.Name = "navPrePage";
            this.navPrePage.RightToLeftAutoMirrorImage = true;
            this.navPrePage.Size = new System.Drawing.Size(23, 22);
            this.navPrePage.Text = "移到上一条记录";
            this.navPrePage.Click += new System.EventHandler(this.navPrePage_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // navPageIndex
            // 
            this.navPageIndex.AccessibleName = "位置";
            this.navPageIndex.AutoSize = false;
            this.navPageIndex.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.navPageIndex.Name = "navPageIndex";
            this.navPageIndex.Size = new System.Drawing.Size(50, 25);
            this.navPageIndex.Text = "0";
            this.navPageIndex.ToolTipText = "当前位置";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // navNextPage
            // 
            this.navNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navNextPage.Image = ((System.Drawing.Image)(resources.GetObject("navNextPage.Image")));
            this.navNextPage.Name = "navNextPage";
            this.navNextPage.RightToLeftAutoMirrorImage = true;
            this.navNextPage.Size = new System.Drawing.Size(23, 22);
            this.navNextPage.Text = "移到下一条记录";
            this.navNextPage.Click += new System.EventHandler(this.navNextPage_Click);
            // 
            // navLastPage
            // 
            this.navLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navLastPage.Image = ((System.Drawing.Image)(resources.GetObject("navLastPage.Image")));
            this.navLastPage.Name = "navLastPage";
            this.navLastPage.RightToLeftAutoMirrorImage = true;
            this.navLastPage.Size = new System.Drawing.Size(23, 22);
            this.navLastPage.Text = "移到最后一条记录";
            this.navLastPage.Click += new System.EventHandler(this.navLastPage_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // navGoPageButton
            // 
            this.navGoPageButton.Image = ((System.Drawing.Image)(resources.GetObject("navGoPageButton.Image")));
            this.navGoPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navGoPageButton.Name = "navGoPageButton";
            this.navGoPageButton.Size = new System.Drawing.Size(52, 22);
            this.navGoPageButton.Text = "跳转";
            this.navGoPageButton.Click += new System.EventHandler(this.navGoPageButton_Click);
            // 
            // PageCon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridPagingNavigator);
            this.Name = "PageCon";
            this.Size = new System.Drawing.Size(496, 23);
            ((System.ComponentModel.ISupportInitialize)(this.GridPagingNavigator)).EndInit();
            this.GridPagingNavigator.ResumeLayout(false);
            this.GridPagingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator GridPagingNavigator;
        private System.Windows.Forms.ToolStripLabel navPageCount;
        private System.Windows.Forms.ToolStripLabel navRefreshButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton navFirstPage;
        private System.Windows.Forms.ToolStripButton navPrePage;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox navPageIndex;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton navNextPage;
        private System.Windows.Forms.ToolStripButton navLastPage;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton navGoPageButton;
    }
}
