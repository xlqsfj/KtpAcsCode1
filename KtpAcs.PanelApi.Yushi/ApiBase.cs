using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Yushi
{
    public abstract class ApiBase<Ts, Tr> : IMulePusherYs where Ts : class where Tr : new()
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

        private string panelUrl = "/LAPI/V1.0";
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
                s_rootUrl = $"http://{_panelIp}{panelUrl}";
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
        public PushSummarYs Push()
        {
            return this.InternalPush();
        }

        private PushSummarYs InternalPush()
        {

            Ts senddata = FetchDataToPush();
            if (senddata == null && (MethodType != Method.GET && MethodType != Method.POST && MethodType != Method.DELETE))
            {
                return PushSummarYs.NoDataResult;
            }

            RichRestRequest request = CreateRestRequest(senddata);
            List<Parameter> ts = request.Parameters;
            PushSummarYs pushSummary = null;

            DateTime beginTime = DateTime.Now;
            var response = Client.Execute(request);
            DateTime endTime = DateTime.Now;
            int interval = (endTime - beginTime).Seconds;
            if (interval <= 0)
            { }
            else if (interval > 20)
            {
                LogHelper.Info($"{response.ResponseUri}接口请求时间:{beginTime.ToString("yyyy-MM-dd HH:mm:ss")}");

                LogHelper.Info($"{request.Resource}响应状态:{(int)response.StatusCode}接口结束时间:{endTime.ToString("yyyy-MM-dd HH:mm:ss")}相差:{interval}秒,宇视面板慢");

            }
            else
            {
                LogHelper.Info($"{response.ResponseUri}接口请求时间:{beginTime.ToString("yyyy-MM-dd HH:mm:ss")}");

                LogHelper.Info($"{request.Resource}响应状态:{(int)response.StatusCode}接口结束时间:{endTime.ToString("yyyy-MM-dd HH:mm:ss")}相差:{interval}秒");
            }

            dynamic contentPost = response.Content;

            bool isDataNull = true;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //  throw new InvalidOperationException("调用接口失败, 错误信息：" + response.ErrorException.Message);
                LogHelper.Info(response.Content);

                return InvokeOnPushFailed(request, response);
            }
            if (isDataNull)
            {
                Tr receiveData = JsonConvert.DeserializeObject<Tr>(contentPost);
                pushSummary = OnPushSuccess(request, receiveData);

            }


            string summary = string.Empty;

            return pushSummary;


        }
        private PushSummarYs InvokeOnPushFailed(RichRestRequest request, IRestResponse response)
        {
            string errorSummary = "";
            if (response.StatusCode == HttpStatusCode.BadGateway)
            {

                errorSummary = "调用服务失败";
                return new PushSummarYs(false, errorSummary, this.ServiceName, request, "接口", (int)response.StatusCode);

            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                errorSummary = "找不到服务";
                return new PushSummarYs(false, errorSummary, this.ServiceName, request, "接口", (int)response.StatusCode);


            }
            return new PushSummarYs(false, errorSummary, this.ServiceName, request, "接口");


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

            if (!string.IsNullOrEmpty(this.Token))
            {//平台加 登录token验证
                request.AddHeader("token", this.Token);

            }
            request.UserState = postdata;
            request.AddJsonBody(postdata);
            return request;
        }

        public PushSummarYs PushForm()
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
        protected abstract PushSummarYs OnPushSuccess(RichRestRequest request, Tr receiveData);

    }
}
