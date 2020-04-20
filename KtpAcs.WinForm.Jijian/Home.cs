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
using KtpAcs.WinForm.Jijian.Device;
using DevExpress.Utils.Extensions;
using System.IO;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Project;
using static KtpAcs.KtpApiService.Result.ProjectListResult;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.WinForm.Jijian.Workers;

namespace KtpAcs.WinForm.Jijian
{
    public partial class Home : DevExpress.XtraEditors.XtraForm
    {

        DeviceListForm _DeivceForm = null; //闸机页面
        WorkerAdminForm _workerAdminForm = null;//工人管理 
        WorkerProjectForm _workerProjectForm = null;//项目管理
        public Home()
        {
            InitializeComponent();
            GetProjectList();
            GetProjectCount();
            flowDevice_Click(null, null);
        }


        /// <summary>
        ///查询项目列表
        /// </summary>
        private void GetProjectList()
        {


            IMulePusher pusherLogin = new GetProjectListApi() { RequestParam = new { pageNum = 0, pageSize = 0, type = 0 } };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                List<ProjectList> pList = pushLogin.ResponseData;
                if (pList.Count < 1)
                {

                    MessageHelper.Show("该账号未添加项目,请在后台添加在继续操作");
                    return;

                }
                this.comProjectList.Properties.DisplayMember = "projectName";
                this.comProjectList.Properties.ValueMember = "projectUuid";
                this.comProjectList.Properties.DataSource = pList;
                this.comProjectList.EditValue = pList[0].projectUuid;
                ConfigHelper._KtpLoginProjectId = pList[0].projectUuid;

            }
        }
        /// <summary>
        /// 查询项目人数
        /// </summary>
        private void GetProjectCount()
        {
            IMulePusher pusherLogin = new GetProjectCountApi() { RequestParam = new { projectUuid = ConfigHelper.KtpLoginProjectId } };
            PushSummary pushLogin = pusherLogin.Push();
            if (pushLogin.Success)
            {
                ProjectCountResult.Data projectCountResult = pushLogin.ResponseData;

                this.labProjectCode.Text = projectCountResult.projectCode;
                this.labProjectManageNum.Text = (projectCountResult.projectWorkerNum + projectCountResult.jiaziNum + projectCountResult.projectManageNum).ToString();
                this.labProjectManageNum.ToolTip = $"花名册:{ projectCountResult.projectWorkerNum} 甲指分包人员:{ projectCountResult.jiaziNum} 项目人员:{projectCountResult.projectManageNum}";
                this.labVerificationNum.Text = projectCountResult.workerVerificationNum.ToString();


            }
        }


        private void pictureEdit1_MouseEnter(object sender, EventArgs e)
        {
            this.flowAdmin.Visible = true;
            this.label1.ForeColor = Color.White;
            this.flowAdmin.BackColor = Color.GhostWhite;
        }



        private void picExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void flowDevice_Click(object sender, EventArgs e)
        {
            this.panelWorker.Visible = false;
            this.panelDevice.Visible = true;
            _DeivceForm = new DeviceListForm();
            TabForm(_DeivceForm, flowDevice.Name);

        }

        public void TabForm(DevExpress.XtraEditors.XtraForm form, string cName)
        {



            foreach (Control c in spl.Panel1.Controls)
            {
                if (c is FlowLayoutPanel && c.Name.Contains(cName))
                {
                    c.BackColor = Color.Transparent;
                    c.BackgroundImage = Jijian.Properties.Resources.blue_03;
                }
                else if (c is FlowLayoutPanel)
                {

                    c.BackgroundImage = null;
                }

            }
            if (_workerProjectForm != null)
            {
                if (_workerProjectForm.isOpen)
                {

                    _workerProjectForm.GetIsOpen();
                }

            }
            if (form.Name != "WorkerAdminForm")
            {
                if (_workerAdminForm != null)
                    _workerAdminForm.isExiet();
            }

            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false;
            this.panelContent.Controls.Clear();
            this.panelContent.Controls.Add(form);
            form.Show();
        }



        /// <summary>
        /// 点击劳务管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flowWorerk_Click(object sender, EventArgs e)
        {
            _DeivceForm = null;
            this.panelWorker.Visible = true;
            this.panelDevice.Visible = false;
            _workerAdminForm = new WorkerAdminForm();
            TabForm(_workerAdminForm, flowWorerk.Name);


        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            AddDevice addDevice = new AddDevice();
            addDevice.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AddDevice addDevice = new AddDevice();
            addDevice.ShowDialog();
        }



        private void radHMC_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (_workerAdminForm != null)
                _workerAdminForm.isExiet();

            if (radHMC.SelectedIndex == 0)
            {//花名册
                _workerAdminForm = new WorkerAdminForm(0);

            }
            else
            {
                //甲子分包
                _workerAdminForm = new WorkerAdminForm(1);
            }
            this.panelContent.Controls.Clear();
            _workerAdminForm.FormBorderStyle = FormBorderStyle.None;
            _workerAdminForm.TopLevel = false;
            _workerAdminForm.Show();
            this.panelContent.Controls.Add(_workerAdminForm);

        }

        private void flowAdmin_Click(object sender, EventArgs e)
        {

            _workerProjectForm = new WorkerProjectForm();
            this.panelWorker.Visible = false;
            this.panelDevice.Visible = false;
            TabForm(_workerProjectForm, flowAdmin.Name);

        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetProjectCount();

            if (_DeivceForm != null)
            {
                _DeivceForm.GetDevice();
            }
        }

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyn_Click(object sender, EventArgs e)
        {

            _DeivceForm.SysWorkerToPanel();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void flowDevice_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 点击项目下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comProjectList_EditValueChanged(object sender, EventArgs e)
        {
            ConfigHelper.KtpLoginProjectId = this.comProjectList.EditValue.ToString();
            GetProjectCount();

            if (_DeivceForm != null)
            {
                _DeivceForm.GetDevice();
            }
            flowDevice_Click(null, null);
        }

        private void picExit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}