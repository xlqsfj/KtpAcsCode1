using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Base
{
    /// <summary>
    /// 工人类型
    /// </summary>
   public enum EnumWorkerType
    {
        [Description("花名册人员")] Hmc = 0,
        [Description("甲指分包人员")] Jzfb = 1,
        [Description("项目人员")] Hmry = 2
    }
}
