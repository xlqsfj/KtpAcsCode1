using KtpAcs.PanelApi.Haiqing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Haiqing
{
    /// <summary>
    /// mule数据推送器接口
    /// </summary>
    public interface IMulePusherHq
    {
        /// <summary>
        /// 发起数据推送
        /// </summary>
        /// <returns></returns>
        PushSummaryHq Push();
        /// <summary>
        /// 发起数据表单推送
        /// </summary>
        /// <returns></returns>
        PushSummaryHq PushForm();

        ///// <summary>
        ///// 推送某一张单据
        ///// </summary>
        ///// <param name="billID"></param>
        ///// <returns></returns>
        //PushSummaryHq Push(int billID);

        /// <summary>
        ///单次推送数据最多包含的数据条目数。返回0表示不限制大小
        /// </summary>
        int MaxPackageSize { get; }




    }
}
