using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Device
{
    public class GetDeviceApi : ApiBase<dynamic, DeviceListResult>
    {
        public GetDeviceApi()
           : base()
        {

            base.API = "/projectInfoPanel/projectDeviceList";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }


        protected override dynamic FetchDataToPush()
        {
            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, DeviceListResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "查询设备列表接口");

            if (receiveData.data != null)
            {

                mag.ResponseData = receiveData.data;


            }
            return mag;
        }
    }
}
