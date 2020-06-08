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
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using DevExpress.XtraTab;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using KtpAcs.PanelApi.Yushi;
using KtpAcs.PanelApi.Yushi.Api;
using KtpAcs.PanelApi.Yushi.Model;
using RestSharp;
using KtpAcs.KtpApiService.Model;
using KtpAcs.WinForm.Jijian.Device;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerProjectForm : DevExpress.XtraEditors.XtraForm
    {
        public bool isOpen = false; //是否打开摄像头
        AddWorker addWorker = null;
        public WorkerProjectForm()
        {
            InitializeComponent();
            GetStateList();
            GetWorkerList();
            RepositoryItemHyperLinkEdit linkSalesMoney = CreateRepositoryItemHyperLinkEdit("销售金额");
            linkSalesMoney.OpenLink += new OpenLinkEventHandler(repositoryItemButtonEdit3_Click);  //事件
            this.SalesMoney.ColumnEdit = linkSalesMoney;  //绑定
        }




        public void GetWorkerList(string Query = "")
        {

            try
            {
                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = 25,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    pageNum = 1,
                    status = (int)this.ComUsable.EditValue,
                    keyWord = txtQuery.Text
                };

                IMulePusher pusherDevice = new GetWorkersProjectApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {


                    WorkerProjectListResult.Data data1 = push.ResponseData;
                    this.gridControl1.DataSource = data1.list;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }

        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            try
            {


                dynamic row = this.grid_WorkerProject.GetFocusedRow();
                string id = row.organizationUserUuid;
                string phone = row.phone;
                string state = row.status;
                string name = row.name;

                if (state == "未进场" || state == "已离场")
                {
                    if (xtraTabControl1.TabPages.Count > 1)
                    {
                        if (xtraTabControl1.TabPages[1].Text == "项目人员办理入场")
                        {

                            xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[1]);
                        }
                    }

                    XtraTabPage page = new XtraTabPage();
                    addWorker = new AddWorker(id,true);
                    addWorker.ShowProjectList += new AgainSubmit(a => GetIsClose(a));
                    addWorker.FormBorderStyle = FormBorderStyle.None;
                    addWorker.TopLevel = false;
                    page.Controls.Add(addWorker);
                    addWorker.Show();
                    page.Text = "项目人员办理入场";
                    xtraTabControl1.SelectedTabPage = page;
                    isOpen = true;
                    xtraTabControl1.TabPages.Add(page);

                }
                else
                {

                    var userId = row.organizationUserId;

                    userId = FormatHelper.GetToString(userId);

                    DialogResult result = XtraMessageBox.Show("确定要离场吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        AddWorerkSend baseSend = new AddWorerkSend
                        {
                            status = 3,
                            projectUuid = ConfigHelper.KtpLoginProjectId,
                            organizationUserUuid = id

                        };

                        IMulePusher addworkers = new SetWorkerProjectApi() { RequestParam = baseSend };
                        PushSummary pushAddworkers = addworkers.Push();
                        if (pushAddworkers.Success)
                        {
                            DeletePanelProjectUser(userId);
                            MessageHelper.Show($"{ name}离场成功");
                            GetWorkerList();
                        }
                        else
                        {
                            MessageHelper.Show("离场失败:" + pushAddworkers.Message);
                        }



                    }
                }

            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);

            }


        }
        public void GetIsClose(string state)
        {


            //判断是否已创建过
            foreach (XtraTabPage page1 in xtraTabControl1.TabPages)
            {
                if (page1.Text == state)
                {

                    GetIsOpen();
                    addWorker = null;
                    xtraTabControl1.SelectedTabPage = page1;//显示该页
                    xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[1]);
                    page1.Dispose();
                    break;
                }
            }

        }
        private void GetStateList()
        {
            List<DicKeyValueDto> list = new List<DicKeyValueDto>();
            list.Add(new DicKeyValueDto { Key = 0, Value = "全部" });
            list.Add(new DicKeyValueDto { Key = 1, Value = "未入场" });
            list.Add(new DicKeyValueDto { Key = 2, Value = "已入场" });
            list.Add(new DicKeyValueDto { Key = 3, Value = "已退场" });
            this.ComUsable.Properties.DisplayMember = "Value";
            this.ComUsable.Properties.ValueMember = "Key";
            this.ComUsable.EditValue = 0;
            this.ComUsable.Properties.DataSource = list;
            this.ComUsable.ItemIndex = 0;
            this.ComUsable.Properties.Columns.Add(
             new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value"));
            //是否显示列名

            ComUsable.Properties.ShowHeader = false;

            //是否显示底部

            //ComUsable.Properties.ShowFooter = false;

        }
        public void DeletePanelProjectUser(string userId)
        {

            //宇视产品
            foreach (WorkAddInfo device in WorkSysFail.workAdd)
            {

                IMulePusherYs PanelLibrarySet = new PanelWorkerDeleteApi() { API = "/PeopleLibraries/3/People/" + userId + $"?Lastchange={DateTime.Now.Ticks}", MethodType = Method.DELETE, PanelIp = device.deviceIp };
                PushSummarYs pushSummary = PanelLibrarySet.Push();
                PanelDeleteResult rr = pushSummary.ResponseData;
            }
        }
        public void GetIsOpen()
        {
            if (addWorker != null)
                addWorker.GetIsAVide();
        }
        private void grid_WorkerProject_CustomFilterDisplayText(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {

        }
        protected virtual RepositoryItemHyperLinkEdit CreateRepositoryItemHyperLinkEdit(string caption)
        {
            RepositoryItemHyperLinkEdit link = new RepositoryItemHyperLinkEdit();
            link.AutoHeight = false;
            link.TextEditStyle = TextEditStyles.Standard;
            link.ReadOnly = true;
            link.SingleClick = true;
            return link;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "xtraTabPage2")
            {
                GetWorkerList();
            }
        }
        public int BtnCurrentEdit()
        {

            if (addWorker != null)
                addWorker.CurrentManualEdit();
            else
                return 1;
            return 0;


        }

        private void ComEducationLevel_Properties_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetWorkerList();
        }

        /// <summary>
        /// 添加查看详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit4_Click(object sender, EventArgs e)
        {
            dynamic row = this.grid_WorkerProject.GetFocusedRow();
            string id = row.organizationUserUuid;
            string phone = row.phone;
            string state = row.status;
            string name = row.name;

        
                //if (xtraTabControl1.TabPages.Count > 1)
                //{
                //    if (xtraTabControl1.TabPages[1].Text == "项目人员办理入场")
                //    {

                //        xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[1]);
                //    }
                //}

                XtraTabPage page = new XtraTabPage();
                addWorker = new AddWorker(id,false);
                addWorker.ShowProjectList += new AgainSubmit(a => GetIsClose(a));
                addWorker.FormBorderStyle = FormBorderStyle.None;
                addWorker.TopLevel = false;
                page.Controls.Add(addWorker);
                addWorker.Show();
                page.Text = "项目人员详情";
                xtraTabControl1.SelectedTabPage = page;
                isOpen = true;
                xtraTabControl1.TabPages.Add(page);
            }
    }
}