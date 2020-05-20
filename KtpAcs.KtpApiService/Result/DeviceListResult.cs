using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class DeviceListResult
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
            public List<DeviceList> list { get; set; }
            public int navigateFirstPage { get; set; }
            public int navigateLastPage { get; set; }
            public int navigatePages { get; set; }
            public int[] navigatepageNums { get; set; }
            public int nextPage { get; set; }
            public int pageNum { get; set; }
            public int pageSize { get; set; }
            public int pages { get; set; }
            public int prePage { get; set; }
            public int size { get; set; }
            public int startRow { get; set; }
            public int total { get; set; }
        }

        public class DeviceList
        {
            public DateTime createTime { get; set; }
            public string creators { get; set; }
            public string description { get; set; }
            public string deviceId { get; set; }
            public string deviceIp { get; set; }
        

            /// <summary>
            ///是否进场方向 0否 1是
            /// </summary>
            private String _gateType;

            public String gateType

            {

                get { return _gateType; }

                set

                {


                    _gateType = value == "1" ? "进口" : "出口";

                }

            }
            public string modifier { get; set; }
            public DateTime modifyTime { get; set; }
            public int presentState { get; set; }
            /// <summary>
            /// 在线状态
            /// </summary>
            public string deviceStatus { get; set; }
            /// <summary>
            /// 在场人数
            /// </summary>
            public int deviceCount { get; set; }
            public string projectUuid { get; set; }
            public string uuid { get; set; }

            public bool isSeleced { get; set; }
            public string deviceToServiceState { get; set; }
        }

    }
}