using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Send
{
    public class DeviceSend
    {

        public string description { get; set; }
        public string deviceId { get; set; }
        public string deviceIp { get; set; }
        public int gateType { get; set; }
        public int presentState { get; set; }
        public string projectUuid { get; set; }

        public string deviceUuid { get; set; }


    }
}
