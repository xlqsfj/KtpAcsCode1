using DevExpress.XtraEditors;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using System;
using System.Linq;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerListForm : DevExpress.XtraEditors.XtraForm
    {
        public WorkerListForm()
        {
            InitializeComponent();
            GetWorkerList();
        }

        public void GetWorkerList(string  Query="")
        {

            try
            {
                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = 10,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    pageNum = 1,
                    status = 2,
                    keyWord=Query
                    
                };

                IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {


                    WorkerListResult.Data data = push.ResponseData;
                    this.gridControl1.DataSource = data.list;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetWorkerList(txtQuery.Text);
        }
    }
}