using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerProjectForm
    {
        public void GetWorkerList(string Query = "")
        {
            var pageSize = WorkersGridPager.PageSize;
            var pageIndex = WorkersGridPager.PageIndex;

            try
            {
                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = pageSize,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    pageNum = pageIndex,
                    status = (int)this.ComUsable.EditValue,
                    keyWord = txtQuery.Text
                };

                IMulePusher pusherDevice = new GetWorkersProjectApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {

                    WorkerProjectListResult.Data data1 = push.ResponseData;
                    WorkersGridPager.PageCount = (data1.total + pageSize - 1) / pageSize;
                    this.gridControl1.DataSource = data1.list;
                }

            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);

            }



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
      
    }
}
