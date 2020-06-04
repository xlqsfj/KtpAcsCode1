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

namespace KtpAcs.KtpApiService.Worker
{

    //查询项目人员详细信息
    public class GetWorkerProjectApi : ApiBase<dynamic, WorkerResult>
    {
        public GetWorkerProjectApi()
     : base()
        {


            base.API = "/projectInfoPanel/getProjectManageDetails";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override dynamic FetchDataToPush()
        {
            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, WorkerResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "查询人员列表接口");
            mag.ResponseData = receiveData.data;
            return mag;
        }
    }
}
