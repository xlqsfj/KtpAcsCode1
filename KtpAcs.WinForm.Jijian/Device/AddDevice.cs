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
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Device;
using KtpAcs.KtpApiService.Send;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using static KtpAcs.KtpApiService.Result.DeviceListResult;

namespace KtpAcs.WinForm.Jijian.Device
{
    public partial class AddDevice : DevExpress.XtraEditors.XtraForm
    {
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
                IMulePusher pusherDevice = new GetDeviceApi() { RequestParam = new { uuid = id, pageNum = 0, pageSize = 0, projectUuid = ConfigHelper.KtpLoginProjectId } };
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

            DeviceSend deviceSend = new DeviceSend
            {

                description = this.txt_description.Text,
                deviceId = this.txt_deviceId.Text,
                gateType = 1,
                deviceIp = txtDeviceIp.Text,
                projectUuid = ConfigHelper.KtpLoginProjectId,
                uuid= this._Id
            };

            IMulePusher pusherLogin = new SetDeviceApi() { RequestParam = deviceSend };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {


                XtraMessageBox.Show($"添加成功");
            }
            else
            {

                XtraMessageBox.Show($"添加失败:" + pushLogin.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}