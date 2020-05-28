using KtpAcs.Infrastructure.Utilities;

using KtpAcs.PanelApi.Haiqing.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KtpAcs.KtpApiService.Model;

namespace KtpAcs.PanelApi.Haiqing.Api
{

    public class PanelBaseHq
    {

        /// <summary>
        /// 查询设备id
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static int GetDeviceId(string ip)
        {

            List<WorkAddInfo> workAddInfos = WorkSysFail.workAdd;
            var deviceId = workAddInfos.FirstOrDefault(a => a.deviceIp == ip).deviceNo;
            return FormatHelper.StringToInt(deviceId);
        }

        /// <summary>
        /// 查询人员列表
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<PanelHqUserInfo> GetPersonInfoList(string ip, int id = -1)
        {

      

            PanelSearchSend panelSearchSend = new PanelSearchSend()
            {

                _operator = "SearchPersonList",
                info = new SearchInfo()
                {
                    DeviceID = GetDeviceId(ip),
                    CustomizeID = id.ToString(),
                    Name = ""
                }
            };
            List<PanelHqUserInfo> panelUserInfos = new List<PanelHqUserInfo>();
            //返回设备的数量
            IMulePusherHq PanelLibraryGet = new PanelSearchPersonListApi() { PanelIp = ip, RequestParam = panelSearchSend };

            PushSummaryHq pushSummary = PanelLibraryGet.Push();
            if (!pushSummary.Success)
            {
                //if (ipList.Keys.Contains(ip))
                //{
                //    ipList.Remove(ip);

                //}
                return panelUserInfos;
            }
            HqResult panelListResult = null;
            if (pushSummary.ResponseData != null)
            {
                panelListResult = pushSummary.ResponseData;

                panelUserInfos = panelListResult.info.List;


            }
            return panelUserInfos;

        }


        /// <summary>
        /// 单个查询人员
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Info GetPersonInfo(string ip, int id = -1)
        {

            //if (id > 0)
            //{
            //    var currentId = ipList[ip].Where(a => a.id == id.ToString()).ToList();
            //    return currentId;

            //}

            PanelSearchSend panelSearchSend = new PanelSearchSend()
            {

                _operator = "SearchPerson",
                info = new SearchInfo()
                {
                    DeviceID = GetDeviceId(ip),
                    SearchID = id.ToString(),
                    Picture = 0,
                    SearchType = 0
                }
            };
            Info panelUserInfos = new Info();
            //返回设备的数量
            IMulePusherHq PanelLibraryGet = new PanelSearchPersonApi() { PanelIp = ip, RequestParam = panelSearchSend };

            PushSummaryHq pushSummary = PanelLibraryGet.Push();
            if (!pushSummary.Success)
            {

                return panelUserInfos;
            }
            HqResult panelListResult = null;
            if (pushSummary.ResponseData != null)
            {
                panelListResult = pushSummary.ResponseData;
                panelUserInfos = panelListResult.info;

            }
            return panelUserInfos;

        }

        /// <summary>
        /// 查询人数
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int GetPanelUserCount(string ip)
        {

            PanelSearchSend panelSearchSend = new PanelSearchSend()
            {

                _operator = "SearchPersonNum",
                info = new SearchInfo()
                {
                    DeviceID = GetDeviceId(ip),
                    Name = ""
                }
            };
            //返回设备的数量
            IMulePusherHq PanelLibraryGet = new PanelSearchPersonNumApi() { PanelIp = ip, RequestParam = panelSearchSend };

            PushSummaryHq pushSummary = PanelLibraryGet.Push();
            if (!pushSummary.Success)
            {
                return -1;
            }
            HqResult hqResult = pushSummary.ResponseData;
            return hqResult.info.PersonNum;
        }

        /// <summary>
        /// 查询面板是否连接网络或服务器
        /// </summary>
        /// <returns></returns>
        public string GetIsNetworkServiceTest(string ip)
        {

            string stateName = "连接异常";
            PanelSearchSend panelSearchSend = new PanelSearchSend()
            {

                _operator = "CheckNet"
            };
            //返回设备的数量
            IMulePusherHq PanelLibraryGet = new PanelGetServiceParamApi() { PanelIp = ip, RequestParam = panelSearchSend };

            PushSummaryHq pushSummary = PanelLibraryGet.Push();
            //if (!pushSummary.Success)
            //{
            //    return "连接异常";
            //}
            HqResult hqResult = pushSummary.ResponseData;
            if (hqResult == null)
                return "未连接";
            switch (hqResult.code)
            {
                case 463:
                    stateName = "网络异常";
                    break;
                case 462:
                    stateName = "云端服务异常";
                    break;
                case 461:
                    stateName = "未知操作";
                    break;
                default:
                    stateName = "正常";
                    break;
            }

            return stateName;


        }

     

        /// <summary>
        /// 删除面板用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceIp"></param>
        public static void PanelDeleteUser(int id, string deviceIp)
        {
            //海清
            List<int> attrId = new List<int> { id };
            DeleteSend deleteSend = new DeleteSend()
            {
                _operator = "DeletePerson",
                info = new UserDelete()
                {
                    CustomizeID = attrId,
                    TotalNum = 0,
                    IdType = 0,
                    DeviceID = PanelBaseHq.GetDeviceId(deviceIp),


                }
            };
            IMulePusherHq panelDeleteApi = new PanelHqDeletePersonApi()

            { PanelIp = deviceIp, RequestParam = deleteSend };

            PushSummaryHq pushSummarySet = panelDeleteApi.Push();

        }

    }
}
