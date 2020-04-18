using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
  public   class OrganizationListResult
    {

        public List<OrganizationList> data { get; set; }
        public string msg { get; set; }
        public int result { get; set; }


        public class OrganizationList
        {
            //public string address { get; set; }
            //public string businessLicense { get; set; }
            //public string certificate { get; set; }
            //public string createTime { get; set; }
            //public string creators { get; set; }
            //public string creditCode { get; set; }
            //public string idCard { get; set; }
  
            //public string modifier { get; set; }
            //public string modifyTime { get; set; }
            public string name { get; set; }
            //public string organizationCode { get; set; }
            //public string parentUuid { get; set; }
            //public string phone { get; set; }
            //public string realmName { get; set; }
     
            //public int type { get; set; }
            public string uuid { get; set; }
        }

    }
}
