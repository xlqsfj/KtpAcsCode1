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


    public class GetOrganizationApi : ApiBase<dynamic, OrganizationListResult>
    {

        public GetOrganizationApi()
        : base()
        {

            base.API = "/projectInfoPanel/queryOrganization";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override dynamic FetchDataToPush()
        {

            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, OrganizationListResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "劳务公司列表接口");
            if (mag.Success)
            {

                mag.ResponseData = receiveData.data;


            }

            return mag;
        }
    }

}

