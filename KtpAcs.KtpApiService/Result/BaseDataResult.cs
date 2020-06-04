
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class BaseDataResult
    {

        /// <summary>
        /// 仅表示接口调用状态，1 成功，0 失败
        /// </summary>
        public int result { get; set; }
        public string id { get; set; }
        public string msg { get; set; }
        public string  data { get; set; }

      

    }
}
