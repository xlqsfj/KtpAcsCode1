using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.BaseApi
{
    ///结算方式
    public class GeSettlementApi : ApiBase<dynamic, SettlementMethodResult>
    {

        public GeSettlementApi()
        : base()
        {

            base.API = "/projectInfoPanel/getSettlementMethod";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.GET;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override dynamic FetchDataToPush()
        {

            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, SettlementMethodResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "班组列表接口");
            if (mag.Success)
            {

                mag.ResponseData = receiveData.data;


            }

            return mag;
        }
    }
}
