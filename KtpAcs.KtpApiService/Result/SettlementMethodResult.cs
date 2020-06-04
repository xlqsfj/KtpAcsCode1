using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{

    public class SettlementMethodResult
    {
        public int result { get; set; }
        public List<Data> data { get; set; }
        public string msg { get; set; }
        public class Data
        {
            public string createTime { get; set; }
            public string creators { get; set; }
            public string modifyTime { get; set; }
            public string modifier { get; set; }
            public string uuid { get; set; }
            public string clearingForm { get; set; }
        }
    }



}
