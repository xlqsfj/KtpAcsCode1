using System;
using System.Linq;

namespace KtpAcs.KtpApiService.Send
{
    public class WorkerSend
    {

        //是否甲指分包：false否，true是
        public bool designatedFlag { get; set; }
        //关键字
        public string keyWord { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        //项目uuid
        public string projectUuid { get; set; }
        //入场状态（0全部 1.待入场 2.已入场 3.已退场 
        public int status { get; set; }
        //工人启用状态：0.全部 1.启用 2.停用
        public int workerStatus { get; set; }

        //用户id
        public string userUuid { get; set; }


    }
}
