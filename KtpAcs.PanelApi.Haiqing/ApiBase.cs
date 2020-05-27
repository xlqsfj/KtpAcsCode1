using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.PanelApi.Haiqing;

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Haiqing
{
    public abstract class ApiBase<Ts, Tr> : IMulePusherHq where Ts : class where Tr : new()
    {
        public ApiBase()
        {

            s_rootUrl = ConfigHelper.KtpApiAspBaseUrl;
        }
        public int MaxPackageSize => throw new NotImplementedException();
        /// <summary>
        /// 登录返回的Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        ///请求参数
        /// </summary>
        public dynamic RequestParam { get; set; }

        /// <summary>
        /// 如：condition
        /// </summary>
        public string API
        {
            get;
            set;
        }



        /// <summary>
        /// 对应header里的biz_event
        /// </summary>
        public string BizEvent
        {
            get;
            set;
        }

        /// <summary>
        /// 对应header里的service_name
        /// </summary>
        public ApiType ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 请求的类型
        /// </summary>
        public Method MethodType
        {
            get;
            set;
        }

        //private string panelUrl = "/LAPI/V1.0";
        private string _panelIp;
        /// <summary>
        /// 面板的ip号
        /// </summary>
        public string PanelIp
        {
            get { return _panelIp; }
            set
            {
                if (value != "")
                {
                    _panelIp = value;

                }
                s_rootUrl = $"http://{_panelIp}"; 
            }
        }


        public string s_rootUrl;
        /// <summary>
        /// 例如http://10.115.64.194:8089/sap-api/sap/
        /// </summary>
        public string RootUrl
        {
            get { return s_rootUrl; }
            set { s_rootUrl = value; }
        }
        private IRestClient _client;
        /// <summary>
        /// 
        /// </summary>
        public IRestClient Client
        {
            get
            {
                if (_client == null)
                    _client = CreateRestClient();
                return _client;
            }
        }
        protected virtual RichRestClient CreateRestClient()
        {
            RichRestClient client = new RichRestClient(RootUrl);
            return client;
        }
        public PushSummaryHq Push()
        {
            return this.InternalPush();
        }

        private PushSummaryHq InternalPush()
        {
            PushSummaryHq PushSummaryHq = null;

            Ts senddata = FetchDataToPush();
            if (senddata == null && (MethodType != Method.GET && MethodType != Method.POST && MethodType != Method.DELETE))
            {
                return PushSummaryHq.NoDataResult;
            }

            RichRestRequest request = CreateRestRequest(senddata);
            List<Parameter> ts = request.Parameters;
            var response = Client.Execute(request);

            dynamic contentPost = response.Content;

            bool isDataNull = true;
        
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //  throw new InvalidOperationException("调用接口失败, 错误信息：" + response.ErrorException.Message);

                return InvokeOnPushFailed(request, response);
            }
            if (isDataNull)
            {
                Tr receiveData = JsonConvert.DeserializeObject<Tr>(contentPost);
                PushSummaryHq = OnPushSuccess(request, receiveData);

            }


            string summary = string.Empty;

            return PushSummaryHq;


        }
        private PushSummaryHq InvokeOnPushFailed(RichRestRequest request, IRestResponse response)
        {
            string errorSummary = "";
            if (response.StatusCode == HttpStatusCode.BadGateway)
            {

                errorSummary = "调用服务失败";
                return new PushSummaryHq(false, errorSummary, this.ServiceName, request, "接口",(int) response.StatusCode);

            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                errorSummary = "找不到服务";
                return new PushSummaryHq(false, errorSummary, this.ServiceName, request, "接口", (int)response.StatusCode);


            }
            return new PushSummaryHq(false, errorSummary, this.ServiceName, request, "接口");


            //OnPushFailed(request, errorSummary);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errorSummary"></param>
        protected virtual void OnPushFailed(RichRestRequest request, string errorSummary)
        {

        }

        private RichRestRequest CreateRestRequest<T>(T postdata)
        {
            // Method.POST
            var request = new RichRestRequest(API, this.MethodType);
            request.JsonSerializer = new RestJsonSerializer();
            request.BizName = this.BizEvent;

            request.AddHeader("Authorization", $"Basic {FormatHelper.GetUserPwdToBase("admin", "admin")}");

            request.UserState = postdata;
            request.AddJsonBody(postdata);
            return request;
        }

        public PushSummaryHq PushForm()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取要推送的数据，如果没有需要推送的数据，请返回null
        /// </summary>
        /// <returns></returns>
        protected abstract Ts FetchDataToPush();

        /// <summary>
        /// 返回的参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="receiveData"></param>
        /// <returns></returns>
        protected abstract PushSummaryHq OnPushSuccess(RichRestRequest request, Tr receiveData);

    }
}
