using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class LoginResult
    {

   
            public int result { get; set; }
            public Data data { get; set; }
            public string msg { get; set; }
      

        public class Data
        {
            public string uuid { get; set; }
            public string phone { get; set; }
            public string token { get; set; }
            public string organizationUserUuid { get; set; }
            public bool loginFlag { get; set; }
        }

    }
}
