using KtpAcs.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.KtpApiService.Send
{

    public class AddWorerkSend
    {
        public string icon { get; set; }

        public string address { get; set; }
        public int age { get; set; }
        public string bankName { get; set; }
        public string bankNo { get; set; }

        private string _birthday;
        /// <summary>
        /// 性别 1男2 女
        /// </summary>
        public string birthday
        {
            get { return _birthday; }
            set
            {


                if (!string.IsNullOrEmpty(value))
                {
                    _birthday = FormatHelper.GetIsoDateString(Convert.ToDateTime(value));
                }
            }
        }
        //文化程度:1.小学，2.初中，3.高中，4.大专，5.本科，6.硕士，7.博士 8中专 9无
        public int educationLevel { get; set; }
        public string emergencyContactName { get; set; }
        public string emergencyContactPhone { get; set; }
        public string expireTime { get; set; }
        public string facePic { get; set; }
        //性别：1男，2女
        public int gender { get; set; }
        public string idCard { get; set; }
        public string name { get; set; }
        public string nation { get; set; }
        public string nativePlace { get; set; }
        public string organizationUuid { get; set; }
        public string phone { get; set; }
        public string picturePositive { get; set; }
        public string pictureReverse { get; set; }
        public string projectUuid { get; set; }
        public string startTime { get; set; }
        public string workType { get; set; }
        public string workerTeamUuid { get; set; }
        public string cardAgency { get; set; }

        /// <summary>
        /// 保存本地的文件名的识别人像
        /// </summary>
        public string localImgFileName { get; set; }
        /// <summary>
        /// 保存本地的文件名的正面身份证
        /// </summary>
        public string localImgFileName1 { get; set; }
        /// <summary>
        /// 保存本地的文件名的反面身份证
        /// </summary>
        public string localImgFileName2 { get; set; }


        /// <summary>
        /// 保存本地的文件名的头像
        /// </summary>
        public string localImgUpic { get; set; }

        public string userUuid { get; set; }

        /// <summary>
        /// 结算单价
        /// </summary>
        public double clearingPrice { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string clearingType { get; set; }
        /// <summary>
        /// 结算单位
        /// </summary>
        public string clearingUnit { get; set; }
        /// <summary>
        /// 预发日薪
        /// </summary>
        public double pretestSalary { get; set; }

    }

}
