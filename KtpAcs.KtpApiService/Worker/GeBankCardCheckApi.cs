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

    public class GeBankCardCheckApi : ApiBase<dynamic, BankCardCheckResult>
    {

        public GeBankCardCheckApi()
        : base()
        {

            base.API = "/userPanel/threeVerification";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override dynamic FetchDataToPush()
        {

            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, BankCardCheckResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "银行卡验证接口");
            if (mag.Success)
            {

                mag.ResponseData = receiveData.data;


            }

            return mag;
        }

    }
}
