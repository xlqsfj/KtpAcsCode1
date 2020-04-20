using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Model;
using KtpAcs.KtpApiService.Send;
using KtpAcs.PanelApi.Yushi.Api;
using KtpAcs.PanelApi.Yushi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KtpAcs.PanelApi.Yushi.Api.PanelWorkerSend;

namespace KtpAcs.PanelApi.Yushi
{
    public class AddFaceToPanel
    {
        string panelMag = "";
        public void AddFaceInfo(AddWorerkSend workers, int? uid)
        {

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

                    try
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
                        Thread thread = new Thread(new ParameterizedThreadStart(AddPanelWorkerAPi));
                        thread.Start(result);

                        LogHelper.Info("已添加ip_" + usable + "" + result.ip);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);


                    }
                    finally
                    {

                    }

                }
            }
        }
        /// <summary>
        /// 添加到宇视的面板
        /// </summary>
        /// <param name="receiveData"></param>
        public void AddPanelWorkerAPi(dynamic receiveData)
        {

            LogHelper.Info("已添加ip_" + receiveData.ip);


            PersonInfoListItem personInfoListItem = new PersonInfoListItem
            {
                Gender = receiveData.usex,
                PersonName = receiveData.uname,
                ImageNum = 1,
                PersonID = receiveData.userId,
                IdentificationNum = 1,
                IdentificationList = new List<IdentificationListItem> { new IdentificationListItem { Number = receiveData.usfz, Type = 0 } },
                ImageList = new List<ImageListItem> { new ImageListItem { Name = $"{receiveData.userId}_{DateTime.Now}.jpg", Data = receiveData.imgBase64, Size = receiveData.imgBase64.Length, FaceID = receiveData.userId } }

            };
            PanelWorkerSend PanelWorkerSend = new PanelWorkerSend();
            PanelWorkerSend.Num = 1;
            PanelWorkerSend.PersonInfoList = new List<PanelWorkerSend.PersonInfoListItem>() { personInfoListItem };

            IMulePusherYs PanelLibrarySet = new PanelWorkerApi() { RequestParam = PanelWorkerSend, MethodType = Method.POST, PanelIp = receiveData.ip };

            PushSummarYs pushSummary = PanelLibrarySet.Push();
            if (!pushSummary.Success)
            {
                panelMag = pushSummary.Message;
                WorkSysFail.dicAddMag.Add(receiveData.ip, panelMag);
            }
            else
            {
                WorkSysFail.dicAddMag.Add(receiveData.ip, "添加成功");
            }

            PanelResult pr = pushSummary.ResponseData;

        }

    }
}
