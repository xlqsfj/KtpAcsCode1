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
using System.Threading;
using KtpAcs.KtpApiService.Model;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;

using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;

using static KtpAcs.KtpApiService.Result.WorkerListResult;

using static KtpAcs.KtpApiService.Result.WorkerProjectListResult;
using KtpAcs.KtpApiService.Base;
using KtpAcs.Infrastructure.Serialization;
using KtpAcs.PanelApi.Haiqing.Model;
using KtpAcs.PanelApi.Haiqing.Api;
using KtpAcs.PanelApi.Haiqing;

namespace KtpAcs.WinForm.Jijian.Device
{
    public partial class WorkerSynForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        ///     类型：1=推送，2=拉取，3=同步，4=拉取所有班组和工人并推送考勤
        /// </summary>
        private List<string> _ip;
        private readonly Thread _workthread;
        private static object objLock = new object();//对象锁的对象
                                                     //声明委托重新提交
        public delegate void AddExceptionShow();
        //声明事件
        public event AddExceptionShow ShowSubmit;
        private static ManualResetEvent _hasNew = new ManualResetEvent(false);
        private List<WorkerList> workers;
        private static int _numerOfThreadsNotYetCompleted = 100;
        public WorkerSynForm()
        {
            InitializeComponent();
        }



        /// <summary>
        ///     入口：同步全部工人，走同步逻辑
        /// </summary>
        /// <param name="type">类型：1=推送，2=拉取，3=同步，4=拉取所有班组和工人并推送考勤</param>
        public WorkerSynForm(List<string> list)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _ip = list;

