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

namespace KtpAcs.KtpApiService.BaseApi
{
    /// <summary>
    /// 获取日薪
    /// </summary>
    public class GetDailySalaryApi : ApiBase<BaseSend, BaseDataResult>
    {

        public GetDailySalaryApi()
        : base()
        {

            base.API = "/projectInfoPanel/getDailySalary";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }
        protected override BaseSend FetchDataToPush()
        {

            return base.RequestParam;
        }

        protected override PushSummary OnPushSuccess(RichRestRequest request, BaseDataResult receiveData)
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
