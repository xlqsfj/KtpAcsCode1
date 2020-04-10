using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Project
{
    public class GetProjectListApi : ApiBase<dynamic, ProjectListResult>
    {
        public GetProjectListApi()
       : base()
        {

            base.API = "/projectInfoPanel/projectList";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override dynamic FetchDataToPush()
        {
           return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, ProjectListResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "项目列表");
            if (mag.Success)
            {

                mag.ResponseData = receiveData.data.list;
            }
        
            return mag;


        }
    }
}
