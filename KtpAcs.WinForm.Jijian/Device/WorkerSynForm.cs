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
using KtpAcs.PanelApi.Yushi.Model;
using KtpAcs.PanelApi.Yushi.Api;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.PanelApi.Yushi;
using static KtpAcs.KtpApiService.Result.WorkerListResult;
using static KtpAcs.PanelApi.Yushi.Api.PanelWorkerSend;

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

                    MessageHelper.Show("同步失败,请选择人员，重新拍照");
                    ShowSubmit();

                }
                else
                {


                    MessageHelper.Show("同步成功");


                }


            }
            catch (Exception ex)
            {
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
                    _workthread.Resume();
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

        public void AddPaneIpInfo(dynamic ip)
        {

            lock (objLock)
            {
                if (workers == null)
                {
                    WorkerSend workerSend = new WorkerSend()
                    {
                        designatedFlag = null,
                        pageSize = 0,
                        projectUuid = ConfigHelper.KtpLoginProjectId,
                        pageNum = 0,
                        status = 2
                    };
                    //查询所有人员的详细接口
                    IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                    PushSummary push = pusherDevice.Push();
                    WorkerListResult.Data rr = push.ResponseData;

                    workers = rr.list;
                }
            }
            Liblist liblist = PanelBase.GetPanelDeviceInfo(ip);
            int Limit = 10;
            if (liblist != null)
                Limit = liblist.MemberNum == 0 ? 10 : liblist.MemberNum;
            List<Personlist> personlist = new List<Personlist>();
            if (Limit > 1000)
            {
                Limit = Limit / 2;
                //查询所设备的ip //Limit 结束人数查询 Offset=开始查询人数 
                IMulePusherYs PanelList = new PanelWorkerListApi() { RequestParam = new { Limit = Limit, Offset = 0 }, PanelIp = ip };

                PushSummarYs paneSummary = PanelList.Push();

                List<Personlist> personlist1 = paneSummary.ResponseData;

                IMulePusherYs PanelList2 = new PanelWorkerListApi() { RequestParam = new { Limit = Limit, Offset = Limit }, PanelIp = ip };

                PushSummarYs paneSummary2 = PanelList2.Push();

                List<Personlist> personlist2 = paneSummary2.ResponseData;
                personlist = personlist1.Union(personlist2).ToList<Personlist>();
            }
            else
            {

                IMulePusherYs PanelList = new PanelWorkerListApi() { RequestParam = new { Limit = Limit, Offset = 0 }, PanelIp = ip };

                PushSummarYs paneSummary = PanelList.Push();

                personlist = paneSummary.ResponseData;
            }

            //循环面板的人员，跟项目人员对比，不存在进行删除
            foreach (Personlist items in personlist)
            {
                var p = workers.Where(a => a.userId == items.PersonID).Count();
                if (p == 0)
                {

                    IMulePusherYs panelWorkerApi = new PanelWorkerDeleteApi() { API = "/PeopleLibraries/3/People/" + items.PersonID + $"?Lastchange={DateTime.Now.Ticks}", PanelIp = ip };

                    PushSummarYs pushDeleteSummary = panelWorkerApi.Push();
                    PanelDeleteResult pr = pushDeleteSummary.ResponseData;
                }


            }
            //循环项目的人员，跟面板人员对比，不存在进行添加，存在跳过
            int panelCount = 0;
            _numerOfThreadsNotYetCompleted = workers.Count();
            foreach (WorkerList items in workers)
            {

                panelCount++;
                var isExit = personlist.Where(a => a.PersonID == items.userId).Count();
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
                if (string.IsNullOrEmpty(items.name) || string.IsNullOrEmpty(items.idCard))
                {
                    AddSysFail(items, "错误信息:基本信息不全");
                    _numerOfThreadsNotYetCompleted = _numerOfThreadsNotYetCompleted - 1;
                    continue;

                }
                items.panelIp = ip;





                //ThreadPool.QueueUserWorkItem(new WaitCallback(AddPanePerson),

                //   (object)items);

                AddPanePerson(items);


            }
            _ip = _ip.Where(a => a != ip).ToList();
            if (_ip.Count < 1)
                _hasNew.Set();



        }
        public void AddPanePerson(WorkerList items)
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
                string fileName = items.facePic.Substring(items.facePic.LastIndexOf("/", StringComparison.Ordinal));
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



                PersonInfoListItem personInfoListItem = new PersonInfoListItem
                {
                    Gender = items.sex == "1" ? 1 : 2,
                    PersonName = items.name,
                    ImageNum = 1,
                    PersonID = items.userId,
                    IdentificationNum = 1,
                    IdentificationList = new List<IdentificationListItem> { new IdentificationListItem { Number = items.idCard, Type = 0 } },
                    ImageList = new List<ImageListItem> { new ImageListItem { Name = $"{items.userId}_{DateTime.Now}.jpg", Data = items.imgBase64, Size = items.imgBase64.Length, FaceID = items.userId } }

                };
                PanelWorkerSend PanelWorkerSend = new PanelWorkerSend();
                PanelWorkerSend.Num = 1;
                PanelWorkerSend.PersonInfoList = new List<PanelWorkerSend.PersonInfoListItem>() { personInfoListItem };

                try
                {
                    IMulePusherYs panelApi = new PanelWorkerApi() { RequestParam = PanelWorkerSend, PanelIp = items.panelIp.ToString() };

                    PushSummarYs push = panelApi.Push();

                    if (!push.Success)
                    {


                        AddSysFail(items, push.Message);


                    }
                }
                catch (Exception ex)
                {

                    AddSysFail(items, ex.Message);
                }

            }
            finally
            {

                //if (Interlocked.Decrement(ref _numerOfThreadsNotYetCompleted) == 0)
                //{
                //    _FinishListIp = _ip.Where(a => a != items.panelIp).ToList();
                //    if (_FinishListIp.Count <1)
                //        _hasNew.Set();

                //}
            }


        }
        /// <summary>
        /// 添加到同步失败列表
        /// </summary>
        /// <param name="items">详情</param>
        /// <param name="mag">失败原因</param>
        public void AddSysFail(WorkerList items, string mag)
        {
            try
            {
                object isExit = null;
                if (string.IsNullOrEmpty(items.idCard) || string.IsNullOrEmpty(items.name))
                {

                }
                else
                {
                    isExit = WorkSysFail.list.FirstOrDefault(a => a.idCard == items.idCard && a.name == items.name);
                }

                if (isExit == null)
                {
                    WorkerList wokersList = new WorkerList
                    {
                        idCard = items.idCard,
                        sex = FormatHelper.GetToString(items.sex),
                        userId = items.userId,
                        name = items.name,
                        phone = items.phone,
                        reason = mag
                    };
                    WorkSysFail.list.Add(wokersList);
                }

            }
            catch (Exception ex)
            {
                LogHelper.ExceptionLog(ex);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  new AddWorkerInfo(1).ShowDialog();
        }

        private void WorkerSyncPrompt_Load(object sender, EventArgs e)
        {

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

        private void WorkerSyncPrompt_Load_1(object sender, EventArgs e)
        {

        }
    }
    public class CONSTANTDEFINE
    {

        public delegate void SetUISomeInfo();
    }
}