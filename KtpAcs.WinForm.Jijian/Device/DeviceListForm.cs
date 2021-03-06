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
using static KtpAcs.WinForm.Jijian.Device.AddDevice;
using KtpAcs.WinForm.Jijian.Base;

namespace KtpAcs.WinForm.Jijian
{
    public partial class DeviceListForm : DevExpress.XtraEditors.XtraForm
    {

 
        public DeviceListForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetDevice();
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
                frm.StartPosition = FormStartPosition.CenterParent;
                //注册事件
                frm.ShowSubmit += ShowExptForm;
                frm.ShowDialog();
            }
            else
            {
                MessageHelper.Show("请先选择同步的面板,未连接的人脸识别设备不能同步!");
            }

        }
        public void ShowExptForm()
        {
            if (WorkSysFail.list.Count() > 0)
            {
                WorkerSynFail workerSynFail = new WorkerSynFail();
                workerSynFail.StartPosition = FormStartPosition.CenterParent;
                workerSynFail.ShowDialog();
            }
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

                    if (data.list.Count > 0)
                    {
                      
                        WorkSysFail.workAdd.Clear();
                        TaskFactory taskFactory = new TaskFactory();
                        List<Task> taskList = new List<Task>();
                        LoadingHelper.ShowLoadingScreen();//显示
                        Parallel.ForEach(data.list, (list, DeviceList) =>
                        {
                            taskList.Add(taskFactory.StartNew(() => NewMethod(list)));
                        });

                        taskFactory.ContinueWhenAll(taskList.ToArray(), a =>
                        {
                            try
                            {
                                this.gridControl.DataSource = data.list;
                                taskList.Clear();
                                LoadingHelper.CloseForm();//关闭
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        });
                  
                        panelContent.Visible = false;
                        gridControl.Visible = true;
                    }
                    else
                    {
                        panelContent.Visible = true;
                        gridControl.Visible = false;
                      //LoadingHelper.CloseForm();//关闭
                    }

                }

            }
            catch (Exception ex)
            {
                MessageHelper.Show($"错误信息:{ex.Message}",ex);

            }



        }

        private static void NewMethod(DeviceList list)
        {
            try
            {
                bool isConn = true;


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
                    if (!WorkSysFail.workAdd.Contains(okConnPanelInfo))
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
            catch (Exception ex)
            {

                LogHelper.ExceptionLog(ex);
                
            }
         
        }

        private void grid_Device_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == MouseButtons.Right)
                {

                    //this.popupMenu2.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                    //    popupMenu1.ShowPopup(Control.MousePosition);
                    this.popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
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
                addDevice.StartPosition = FormStartPosition.CenterParent;
                addDevice.ShowSubmit += new AgainSubmit(o => GetDevice());
                addDevice.ShowDialog();
             
       
             

            }
            catch (Exception ex)
            {
                MessageHelper.Show($"错误信息:{ex.Message}", ex);

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
                        XtraMessageBox.Show($"{ip}:删除成功！");
                        this.grid_Device.DeleteRow(this.grid_Device.FocusedRowHandle);//删除行
                        WorkSysFail.DeleteDeviceInfo(ip);

                        if (this.grid_Device.RowCount < 1) {
                            panelContent.Visible = true;
                            gridControl.Visible = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageHelper.Show($"错误信息:{ex.Message}", ex);

                }




            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                AddDevice addDevice = new AddDevice();
                addDevice.ShowDialog();
                GetDevice();
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
            }
        }

        private void grid_Device_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            try
            {
                if (e.Column.FieldName == "deviceStatus") //指定列
                {
                    if ((string)e.CellValue == "否")  //条件  e.CellValue 为object类型
                        e.Appearance.BackColor = Color.FromArgb(0, 118, 248);
                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }
    }
}