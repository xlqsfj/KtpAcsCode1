using KS.Resting;

using KtpAcs.PanelApi.Haiqing.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Haiqing.Api
{
  public   class PanelSearchPersonListApi : ApiBase<PanelSearchSend, HqResult>
    {
        public PanelSearchPersonListApi()
         : base()
        {


            base.API = "/action/SearchPersonList";
            base.MethodType = Method.POST;
            base.ServiceName = ApiType.PanelHq;

        }

        /// <summary>
        /// 请求的方法
        /// </summary>
        /// <returns>传输的参数</returns>
        protected override PanelSearchSend FetchDataToPush()
        {

            return base.RequestParam;
        }

        /// <summary>
        /// 返回信息方法
        /// </summary>
        /// <param name="request">请求的参数</param>
        /// <param name="receiveData">返回的参数</param>
        /// <returns></returns>
        protected override PushSummaryHq OnPushSuccess(RichRestRequest request, HqResult receiveData)
        {
            string isSuccess = receiveData.info.Result;
            PushSummaryHq mag = new PushSummaryHq(isSuccess == "Fail" ? false : true, receiveData.info.Detail, ApiType.Panel, request, "闸门面板人员查询列表接口");
            mag.ResponseData = receiveData;
            return mag;
        }
    }
}
