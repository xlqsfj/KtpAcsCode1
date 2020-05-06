using AForge.Imaging.Filters;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.WinForm.Jijian.Device;
using System;
using System.Linq;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerListForm : DevExpress.XtraEditors.XtraForm
    {
        private int _isHmc = 0;

        //声明事件用于显示详细页面
        public event Action<DevExpress.XtraEditors.XtraForm> ShowDetail;
        public WorkerListForm(int isHmc = 0)
        {
            _isHmc = isHmc;
            InitializeComponent();
            GetWorkerList("");
            InitGridPagingNavigatorControl();
        }
        /// <summary>
        ///     分页控件翻页事件绑定
        /// </summary>
        private void InitGridPagingNavigatorControl()
        {
            WorkersGridPager.PagingHandler = GridPagingNavigatorControlPagingEvent;
        }

        /// <summary>
        ///     分页控件翻页事件
        /// </summary>
        public void GridPagingNavigatorControlPagingEvent()
        {
            //不是点击搜索的情况下

            GetWorkerList("");
        }
        public void GetWorkerList(string Query = "")
        {

            try
            {
                var pageSize = WorkersGridPager.PageSize;
                var pageIndex = WorkersGridPager.PageIndex;

                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = pageSize,
                    pageNum = pageIndex,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                  
                    status = 2,
                    keyWord = this.txtQuery.Text,
                    designatedFlag = _isHmc == 0 ? false : true

                };
            
                IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {

                 
                    WorkerListResult.Data data = push.ResponseData;
                    WorkersGridPager.PageCount = (data.total + pageSize - 1) / pageSize;
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
            WorkersGridPager.PageIndex = 1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtQuery.Text = "";
        }

      

        private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            dynamic row = this.gridView1.GetFocusedRow();
            string userUuid = row.userUuid;
            AddWorker addWorker = new AddWorker(userUuid, _isHmc, false);
            addWorker.CloseDdetailedWinform += new Action<DevExpress.XtraEditors.XtraForm>(ShowDetail);
            //addWorker.StartPosition = FormStartPosition.CenterParent;
            //addWorker.Show();
            ShowDetail(addWorker);


        }
    }
}