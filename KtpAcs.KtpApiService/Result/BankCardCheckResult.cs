using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class BankCardCheckResult
    {


        public BankInfo data { get; set; }
        public string msg { get; set; }
        public int result { get; set; }


        public class BankInfo
        {
            public string accountNo { get; set; }
            public string area { get; set; }
            public string bank { get; set; }
            public string cardName { get; set; }
            public string cardType { get; set; }
            public string city { get; set; }
            public string idCard { get; set; }
            public string msg { get; set; }
            public string name { get; set; }
            public string prefecture { get; set; }
            public string province { get; set; }
            public string sex { get; set; }
            public string status { get; set; }
        }


    }
}
