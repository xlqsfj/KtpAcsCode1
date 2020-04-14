using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class WorkerResult
    {

        public Data data { get; set; }
        public string msg { get; set; }
        public int result { get; set; }
        public class Data
        {
            public string address { get; set; }
            public int age { get; set; }
            public string bankName { get; set; }
            public string bankNo { get; set; }
            public string birthday { get; set; }
            public int educationLevel { get; set; }
            public string emergencyContactName { get; set; }
            public string emergencyContactPhone { get; set; }
            public string facePic { get; set; }
            public int gender { get; set; }
            public string icon { get; set; }
            public string idCard { get; set; }
            public string name { get; set; }
            public string nation { get; set; }
            public string nativePlace { get; set; }
            public string organizationName { get; set; }
            public string organizationUuid { get; set; }
            public string phone { get; set; }
            public string picturePositive { get; set; }
            public string pictureReverse { get; set; }
            public string projectName { get; set; }
            public string projectUuid { get; set; }
            public string uuid { get; set; }
            public string workType { get; set; }
            public string workerTeamName { get; set; }
            public string workerTeamUuid { get; set; }
        }

    }
}
