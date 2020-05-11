using KtpAcs.Infrastructure.Exceptions;
using KtpAcs.Infrastructure.Serialization;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.PanelApi.Yushi;
using KtpAcs.PanelApi.Yushi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Yushi.Api
{
    public static class PanelBase
    {
        /// <summary>
        /// 查询设备库信息
        /// </summary>
        /// <param name="ip">设备ip号</param>
        /// <returns>设备库的信息</returns>
        public static Liblist GetPanelDeviceInfo(string ip)
        {

            Liblist liblist = null;
            try
            {

                //返回设备的数量
                IMulePusherYs PanelLibraryGet = new PanelLibraryApi() { PanelIp = ip };

                PushSummarYs pushSummary = PanelLibraryGet.Push();
                PanelResult pr = pushSummary.ResponseData;
                if (pr.Response.Data == null) {

                    throw new Exception($"{ip}:面板出错,请联系管理员，或重启{ip}面板");
                
                }
                if (pr.Response.Data.Num > 0)
                {
                    liblist = pr.Response.Data.LibList.Where(a => a.ID == 3).FirstOrDefault();
                }
                if (liblist == null)
                {
                    //创建面板创建库
                    List<PanelLibrarySend.LibListItem> libListItems = new List<PanelLibrarySend.LibListItem> {
                    new PanelLibrarySend.LibListItem {  Type=(int)UserType.staff, Name=UserType.staff.GetDescription(), LastChange=DateTime.Now.Ticks ,ID=(int)UserType.staff} };
                    PanelLibrarySend geteLibraryRequest = new PanelLibrarySend
                    {
                        Num = "1",
                        LibList = libListItems

                    };
                    IMulePusherYs PanelLibrarySet = new PanelLibraryApi() { RequestParam = geteLibraryRequest, MethodType = Method.POST, PanelIp = ip };

                    PushSummarYs pushSummarySet = PanelLibrarySet.Push();
                    if (!pushSummarySet.Success)
                    {
                        return null;


                    }
                    PanelResult rr = pushSummarySet.ResponseData;
                }
            }
            catch (PreValidationException ex)
            {
                throw new PreValidationException("调用人像识别设备失败" + ex.Message);
            }
            catch (Exception ex)
            {
                LogHelper.ExceptionLog(ex);

            }
            return liblist;
        }



    }
}
