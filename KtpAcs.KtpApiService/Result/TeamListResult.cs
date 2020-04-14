using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{
    public class TeamListResult
    {


        public List<TeamList> data { get; set; }
        public string msg { get; set; }
        public int result { get; set; }

        public class TeamList
        {
            public string createTime { get; set; }
            public string creators { get; set; }
            public string descreption { get; set; }
            public string modifier { get; set; }
            public string modifyTime { get; set; }
            public string organization { get; set; }
            public string teamLeaderUuid { get; set; }
            public string teamName { get; set; }
            public string uuid { get; set; }
        }



    }
}
