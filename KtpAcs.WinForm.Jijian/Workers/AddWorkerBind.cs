using KtpAcs.Infrastructure.Serialization;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Base;
using KtpAcs.KtpApiService.Model;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.PanelApi.Yushi;
using KtpAcs.WinForm.Jijian.Base;
using KtpAcs.WinForm.Jijian.Workers;
using KtpAcsMiddleware.KtpApiService.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static KtpAcs.KtpApiService.Result.OrganizationListResult;
using static KtpAcs.KtpApiService.Result.WorkerTypeListResult;
using static KtpAcs.WinForm.Jijian.Workers.WorkerAddStateForm;

namespace KtpAcs.WinForm.Jijian
{
    public partial class AddWorker
    {
        #region 下拉列表数据

        /// <summary>
        /// 民族列表
        /// </summary>
        /// <param name="selectedValue"></param>
        private void BindNationsCb(string selectedValue = null)
        {
            IList<DicKeyValueDto> nations = IdentityNation.Wu.GetDescriptions().Where(i => i.Key != 0).ToList();
            this.ComNation.Properties.DisplayMember = "Value";
            this.ComNation.Properties.ValueMember = "Key";
            this.ComNation.EditValue = "Value";
          
            this.ComNation.Properties.DataSource = nations;
            this.ComNation.Properties.NullText = "==请选择==";

            if (selectedValue != null)
            {
                this.ComNation.SelectedText = selectedValue;
            }
            else
            {
                this.ComNation.ItemIndex = 1;
            }
        }

        /// <summary>
        /// 文凭
        /// </summary>
        /// <param name="selectedValue"></param>
        private void BindEducationLeveCb(string selectedValue = null)
        {
            IList<DicKeyValueDto> nations = EnumDiploma.wu.GetDescriptions().Where(i => i.Key != 0).ToList();
            this.ComEducationLevel.Properties.DisplayMember = "Value";
            this.ComEducationLevel.Properties.ValueMember = "Key";
            this.ComEducationLevel.EditValue = "Value";

            this.ComEducationLevel.Properties.DataSource = nations;
            this.ComEducationLevel.Properties.NullText = "==请选择==";
            //是否显示列名

            ComEducationLevel.Properties.ShowHeader = false;

            //是否显示底部

            ComEducationLevel.Properties.ShowFooter = false;
            //显示第一项

            ComEducationLevel.ItemIndex = 0;

            if (selectedValue != null)
            {
                this.ComEducationLevel.SelectedText = selectedValue;
            }
            else
            {
                this.ComEducationLevel.ItemIndex = 1;
            }
        }

        /// <summary>
        ///查询工种
        /// </summary>
        private void GetProjectList()
        {


            IMulePusher pusherLogin = new GetWorkerTypesApi();
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                List<TypeList> pList = pushLogin.ResponseData;
                this.comWorkType.Properties.DisplayMember = "name";
                this.comWorkType.Properties.ValueMember = "code";
                this.comWorkType.Properties.DataSource = pList;
                this.comWorkType.EditValue = "code";

            }
        }

