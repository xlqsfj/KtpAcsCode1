using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Worker
{
    public class GetWorkerIsExistApi : ApiBase<dynamic, BaseResult>
    {

        public GetWorkerIsExistApi()
    : base()
        {


            base.API = "/userPanel/userFlag";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override dynamic FetchDataToPush()
        {
            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, BaseResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "判断是否存在人员接口");
            mag.ResponseData = receiveData;
            return mag;
        }
    }
}
