﻿using KS.Resting;
using KtpAcs.PanelApi.Yushi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Yushi.Api
{

    [Description("闸门面板库管理接口")]
    public class PanelLibraryDeleteApi : ApiBase<PanelLibrarySend, PanelDeleteResult>
    {
        public PanelLibrarySend libraryRequest;

        public PanelLibraryDeleteApi()
          : base()
        {


            base.API = "/PeopleLibraries/3";
            MethodType = Method.DELETE;
            base.ServiceName = ApiType.Panel;

        }

        /// <summary>
        /// 请求的方法
        /// </summary>
        /// <returns>传输的参数</returns>
        protected override PanelLibrarySend FetchDataToPush()
        {

            return base.RequestParam;
        }

        /// <summary>
        /// 返回信息方法
        /// </summary>
        /// <param name="request">请求的参数</param>
        /// <param name="receiveData">返回的参数</param>
        /// <returns></returns>
        protected override PushSummarYs OnPushSuccess(RichRestRequest request, PanelDeleteResult receiveData)
        {
            PushSummarYs mag = new PushSummarYs(receiveData.Response.StatusCode == 0 ? true : false, receiveData.Response.StatusString, ApiType.Panel, request, "库删除接口");
            mag.ResponseData = receiveData;
            return mag;
        }
    }
}