        /// <summary>
        ///查询劳务公司
        /// </summary>
        private void GetOrganizationUuidList()
        {


            IMulePusher pusherLogin = new GetOrganizationApi() { RequestParam = new { uuid = 1, projectUuid = ConfigHelper.KtpLoginProjectId } };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                List<OrganizationList> pList = pushLogin.ResponseData;
                this.ComOrganizationUuid.Properties.DisplayMember = "name";
                this.ComOrganizationUuid.Properties.ValueMember = "uuid";
                this.ComOrganizationUuid.Properties.DataSource = pList;
                this.ComOrganizationUuid.Properties.NullText = "===请选择===";
                this.comWorkerTeamUuid.Properties.NullText = "===请先选择劳务公司===";
                
            }
        }


        private string GetPic(DevExpress.XtraEditors.PictureEdit pictureBox)
        {
            try
            {

                return AForgeWorkerPicHelper.GetPicLocal(AVidePlayer, pictureBox);

            }
            catch (NullReferenceException)
            {
                MessageHelper.Show(@"获取图像失败，请检查摄像头是否正常", _msgCaption);
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.ExceptionLog(ex);
                MessageHelper.Show(ex);
                return string.Empty;
            }
        }


        #endregion

        /// <summary>
        /// 添加花名册
        /// </summary>
        /// <param name="add"></param>
        private int? addUser(AddWorerkSend add)
        {

            int? userId = 0;
            add.organizationUuid = this.ComOrganizationUuid.EditValue.ToString();
            add.workType = this.comWorkType.EditValue.ToString();
            add.workerTeamUuid = this.comWorkerTeamUuid.EditValue.ToString();

            IMulePusher imIsexits = new GetWorkerIsExistApi() { RequestParam = new { phone= add.phone } };
            PushSummary PuIsexits = imIsexits.Push();
            if (PuIsexits.Success) {

                BaseResult data = PuIsexits.ResponseData;
                add.userUuid = data.data.userUuid;
            }

            IMulePusher addworkers = new WorkerSet() { RequestParam = add };
            PushSummary pushAddworkers = addworkers.Push();
            if (pushAddworkers.Success)
            {
                BaseResult data = pushAddworkers.ResponseData;
                userId = data.data.userId;
            }
            return userId;

        }



        /// <summary>
        /// 甲子分包
        /// </summary>
        /// <param name="add"></param>
        private int? addJiaZiUser(AddWorerkSend add)
        {
            int? userId = 0;

            IMulePusher imIsexits = new GetWorkerIsExistApi() { RequestParam = new { phone = add.phone } };
            PushSummary PuIsexits = imIsexits.Push();
            if (PuIsexits.Success)
            {

                BaseResult data = PuIsexits.ResponseData;
                add.userUuid = data.data.userUuid;
            }
            IMulePusher addworkers = new WorkerSet() { RequestParam = add, API = "/userPanel/addJiaZiUser" };
            PushSummary pushAddworkers = addworkers.Push();
            if (pushAddworkers.Success)
            {
                BaseResult data = pushAddworkers.ResponseData;
                userId = data.data.userId;
            }
            return userId;
        }

        private bool SubmitBtnPreValidation()
        {
            var isPrePass = true;
            PreValidationHelper.InitPreValidation(FormErrorProvider);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtName, "姓名不允许为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtIdCard, "身份证号不允许为空", ref isPrePass);
            PreValidationHelper.IsIdCard(FormErrorProvider, txtIdCard, "身份证号格式错误", ref isPrePass);
            PreValidationHelper.MustNotBeNull(FormErrorProvider, ComNation, "民族必须选择", ref isPrePass);
            PreValidationHelper.MustNotBeNull(FormErrorProvider, ComEducationLevel, "文化程度不能为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtAddress, "身份证地址不允许为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtPhone, "手机号码不允许为空", ref isPrePass);
            PreValidationHelper.IsMobile(FormErrorProvider, txtPhone, "手机号码格式错误", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtStartTime, "身份证有效开始时间不允许为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtExpireTime, "身份证有效结束时间不允许为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtEmergencyContactPhone, "紧急联系人手机号码不允许为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtEmergencyContactPhone, "紧急联系人手机号码格式错误", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtEmergencyContactName, "紧急联系人不允许为空", ref isPrePass);

            if (txtBirthday.DateTime > DateTime.Now.Date.AddYears(-18))
            {
                FormErrorProvider.SetError(txtBirthday, "出生日期不得小于成年人年龄");
                isPrePass = false;
            }

            if (txtGender.SelectedIndex == -1)
            {
                FormErrorProvider.SetError(txtGender, "性别必须选择");
                isPrePass = false;
            }
            if (pic_facePic.Image == null)
            {

                FormErrorProvider.SetError(pic_facePic, "人脸信息采集照片不能为空");
                isPrePass = false;
            }
            if (pic_picturePositive.Image == null)
            {

                FormErrorProvider.SetError(pic_picturePositive, "身份证正面照片不能为空");
                isPrePass = false;
            }
            if (pic_pictureReverse.Image == null)
            {

                FormErrorProvider.SetError(pic_pictureReverse, "身份证反面照片不能为空");
                isPrePass = false;
            }
            if (_state == 0)
            {
                PreValidationHelper.MustNotBeNull(FormErrorProvider, ComOrganizationUuid, "劳务公司不能为空", ref isPrePass);
                PreValidationHelper.MustNotBeNull(FormErrorProvider, comWorkerTeamUuid, "班组不能为空", ref isPrePass);
                PreValidationHelper.MustNotBeNull(FormErrorProvider, comWorkType, "工种不能为空", ref isPrePass);
                PreValidationHelper.MustNotBeNull(FormErrorProvider, txtBankNo, "银行卡不能为空", ref isPrePass);
                PreValidationHelper.MustNotBeNull(FormErrorProvider, txtBankName, "银行卡开户行不能为空", ref isPrePass);

            }
            return isPrePass;
        }
        /// <summary>
        /// 设置控件的隐藏或只读
        /// </summary>
        /// <param name="state">0、工人1、分包2、项目人员</param>
        public void ContentState(int state)
        {

            this.txtAddress.ReadOnly = true;
            this.txtAvg.ReadOnly = true;
            this.txtBankName.ReadOnly = false;

            this.txtBirthday.ReadOnly = true;
            //this.txtEmergencyContactName.ReadOnly = true;
            //this.txtEmergencyContactPhone.ReadOnly = true;
            this.txtGender.ReadOnly = true;
            this.txtIdCard.ReadOnly = true;
            this.txtName.ReadOnly = true;
            //this.ComNation.ReadOnly = true;
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
                this.txtPhone.ReadOnly = true;
            }
            else
            {
                panelProjectInfo.Visible = true;
                panelBankInfo.Visible = true;

            }

        }
     


    }
}