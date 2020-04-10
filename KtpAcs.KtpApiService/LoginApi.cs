using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService
{
    public class LoginApi : ApiBase<dynamic, LoginResult>
    {

        public LoginApi()
        : base()
        {

            base.API = "/loginPanel/login";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
        }
        protected override dynamic FetchDataToPush()
        {

            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, LoginResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "登录接口");
            if (mag.Success)
            {

                ConfigHelper._KtpLoginToken = receiveData.data.token;
                ConfigHelper._KtpLoginPhone = receiveData.data.phone;


            }
            mag.ResponseData = receiveData;
            return mag;
        }
    }
}
