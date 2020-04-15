using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Send
{
    public class BaseSend
    {

        public string uuid;
        /// <summary>
        /// 公司用户uuid
        /// </summary>
        public string organizationUserUuid { get; set; }
        /// <summary>
        ///  公司用户
        /// </summary>
        public string projectUuid { get; set; }
        /// <summary>
        ///  人员状态：1.未进场；2.已进场；3.已退场',
        /// </summary>
        public string status { get; set; }


    

    }



}
