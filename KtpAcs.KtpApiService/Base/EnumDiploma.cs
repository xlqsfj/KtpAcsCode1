using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Base
{
    /// <summary>
    /// 1.小学，2.初中，3.高中，4.大专，5.本科，6.硕士，7.博士 8中专 9无
    /// </summary>
    public enum EnumDiploma
    {

        [Description("小学")] XueXun = 1,
        [Description("初中")] ChuZhong = 2,
        [Description("高中")] gaozhong = 3,
        [Description("大专")] dazhuan = 4,
        [Description("本科")] benkao = 5,
        [Description("硕士")] shuoshi = 6,
        [Description("博士")] boshi = 7,
        [Description("中专")] zhongzhuan = 8,
        [Description("无")] wu = 9,

    }
}
