
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using KtpAcs.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Base
{

    internal class PreValidationHelper
    {
        public static string ErroMsg { get; set; } = "输入信息验证失败，请按感叹号提示输入完整信息";

        public static void InitPreValidation(DXErrorProvider errorProvider)
        {
            errorProvider.ClearErrors();
            ErroMsg = "输入信息验证失败，请按感叹号提示输入完整信息";
        }

        public static void MustNotBeNullOrEmpty(
            DXErrorProvider errorProvider, TextEdit textBox, string msg, ref bool result)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                errorProvider.SetError(textBox, msg);
                result = false;
            }
        }

        public static void MustNotBeNull(
            DXErrorProvider errorProvider, TextEdit comboBox, string msg, ref bool result)
        {
            if (comboBox.EditValue == null || comboBox.EditValue.ToString() == string.Empty)
            {
                errorProvider.SetError(comboBox, msg);
                result = false;
            }
        }

        public static void MustChecked(
            DXErrorProvider errorProvider, TextEdit comboBox, string msg, ref bool result)
        {
            if (comboBox.EditValue == null || comboBox.EditValue.ToString() == string.Empty)
            {
                errorProvider.SetError(comboBox, msg);
                result = false;
            }
        }

        public static void IsMail(
            DXErrorProvider errorProvider, TextEdit textBox, string msg, ref bool result)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return;
            }
            if (!Infrastructure.Exceptions.ValidationHelper.IsMail(textBox.Text))
            {
                errorProvider.SetError(textBox, msg);
                result = false;
            }
        }

        public static void IsMobile(
            DXErrorProvider errorProvider, TextEdit textBox, string msg, ref bool result)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return;
            }
            if (!Infrastructure.Exceptions.ValidationHelper.IsMobile(textBox.Text))
            {
                errorProvider.SetError(textBox, msg);
                result = false;
            }
        }

        public static void IsIpAddress(
            DXErrorProvider errorProvider, TextEdit textBox, string msg, ref bool result)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return;
            }
            if (!Infrastructure.Exceptions.ValidationHelper.IsIpAddress(textBox.Text))
            {
                errorProvider.SetError(textBox, msg, ErrorType.Warning);
                result = false;
            }
        }

        public static void IsIdCard(
            DXErrorProvider errorProvider, TextEdit textBox, string msg, ref bool result)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return;
            }
            if (!Infrastructure.Exceptions.ValidationHelper.IsIdCard(textBox.Text))
            {
                errorProvider.SetError(textBox, msg);
                result = false;
            }
        }
    }
    }