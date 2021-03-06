﻿using KS.Resting;
using KtpAcs.Infrastructure.Serialization;
using KtpAcs.Infrastructure.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Yushi
{
    /// <summary>
    /// 表示一次推送的推送结果（并非数据接收方的返回结果）
    /// </summary>
    public class PushSummarYs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public PushSummarYs(bool success, string message)
        {
            this.Message = "";
            this.Success = success;
            this.Message = message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public PushSummarYs(bool success, string message, ApiType appType, RichRestRequest request, string apiName,int  state=200)
        {
            this.Message = "";
            this.Success = success;
            List<Parameter> ts = request.Parameters;
            this.RequestParam = $"传的json参数:" + request.Parameters[ts.Count - 2];

          if (appType == ApiType.Panel && success == false)
            {
                if (state == 502)
                    this.Message = "调用人脸识别设备接口失败502:请重试!";
                if (state == 404)
                    this.Message = "调用人脸识别设备接口失败404:请重试!";
                this.Message = "调用人脸识别设备接口失败。错误信息：" + message;
                LogHelper.Info(ApiType.Panel.ToEnumText() + apiName);
                LogHelper.EntryLog(this.RequestParam, "url:" + request.Resource);
                LogHelper.ExceptionLog(this.Message);
                // throw new Exception(this.Message);
            }
        }

        /// <summary>
        /// 无数据传输的推送结果
        /// </summary>
        public static PushSummarYs NoDataResult
        {
            get { return new PushSummarYs(true, "没有数据要传输。"); }
        }

        public dynamic ResponseData { get; set; }
        /// <summary>
        /// 推送是否成功
        /// </summary>
        public bool Success
        {
            get;
            private set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get;
            private set;
        }

        /// <summary>
        /// 请求的参数
        /// </summary>
        public string RequestParam
        {
            get;
            set;
        }



    }
}
