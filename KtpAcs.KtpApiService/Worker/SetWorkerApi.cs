using KS.Resting;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Model;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using RestSharp;
using System;
using System.Linq;

namespace KtpAcs.KtpApiService.Worker
{
    /// <summary>
    /// 人员新增接口
    /// </summary>
    public class WorkerSet : ApiBase<AddWorerkSend, BaseResult>
    {
        public WorkerSet()
          : base()
        {

            base.API = "/userPanel/addUser";
            base.ServiceName = ApiType.KTP;
            base.MethodType = Method.POST;
            base.Token = ConfigHelper.KtpLoginToken;
        }

        /// <summary>
        /// 请求的方法
        /// </summary>
        /// <returns>传输的参数</returns>
        protected override AddWorerkSend FetchDataToPush()
        {

            AddWorerkSend workers = base.RequestParam;

            if (!string.IsNullOrEmpty(workers.localImgFileName))

                workers.facePic = GetImgUrl(workers.localImgFileName);
            if (!string.IsNullOrEmpty(workers.localImgFileName1))
                workers.picturePositive = GetImgUrl(workers.localImgFileName1);
            if (!string.IsNullOrEmpty(workers.localImgFileName2))
                workers.pictureReverse = GetImgUrl(workers.localImgFileName2);
            if (!string.IsNullOrEmpty(workers.localImgUpic))
                workers.icon = GetImgUrl(workers.localImgUpic);

            return workers;
        }


        /// <summary>
        /// 返回信息方法
        /// </summary>
        /// <param name="request">请求的参数</param>
        /// <param name="receiveData">返回的参数</param>
        /// <returns></returns>
        protected override PushSummary OnPushSuccess(RichRestRequest request, BaseResult receiveData)
        {
            PushSummary mag = new PushSummary(receiveData.result == 1 ? true : false, receiveData.msg, ApiType.KTP, request, "人员新增接口");
            mag.ResponseData = receiveData;


            if (!mag.Success)
            {

                WorkSysFail.dicWorkadd.Add(false, mag.Message);
            }

            return mag;

        }
        public bool isE = false;

        public string GetImgUrl(string fName)
        {

            var fileName = $"{ConfigHelper.CustomFilesDir}{fName}";
            var qinieKey = QiniuHelper.UploadFile(fileName);
            var qinieUrl = $"{QiniuHelper.QiniuBaseUrl}{qinieKey}";
            return qinieUrl;
        }
    }
}
