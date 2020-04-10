using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class ProjectCountResult
    {


     
            public Data data { get; set; }
            public string msg { get; set; }
            public int result { get; set; }
       

        public class Data
        {

            //甲子人数
            public int jiaziNum { get; set; }
            public int jiaziVerificationNum { get; set; }
            public int manageVerificationNum { get; set; }
            public string projectAddress { get; set; }
            public string projectCode { get; set; }
            public int projectManageNum { get; set; }
            public string projectName { get; set; }
            public string projectUuid { get; set; }
            public int projectWorkerNum { get; set; }
            public int workerVerificationNum { get; set; }
        }

    }
}
