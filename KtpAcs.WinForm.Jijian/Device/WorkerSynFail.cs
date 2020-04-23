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

namespace KtpAcs.WinForm.Jijian.Device
{
    public partial class WorkerSynFail : DevExpress.XtraEditors.XtraForm
    {
        public WorkerSynFail()
        {
            InitializeComponent();
            GetSysFail();
        }
        public void GetSysFail()
        {

            BindingSource bingding = new BindingSource();
            bingding.DataSource = WorkSysFail.list;
            bingding.ResetBindings(true);
            bingding.CurrencyManager.Refresh();
            this.gridControl.DataSource = null;
            this.gridControl.DataSource = bingding;//绑定数据源 

        }
    }
}