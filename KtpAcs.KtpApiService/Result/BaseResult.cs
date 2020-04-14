
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
  public   class BaseResult
    {

        public int result { get; set; }
        public string id { get; set; }
        public string msg { get; set; }

        public int organizationUserId { get; set; }
        public string organizationUserUuid { get; set; }
        public int userId { get; set; }
        public string userUuid { get; set; }
    }
}
