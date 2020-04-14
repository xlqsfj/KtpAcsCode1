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
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.WinForm.Jijian.Device;

namespace KtpAcs.WinForm.Jijian
{
    public partial class DeviceListForm : DevExpress.XtraEditors.XtraForm
    {
        public DeviceListForm()
        {
            InitializeComponent();
            GetDevice();
        }



        public void GetDevice()
        {

            try
            {
                IMulePusher pusherDevice = new GetDeviceApi() { RequestParam = new { pageNum = 0, pageSize = 0, projectUuid = ConfigHelper.KtpLoginProjectId } };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {


                    DeviceListResult.Data data = push.ResponseData;
                    this.gridControl1.DataSource = data.list;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult result = XtraMessageBox.Show("确定要删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                ////访问数据库删除
                //int num = DBHelper.ExecuteNonQuery("delete Users where qq=" + qq);
                //if (num > 0)
                //{
                //    //InitData();//刷新
                //    this.gvDatas.DeleteRow(this.gvDatas.FocusedRowHandle);//删除行
                //    XtraMessageBox.Show($"QQ：{qq}删除成功！");
                //}
                XtraMessageBox.Show($"QQ：删除成功！");
            }
        }





        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {

            //if (e.Button == MouseButtons.Right)
            //{
            //    //  this.popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            //    this.popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            //}
        }

        private void grid_Device_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                //this.popupMenu2.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                //    popupMenu1.ShowPopup(Control.MousePosition);
                this.popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            try
            {
                //获取焦点数据行
                dynamic row = this.grid_Device.GetFocusedRow();
                string id = row.uuid;
                AddDevice addDevice = new AddDevice(id);
                addDevice.Show();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("确定要删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
         

                try
                {
                    //获取焦点数据行
                    dynamic row =this.grid_Device.GetFocusedRow();
                    string id = row.uuid;
                    IMulePusher pusherDevice = new DelDeviceApi() { RequestParam = new BaseSend() { uuid = id } };
                    PushSummary push = pusherDevice.Push();
                    if (push.Success)
                    {
                        XtraMessageBox.Show($"QQ：删除成功！");
                        this.grid_Device.DeleteRow(this.grid_Device.FocusedRowHandle);//删除行

                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"错误信息:{0}", ex.Message);

                }



            
            }
        }
    }
}