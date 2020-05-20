using System;
using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using RestSharp;

namespace KtpAcs.KtpApiService.Device
{

    public class GetDeviceStateApi : ApiBase<dynamic, DeviceStateResult>
    {
        public GetDeviceStateApi()
           : base()
        {

            base.API = "/projectInfoPanel/getDeviceHeart";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            
        }


        protected override dynamic FetchDataToPush()
        {
            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, DeviceStateResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "查询设备状态接口");

            if (receiveData != null)
            {

                mag.ResponseData = receiveData;


            }
            return mag;
        }
    }

}