using KtpAcs.Infrastructure.Serialization;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Base;
using KtpAcs.KtpApiService.Send;

using KtpAcs.KtpApiService.Worker;
using KtpAcs.WinForm.Jijian.Base;
using KtpAcsMiddleware.KtpApiService.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static KtpAcs.KtpApiService.Result.OrganizationListResult;
using static KtpAcs.KtpApiService.Result.WorkerTypeListResult;
using KtpAcs.KtpApiService.Result;
using KtpAcs.WinForm.Jijian.Device;

namespace KtpAcs.WinForm.Jijian
{


    public partial class AddWorker
    {

        //声明事件判断是否关闭项目人员信息录入
        public event AgainSubmit ShowProjectList;
        private string _status;
        private string _organizationUserUuid;
        public AddWorker(string phone, string name, string organizationUserUuid, int status = 2)
        {

            _state = 2;
            _organizationUserUuid = organizationUserUuid;
            _status = status.ToString();
            InitializeComponent();
            this.txtPhone.Text = phone;
            this.txtName.Text = name;

            if (_state == 2)
                panelProjectInfo.Visible = false;
            else
                panelProjectInfo.Visible = true;
            CameraConn();
            BindNationsCb();
            BindEducationLeveCb();
            ContentState(2);
        }


        /// <summary>
        /// 添加项目人员
        /// </summary>
        /// <param name="add"></param>
        private int addProject(AddWorerkSend add)
        {
            int userId = 0;
            BaseSend baseSend = new BaseSend
            {
                status = _status,
                projectUuid = ConfigHelper.KtpLoginProjectId,
                organizationUserUuid = _organizationUserUuid,
                facePic = add.facePic,
                localImgFileName = add.localImgFileName

            };

            IMulePusher addworkers = new SetWorkerProjectApi() { RequestParam = add };
            PushSummary pushAddworkers = addworkers.Push();
            string i = "0";
            string k = "";
            if (pushAddworkers.Success)
            {
                BaseResult data = pushAddworkers.ResponseData;
                k = data.data.organizationUserId.ToString();
                userId = Convert.ToInt32(i + k);

            }
            return userId;
        }

    }
}

