using KtpAcs.KtpApiService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
 public   class WorkerListResult
    {
      
            public Data data { get; set; }
            public string msg { get; set; }
            public int result { get; set; }
    
        public class Data
        {
            public int endRow { get; set; }
            public int firstPage { get; set; }
            public bool hasNextPage { get; set; }
            public bool hasPreviousPage { get; set; }
            public bool isFirstPage { get; set; }
            public bool isLastPage { get; set; }
            public int lastPage { get; set; }
            public List<WorkerList> list { get; set; }
            public int navigateFirstPage { get; set; }
            public int navigateLastPage { get; set; }
            public int navigatePages { get; set; }
            public object[] navigatepageNums { get; set; }
            public int nextPage { get; set; }
            public int pageNum { get; set; }
            public int pageSize { get; set; }
            public int pages { get; set; }
            public int prePage { get; set; }
            public int size { get; set; }
            public int startRow { get; set; }
            public int total { get; set; }
        }

        public class WorkerList
        {
            public string imgBase64 { get; set; }
            public string panelIp { get; set; }
            public int age { get; set; }
            public string birthday { get; set; }
            public string idCard { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string reason { get; set; }

            private string _sex;
            /// <summary>
            /// 性别 1男2 女
            /// </summary>
            public string sex
            {
                get { return _sex; }
                set
                {
                    if (value == "1")
                        _sex  = "男";
                    else if (value == "2")
                        _sex  = "女";
                    else
                        _sex = value;
                 
                }
            }

            public string takeOfficeTime { get; set; }
            public string uuid { get; set; }
            /// <summary>
            /// 用户id
            /// </summary>
           public string userUuid { get; set; }
            /// <summary>
            /// 用户主键id SalesMoney
            /// </summary>
            public int  userId { get; set; }

            /// <summary>
            /// 详情
            /// </summary>
            public string details{ get { return "详情"; } }
            /// <summary>
            /// 用户人脸图片
            /// </summary>
            public string facePic { get; set; }


            public string _workerStatus;
            public string workerStatus {
                get { return _workerStatus; }
                set
                { 
                    _workerStatus=value=="1"?"启用" : "停用";
                  
                }
            }
            /// <summary>
            /// 工人类型
            /// </summary>
            public EnumWorkerType enumWorkerType { get ; set; }
            public int  workerIntType { get; set; }

            public String workerType { get; set; }
        }

    }
}
