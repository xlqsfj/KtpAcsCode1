using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class DeviceStateResult
    {
     
            public int result { get; set; }
            public List<DeviceServiceStateList> data { get; set; }
            public string msg { get; set; }
 

    

    }
    public class DeviceServiceStateList
    {
        public string deviceId { get; set; }
        public int status { get; set; }
    }
}
