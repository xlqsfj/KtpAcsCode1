﻿using System;
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
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTreeList;
using System.IO;
using System.Reflection;
using static KtpAcs.KtpApiService.Result.WorkerListResult;

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

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                dynamic row = this.grid_Device.GetFocusedRow();
                string userUuid = row.userUuid;
                string userName = row.name;
                int wType = row.workerIntType;
                AddWorker addWorker = new AddWorker(userUuid, wType, false);
                // addWorker.CloseDdetailedWinform += new Action<DevExpress.XtraEditors.XtraForm, bool, string>(ShowDetail);
                addWorker.StartPosition = FormStartPosition.CenterParent;
                addWorker.Show();
                // ShowDetail(addWorker, false, userName);
            }
            catch (Exception ex)
            {

                MessageHelper.Show(ex.Message, ex);
            }


        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"Excel\";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                path += "同步失败人员_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                if (dt2csv(WorkSysFail.list, path))
                {
                    System.Diagnostics.Process.Start(path);//打开指定路径下的文件
                                                           // MessageBox.Show("导出成功,文件位置:" + path);
                }
                else { MessageBox.Show("导出失败"); }

            }
            catch (Exception ex)
            {

                MessageHelper.Show("导出失败:" + ex.Message, ex);
            }

        }

        public Dictionary<string, string> GetShowData()
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();

            keys.Add("workerType", "工人类型");
            keys.Add("name", "姓名");
            keys.Add("phone", "手机号 ");
            keys.Add("sex", "性别");
            keys.Add("idCard", "身份证");
            keys.Add("reason", "同步失败原因");
            return keys;

        }


        /// <summary>        /// 导出报表为Csv      
        /// </summary>        /// <param name="dt">DataTable</param>       
        /// /// <param name="strFilePath">物理路径</param>       
        /// /// <param name="tableheader">表头</param>        
        /// /// <param name="columname">字段标题,逗号分隔</param>       
        public bool dt2csv(List<WorkerList> list, string strFilePath)
        {



            StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);

            var showCol = GetShowData();
            StringBuilder stringBuilder = new StringBuilder();
            //获取标题
            foreach (var item in showCol)
            {
                // stringBuilder.Append("<th>"+item.Value + "</th>");
                stringBuilder.Append(item.Value + ",\t");
            }
            //strmWriterObj.WriteLine("<table>");
            //strmWriterObj.WriteLine("<tr>");
            strmWriterObj.WriteLine(stringBuilder.ToString());
            // strmWriterObj.WriteLine("</tr>");

            foreach (WorkerList item in list)
            {
                stringBuilder.Clear();
                //strmWriterObj.WriteLine("<tr>");
                //stringBuilder.Append("<td>"+item.workerType + "</td>");
                //stringBuilder.Append("<td>" + item.name + "</td>");
                //stringBuilder.Append("<td>" + item.phone + "</td>");
                //stringBuilder.Append("<td>" + item.sex + "</td>");
                //stringBuilder.Append("<td>" + item.idCard + "</td>");
                //stringBuilder.Append("<td>" + item.reason + "</td>");
                stringBuilder.Append(item.workerType + ",\t");
                stringBuilder.Append(item.name + ",\t");
                stringBuilder.Append(item.phone + ",\t");
                stringBuilder.Append(item.sex + ",\t");
                stringBuilder.Append(item.idCard + ",\t");
                stringBuilder.Append(item.reason + ",\t");
                // strmWriterObj.WriteLine("</tr>");
                // strmWriterObj.WriteLine(Environment.NewLine);
                strmWriterObj.WriteLine(stringBuilder.ToString());
            }




            //  strmWriterObj.WriteLine("</table>");
            strmWriterObj.Close(); return true;


        }


        protected virtual void ExportToExcel(Object grid)
        {
            //EditorContainer定义GridControl之类的容器
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "导出Excel",
                Filter = "Excel文件(*.xls)|*.xls"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                //XlsExportOptions
                XlsExportOptionsEx options = new XlsExportOptionsEx();
                if (grid is GridControl)
                {
                    (grid as GridControl).ExportToXls(dialog.FileName, options);
                }
                else if (grid is BandedGridView)
                {
                    options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    (grid as BandedGridView).OptionsPrint.AutoWidth = false;
                    (grid as BandedGridView).OptionsPrint.PrintBandHeader = true;
                    (grid as BandedGridView).ExportToXls(dialog.FileName, options);
                }
                else if (grid is TreeList)
                {
                    (grid as TreeList).ExpandAll();
                    (grid as TreeList).ExportToXls(dialog.FileName, options);
                }
                XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}