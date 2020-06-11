using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KtpAcs.KtpApiService.Device;
using KtpAcs.KtpApiService.Send;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using static KtpAcs.KtpApiService.Result.DeviceListResult;
using KtpAcs.WinForm.Jijian.Base;
using KtpAcs.Infrastructure.Exceptions;
using KtpAcs.KtpApiService;
using KtpAcs.PanelApi.Haiqing;
using KtpAcs.PanelApi.Haiqing.Api;
using KtpAcs.PanelApi.Haiqing.Model;

namespace KtpAcs.WinForm.Jijian.Device
{
    //声明委托重新提交
    public delegate void AgainSubmit(string close);

    public partial class AddDevice : DevExpress.XtraEditors.XtraForm
    {
        //声明事件
        public event AgainSubmit ShowSubmit;

        public AddDevice()
        {
            InitializeComponent();
        }
        private string _Id = "";
        public AddDevice(string id)
        {
            InitializeComponent();
            this._Id = id;
            GetDevice(id);

        }

        public void GetDevice(string id)
        {

            try
            {
                IMulePusher pusherDevice = new GetDeviceApi() { RequestParam = new { deviceUuid = id, pageNum = 0, pageSize = 0, projectUuid = ConfigHelper.KtpLoginProjectId } };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {

                    DeviceListResult.Data data = push.ResponseData;
                    DeviceList deviceList = data.list[0];
                    this.txt_description.Text = deviceList.description;
                    this.txt_deviceId.Text = deviceList.deviceId;
                    this.txtDeviceIp.Text = deviceList.deviceIp;



                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                CheckVerification();

     
                //查询设备序列号
                IMulePusherHq PanelDevice = new PanelGetSysParamApi() { PanelIp = txtDeviceIp.Text };
                PushSummaryHq PanelMag = PanelDevice.Push();
                HqResult result = PanelMag.ResponseData;
                if (result == null)
                {
                    throw new PreValidationException("未找到该设备，请检查或请确定该设备是否是该厂商面板!");
                }
                string deviceNo = result.info.DeviceID.ToString();
                this.txt_deviceId.Text = deviceNo;
                DeviceSend deviceSend = new DeviceSend
                {

                    description = this.txt_description.Text,
                    deviceId = this.txt_deviceId.Text,
                    gateType = this.radIsEnter.SelectedIndex == 0 ? 1 : 2,
                    deviceIp = txtDeviceIp.Text,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    uuid = this._Id
                };

                IMulePusher pusherLogin = new SetDeviceApi() { RequestParam = deviceSend };
                PushSummary pushLogin = pusherLogin.Push();
                if (pushLogin.Success)
                {
                 
           
                    btnSave.Enabled = true;
                    btnSave.Text = "保存";
                    if (this._Id != "")
                    {
                        MessageHelper.Show($"修改成功");
                        if(ShowSubmit != null) ShowSubmit("");
                 
                    }
                    else
                        MessageHelper.Show($"添加成功");
                    this.Close();
                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave.Text = "保存";

                    MessageHelper.Show($"添加失败:" + pushLogin.Message);
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                btnSave.Text = "保存";
                MessageHelper.Show($"添加失败:" + ex.Message);
            }
        }

        private void CheckVerification()
        {
            var isPrePass = true;
            PreValidationHelper.InitPreValidation(FormErrorProvider);
            //PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, CodeTxt, "编号(设备号)不能为空", ref isPrePass);
            PreValidationHelper.MustNotBeNullOrEmpty(FormErrorProvider, txtDeviceIp, "IP地址不能为空", ref isPrePass);
            PreValidationHelper.IsIpAddress(FormErrorProvider, txtDeviceIp, "IP地址格式错误", ref isPrePass);


            if (this.radIsEnter.SelectedIndex == -1)
            {
                FormErrorProvider.SetError(this.radIsEnter, "必须选择是否是进场方向");
                isPrePass = false;
            }
            if (!isPrePass)
            {
                throw new PreValidationException(PreValidationHelper.ErroMsg);

            }
            if (!ConfigHelper.MyPing(txtDeviceIp.Text))
            {
                FormErrorProvider.SetError(txtDeviceIp, "IP地址不通，请确定同一个网段");
                throw new PreValidationException("IP地址不通，请确定同一个网段");

            }
            btnSave.Text = "正在添加。。";
            btnSave.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}