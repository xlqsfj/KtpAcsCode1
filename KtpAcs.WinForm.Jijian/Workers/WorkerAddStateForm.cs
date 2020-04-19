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
using KtpAcs.KtpApiService.Model;
using System.Threading;
using KtpAcs.Infrastructure.Utilities;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerAddStateForm : DevExpress.XtraEditors.XtraForm
    {
        //声明委托重新提交
        public delegate void AgainSubmit(string close);
        //声明事件
        public event AgainSubmit ShowSubmit;
        //添加是否成功
        private bool isSubSuccess = true;
        private string _name = "";
        private string _idCard = "";
        private int addCount = 0;
        //启动更新线程
        Thread myThread;
        public WorkerAddStateForm()
        {
            InitializeComponent();
        }
        public WorkerAddStateForm(string name, string idCard)
        {
            this._name = name;
            this._idCard = idCard;

            InitializeComponent();
            this.Text = name + "提交中";

            WorkSysFail.workAdd.ForEach(w => w.magAdd = "添加中..");

            this.gridControl1.DataSource = WorkSysFail.workAdd;



        }

        private void WorkerAddStateForm_Load(object sender, EventArgs e)
        {
            addCount = 0;
            //
            //myThread = new Thread(startFillDv);//实例化线程
            //myThread.IsBackground = true;
            //myThread.Start();

        }
        private void startFillDv()
        {

            while (true)
            {
                Dictionary<string, string> d = WorkSysFail.dicAddMag;
                lock (this)
                {


                    if (WorkSysFail.dicWorkadd.Count > 0)
                    {


                        var dicWAddImg = WorkSysFail.dicWorkadd.LastOrDefault();
                        var mag = dicWAddImg.Value;
                        if (!dicWAddImg.Key)
                        { //请求云端出错
                            isSubSuccess = false;
                            //skin_retry.Visible = true;
                            //skinlable_addworkImg.ForeColor = Color.Red;
                            //panel1.Visible = true;
                            //skingrid_sysPanel.Visible = false;
                         
                            //skin_close.Enabled = true;
                        }
                        else
                        { //成功

                            //panel1.Visible = false;
                            //if (ConfigHelper.IsDivceAdd)
                            //{
                            //    skingrid_sysPanel.Dock = DockStyle.Fill;
                            //    skingrid_sysPanel.Visible = true;
                            //    label2.Visible = false;
                            //}
                            //else
                            //{

                            //    label2.Visible = true;
                            //    label2.Text = "保存成功";

                            //    skingrid_sysPanel.Visible = false;
                            //}
                            isSubSuccess = true;
                        }
                       // skinlable_addworkImg.Text = mag;

                        //WorkSysFail.dicWorkadd.Clear();
                        WorkSysFail.dicWorkadd.Reverse();
                    }

                }

              
                    if (isSubSuccess)
                    {
                        btnClose.Text = "关 闭";
                        myThread.Abort();
                    }
                    else
                    {
                        myThread.Abort();
                        btnClose.Text = "返 回 编 辑";
                     
                    }

                }



            }

        }
    }
