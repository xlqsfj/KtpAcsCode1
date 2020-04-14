using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
 public   class WorkerTypeListResult
    {

        public List<TypeList> data { get; set; }
        public string msg { get; set; }
        public int result { get; set; }
        public class TypeList
        {

            public string code { get; set; }
            public string createTime { get; set; }
            public string creators { get; set; }
            public string iconUrl { get; set; }
  
            public string modifier { get; set; }
            public string modifyTime { get; set; }
            public string name { get; set; }
 
      
        }
    }
}
