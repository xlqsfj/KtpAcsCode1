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
using static KtpAcs.KtpApiService.Result.DeviceListResult;
using KtpAcs.KtpApiService.Model;

using KtpAcs.PanelApi.Yushi.Model;
using KtpAcs.PanelApi.Yushi.Api;
using KtpAcs.PanelApi.Yushi;

namespace KtpAcs.WinForm.Jijian
{
    public partial class DeviceListForm : DevExpress.XtraEditors.XtraForm
    {

        public DeviceListForm()
        {
            InitializeComponent();

        }


        public void SysWorkerToPanel()
        {

            List<string> list = new List<string>();

            for (int i = 0; i < this.grid_Device.RowCount; i++)
            {
                dynamic row = this.grid_Device.GetRow(i);
                if (row.isSeleced && row.deviceStatus == "是")
                {

                    list.Add(row.deviceIp);
                }
            }
            if (list.Count > 0)
            {
                //清空上次同步失败的人员
                WorkSysFail.list.Clear();
                WorkerSynForm frm = new WorkerSynForm(list);
                //注册事件
                frm.ShowSubmit += ShowExptForm;
                frm.ShowDialog();
            }
            else
            {

            }

        }
        public void ShowExptForm()
        {
            if (WorkSysFail.list.Count() > 0)
            {

                new WorkerSynForm().ShowDialog();
            }

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

                    if (data.list.Count > 0)
                    {
                        WorkSysFail.workAdd.Clear();
                        Parallel.ForEach(data.list, (list, DeviceList) =>
                        {

                            bool isConn = true;
                            var workAdd = WorkSysFail.workAdd.FirstOrDefault(a => a.deviceIp == list.deviceIp);
                            if (workAdd != null)
                            {
                                isConn = workAdd.isConn;

                            }
                            else
                            {

                                //设备是否连接
                                isConn = ConfigHelper.MyPing(list.deviceIp);

                                if (isConn)
                                {
                                    var okConnPanelInfo = new WorkAddInfo
                                    {
                                        deviceIp = list.deviceIp,
                                        isConn = true,
                                        deviceIn = list.gateType,
                                        deviceNo = list.deviceId,
                                        magAdd = "添加中.."
                                    };
                                    WorkSysFail.workAdd.Add(okConnPanelInfo);

                                    //返回设备的数量

                                    Liblist liblist = PanelBase.GetPanelDeviceInfo(list.deviceIp);
                                    if (liblist != null)
                                    {

                                        //设备数量

                                        list.deviceCount = liblist.MemberNum;
                                    }
                                    else
                                    {
                                        list.deviceStatus = "否";
                                        WorkSysFail.DeleteDeviceInfo(list.deviceIp);
                                    }

                                }
                                list.deviceStatus = isConn ? "是" : "否";

                            }



                        });

                        this.gridControl.DataSource = data.list;
                        panelContent.Visible = false;
                        gridControl.Visible = true;
                    }
                    else
                    {
                        panelContent.Visible = true;
                        gridControl.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



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
                GetDevice();

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
                    dynamic row = this.grid_Device.GetFocusedRow();
                    string id = row.uuid;
                    string ip = row.deviceIp;
                    IMulePusher pusherDevice = new DelDeviceApi() { RequestParam = new BaseSend() { uuid = id } };
                    PushSummary push = pusherDevice.Push();
                    if (push.Success)
                    {

                        //宇视产品
                        IMulePusherYs panelDeleteApi = new PanelLibraryDeleteApi() { PanelIp = ip };

                        PushSummarYs pushSummarySet = panelDeleteApi.Push();
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

        private void DeviceListForm_Load(object sender, EventArgs e)
        {
            GetDevice();
        }

        private void grid_Device_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int index = this.grid_Device.FocusedRowHandle;
            dynamic d = this.grid_Device.GetRow(index);
            if (e.Column.Caption == "checkbox")
            {
                int i = 0;
            }
            //DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)grid_Device.Cells[0];
            //for (int i = 0; i < grid_Device.GetChildRowCount(index); i++)
            //{
            //    int row = _view.GetChildRowHandle(rowHandle, i);

            //}
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            AddDevice addDevice = new AddDevice();
            addDevice.ShowDialog();
        }
    }
}