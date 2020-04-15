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
        public WorkerAdminForm()
        {
            InitializeComponent();

            AddWorker workerform = new AddWorker()
            {
                Visible = true,
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false//在这里一定要注意  不然加载不出来
            };
            xTabPage.Controls.Add(workerform);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "tabPageWorkerList")
            {
                WorkerListForm workerform = new WorkerListForm() {

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