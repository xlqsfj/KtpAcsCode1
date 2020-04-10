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
    public class SetDeviceApi : ApiBase<DeviceSend, BaseResult>
    {
        public SetDeviceApi()
      : base()
        {

            base.API = "/devicePanel/addDevice";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override DeviceSend FetchDataToPush()
        {
            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, BaseResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg);
            mag.ResponseData = receiveData;
            if (!mag.Success)
            {
                return mag;
            }



            return mag;
        }
    }
}
