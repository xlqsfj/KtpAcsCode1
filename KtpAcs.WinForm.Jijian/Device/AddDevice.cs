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

namespace KtpAcs.WinForm.Jijian.Device
{
    public partial class AddDevice : DevExpress.XtraEditors.XtraForm
    {
        public AddDevice()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            DeviceSend deviceSend = new DeviceSend
            {

                description = this.txt_description.Text,
                deviceId = this.txt_deviceId.Text,
                gateType = 1,
                deviceIp = txtDeviceIp.Text,
                projectUuid=ConfigHelper.KtpLoginProjectId
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