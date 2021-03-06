﻿using System;
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
using KtpAcs.PanelApi.Yushi.Api;
using KtpAcs.PanelApi.Yushi.Model;
using KtpAcs.PanelApi.Yushi;
using KtpAcs.KtpApiService;

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
                IMulePusherYs PanelDevice = new PanelGetDeviceApi() { PanelIp = txtDeviceIp.Text };
                PushSummarYs PanelMag = PanelDevice.Push();
                PanelResult result = PanelMag.ResponseData;
                string deviceNo = result.Response.Data.SerialNumber;
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
                    //Device device = this.RequestParam;
                    Liblist liblist = PanelBase.GetPanelDeviceInfo(txtDeviceIp.Text);
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