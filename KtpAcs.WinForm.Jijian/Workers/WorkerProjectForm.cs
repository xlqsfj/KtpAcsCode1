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
using KtpAcs.KtpApiService;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerProjectForm : DevExpress.XtraEditors.XtraForm
    {
        public WorkerProjectForm()
        {
            InitializeComponent();
            GetWorkerList();
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
                    status = 0,
                    keyWord = Query
                };

                IMulePusher pusherDevice = new GetWorkersProjectApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {


                    WorkerProjectListResult.Data data1 = push.ResponseData;
                    this.gridControl1.DataSource = data1.list;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }
    }
}