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
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //列表显示
            if (e.Page.Name == "tabPageWorkerList")
            {
                WorkerListForm listform = new WorkerListForm(_state);
                listform.FormBorderStyle = FormBorderStyle.None;
                listform.TopLevel = false;
                this.tabPageWorkerList.Controls.Clear();
                this.tabPageWorkerList.Controls.Add(listform);
                listform.Show();

            }
            //else
            //{
            //    AddWorker workerform = new AddWorker();
            //    workerform.TopLevel = false;
            //    this.tabPageWorkerList.Controls.Add(workerform);
            //    workerform.Show();

            //}


        }


    }
}