using KS.Resting;
using KtpAcs.KtpApiService.Result;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Worker
{


    public class GetWorkerTypesApi : ApiBase<dynamic, WorkerTypeListResult>
    {

        public GetWorkerTypesApi()
        : base()
        {

            base.API = "/projectInfoPanel/queryWorkerType";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
        }
        protected override dynamic FetchDataToPush()
        {

            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, WorkerTypeListResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "工种接口");
            if (mag.Success)
            {

                mag.ResponseData = receiveData.data;


            }

            return mag;
        }
    }

}

