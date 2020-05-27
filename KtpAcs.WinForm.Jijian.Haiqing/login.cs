using KtpAcs.Infrastructure.Exceptions;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcsAotoUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace KtpAcs.WinForm.Jijian
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {    // 定时间隔：1分钟
        int Seconds = 60;
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            ConfigHelper.KtpUploadNetWork = true;
            Thread thread = new Thread(CheckUpdateApplication);

            thread.IsBackground = false;
            thread.Start();
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private static void CheckUpdateApplication()
        {
            if (ConfigurationManager.AppSettings["IsAutoUpdater"] == "True")
            {


                Application.EnableVisualStyles();


                AutoUpdater au = new AutoUpdater();
                try
                {
                    au.Update();
                }
                catch (WebException exp)
                {
                    MessageBox.Show(String.Format("更新无法找到指定资源\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (XmlException exp)
                {
                    MessageBox.Show(String.Format("下载的升级文件有错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (NotSupportedException exp)
                {
                    MessageBox.Show(String.Format("升级地址配置错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentException exp)
                {
                    MessageBox.Show(String.Format("下载的升级文件有错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format("升级过程中发生错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {


            var loginBtnText = LoginBtn.Text;
            try
            {
                LoginBtn.Text = @"正在登录";
                LoginBtn.Enabled = false;
                FormErrorProvider.ClearErrors();
                var loginErroMsg = @"用户名或者验证码错误";
                if (string.IsNullOrEmpty(UserNameTxt.Text))
                {
                    loginErroMsg = "手机号不允许为空";
                    FormErrorProvider.SetError(UserNameTxt, loginErroMsg);
                    throw new PreValidationException(loginErroMsg);
                }
                if (string.IsNullOrEmpty(PasswordTxt.Text))
                {
                    loginErroMsg = "验证码不允许为空";
                    FormErrorProvider.SetError(PasswordTxt, loginErroMsg);
                    throw new PreValidationException(loginErroMsg);
                }

                IMulePusher pusherLogin = new LoginApi() { RequestParam = new { phone = UserNameTxt.Text, code = PasswordTxt.Text } };
                PushSummary pushLogin = pusherLogin.Push();
                if (!pushLogin.Success)
                {
                    MessageHelper.Show(pushLogin.Message);

                    //FormErrorProvider.SetError(PasswordTxt, loginErroMsg);
                    //throw new PreValidationException(loginErroMsg);

                    //ConfigHelper.KtpUploadNetWork = true;
                    return;
                }



                this.timer1.Stop();
                Hide();
                new Home().Show();
            }
            catch (PreValidationException ex)
            {
                MessageHelper.Show(ex.Message);
            }
            catch (Exception ex)
            {
                LogHelper.ExceptionLog(ex);
                MessageHelper.Show(ex.Message);
            }
            finally
            {
                LoginBtn.Text = loginBtnText;
                LoginBtn.Enabled = true;
            }
        }

        private void btnVerification_Click(object sender, EventArgs e)
        {
            try
            {
                //发送手机验证码
                IMulePusher phoneApi = new LoginVerificationApi() { RequestParam = new { phone = this.UserNameTxt.Text } };
                PushSummary pushSummary = phoneApi.Push();
                if (pushSummary.Success)
                {
                    btn_send.Enabled = false;
                    timer1.Enabled = true;
                }
                else
                {
                    MessageHelper.Show("验证码发送失败:" + pushSummary.Message);
                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                btn_send.Enabled = false;
                this.timer1.Interval = 1000;
                btn_send.Text = "倒计时:" + Seconds.ToString();
                if (Seconds == 0)
                {
                    //倒计时到“00”，计时器停止
                    this.timer1.Stop();
                    //去做其他事情
                    //......
                    btn_send.Enabled = true;
                    timer1.Enabled = false;
                    btn_send.Text = "重新发送验证码";
                    Seconds = 60;
                }
                else
                {
                    Seconds--;
                }
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }

        }

        private void picClose_EditValueChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }


        }
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }

        }

        private void panelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void panelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }

        }

        private void panelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
        }

        private void pictureEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void pictureEdit1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }

        }

        private void pictureEdit1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
        }

        private void PasswordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LoginBtn_Click(sender, e);//触发button事件
            }

        }
    }
}