            _workthread = null;
            _workthread = new Thread(SyncWorkers) { IsBackground = true };
            _workthread.Start();
        }



        /// <summary>
        ///     同步(拉取/推送/双向同步)全部工人
        /// </summary>
        private void SyncWorkers()
        {
            try
            {

                workerSyn();
                closeOrder();
                if (WorkSysFail.list.Count() > 0)
                {

                    MessageHelper.Show("同步失败,请点击ok选择人员，重新拍照");
                    ShowSubmit();

                }
                else
                {


                    MessageHelper.Show("同步成功");
                    ShowSubmit();


                }


            }
            catch (Exception ex)
            {
                closeOrder();
                LogHelper.ExceptionLog(ex);
                MessageHelper.Show(ex.Message);
            }
            finally
            {
                ControlBox = true;

            }
        }

        /// <summary>
        ///     确认按钮事件
        /// </summary>
        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     窗口关闭事件
        /// </summary>
        private void WorkerSyncPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_workthread != null && _workthread.IsAlive)
                {

                    _workthread.Abort();
                }
            }
            catch
            {
                // ignored
            }
        }
        /// <summary>
        /// 同步
        /// </summary>
        /// <returns></returns>
        private string workerSyn()
        {
            //请求平台数据
            if (workers == null)
            {
                workers = new List<WorkerList>();
                workers = GetWorkerLists();

            }


            foreach (var ip in _ip)
            {


                if (string.IsNullOrEmpty(ip))
                {
                    continue;
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(AddPaneIpInfo),

                   (object)ip);

            }
            _hasNew.WaitOne();
            // 接收到信号后，重置“信号器”，信号关闭。
            _hasNew.Reset();

            return "";
        }

        public List<WorkerList> AddWokerLists(List<WorkerList> list, EnumWorkerType type)
        {


            foreach (WorkerList workerList in list)
            {
                workerList.enumWorkerType = type;
                workers.Add(workerList);
            }
            return workers;
        }
        public void AddPaneIpInfo(dynamic ip)
        {
            List<PanelHqUserInfo> personlist = GetPanelData(ip);
            //循环面板的人员，跟项目人员对比，不存在进行删除
            DeletePanelWorek(ip, personlist);
            //循环项目的人员，跟面板人员对比，不存在进行添加，存在跳过
            AddWorerToPanel(ip, personlist);
            _ip = _ip.Where(a => a != ip).ToList();
            if (_ip.Count < 1)
                _hasNew.Set();



        }

        private static List<PanelHqUserInfo> GetPanelData(dynamic ip)
        {
            return PanelBaseHq.GetPersonInfoList(ip);
        }

        private void AddWorerToPanel(dynamic ip, List<PanelHqUserInfo> personlist)
        {
            int panelCount = 0;
            _numerOfThreadsNotYetCompleted = workers.Count();
            foreach (WorkerList items in workers)
            {

                panelCount++;
                var isExit = personlist.Where(a => a.CustomizeID == items.userId).Count();
                if (isExit > 0)
                {
                    _numerOfThreadsNotYetCompleted = _numerOfThreadsNotYetCompleted - 1;
                    continue;

                }
                if (WorkSysFail.list.Where(a => a.userId == items.userId).Count() > 0)
                {
                    _numerOfThreadsNotYetCompleted = _numerOfThreadsNotYetCompleted - 1;
                    continue;
                }

                if (string.IsNullOrEmpty(items.facePic))
                {
                    AddSysFail(items, "错误信息:不存在人脸识别的图片");
                    _numerOfThreadsNotYetCompleted = _numerOfThreadsNotYetCompleted - 1;
                    continue;
                }
                //string.IsNullOrEmpty(items.idCard)
                if (string.IsNullOrEmpty(items.name))
                {
                    AddSysFail(items, "错误信息:基本信息不全");
                    _numerOfThreadsNotYetCompleted = _numerOfThreadsNotYetCompleted - 1;
                    continue;

                }
                items.panelIp = ip;



                AddPanePerson(items);


            }
        }

        private void DeletePanelWorek(dynamic ip, List<PanelHqUserInfo> personlist)
        {

            foreach (PanelHqUserInfo items in personlist)
            {
                var p = workers.Where(a => a.userId == items.CustomizeID).Count();
                if (p == 0)
                {

                    //海清
                    PanelBaseHq.PanelDeleteUser(items.CustomizeID, ip);
                }


            }
        }

        private List<WorkerList> GetWorkerLists()
        {
            int sizeCount = 100;
            int currentPage = 1;
            EnumWorkerType enumWorkerType = EnumWorkerType.Hmc;
            WorkerSend workerSend = new WorkerSend()
            {
                designatedFlag = false, //花名册
                pageSize = sizeCount,
                projectUuid = ConfigHelper.KtpLoginProjectId,
                pageNum = currentPage,
                status = 2
            };
            //查询所有人员的详细接口
            IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
            PushSummary push = pusherDevice.Push();
            if (!push.Success)
                throw new Exception(push.Message);
            WorkerListResult.Data rr = push.ResponseData;
            if (rr.list.Count > 0)
            {

                workers = AddWokerLists(rr.list, EnumWorkerType.Hmc);
                if (rr.total > sizeCount)
                {
                    GetPageWorkerData(sizeCount, enumWorkerType, workerSend, ref pusherDevice, ref push, ref rr);

                }
            }

            //甲子分包
            workerSend.designatedFlag = true;
            pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
            push = pusherDevice.Push();
            if (!push.Success)
                throw new Exception(push.Message);
            rr = push.ResponseData;
            if (rr.list.Count > 0)
            {
                workers = AddWokerLists(rr.list, EnumWorkerType.Jzfb);
                if (rr.total > sizeCount)
                {
                    enumWorkerType = EnumWorkerType.Jzfb;
                    GetPageWorkerData(sizeCount, enumWorkerType, workerSend, ref pusherDevice, ref push, ref rr);

                }
            }
            WorkerSend projectSend = new WorkerSend()
            {

                pageSize = 0,
                projectUuid = ConfigHelper.KtpLoginProjectId,
                pageNum = 0,
                status = 2
            };
            //项目部人员
            pusherDevice = new GetWorkersProjectApi() { RequestParam = projectSend };
            push = pusherDevice.Push();
            if (!push.Success)
                throw new Exception(push.Message);
            if (push.Success)
            {
                WorkerProjectListResult.Data data1 = push.ResponseData;
                List<WorkerProjectList> projectList = data1.list;
                foreach (var plist in projectList)
                {
                    WorkerList worker = new WorkerList();
                    worker.phone = plist.phone;
                    worker.facePic = plist.facePic;
                    worker.name = plist.name;
                    worker.enumWorkerType = EnumWorkerType.Hmry;
                    worker.userId = plist.newOrganizationUserId;
                    workers.Add(worker);
                }
            }

            return workers;
        }

        private void GetPageWorkerData(int sizeCount, EnumWorkerType enumWorkerType, WorkerSend workerSend, ref IMulePusher pusherDevice, ref PushSummary push, ref WorkerListResult.Data rr)
        {
            for (int i = 2; i <= (rr.total + sizeCount - 1) / sizeCount; i++)
            {
                workerSend.designatedFlag = enumWorkerType == EnumWorkerType.Hmc ? false : true;
                workerSend.pageNum = i;
                pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                push = pusherDevice.Push();
                rr = push.ResponseData;
                workers = AddWokerLists(rr.list, enumWorkerType);
            }
        }

        public void AddPanePerson(dynamic items)
        {

            try

            {
                string img64 = "";
                //保存图片


                if (string.IsNullOrEmpty(items.facePic))
                {
                    LogHelper.ExceptionLog("找不到人脸识别的图片详细错误");
                    AddSysFail(items, "找不到人脸识别的图片详细错误");

                    return;
                }
                string fileName = items.facePic.Substring(items.facePic.LastIndexOf("/", StringComparison.Ordinal) + 1);
                try
                {

                    var picPhysicalFileName = FileIoHelper.GetImageFromUrl(items.facePic, fileName);
                    // 图片转64位
                    var file = $"{ConfigHelper.CustomFilesDir}{picPhysicalFileName}";
                    img64 = FileIoHelper.GetFileBase64String(file);
                    items.imgBase64 = img64;
                }
                catch (Exception ex)
                {
                    AddSysFail(items, "找不到人脸识别的图片详细错误：" + ex.Message);
                    LogHelper.ExceptionLog(ex);
                    return;
                }

                AddHqPanel(items);


            }
            finally
            {

                //if (Interlocked.Decrement(ref _numerOfThreadsNotYetCompleted) == 0)
                //    _hasNew.Set();
            }


        }

        /// <summary>
        /// 添加到面板
        /// </summary>
        /// <param name="info"></param>
        public void AddHqPanel(dynamic receiveData)
        {


            string beginDate = "";
            string endDate = "";
            int tempvalid = 0;
            ////进场截止时间
            //if (!string.IsNullOrEmpty(receiveData.planExitTime))
            //{
            //    tempvalid = 1;
            //    beginDate = DateTime.Now.ToString();
            //    endDate = receiveData.planExitTime;

            //}

            PanelPersonSend panelSearchSend = new PanelPersonSend()
            {

                _operator = "AddPerson",
                info = new PanelHqUserInfo()
                {
                    DeviceID = PanelBaseHq.GetDeviceId(receiveData.panelIp),
                    IdCard = receiveData.idCard,
                    CustomizeID = receiveData.userId,
                    Name = receiveData.name,
                    Telnum = receiveData.phone,
                    Gender = receiveData.sex == "男" ? 0 : 1,
                    //ValidBegin = beginDate,
                    //ValidEnd = endDate,
                    Tempvalid = 0,
                    RFIDCard = "",
                    PersonUUID = ""
                },
                picinfo = receiveData.imgBase64

            };
            //返回设备的数量
            IMulePusherHq PanelLibraryGet = new PanelAddPersonApi() { PanelIp = receiveData.panelIp, RequestParam = panelSearchSend };

            PushSummaryHq pushSummary = PanelLibraryGet.Push();
            if (!pushSummary.Success)
            {
                string panelMag = pushSummary.Message;
                AddSysFail(receiveData, panelMag);
            }



        }

        /// <summary>
        /// 添加到同步失败列表
        /// </summary>
        /// <param name="items">详情</param>
        /// <param name="mag">失败原因</param>
        public void AddSysFail(WorkerList items, string mag)
        {
            LogHelper.Info("同步失败:" + items.name);
            try
            {
                object isExit = null;
                if (string.IsNullOrEmpty(items.name))
                {

                }
                else
                {
                    isExit = WorkSysFail.list.FirstOrDefault(a => a.idCard == items.idCard && a.name == items.name);
                }

                if (isExit == null)
                {
                    LogHelper.Info("同步失败:isExit");
                    WorkerList wokersList = new WorkerList
                    {
                        idCard = items.idCard,
                        sex = FormatHelper.GetToString(items.sex),
                        userUuid = items.userUuid,
                        name = items.name,
                        phone = items.phone,
                        reason = mag,
                        workerType = items.enumWorkerType.GetDescription(),
                        workerIntType = (int)items.enumWorkerType
                    };
                    WorkSysFail.list.Add(wokersList);
                    LogHelper.Info("同步失败:isExit结束");
                }

            }
            catch (Exception ex)
            {
                LogHelper.Info("同步失败ex:" + items.name);
                LogHelper.ExceptionLog(ex);


            }
        }



        /// <summary>
        /// 关闭命令
        /// </summary>
        public void closeOrder()
        {
            if (this.InvokeRequired)
            {
                //这里利用委托进行窗体的操作，避免跨线程调用时抛异常，后面给出具体定义
                CONSTANTDEFINE.SetUISomeInfo UIinfo = new CONSTANTDEFINE.SetUISomeInfo(new Action(() =>
                {
                    while (!this.IsHandleCreated)
                    {
                        ;
                    }
                    if (this.IsDisposed)
                        return;
                    if (!this.IsDisposed)
                    {
                        this.Dispose();
                    }

                }));
                this.Invoke(UIinfo);
            }
            else
            {
                if (this.IsDisposed)
                    return;
                if (!this.IsDisposed)
                {
                    this.Dispose();
                }
            }
        }


    }
    public class CONSTANTDEFINE
    {

        public delegate void SetUISomeInfo();
    }
}