using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Model;
using KtpAcs.KtpApiService.Send;
using KtpAcs.PanelApi.Haiqing.Api;
using KtpAcs.PanelApi.Haiqing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Haiqing
{
   public  class AddWorkerToHqPanel

    {

        /// <summary>
        /// 添加到面板
        /// </summary>
        /// <param name="info"></param>
        public static void AddHqPanel(dynamic receiveData)
        {


            string beginDate = "";
            string endDate = "";
            int tempvalid = 0;
            ////进场截止时间
            //if (!string.IsNullOrEmpty(receiveData.planExitTime))
            //{
            //    tempvalid = 1;
            //    beginDate = FormatHelper.GetIsoDateTimeTString(DateTime.Now);
            //    endDate = FormatHelper.GetIsoDateTimeTString(Convert.ToDateTime(receiveData.planExitTime));
            //    //beginDate = "2019-12-23T14:25:03";
            //    // endDate = "2019-12-23T15:00:00";

            //}

            PanelPersonSend panelSearchSend = new PanelPersonSend()
            {

                _operator = "AddPerson",
                info = new PanelHqUserInfo()
                {
                    DeviceID = PanelBaseHq.GetDeviceId(receiveData.ip),
                    IdCard = receiveData.usfz,
                    CustomizeID = receiveData.userId,
                    Name = receiveData.uname,
                    Telnum = receiveData.urealname,
                    Gender = receiveData.usex == 1 ? 0 : 1,
                    //ValidBegin = beginDate,
                    //ValidEnd = endDate,
                    Tempvalid = tempvalid,
                    RFIDCard = "",
                    PersonUUID = ""
                },
                picinfo = receiveData.imgBase64

            };
            //返回设备的数量
            IMulePusherHq PanelLibraryGet = new PanelAddPersonApi() { PanelIp = receiveData.ip, RequestParam = panelSearchSend };

            PushSummaryHq pushSummary = PanelLibraryGet.Push();
            if (!pushSummary.Success)
            {
                string panelMag = pushSummary.Message;
                WorkSysFail.dicAddMag.Add(receiveData.ip, panelMag);
            }
            else
            {



                WorkSysFail.dicAddMag.Add(receiveData.ip, "添加成功");

            }
            HqResult hqResult = pushSummary.ResponseData;

        }
        string panelMag = "";
        public void AddFaceInfo(AddWorerkSend workers, int? uid)
        {

            int usable = 0;
            panelMag = null;
            var fileName = "";

            if (!string.IsNullOrEmpty(workers.localImgFileName))
            {
                fileName = $"{ConfigHelper.CustomFilesDir}{workers.localImgFileName}";
            }
            else
            {

                fileName = workers.facePic.Substring(workers.facePic.LastIndexOf("/", StringComparison.Ordinal));
                var picPhysicalFileName = FileIoHelper.GetImageFromUrl(workers.facePic, fileName);
                // 图片转64位
                fileName = $"{ConfigHelper.CustomFilesDir}{picPhysicalFileName}";
            }
            var avatar = FileIoHelper.GetFileBase64String(fileName);
            foreach (WorkAddInfo device in WorkSysFail.workAdd)
            {


                usable++;
                var result = new
                {
                    ip = device.deviceIp,
                    imgBase64 = avatar,
                    usex = workers.gender,
                    uname = workers.name,
                    urealname = workers.phone,
                    userId = uid,
                    usfz = workers.idCard
                };
                //宇视产品
                Thread thread = new Thread(new ParameterizedThreadStart(AddHqPanel));
                thread.Start(result);


            }

        }

    }
}
