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
            this.ComNation.Properties.NullText = "==请选择==";
            this.ComNation.Properties.DataSource = nations;

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
        /// 银行列表
        /// </summary>
        /// <param name="selectedValue"></param>
        private void BindBankCardCb(string selectedValue = null)
        {
            IList<DicKeyValueDto> nations = EnumBankCardType.Bohai.GetDescriptions().Where(i => i.Key != 0).ToList();
            this.comBankName.Properties.DisplayMember = "Value";
            this.comBankName.Properties.ValueMember = "Key";
            this.comBankName.EditValue = "Value";
            this.comBankName.Properties.NullText = "==请选择==";
            this.comBankName.Properties.DataSource = nations;

            if (selectedValue != null)
            {
                this.comBankName.SelectedText = selectedValue;
            }
            else
            {
                this.comBankName.ItemIndex = 1;
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
            this.ComEducationLevel.Properties.NullText = "==请选择==";
            this.ComEducationLevel.Properties.DataSource = nations;
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


            IMulePusher pusherLogin = new GetOrganizationApi() { RequestParam = new { uuid = 1 } };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                List<OrganizationList> pList = pushLogin.ResponseData;
                this.ComOrganizationUuid.Properties.DisplayMember = "name";
                this.ComOrganizationUuid.Properties.ValueMember = "uuid";
                this.ComOrganizationUuid.Properties.DataSource = pList;
                //this.ComOrganizationUuid.EditValue = "uuid";

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
        private  void addUser(AddWorerkSend add)
        {
            add.organizationUuid = this.ComOrganizationUuid.EditValue.ToString();
            add.workType = this.comWorkType.EditValue.ToString();
            add.workerTeamUuid = this.comWorkerTeamUuid.EditValue.ToString();

            IMulePusher addworkers = new WorkerSet() { RequestParam = add };
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

        /// <summary>
        /// 甲子分包
        /// </summary>
        /// <param name="add"></param>
        private  void addJiaZiUser(AddWorerkSend add)
        {
            IMulePusher addworkers = new WorkerSet() { RequestParam = add, API= "/userPanel/addJiaZiUser" };
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