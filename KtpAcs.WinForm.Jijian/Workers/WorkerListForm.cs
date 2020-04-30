﻿using AForge.Imaging.Filters;
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
using System.Linq;
using System.Windows.Forms;

namespace KtpAcs.WinForm.Jijian.Workers
{
    public partial class WorkerListForm : DevExpress.XtraEditors.XtraForm
    {
        private int _isHmc = 0;

        //声明事件用于显示详细页面
        public event Action<DevExpress.XtraEditors.XtraForm> ShowDetail;
        public WorkerListForm(int isHmc = 0)
        {
            _isHmc = isHmc;
            InitializeComponent();
            GetWorkerList("");
        }

        public void GetWorkerList(string Query = "")
        {

            try
            {
                WorkerSend workerSend = new WorkerSend()
                {

                    pageSize = 20,
                    projectUuid = ConfigHelper.KtpLoginProjectId,
                    pageNum = 1,
                    status = 2,
                    keyWord = Query,
                    designatedFlag = _isHmc == 0 ? false : true

                };

                IMulePusher pusherDevice = new GetWorkersApi() { RequestParam = workerSend };
                PushSummary push = pusherDevice.Push();
                if (push.Success)
                {


                    WorkerListResult.Data data = push.ResponseData;
                    this.gridControl1.DataSource = data.list;
                }
              
          }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"错误信息:{0}", ex.Message);

            }



        }
        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            dynamic row = this.gridView1.GetFocusedRow();
            string uuid = row.uuid;
            AddWorker addWorker = new AddWorker(uuid, _isHmc, false);
            addWorker.StartPosition = FormStartPosition.CenterParent;
            addWorker.Show();
        }
        protected virtual RepositoryItemHyperLinkEdit CreateRepositoryItemHyperLinkEdit(string caption)
        {
            RepositoryItemHyperLinkEdit link = new RepositoryItemHyperLinkEdit();
            link.AutoHeight = false;
            link.TextEditStyle = TextEditStyles.Standard;
            link.ReadOnly = true;
            link.SingleClick = true;
            link.Caption = caption;
            return link;
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetWorkerList(txtQuery.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtQuery.Text = "";
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            dynamic row = this.gridView1.GetFocusedRow();
            string userUuid = row.userUuid;
            AddWorker addWorker = new AddWorker(userUuid, _isHmc, false);
            addWorker.CloseDdetailedWinform += new Action<DevExpress.XtraEditors.XtraForm>(ShowDetail);
            //addWorker.StartPosition = FormStartPosition.CenterParent;
            //addWorker.Show();
            ShowDetail(addWorker);


        }
    }
}