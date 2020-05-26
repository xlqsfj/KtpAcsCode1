using AForge.Imaging.Filters;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using KtpAcs.Infrastructure.Utilities;
using KtpAcs.KtpApiService;
using KtpAcs.KtpApiService.Result;
using KtpAcs.KtpApiService.Send;
using KtpAcs.KtpApiService.Worker;
using KtpAcs.WinForm.Jijian.Device;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerListForm : DevExpress.XtraEditors.XtraForm
    {
        private int _isHmc = 0;

        private int _addState = 2;

        //声明事件用于显示详细页面
        public event Action<DevExpress.XtraEditors.XtraForm,bool,string> ShowDetail;
        public WorkerListForm(int isHmc = 0)
        {
            _isHmc = isHmc;
            InitializeComponent();
            GetStateList();
            GetWorkerList("");
            InitGridPagingNavigatorControl();
        }
        /// <summary>
        ///     分页控件翻页事件绑定
        /// </summary>
        private void InitGridPagingNavigatorControl()
        {
            WorkersGridPager.PagingHandler = GridPagingNavigatorControlPagingEvent;
        }

        /// <summary>
        ///     分页控件翻页事件
        /// </summary>
        public void GridPagingNavigatorControlPagingEvent()
        {
            //不是点击搜索的情况下

            GetWorkerList("");
        }
        public void GetWorkerList(string Query = "")
        {

            try
            {
                var pageSize = WorkersGridPager.PageSize;
                var pageIndex = WorkersGridPager.PageIndex;

                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = pageSize,
                    pageNum = pageIndex,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    status = (int)this.ComUsable.EditValue,
                    keyWord = this.txtQuery.Text,
                    designatedFlag = _isHmc == 0 ? false : true

                };
            
                IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {

                 
                    WorkerListResult.Data data = push.ResponseData;
                    WorkersGridPager.PageCount = (data.total + pageSize - 1) / pageSize;
                    this.gridControl1.DataSource = data.list;
                }
              
          }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }
   
       
        private void btnQuery_Click(object sender, EventArgs e)
        {
            WorkersGridPager.PageIndex = 1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtQuery.Text = "";
            this.ComUsable.ItemIndex = 1;
            WorkersGridPager.PageIndex = 1;
        }

      

        /// <summary>
        /// 打开详情页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                dynamic row = this.gridView1.GetFocusedRow();
                string userUuid = row.userUuid;
                string userName = row.name;
                AddWorker addWorker = new AddWorker(userUuid, _isHmc, false);
                addWorker.CloseDdetailedWinform += new Action<DevExpress.XtraEditors.XtraForm, bool, string>(ShowDetail);
                //addWorker.StartPosition = FormStartPosition.CenterParent;
                //addWorker.Show();
                ShowDetail(addWorker, false, userName);
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }


        }

        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                dynamic row = this.gridView1.GetFocusedRow();
                string userUuid = row.userUuid;
                string userName = row.name;
                AddWorker addWorker = new AddWorker(userUuid, _isHmc, true);
                addWorker.CloseDdetailedWinform += new Action<DevExpress.XtraEditors.XtraForm, bool, string>(ShowDetail);
                //addWorker.StartPosition = FormStartPosition.CenterParent;
                //addWorker.Show();
                ShowDetail(addWorker, true, userName);
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {

            try
            {

                if (e.Button == MouseButtons.Right)
                {

                    //this.popupMenu2.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                    //    popupMenu1.ShowPopup(Control.MousePosition);
                    this.popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Show(ex.Message, ex);
            }
        }

        private void WorkerListForm_Load(object sender, EventArgs e)
        {
       
        }

        private void GetStateList()
        {
            List<DicKeyValueDto> list = new List<DicKeyValueDto>();
            list.Add(new DicKeyValueDto { Key = 1, Value = "待入场" });
            list.Add(new DicKeyValueDto { Key = 2, Value = "已入场" });
            list.Add(new DicKeyValueDto { Key = 3, Value = "已退场" });
            this.ComUsable.Properties.DisplayMember = "Value";
            this.ComUsable.Properties.ValueMember = "Key";
            this.ComUsable.EditValue =2;
            this.ComUsable.Properties.DataSource = list;
            this.ComUsable.ItemIndex = 1;
            this.ComUsable.Properties.Columns.Add(
             new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value"));
            //是否显示列名

            ComUsable.Properties.ShowHeader = false;

            //是否显示底部

            //ComUsable.Properties.ShowFooter = false;
          
        }

        private void ComEducationLevel_Properties_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void ComUsable_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _addState = (int)ComUsable.EditValue;
                WorkersGridPager.PageIndex = 1;
                if (_addState != 2)
                    btnUpdate.Enabled = false;
                else
                    btnUpdate.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WorkersGridPager.PageIndex = 1;
            }

        }
    }
}