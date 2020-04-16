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
    }
}