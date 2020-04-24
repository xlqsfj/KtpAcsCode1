using AForge.Imaging.Filters;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using System;
using System.Linq;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerListForm : DevExpress.XtraEditors.XtraForm
    {
        private int _isHmc = 0;
        public WorkerListForm(int isHmc = 0)
        {
            _isHmc = isHmc;
            InitializeComponent();
            GetWorkerList("");
        }

        public void GetWorkerList(string Query = "")
        {

            try
            {
                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = 10,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    pageNum = 1,
                    status = 2,
                    keyWord = Query,
                    designatedFlag = _isHmc == 0 ? false : true

                };

                IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {


                    WorkerListResult.Data data = push.ResponseData;
                    this.gridControl1.DataSource = data.list;
                }
                //RepositoryItemHyperLinkEdit linkSalesMoney = CreateRepositoryItemHyperLinkEdit("销售金额");
                //linkSalesMoney.OpenLink += new OpenLinkEventHandler(repositoryItemButtonEdit3_Click);  //事件
                //this.SalesMoney.ColumnEdit = linkSalesMoney;  //绑定
              //  this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1ButtonClick);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }
        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            dynamic row = this.gridView1.GetFocusedRow();
            string uuid = row.uuid;
            AddWorker addWorker = new AddWorker(uuid, _isHmc, false);
            addWorker.StartPosition = FormStartPosition.CenterParent;
            addWorker.Show();
        }
        protected virtual RepositoryItemHyperLinkEdit CreateRepositoryItemHyperLinkEdit(string caption)
        {
            RepositoryItemHyperLinkEdit link = new RepositoryItemHyperLinkEdit();
            link.AutoHeight = false;
            link.TextEditStyle = TextEditStyles.Standard;
            link.ReadOnly = true;
            link.SingleClick = true;
            return link;
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetWorkerList(txtQuery.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtQuery.Text = "";
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.StartPosition = FormStartPosition.CenterParent;
            addWorker.Show();

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            dynamic row = this.gridView1.GetFocusedRow();
            string uuid = row.uuid;
            AddWorker addWorker = new AddWorker(uuid,_isHmc,false);
            addWorker.StartPosition = FormStartPosition.CenterParent;
            addWorker.Show();
        }
        private void  repositoryItemButtonEdit1ButtonClick() {

            dynamic row = this.gridView1.GetFocusedRow();
            string uuid = row.uuid;
            AddWorker addWorker = new AddWorker(uuid, _isHmc, false);
            addWorker.StartPosition = FormStartPosition.CenterParent;
            addWorker.Show();
        }
    }
}