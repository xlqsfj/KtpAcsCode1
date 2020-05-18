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
using System.Runtime.InteropServices;
using System.Configuration;
using System.Reflection;
using System.Net;

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
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            GetIp();
            GetProjectList();
            GetProjectCount();
            flowDevice_Click(null, null);
        }

        public void GetIp()
        {

            string hostName = Dns.GetHostName();//本机名              
            System.Net.IPAddress[] ipHost = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6 
            foreach (IPAddress ip in ipHost)

            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    this.Text = "开太平云建筑      本地IP:" + ip.ToString();
                }

            }

        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                if (m.WParam.ToInt32() == 61539 || m.WParam.ToInt32() == 61587)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }


        /// <summary>
        ///查询项目列表
        /// </summary>
        private void GetProjectList()
        {
            try
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
                    string currentProject = SetProjectId(pList);
                    this.comProjectList.EditValue = currentProject;


                    this.comProjectList.Properties.Columns.Add(
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("organizationName", "公司名称"));
                    this.comProjectList.Properties.Columns.Add(
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("projectName", "项目名称"));

                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }
        /// <summary>
        /// 设置项目选中的项目
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        public string SetProjectId(List<ProjectList> pList)
        {
            bool isMatching = false;
            string pid = ConfigHelper.ProjectId;

            foreach (var item in pList)
            {
                if (item == null)
                    continue;
                if (item.projectUuid == pid)
                {
                    isMatching = true;
                    this.comProjectList.ToolTip = item.projectName;
                }


            }
            if (!isMatching)
            {

                foreach (var item in pList)
                {
                    if (item == null)
                        continue;
                    pid = item.projectUuid;
                    modifyItem("ProjectId", pid);
                    this.comProjectList.ToolTip = item.projectName;
                    break;

                }

            }
            ConfigHelper._KtpLoginProjectId = pid;
            return pid;
        }
        /// <summary>
        /// 查询项目人数
        /// </summary>
        private void GetProjectCount()
        {
            try
            {

                IMulePusher pusherLogin = new GetProjectCountApi() { RequestParam = new { projectUuid = ConfigHelper.KtpLoginProjectId } };
                PushSummary pushLogin = pusherLogin.Push();
                if (pushLogin.Success)
                {
                    ProjectCountResult.Data projectCountResult = pushLogin.ResponseData;

                    this.labProjectCode.Text = projectCountResult.projectCode;
                    //总人数
                    int countSum = (projectCountResult.projectWorkerNum + projectCountResult.jiaziNum + projectCountResult.projectManageNum);
                    this.labProjectManageNum.Text = countSum.ToString();
                    this.labProjectManageNum.ToolTip = $"花名册:{ projectCountResult.projectWorkerNum} 甲指分包人员:{ projectCountResult.jiaziNum} 项目人员:{projectCountResult.projectManageNum}";
                    //已认证人数
                    int okNum = projectCountResult.workerVerificationNum + projectCountResult.manageVerificationNum + projectCountResult.jiaziVerificationNum;
                    this.labVerificationNum.Text = okNum.ToString();
                    this.labPhone.Text = ConfigHelper.KtpLoginPhone;
                    //未认证人数
                    int noNum = countSum - okNum;
                    this.labwei.Text = noNum.ToString();

                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }





        private void picExit_Click(object sender, EventArgs e)
        {
            if (_workerAdminForm != null)
                _workerAdminForm.isExiet();
            if (_workerProjectForm != null)
            {
                if (_workerProjectForm.isOpen)
                {

                    _workerProjectForm.GetIsOpen();
                }

            }
            Application.Exit();
        }

        public void modifyItem(string keyName, string newKeyValue)
        {    //修改配置文件中键为keyName的项的值   

            //读取程序集的配置文件
            string assemblyConfigFile = Assembly.GetEntryAssembly().Location;
            string appDomainConfigFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //获取appSettings节点
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");

            //删除name，然后添加新值
            appSettings.Settings.Remove(keyName);
            appSettings.Settings.Add(keyName, newKeyValue);
            //保存配置文件
            config.Save();
        }
        /// <summary>
        /// 面板管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flowDevice_Click(object sender, EventArgs e)
        {
            try
            {
                this.panelWorker.Visible = false;
                this.panelDevice.Visible = true;
                _DeivceForm = new DeviceListForm();
                TabForm(_DeivceForm, flowDevice.Name);
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }

        }


        /// <summary>
        /// 循环tab切换
        /// </summary>
        /// <param name="form"></param>
        /// <param name="cName"></param>

        public void TabForm(DevExpress.XtraEditors.XtraForm form, string cName)
        {



            foreach (Control c in spl.Panel1.Controls)
            {
                if (c is FlowLayoutPanel && c.Name.Contains(cName))
                {
                    c.BackColor = Color.Transparent;
                    c.BackgroundImage = Jijian.Properties.Resources.blue_03;
                    c.BackgroundImageLayout = ImageLayout.Stretch;
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
                    _workerProjectForm = null;
                }

            }

            if (form.Name != "WorkerAdminForm")
            {
                if (_workerAdminForm != null)
                    _workerAdminForm.isExiet();
                _workerAdminForm = null;
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
            try
            {
                _DeivceForm = null;
                this.panelWorker.Visible = true;
                this.panelDevice.Visible = false;
                if (_workerAdminForm != null)
                    _workerAdminForm.isExiet();
                _workerAdminForm = new WorkerAdminForm(radHMC.SelectedIndex);
                TabForm(_workerAdminForm, flowWorerk.Name);

            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }

        }



        /// <summary>
        /// 花名册和甲指分包切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 管理员管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flowAdmin_Click(object sender, EventArgs e)
        {
            try
            {

                _workerProjectForm = new WorkerProjectForm();
                this.panelWorker.Visible = false;
                this.panelDevice.Visible = false;
                TabForm(_workerProjectForm, flowAdmin.Name);
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
         
            }

        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                GetProjectCount();

                if (_DeivceForm != null)
                {
                    _DeivceForm.GetDevice();
                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
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
        /// <summary>
        /// 点击项目下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comProjectList_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ProjectId = this.comProjectList.EditValue.ToString();
                if (this.comProjectList.Text != "[EditValue is null]")
                    this.comProjectList.ToolTip = this.comProjectList.Text;
                ConfigHelper.KtpLoginProjectId = ProjectId;
                modifyItem("ProjectId", ProjectId);
                GetProjectCount();

                flowDevice_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
            }
        }


        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            try
            {
                AddDevice addDevice = new AddDevice();
                addDevice.ShowDialog();
                if (_DeivceForm != null)
                {
                    _DeivceForm.GetDevice();
                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                if (_workerAdminForm != null)
                    _workerAdminForm.isExiet();
                if (_workerProjectForm != null)
                {
                    if (_workerProjectForm.isOpen)
                    {

                        _workerProjectForm.GetIsOpen();
                    }

                }
                Application.Exit();
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }



        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureEdit8_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //如果是单击的是左键
                if (e.Button == MouseButtons.Left)
                {
                    //  popupMenu1.ShowPopup((Button)sender, new Point(e.X, e.Y)); //在你单击的地方弹出菜单
                    this.popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }

            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }

        /// <summary>
        /// 是否编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsEdit_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = 0;
            if (!btnIsEdit.Checked)
                return;
            try
            {
                if (_workerAdminForm != null)
                {
                    i = _workerAdminForm.BtnCurrentEdit();
                }
                else if (_workerProjectForm != null)
                {
                    i = _workerProjectForm.BtnCurrentEdit();
                }
                else
                {
                    i = 1;

                }
                if (i > 0)
                {
                    MessageHelper.Show("是否手动编辑只有当前录入页面有效");

                }
                btnIsEdit.Checked = false;
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);

            }
        }


        /// <summary>
        /// 调用日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\logs";
                if (Directory.Exists(path))
                    System.Diagnostics.Process.Start(path);
                else
                    MessageHelper.Show("日志目录还未生成！");
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }

        private void comProjectList_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }
    }
}