using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Result
{


    public class WorkerProjectListResult
    {
        public Data data { get; set; }
        public string msg { get; set; }
        public int result { get; set; }
        public class Data
        {
            public int endRow { get; set; }
            public int firstPage { get; set; }
            public bool hasNextPage { get; set; }
            public bool hasPreviousPage { get; set; }
            public bool isFirstPage { get; set; }
            public bool isLastPage { get; set; }
            public int lastPage { get; set; }
            public List<WorkerProjectList> list { get; set; }
            public int navigateFirstPage { get; set; }
            public int navigateLastPage { get; set; }
            public int navigatePages { get; set; }
            public object[] navigatepageNums { get; set; }
            public int nextPage { get; set; }
            public int pageNum { get; set; }
            public int pageSize { get; set; }
            public int pages { get; set; }
            public int prePage { get; set; }
            public int size { get; set; }
            public int startRow { get; set; }
            public int total { get; set; }
        }

        public class WorkerProjectList
        {
            public string mail { get; set; }
            public string name { get; set; }
            public string organizationUserUuid { get; set; }
            public string phone { get; set; }
            public string projectUuid { get; set; }
            public string roleNames { get; set; }
            public string roleUuid { get; set; }
            public string status { get; set; }
            public string uuid { get; set; }
        }

    }



}
