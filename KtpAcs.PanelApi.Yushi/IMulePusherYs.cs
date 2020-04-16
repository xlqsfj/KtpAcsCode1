using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.PanelApi.Yushi
{
    /// <summary>
    /// mule数据推送器接口
    /// </summary>
    public interface IMulePusherYs
    {
        /// <summary>
        /// 发起数据推送
        /// </summary>
        /// <returns></returns>
        PushSummarYs Push();
        /// <summary>
        /// 发起数据表单推送
        /// </summary>
        /// <returns></returns>
        PushSummarYs PushForm();

        ///// <summary>
        ///// 推送某一张单据
        ///// </summary>
        ///// <param name="billID"></param>
        ///// <returns></returns>
        //PushSummary Push(int billID);

        /// <summary>
        ///单次推送数据最多包含的数据条目数。返回0表示不限制大小
        /// </summary>
        int MaxPackageSize { get; }




    }
}
