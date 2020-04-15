using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using RestSharp;
using System;
using System.Linq;

namespace KtpAcs.KtpApiService.Worker
{
    public class GetWorkersProjectApi : ApiBase<WorkerSend, WorkerProjectListResult>
    {

        public GetWorkersProjectApi()
       : base()
        {


            base.API = "/projectInfoPanel/queryProjectManage";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override WorkerSend FetchDataToPush()
        {




            return base.RequestParam;



        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, WorkerProjectListResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "查询人员列表接口");
            mag.ResponseData = receiveData.data;
            return mag;
        }
    }
}
