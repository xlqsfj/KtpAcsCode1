using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KtpAcs.WinForm.Jijian.Device;
using DevExpress.XtraTab;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerAdminForm : DevExpress.XtraEditors.XtraForm
    {
        int _state = 0;
        AddWorker workerform = null;
        public WorkerAdminForm(int i = 0)
        {
            InitializeComponent();
            _state = i;
            workerform = new AddWorker(i);
            workerform.FormBorderStyle = FormBorderStyle.None;
            workerform.TopLevel = false;
            this.xTabPage.Controls.Clear();
            this.xTabPage.Controls.Add(workerform);
            workerform.Show();
        }
        public void isExiet()
        {

            if (workerform != null)
                workerform.GetIsAVide();
        }
        public int BtnCurrentEdit() {

            if (workerform != null)
                workerform.CurrentManualEdit();
            else
                return 1;
            return 0;

                
        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //列表显示
            if (e.Page.Name == "tabPageWorkerList")
            {
                WorkerListForm listform = new WorkerListForm(_state);
                listform.ShowDetail += new Action<DevExpress.XtraEditors.XtraForm>(tabCreate);
                listform.FormBorderStyle = FormBorderStyle.None;
                listform.TopLevel = false;
                this.tabPageWorkerList.Controls.Clear();
                this.tabPageWorkerList.Controls.Add(listform);
                listform.Show();

            }
        


        }
        private void tabCreate(DevExpress.XtraEditors.XtraForm detailedWinform)
        {


            if (detailedWinform != null)
            {
                XtraTabPage page = new XtraTabPage();
                detailedWinform.FormBorderStyle = FormBorderStyle.None;
                detailedWinform.TopLevel = false;
                page.Controls.Add(detailedWinform);
                detailedWinform.Show();
                page.Text = "详情";
                xtraTabControl1.SelectedTabPage = page;
                xtraTabControl1.TabPages.Add(page);
            }
            else
            {
              
                //判断是否已创建过
                foreach (XtraTabPage page1 in xtraTabControl1.TabPages)
                {
                    if (page1.Text == "详情")
                    {

                        xtraTabControl1.SelectedTabPage = page1;//显示该页
                        xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[2]);
                        page1.Dispose();
                        break;
                    }
                }
                xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[1];//显示该页

            }
        }


    }
}