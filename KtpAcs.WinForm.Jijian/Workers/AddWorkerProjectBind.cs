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
            this.txtPhone.Text = phone;
            this.txtName.Text = name;
            InitializeComponent();
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
        /// 设置控件的隐藏或只读
        /// </summary>
        /// <param name="state">0、工人1、分包2、项目人员</param>
        public void ContentState(int state)
        {

            this.txtAddress.ReadOnly = true;
            this.txtAvg.ReadOnly = true;
            this.txtBankName.ReadOnly = true;
            this.txtBankNo.ReadOnly = true;
            this.txtBirthday.ReadOnly = true;
            this.txtEmergencyContactName.ReadOnly = true;
            this.txtEmergencyContactPhone.ReadOnly = true;
            this.txtGender.ReadOnly = true;
            this.txtIdCard.ReadOnly = true;
            this.txtName.ReadOnly = true;
            this.ComNation.ReadOnly = true;
            this.txtNativePlace.ReadOnly = true;
           

            if (state == 1)
            {
                panelProjectInfo.Visible = false;
                panelBankInfo.Visible = false;
            }
            else if (state == 2)
            {
                panelProjectInfo.Visible = false;
                panelBankInfo.Visible = false;
            }
            else
            {
                panelProjectInfo.Visible = true;
                this.txtPhone.ReadOnly = true;
            }

        }
        /// <summary>
        /// 添加项目人员
        /// </summary>
        /// <param name="add"></param>
        private void addProject(AddWorerkSend add)
        {

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

                MessageHelper.Show("添加成功");
            }
            else
            {
                MessageHelper.Show("添加失败:" + pushAddworkers.Message);
            }
        }

    }
}

