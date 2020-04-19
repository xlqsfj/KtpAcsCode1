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
        public WorkerAdminForm(int i=0)
        {
            InitializeComponent();
            _state = i;
        
            workerform = new AddWorker(i)
            {
                Visible = true,
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false//在这里一定要注意  不然加载不出来
            };
            xTabPage.Controls.Add(workerform);
            
        }
        public void isExiet() {

            if (workerform != null)
                workerform.GetIsAVide();
        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "tabPageWorkerList")
            {
                WorkerListForm workerform = new WorkerListForm(_state) {

                    Visible = true,
                    Dock = DockStyle.Fill,
                    FormBorderStyle = FormBorderStyle.None,
                    TopLevel = false//在这里一定要注意  不然加载不出来
                };
                this.tabPageWorkerList.Controls.Add(workerform);
             

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