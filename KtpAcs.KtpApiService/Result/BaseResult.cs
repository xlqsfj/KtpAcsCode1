
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class BaseResult
    {

        /// <summary>
        /// 仅表示接口调用状态，1 成功，0 失败
        /// </summary>
        public int result { get; set; }
        public string id { get; set; }
        public string msg { get; set; }
        public Data data { get; set; }
        public class Data
        {
            /// <summary>
            /// 用户公司id用于闸机
            /// </summary>
            public int? organizationUserId { get; set; }
            //用户公司uuid
            public string organizationUserUuid { get; set; }

            /// <summary>
            /// 用户主键id,用于工人id
            /// </summary>
            public int? userId { get; set; }
            /// <summary>
            /// 用户uuid
            /// </summary>
            public string userUuid { get; set; }

        }
      

    }
}
