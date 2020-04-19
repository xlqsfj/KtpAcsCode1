using KtpAcs.Infrastructure.Serialization;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Base;
using KtpAcs.KtpApiService.Send;
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

namespace KtpAcs.WinForm.Jijian
{


    public partial class AddWorker
    {

        private string _status;
        private string _organizationUserUuid;
        public AddWorker(string phone, string name, string organizationUserUuid, int status = 2)
        {
            phone = "";
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
        private int  addProject(AddWorerkSend add)
        {
            int userId = 0;
            BaseSend baseSend = new BaseSend
            {
                status = _status,
                projectUuid = ConfigHelper.KtpLoginProjectId,
                organizationUserUuid = _organizationUserUuid

            };

            IMulePusher addworkers = new SetWorkerProjectApi() { RequestParam = baseSend };
            PushSummary pushAddworkers = addworkers.Push();
            if (pushAddworkers.Success)
            {
                BaseResult.Data data = pushAddworkers.ResponseData;
                userId = data.userId;
            }
            return userId;
        }

    }
}

