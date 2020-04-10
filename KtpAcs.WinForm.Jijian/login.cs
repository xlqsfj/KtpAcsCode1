using KtpAcs.Infrastructure.Exceptions;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
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

                    FormErrorProvider.SetError(PasswordTxt, loginErroMsg);
                    throw new PreValidationException(loginErroMsg);

                    //ConfigHelper.KtpUploadNetWork = true;
                    return;
                }

                
             

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
