using CCWin;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService.Model;
using KtpAcs.WinForm.Jijian.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerAddStateForm : DevExpress.XtraEditors.XtraForm
    {  //启动更新线程
        Thread myThread;
     
        //声明事件
        public event AgainSubmit ShowSubmit;
        //添加是否成功
        private bool isSubSuccess = true;
        private bool isFinish = true;
        private string _name = "";
        private string _idCard = "";
        private int addSum = 0; //判断是否结束添加
        private int addCount = 0;
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        public WorkerAddStateForm(string name, string idCard)
        {
            this._name = name;
            this._idCard = idCard;

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.Text = name + "提交中";
            this.skingrid_sysPanel.AutoGenerateColumns = false;//不自动 
            WorkSysFail.workAdd.ForEach(w => w.magAdd = "添加中..");

            skingrid_sysPanel.DataSource = WorkSysFail.workAdd;
            if (WorkSysFail.workAdd.Count() < 1)
            {//没有连接成功的面板
                skingrid_sysPanel.Visible = false;
                panel1.Visible = true;
                skin_close.Enabled = true;
            }

            skingrid_sysPanel.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            skingrid_sysPanel.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private delegate void myDelegate(Dictionary<string, string> dicAddMag);//定义委托
        //不断更新UI



        private void startFillDv()
        {

            while (isFinish)
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
                            skin_retry.Visible = true;
                            skinlable_addworkImg.ForeColor = Color.Red;
                            panel1.Visible = true;
                            skingrid_sysPanel.Visible = false;

                            skin_close.Enabled = true;
                            isFinish = false;
                            skinlable_addworkImg.Text = mag;
                        }
                        else
                        { //成功

                            panel1.Visible = false;
                            skingrid_sysPanel.Dock = DockStyle.Fill;
                            //this.Height = 300;
                            skingrid_sysPanel.Visible = true;

                            skinlable_addworkImg.Text = mag;
                         
                        }
                        //WorkSysFail.dicWorkadd.Clear();
                        WorkSysFail.dicWorkadd.Reverse();

                    }



                }


                if (ConfigHelper.IsDivceAdd)
                    Grid(d);
                else
                {
                    if (isSubSuccess)
                    {
                        skin_close.Text = "关 闭";
                        myThread.Abort();
                    }
                    else
                    {
                        myThread.Abort();
                        skin_close.Text = "返 回 编 辑";
                        skin_close.Enabled = true;
                    }

                }

            }

        }
        //更新UI
        private void Grid(Dictionary<string, string> dicAddMag)
        {
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new myDelegate(Grid), new object[] { dicAddMag });
            //}
            //else
            //{

            try
            {

                if (dicAddMag.Count() > 0)
                {
                    //修改改id对应的行
                    for (int i = 0; i < this.skingrid_sysPanel.Rows.Count; i++)
                    {
                        var keyIp = dicAddMag.FirstOrDefault();

                        if (this.skingrid_sysPanel.Rows[i].Cells[0].Value.Equals(keyIp.Key))
                        {
                            if (keyIp.Value != "添加成功")
                            {
                                isSubSuccess = false;

                                this.skingrid_sysPanel.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                                skin_close.Enabled = true;
                            }
                            else
                            {
                                DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                                txtcell.Value = "已成功";
                                skingrid_sysPanel.Rows[i].Cells[3] = txtcell;

                            }
                            addCount++;
                            this.skingrid_sysPanel.Rows[i].Cells[2].Value = keyIp.Value;


                            dicAddMag.Remove(keyIp.Key);
                            if (addCount == this.skingrid_sysPanel.Rows.Count)
                            {
                                //结束添加
                                skin_close.Enabled = true;
                                isFinish = false;
                                //isSubSuccess = true;



                            }
                        }
                    }

                    if (isSubSuccess)
                    {
                        skin_close.Text = "关 闭";
                    }
                    else
                    {
                        skin_close.Text = "返 回 编 辑";
                        skin_close.Enabled = true;
                    }
                  //  myThread.Abort();
                    Thread.Sleep(1000);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            // }

        }
        //  }


        /// <summary>
        ///关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skin_close_Click(object sender, EventArgs e)
        {
            myThread.Abort();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
Color.DimGray, 1, ButtonBorderStyle.Dashed, //左边
　　　　　Color.DimGray, 1, ButtonBorderStyle.Dashed, //上边
　　　　　Color.DimGray, 1, ButtonBorderStyle.Dashed, //右边
　　　　　Color.DimGray, 1, ButtonBorderStyle.Dashed);//底边
        }

        /// <summary>
        /// 重试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skin_retry_Click(object sender, EventArgs e)
        {

            WorkSysFail.dicAddMag.Clear();
            WorkSysFail.dicWorkadd.Clear();
            skinlable_addworkImg.Text = "添加中";


            //刷新时，放在需要执行刷新的事件里

            if (ShowSubmit != null)
            {

                if (skin_retry.Text == "继续添加")
                    ShowSubmit("3");
                else
                    ShowSubmit("");
            }

            if (WorkSysFail.dicWorkadd.Count > 0)
            {

                var dicWAddImg = WorkSysFail.dicWorkadd.LastOrDefault();
                if (!dicWAddImg.Key)
                {
                    isSubSuccess = false;
                    //请求云端出错
                    skin_retry.Visible = false;
                    skinlable_addworkImg.ForeColor = Color.Red;

                }
                skinlable_addworkImg.Text = dicWAddImg.Value;

            }
        }
        /// <summary>
        /// grid点击按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skingrid_sysPanel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string value = this.skingrid_sysPanel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //点击button按钮事件
                if (skingrid_sysPanel.Columns[e.ColumnIndex].Name == "btnReason" && e.RowIndex >= 0 && !value.Equals("已成功"))
                {
                    WorkSysFail.dicAddMag.Clear();
                    //说明点击的列是DataGridViewButtonColumn列
                    DataGridViewColumn column = skingrid_sysPanel.Columns[e.ColumnIndex];
                    WorkSysFail.dicAddMag.Clear();
                    WorkSysFail.dicWorkadd.Clear();
                    //  skinlable_addworkImg.Text = "添加中";
                    //刷新时，放在需要执行刷新的事件里
                    if (ShowSubmit != null) ShowSubmit("");
                }
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerAddState_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void WorkerAddStateForm_Load(object sender, EventArgs e)
        {
            addCount = 0;
            myThread = new Thread(startFillDv);//实例化线程
            myThread.IsBackground = false;
            myThread.Start();
        }

        private void WorkerAddStateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isSubSuccess)
                ShowSubmit("close");
            else
                ShowSubmit("error");
            myThread.Abort();
        }
    }

}
