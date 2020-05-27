using KtpAcs.Infrastructure.Search.Extensions;
using KtpAcs.Infrastructure.Serialization;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Base;
using KtpAcs.KtpApiService.Model;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;

using KtpAcs.WinForm.Jijian.Base;
using KtpAcs.WinForm.Jijian.Workers;
using KtpAcsMiddleware.KtpApiService.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static KtpAcs.KtpApiService.Result.OrganizationListResult;
using static KtpAcs.KtpApiService.Result.TeamListResult;
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
            //this.ComNation.EditValue = "Value";
            this.ComNation.Properties.DataSource = nations;

            this.ComNation.Properties.NullText = "";

            if (selectedValue != null)
            {
                if (selectedValue == "汉")
                    selectedValue = "汉族";
                DicKeyValueDto dicKeyValueDto= nations.Where(a => a.Value == selectedValue).FirstOrDefault();
                int key = 0;
                if (dicKeyValueDto != null)
                    key = dicKeyValueDto.Key;
               
                this.ComNation.EditValue = key;
                this.ComNation.Text = selectedValue;

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
                this.ComOrganizationUuid.Properties.NullText = "===请选择===";
                this.comWorkType.Properties.Columns.Add(
                 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name"));

            }
        }

        /// <summary>
        ///查询劳务公司
        /// </summary>
        private void GetOrganizationUuidList()
        {

            List<OrganizationList> pList = null;
            //Task.Run(() =>
            //{
            IMulePusher pusherLogin = new GetOrganizationApi()
            {
                RequestParam = new
                {
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    pageSize = 0,
                    pageNum = 0,
                }
            };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                pList = pushLogin.ResponseData;

            }
            // });
            this.ComOrganizationUuid.Properties.DisplayMember = "name";
            this.ComOrganizationUuid.Properties.ValueMember = "uuid";
            this.ComOrganizationUuid.Properties.DataSource = pList;
            this.ComOrganizationUuid.Properties.NullText = "===请选择===";
            this.comWorkerTeamUuid.Properties.NullText = "===请先选择劳务公司===";
            this.ComOrganizationUuid.Properties.Columns.Add(
           new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name"));
        }
        private void GetTeamInfo(object uuid)
        {
            IMulePusher pusherLogin = new GeTeamsApi()
            {
                RequestParam = new
                {
                    pageSize = 0,
                    pageNum = 0,
                    organizationUuid = uuid
                }
            };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                this.comWorkerTeamUuid.Properties.Columns.Clear();
                List<TeamList> pList = pushLogin.ResponseData;
                this.comWorkerTeamUuid.Properties.DisplayMember = "teamName";
                this.comWorkerTeamUuid.Properties.ValueMember = "uuid";
                this.comWorkerTeamUuid.Properties.DataSource = pList;
                this.comWorkerTeamUuid.Properties.NullText = "==请选择==";
                this.comWorkerTeamUuid.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("teamName", "选择班组"));

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

            IMulePusher imIsexits = new GetWorkerIsExistApi() { RequestParam = new { phone = add.phone } };
            PushSummary PuIsexits = imIsexits.Push();
            if (PuIsexits.Success)
            {

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
        #region 提交验证

        private bool SubmitBtnPreValidation()
        {
            var isPrePass = true;
            PreValidationHelper.InitPreValidation(FormErrorProvider);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtName, "姓名不允许为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtIdCard, "身份证号不允许为空", ref isPrePass);
            PreValidationHelper.IsIdCard(FormErrorProvider, txtIdCard, "身份证号格式错误", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtCardAgency, "发证机关不能为空", ref isPrePass);
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
            this.txtBankName.ReadOnly = true;

            this.txtBirthday.ReadOnly = true;

            this.txtGender.ReadOnly = true;
            this.txtIdCard.ReadOnly = true;
            this.txtName.ReadOnly = true;
            //this.ComNation.ReadOnly = true;
            this.txtNativePlace.ReadOnly = true;
            this.txtExpireTime.ReadOnly = true;
            this.txtStartTime.ReadOnly = true;
            this.txtCardAgency.ReadOnly = true;
            this.ComNation.ReadOnly = true;
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
            else if (state == 0)
            {
                panelProjectInfo.Visible = true;
                panelBankInfo.Visible = true;

            }

        }
        public void SetIsEdit(bool isEdit)
        {

            if (isEdit)
            {
                //修改
                this.simpleButton1.Text = "关闭";
                this.txtPhone.ReadOnly = true;
                this.comWorkerTeamUuid.ReadOnly = true;
                this.comWorkType.ReadOnly = true;
                this.ComOrganizationUuid.ReadOnly = true;
                this.txtBankNo.ReadOnly = true;
                this.btnPicturePositive.Visible = false;
                this.btnPictureReverse.Visible = false;
                this.btnReadIC.Visible = false;
            }
            else
            {


                //详情
                this.btnSubmit.Visible = false;
                this.AVidePlayer.Visible = false;
                this.simpleButton1.Text = "关闭";
                this.txtEmergencyContactName.ReadOnly = true;
                this.txtEmergencyContactPhone.ReadOnly = true;
                this.txtPhone.ReadOnly = true;
                this.comWorkerTeamUuid.ReadOnly = true;
                this.comWorkType.ReadOnly = true;
                this.ComOrganizationUuid.ReadOnly = true;
                this.txtBankNo.ReadOnly = true;
                this.ComEducationLevel.ReadOnly = true;
                this.btnFacePic.Visible = false;
                this.btnPicturePositive.Visible = false;
                this.btnPictureReverse.Visible = false;
                this.btnReadIC.Visible = false;
            }

        }


        /// <summary>
        /// 点击可以手动编辑
        /// </summary>
        public void CurrentManualEdit()
        {

            _isManualEdit = true;
            this.txtAddress.ReadOnly = false;

            this.txtBirthday.ReadOnly = false;

            this.txtGender.ReadOnly = false;
            this.txtIdCard.ReadOnly = false;
            this.txtName.ReadOnly = false;
            //this.ComNation.ReadOnly = false;
            this.txtNativePlace.ReadOnly = false;
            this.txtExpireTime.ReadOnly = false;
            this.txtStartTime.ReadOnly = false;
            this.txtCardAgency.ReadOnly = false;
            this.ComNation.ReadOnly = false;
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="state"></param>
        public void reslt(int state)
        {
            this.txtAddress.Text = "";
            this.txtAvg.Text = "";
            this.txtBankName.Text = "";
            this.txtBankNo.Text = "";
            this.txtBirthday.Text = "";
            this.txtEmergencyContactName.Text = "";
            this.txtEmergencyContactPhone.Text = "";
            this.txtGender.Text = "";
            this.txtIdCard.Text = "";
            this.txtName.Text = "";
            this.txtNativePlace.Text = "";
            this.txtExpireTime.Text = "";
            this.txtStartTime.Text = "";
            this.txtCardAgency.Text = "";
            this.txtCardAgency.Text = "";

            this.ComEducationLevel.Properties.NullText = "===请选择===";
            this.ComOrganizationUuid.Properties.NullText = "===请选择===";
            this.comWorkerTeamUuid.Properties.NullText = "===请选择===";
            this.comWorkType.Properties.NullText = "===请选择===";
            this.ComNation.Properties.NullText = "===请选择===";
            this.ComNation.EditValue = null;
            this.ComEducationLevel.EditValue = null;
            this.ComOrganizationUuid.EditValue = null;
            this.comWorkerTeamUuid.EditValue = null;
            this.comWorkType.EditValue = null;
            this.pic_facePic.Image = Jijian.Properties.Resources.sfz_r;
            this.pic_picturePositive.Image = Jijian.Properties.Resources.sfz_z;
            this.pic_pictureReverse.Image = Jijian.Properties.Resources.sfz_f;
            this.IdentityHeadPic.Image = Jijian.Properties.Resources.img_zjz1;

            if (state == 1 || state == 0)
            {

                panelBankInfo.Text = "";
                this.txtPhone.Text = "";

            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="state"></param>
        public void GetInfo(int state, string workerId)
        {
            WorkerResult.Data w = new WorkerResult.Data();

            try
            {
                IMulePusher PanelLibrarySet = new GetWorkerApi() { RequestParam = new { projectUuid = ConfigHelper.KtpLoginProjectId, userUuid = workerId, designatedFlag = state } };

                PushSummary pushSummary = PanelLibrarySet.Push();
                if (!pushSummary.Success)
                    MessageHelper.Show(pushSummary.Message);
                else

                    w = pushSummary.ResponseData;
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message);
            }

            this.txtAddress.Text = w.address;
            this.txtAvg.Text = w.age.ToString();
            this.txtBankName.Text = w.bankName;

            this.txtBirthday.Text = w.birthday;
            this.txtEmergencyContactName.Text = w.emergencyContactName;
            this.txtEmergencyContactPhone.Text = w.emergencyContactPhone;
            this.txtGender.SelectedIndex = w.gender == 1 ? 0 : 1;
            this.txtIdCard.Text = w.idCard;
            this.txtName.Text = w.name;
            this.txtNativePlace.Text = w.nativePlace;
            this.txtExpireTime.Text = w.expireTime;
            this.txtStartTime.Text = w.startTime;
            this.txtCardAgency.Text = w.cardAgency;
            this.txtBankNo.Text = w.bankNo;
            //IList<DicKeyValueDto> nations = EnumSerializationExtension.GetEnumValue(IdentityNation.Wu, w.nation);  

            this.ComEducationLevel.EditValue = w.educationLevel;
            this.ComOrganizationUuid.EditValue = w.organizationUuid;
            this.comWorkerTeamUuid.EditValue = w.workerTeamUuid;
            this.comWorkType.EditValue = w.workType;
            BindNationsCb(w.nation);
            //人脸采集照片
            if (!string.IsNullOrEmpty(w.facePic))
            {
                try
                {
                    Image img = Image.FromStream(System.Net.WebRequest.Create(w.facePic).GetResponse().GetResponseStream());
                    _url_facePicId = w.facePic;
                    pic_facePic.Image = pic_facePic.Image = img;

                }
                catch (Exception ex)
                {


                }
            }
            //身份证正面
            if (!string.IsNullOrEmpty(w.picturePositive))
            {
                try
                {
                    Image imgPic2 = Image.FromStream(System.Net.WebRequest.Create(w.picturePositive).GetResponse().GetResponseStream());
                    _url_identityPicId = w.picturePositive;
                    this.pic_picturePositive.Image = imgPic2;
                }
                catch (Exception ex)
                {


                }
            }
            //身份证反面
            if (!string.IsNullOrEmpty(w.pictureReverse))
            {
                try
                {
                    Image imgPic3 = Image.FromStream(System.Net.WebRequest.Create(w.pictureReverse).GetResponse().GetResponseStream());
                    this.pic_pictureReverse.Image = imgPic3;
                    _url_identityBackPicId = w.pictureReverse;
                }
                catch (Exception ex)
                {


                }
            }
            //头像
            if (!string.IsNullOrEmpty(w.icon))
            {
                try
                {
                    Image imgUpic = Image.FromStream(System.Net.WebRequest.Create(w.icon).GetResponse().GetResponseStream());
                    this.IdentityHeadPic.Image = imgUpic;
                    _url_upic = w.icon;
                }
                catch (Exception ex)
                {


                }
            }

            if (state == 1 || state == 0)
            {

                this.txtPhone.Text = w.phone;

            }

        }

        #endregion

    }
}