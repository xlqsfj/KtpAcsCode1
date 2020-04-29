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

namespace KtpAcs.WinForm.Jijian.Device
{
    public partial class FormLoad : DevExpress.XtraEditors.XtraForm
    {

        public FormLoad()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public FormLoad(string showMag)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            CheckForIllegalCrossThreadCalls = false;

          //this.lbl_tips.Text = showMag;
        }

        /// <summary>
        /// 关闭命令
        /// </summary>
        public void closeOrder()
        {
            if (this.InvokeRequired)
            {
                //这里利用委托进行窗体的操作，避免跨线程调用时抛异常，后面给出具体定义
                CONSTANTDEFINE.SetUISomeInfo UIinfo = new CONSTANTDEFINE.SetUISomeInfo(new Action(() =>
                {
                    while (!this.IsHandleCreated)
                    {
                        ;
                    }
                    if (this.IsDisposed)
                        return;
                    if (!this.IsDisposed)
                    {
                        this.Close();
                        this.Dispose();
                    }

                }));
                this.Invoke(UIinfo);
            }
            else
            {
                if (this.IsDisposed)
                    return;
                if (!this.IsDisposed)
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }



        private void FormLoad_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FormLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.Dispose(true);
            }
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
            //设置一些Loading窗体信息
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
      
        }
        public delegate DialogResult InvokeDelegate(Form parent);
        public DialogResult xShowDialog(Form parent)
        {
            if (parent.InvokeRequired)
            {
                InvokeDelegate xShow = new InvokeDelegate(xShowDialog);
                parent.Invoke(xShow, new object[] { parent });
                return DialogResult;
            }

            return this.ShowDialog(parent);
        }
    }

}