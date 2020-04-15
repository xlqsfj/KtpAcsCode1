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
                this.labProjectManageNum.Text = projectCountResult.projectManageNum.ToString();
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

            this.label1.ForeColor = Color.White;
            this.flowDevice.BackColor = Color.Transparent;
            this.flowDevice.BackgroundImage = Image.FromFile(fPath("blue_03.png"));
            DeviceListForm addStep = new DeviceListForm();
            addStep.FormBorderStyle = FormBorderStyle.None;

            addStep.TopLevel = false;
            this.panelContent.Controls.Clear();
            this.panelWorker.Visible = false;
            this.panelDevice.Visible = true;
            this.panelContent.Controls.Add(addStep);
            addStep.Show();
        }
        /// <summary>
        /// 文件路径方法
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回文件的在整个项目中得位置</returns>
        private string fPath(string fileName)
        {
            string SysPath = Application.StartupPath + @"../../../";
            Directory.SetCurrentDirectory(SysPath);
            string filePath = Directory.GetCurrentDirectory() + @"/image/" + fileName;
            return filePath;
        }

        /// <summary>
        /// 点击劳务管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flowWorerk_Click(object sender, EventArgs e)
        {

            this.flowWorerk.BackColor = Color.Transparent;
            this.flowDevice.BackgroundImage = null;
            this.flowWorerk.BackgroundImage = Image.FromFile(fPath("blue_03.png"));
            WorkerAdminForm addStep = new WorkerAdminForm();
            addStep.FormBorderStyle = FormBorderStyle.None;
            addStep.TopLevel = false;
            this.panelContent.Controls.Clear();
            this.panelWorker.Visible = true;
            this.panelDevice.Visible = false;
            this.panelContent.Controls.Add(addStep);
            addStep.Show();
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

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {

        }

        private void radHMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddWorker addStep;
            if (radHMC.SelectedIndex == 0)
            {//花名册
                addStep = new AddWorker(0);

            }
            else
            {
                //甲子分包
                addStep = new AddWorker(1);
            }
            addStep.FormBorderStyle = FormBorderStyle.None;
            addStep.TopLevel = false;
            this.panelContent.Controls.Clear();
            addStep.Show();
            this.panelContent.Controls.Add(addStep);

        }

        private void flowAdmin_Click(object sender, EventArgs e)
        {
            this.flowAdmin.BackColor = Color.Transparent;


            this.flowDevice.BackgroundImage = null;
            this.flowAdmin.BackgroundImage = Image.FromFile(fPath("blue_03.png"));
            WorkerProjectForm addStep = new WorkerProjectForm();
            addStep.FormBorderStyle = FormBorderStyle.None;
            addStep.TopLevel = false;
            this.panelContent.Controls.Clear();
            this.panelWorker.Visible = false;
            this.panelDevice.Visible = false;
            this.panelContent.Controls.Add(addStep);
            addStep.Show();
        }
    }
}