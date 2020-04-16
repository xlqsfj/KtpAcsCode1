using System;
using System.Collections.Generic;
using System.Linq;

namespace KtpAcs.Infrastructure.Utilities
{
    public class WorkerInfoHelper
    {
    

      
       

        #region 婚姻状态 maritalStatus 01 未婚 02 已婚 03 离异 04 丧偶
        /// <summary>
        /// 获取工人婚姻状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string getWorkerMaritalStatusName(string code)
        {
            if (code == null || code.Length <= 0)
                return "";
            if (code == "01")
            {
                return "未婚";
            }
            else if (code == "02")
            {
                return "已婚";
            }
            else if (code == "03")
            {
                return "离异";
            }
            else if (code == "04")
            {
                return "丧偶";
            }
            return "";
        }
        #endregion

        #region 合同计量单位 unit 80 米  81 平方米  82 立方米
        /// <summary>
        /// 获取合同计量单位名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string getWorkerContractUnitName(string code)
        {
            if (code == null || code.Length <= 0)
                return "";
            if (code == "80")
            {
                return "米";
            }
            else if (code == "81")
            {
                return "平方米";
            }
            else if (code == "82")
            {
                return "立方米";
            }
            return "";
        }
        #endregion





    
        #region 省份

       public static IDictionary<string, string> ProvinceDic = new Dictionary<string, string>() {
            { "11","北京市"},
                { "12","天津市"},
                { "13","河北省"},
                { "14","山西省"},
                { "15","内蒙古自治区"},
                { "21","辽宁省"},
                { "22","吉林省"},
                { "23","黑龙江省"},
                { "31","上海市"},
                { "32","江苏省"},
                { "33","浙江省"},
                { "34","安徽省"},
                { "35","福建省"},
                { "36","江西省"},
                { "37","山东省"},
                { "41","河南省"},
                { "42","湖北省"},
                { "43","湖南省"},
                { "44","广东省"},
                { "45","广西壮族自治区"},
                { "46","海南省"},
                { "50","重庆市"},
                { "51","四川省"},
                { "52","贵州省"},
                { "53","云南省"},
                { "54","西藏自治区"},
                { "61","陕西省"},
                { "62","甘肃省"},
                { "63","青海省"},
                { "64","宁夏回族自治区"},
                { "65","新疆维吾尔自治区"},
                { "71","台湾省"},
                { "81","香港特别行政区"},
                { "82","澳门特别行政区"}
            };
        public static List<Tuple<string, string>> PityDic = new List<Tuple<string, string>>()
            {
                new Tuple<string,string>( "1101","北京市市辖区"),
                new Tuple<string,string>( "1102","北京市县"),
                new Tuple<string,string>( "1201","天津市市辖区"),
                new Tuple<string,string>( "1202","天津市县"),
                new Tuple<string,string>( "1301","石家庄市"),
                new Tuple<string,string>( "1302","唐山市"),
                new Tuple<string,string>( "1303","秦皇岛市"),
                new Tuple<string,string>( "1304","邯郸市"),
                new Tuple<string,string>( "1305","邢台市"),
                new Tuple<string,string>( "1306","保定市"),
                new Tuple<string,string>( "1307","张家口市"),
                new Tuple<string,string>( "1308","承德市"),
                new Tuple<string,string>( "1309","沧州市"),
                new Tuple<string,string>( "1310","廊坊市"),
                new Tuple<string,string>( "1311","衡水市"),
                new Tuple<string,string>( "1401","太原市"),
                new Tuple<string,string>( "1402","大同市"),
                new Tuple<string,string>( "1403","阳泉市"),
                new Tuple<string,string>( "1404","长治市"),
                new Tuple<string,string>( "1405","晋城市"),
                new Tuple<string,string>( "1406","晋城市朔州市"),
                new Tuple<string,string>( "1407","晋中市"),
                new Tuple<string,string>( "1408","运城市"),
                new Tuple<string,string>( "1409","忻州市"),
                new Tuple<string,string>( "1410","临汾市"),
                new Tuple<string,string>( "1411","吕梁市"),
                new Tuple<string,string>( "1422","忻州地区"),
                new Tuple<string,string>( "1423","忻州地区吕梁地区"),
                new Tuple<string,string>( "1424","晋中地区"),
                new Tuple<string,string>( "1426","临汾地区"),
                new Tuple<string,string>( "1427","运城地区"),
                new Tuple<string,string>( "1501","呼和浩特市"),
                new Tuple<string,string>( "1502","包头市"),
                new Tuple<string,string>( "1503","乌海市"),
                new Tuple<string,string>( "1504","赤峰市"),
                new Tuple<string,string>( "1505","通辽市"),
                new Tuple<string,string>( "1506","鄂尔多斯市"),
                new Tuple<string,string>( "1507","呼伦贝尔市"),
                new Tuple<string,string>( "1508","巴彦淖尔市"),
                new Tuple<string,string>( "1509","乌兰察布市"),
                new Tuple<string,string>( "1521","呼伦贝尔盟"),
                new Tuple<string,string>( "1522","兴安盟"),
                new Tuple<string,string>( "1523","哲里木盟"),
                new Tuple<string,string>( "1525","锡林郭勒盟"),
                new Tuple<string,string>( "1526","乌兰察布盟"),
                new Tuple<string,string>( "1527","伊克昭盟"),
                new Tuple<string,string>( "1528","巴彦淖尔盟"),
                new Tuple<string,string>( "1529","阿拉善盟"),
                new Tuple<string,string>( "2101","沈阳市"),
                new Tuple<string,string>( "2102","大连市"),
                new Tuple<string,string>( "2103","鞍山市"),
                new Tuple<string,string>( "2104","抚顺市"),
                new Tuple<string,string>( "2105","本溪市"),
                new Tuple<string,string>( "2106","丹东市"),
                new Tuple<string,string>( "2107","锦州市"),
                new Tuple<string,string>( "2108","营口市"),
                new Tuple<string,string>( "2109","阜新市"),
                new Tuple<string,string>( "2110","辽阳市"),
                new Tuple<string,string>( "2111","盘锦市"),
                new Tuple<string,string>( "2112","铁岭市"),
                new Tuple<string,string>( "2113","朝阳市"),
                new Tuple<string,string>( "2114","葫芦岛市"),
                new Tuple<string,string>( "2201","长春市"),
                new Tuple<string,string>( "2202","吉林市"),
                new Tuple<string,string>( "2203","四平市"),
                new Tuple<string,string>( "2204","辽源市"),
                new Tuple<string,string>( "2205","通化市"),
                new Tuple<string,string>( "2206","白山市"),
                new Tuple<string,string>( "2207","松原市"),
                new Tuple<string,string>( "2208","白城市"),
                new Tuple<string,string>( "2224","延边朝鲜族自治州"),
                new Tuple<string,string>( "2301","省哈尔滨市"),
                new Tuple<string,string>( "2302","省齐齐哈尔市"),
                new Tuple<string,string>( "2303","省鸡西市"),
                new Tuple<string,string>( "2304","省鹤岗市"),
                new Tuple<string,string>( "2305","省双鸭山市"),
                new Tuple<string,string>( "2306","省大庆市"),
                new Tuple<string,string>( "2307","省伊春市"),
                new Tuple<string,string>( "2308","省佳木斯市"),
                new Tuple<string,string>( "2309","省七台河市"),
                new Tuple<string,string>( "2310","省牡丹江市"),
                new Tuple<string,string>( "2311","省黑河市"),
                new Tuple<string,string>( "2312","省绥化市"),
                new Tuple<string,string>( "2323","省绥化地区"),
                new Tuple<string,string>( "2327","省大兴安岭地区"),
                new Tuple<string,string>( "3101","市辖区"),
                new Tuple<string,string>( "3102","县"),
                new Tuple<string,string>( "3201","南京市"),
                new Tuple<string,string>( "3202","无锡市"),
                new Tuple<string,string>( "3203","徐州市"),
                new Tuple<string,string>( "3204","常州市"),
                new Tuple<string,string>( "3205","苏州市"),
                new Tuple<string,string>( "3206","南通市"),
                new Tuple<string,string>( "3207","连云港市"),
                new Tuple<string,string>( "3208","淮阴市"),
                new Tuple<string,string>( "3209","盐城市"),
                new Tuple<string,string>( "3210","扬州市"),
                new Tuple<string,string>( "3211","镇江市"),
                new Tuple<string,string>( "3212","泰州市"),
                new Tuple<string,string>( "3213","宿迁市"),
                new Tuple<string,string>( "3301","杭州市"),
                new Tuple<string,string>( "3302","宁波市"),
                new Tuple<string,string>( "3303","温州市"),
                new Tuple<string,string>( "3304","嘉兴市"),
                new Tuple<string,string>( "3305","湖州市"),
                new Tuple<string,string>( "3306","绍兴市"),
                new Tuple<string,string>( "3307","金华市"),
                new Tuple<string,string>( "3308","衢州市"),
                new Tuple<string,string>( "3309","舟山市"),
                new Tuple<string,string>( "3310","台州市"),
                new Tuple<string,string>( "3311","丽水市"),
                new Tuple<string,string>( "3325","丽水地区"),
                new Tuple<string,string>( "3401","合肥市"),
                new Tuple<string,string>( "3402","芜湖市"),
                new Tuple<string,string>( "3403","蚌埠市"),
                new Tuple<string,string>( "3404","淮南市"),
                new Tuple<string,string>( "3405","马鞍山市"),
                new Tuple<string,string>( "3406","淮北市"),
                new Tuple<string,string>( "3407","铜陵市"),
                new Tuple<string,string>( "3408","安庆市"),
                new Tuple<string,string>( "3410","黄山市"),
                new Tuple<string,string>( "3411","滁州市"),
                new Tuple<string,string>( "3412","阜阳市"),
                new Tuple<string,string>( "3413","宿州市"),
                new Tuple<string,string>( "3414","巢湖市"),
                new Tuple<string,string>( "3415","六安市"),
                new Tuple<string,string>( "3416","亳州市"),
                new Tuple<string,string>( "3417","池州市"),
                new Tuple<string,string>( "3418","宣城市"),
                new Tuple<string,string>( "3424","六安地区"),
                new Tuple<string,string>( "3425","宣城地区"),
                new Tuple<string,string>( "3426","巢湖地区"),
                new Tuple<string,string>( "3429","池州地区"),
                new Tuple<string,string>( "3501","福州市"),
                new Tuple<string,string>( "3502","厦门市"),
                new Tuple<string,string>( "3503","莆田市"),
                new Tuple<string,string>( "3504","三明市"),
                new Tuple<string,string>( "3505","泉州市"),
                new Tuple<string,string>( "3506","漳州市"),
                new Tuple<string,string>( "3507","南平市"),
                new Tuple<string,string>( "3508","龙岩市"),
                new Tuple<string,string>( "3509","宁德市"),
                new Tuple<string,string>( "3522","宁德地区"),
                new Tuple<string,string>( "3601","南昌市"),
                new Tuple<string,string>( "3602","景德镇市"),
                new Tuple<string,string>( "3603","萍乡市"),
                new Tuple<string,string>( "3604","九江市"),
                new Tuple<string,string>( "3605","新余市"),
                new Tuple<string,string>( "3606","鹰潭市"),
                new Tuple<string,string>( "3607","赣州市"),
                new Tuple<string,string>( "3608","吉安市"),
                new Tuple<string,string>( "3609","宜春市"),
                new Tuple<string,string>( "3610","抚州市"),
                new Tuple<string,string>( "3611","上饶市"),
                new Tuple<string,string>( "3622","宜春地区"),
                new Tuple<string,string>( "3623","上饶地区"),
                new Tuple<string,string>( "3624","吉安地区"),
                new Tuple<string,string>( "3625","抚州地区"),
                new Tuple<string,string>( "3701","济南市"),
                new Tuple<string,string>( "3702","青岛市"),
                new Tuple<string,string>( "3703","淄博市"),
                new Tuple<string,string>( "3704","枣庄市"),
                new Tuple<string,string>( "3705","东营市"),
                new Tuple<string,string>( "3706","烟台市"),
                new Tuple<string,string>( "3707","潍坊市"),
                new Tuple<string,string>( "3708","济宁市"),
                new Tuple<string,string>( "3709","泰安市"),
                new Tuple<string,string>( "3710","威海市"),
                new Tuple<string,string>( "3711","日照市"),
                new Tuple<string,string>( "3712","莱芜市"),
                new Tuple<string,string>( "3713","临沂市"),
                new Tuple<string,string>( "3714","德州市"),
                new Tuple<string,string>( "3715","聊城市"),
                new Tuple<string,string>( "3716","滨州市"),
                new Tuple<string,string>( "3717","荷泽市"),
                new Tuple<string,string>( "3723","滨州地区"),
                new Tuple<string,string>( "3729","菏泽地区"),
                new Tuple<string,string>( "4101","郑州市"),
                new Tuple<string,string>( "4102","开封市"),
                new Tuple<string,string>( "4103","洛阳市"),
                new Tuple<string,string>( "4104","平顶山市"),
                new Tuple<string,string>( "4105","安阳市"),
                new Tuple<string,string>( "4106","鹤壁市"),
                new Tuple<string,string>( "4107","新乡市"),
                new Tuple<string,string>( "4108","焦作市"),
                new Tuple<string,string>( "4109","濮阳市"),
                new Tuple<string,string>( "4110","许昌市"),
                new Tuple<string,string>( "4111","漯河市"),
                new Tuple<string,string>( "4112","三门峡市"),
                new Tuple<string,string>( "4113","南阳市"),
                new Tuple<string,string>( "4114","商丘市"),
                new Tuple<string,string>( "4115","信阳市"),
                new Tuple<string,string>( "4116","周口市"),
                new Tuple<string,string>( "4117","驻马店市"),
                new Tuple<string,string>( "4127","周口地区"),
                new Tuple<string,string>( "4128","驻马店地区"),
                new Tuple<string,string>( "4201","武汉市"),
                new Tuple<string,string>( "4202","黄石市"),
                new Tuple<string,string>( "4203","十堰市"),
                new Tuple<string,string>( "4205","宜昌市"),
                new Tuple<string,string>( "4206","襄樊市"),
                new Tuple<string,string>( "4207","鄂州市"),
                new Tuple<string,string>( "4208","荆门市"),
                new Tuple<string,string>( "4209","孝感市"),
                new Tuple<string,string>( "4210","荆州市"),
                new Tuple<string,string>( "4211","黄冈市"),
                new Tuple<string,string>( "4212","咸宁市"),
                new Tuple<string,string>( "4213","随州市"),
                new Tuple<string,string>( "4228","施土家族苗族自治州"),
                new Tuple<string,string>( "4290","省直辖县级行政单位"),
                new Tuple<string,string>( "4301","长沙市"),
                new Tuple<string,string>( "4302","株洲市"),
                new Tuple<string,string>( "4303","湘潭市"),
                new Tuple<string,string>( "4304","衡阳市"),
                new Tuple<string,string>( "4305","邵阳市"),
                new Tuple<string,string>( "4306","岳阳市"),
                new Tuple<string,string>( "4307","常德市"),
                new Tuple<string,string>( "4308","张家界市"),
                new Tuple<string,string>( "4309","益阳市"),
                new Tuple<string,string>( "4310","郴州市"),
                new Tuple<string,string>( "4311","永州市"),
                new Tuple<string,string>( "4312","怀化市"),
                new Tuple<string,string>( "4313","娄底市"),
                new Tuple<string,string>( "4325","娄底地区"),
                new Tuple<string,string>( "4330","怀化市"),
                new Tuple<string,string>( "4331","湘西土家族苗族自治州"),
                new Tuple<string,string>( "4401","广州市"),
                new Tuple<string,string>( "4402","韶关市"),
                new Tuple<string,string>( "4403","深圳市"),
                new Tuple<string,string>( "4404","珠海市"),
                new Tuple<string,string>( "4405","汕头市"),
                new Tuple<string,string>( "4406","佛山市"),
                new Tuple<string,string>( "4407","江门市"),
                new Tuple<string,string>( "4408","湛江市"),
                new Tuple<string,string>( "4409","茂名市"),
                new Tuple<string,string>( "4412","肇庆市"),
                new Tuple<string,string>( "4413","惠州市"),
                new Tuple<string,string>( "4414","梅州市"),
                new Tuple<string,string>( "4415","汕尾市"),
                new Tuple<string,string>( "4416","河源市"),
                new Tuple<string,string>( "4417","阳江市"),
                new Tuple<string,string>( "4418","清远市"),
                new Tuple<string,string>( "4419","东莞市"),
                new Tuple<string,string>( "4420","中山市"),
                new Tuple<string,string>( "4451","潮州市"),
                new Tuple<string,string>( "4452","揭阳市"),
                new Tuple<string,string>( "4453","云浮市"),
                new Tuple<string,string>( "4501","南宁市"),
                new Tuple<string,string>( "4502","柳州市"),
                new Tuple<string,string>( "4503","桂林市"),
                new Tuple<string,string>( "4504","梧州市"),
                new Tuple<string,string>( "4505","北海市"),
                new Tuple<string,string>( "4506","防城港市"),
                new Tuple<string,string>( "4507","钦州市"),
                new Tuple<string,string>( "4508","贵港市"),
                new Tuple<string,string>( "4509","玉林市"),
                new Tuple<string,string>( "4510","百色市"),
                new Tuple<string,string>( "4511","贺州市"),
                new Tuple<string,string>( "4512","河池市"),
                new Tuple<string,string>( "4513","来宾市"),
                new Tuple<string,string>( "4514","崇左市"),
                new Tuple<string,string>( "4521","南宁地区"),
                new Tuple<string,string>( "4522","柳州地区"),
                new Tuple<string,string>( "4524","贺州地区"),
                new Tuple<string,string>( "4526","百色地区"),
                new Tuple<string,string>( "4527","河池地区"),
                new Tuple<string,string>( "4601","海口市"),
                new Tuple<string,string>( "4602","三亚市"),
                new Tuple<string,string>( "4690","省直辖县级行政单位"),
                new Tuple<string,string>( "5001","重庆市市辖区"),
                new Tuple<string,string>( "5002","重庆市县"),
                new Tuple<string,string>( "5003","重庆市(市)"),
                new Tuple<string,string>( "5101","成都市"),
                new Tuple<string,string>( "5103","自贡市"),
                new Tuple<string,string>( "5104","攀枝花市"),
                new Tuple<string,string>( "5105","泸州市"),
                new Tuple<string,string>( "5106","德阳市"),
                new Tuple<string,string>( "5107","绵阳市"),
                new Tuple<string,string>( "5108","广元市"),
                new Tuple<string,string>( "5109","遂宁市"),
                new Tuple<string,string>( "5110","内江市"),
                new Tuple<string,string>( "5111","乐山市"),
                new Tuple<string,string>( "5113","南充市"),
                new Tuple<string,string>( "5114","眉山市"),
                new Tuple<string,string>( "5115","宜宾市"),
                new Tuple<string,string>( "5116","广安市"),
                new Tuple<string,string>( "5117","达州市"),
                new Tuple<string,string>( "5118","雅安市"),
                new Tuple<string,string>( "5119","巴中市"),
                new Tuple<string,string>( "5120","资阳市"),
                new Tuple<string,string>( "5130","达川地区"),
                new Tuple<string,string>( "5131","雅安地区"),
                new Tuple<string,string>( "5132","阿坝藏族羌族自治州"),
                new Tuple<string,string>( "5133","甘孜藏族自治州"),
                new Tuple<string,string>( "5134","凉山彝族自治州"),
                new Tuple<string,string>( "5137","巴中地区"),
                new Tuple<string,string>( "5138","眉山地区"),
                new Tuple<string,string>( "5139","眉山地区资阳地区"),
                new Tuple<string,string>( "5201","贵阳市"),
                new Tuple<string,string>( "5202","六盘水市"),
                new Tuple<string,string>( "5203","遵义市"),
                new Tuple<string,string>( "5204","安顺市"),
                new Tuple<string,string>( "5222","铜仁地区"),
                new Tuple<string,string>( "5223","黔西南布依族苗族自治州"),
                new Tuple<string,string>( "5224","毕节地区"),
                new Tuple<string,string>( "5225","安顺地区"),
                new Tuple<string,string>( "5226","黔东南苗族侗族自治州"),
                new Tuple<string,string>( "5227","黔南布依族苗族自治州"),
                new Tuple<string,string>( "5301","昆明市"),
                new Tuple<string,string>( "5303","曲靖市"),
                new Tuple<string,string>( "5304","玉溪市"),
                new Tuple<string,string>( "5305","保山市"),
                new Tuple<string,string>( "5306","昭通市"),
                new Tuple<string,string>( "5307","丽江市"),
                new Tuple<string,string>( "5308","思茅市"),
                new Tuple<string,string>( "5309","临沧市"),
                new Tuple<string,string>( "5321","昭通地区"),
                new Tuple<string,string>( "5323","楚雄彝族自治州"),
                new Tuple<string,string>( "5325","红河哈尼族彝族自治州"),
                new Tuple<string,string>( "5326","文山壮族苗族自治州"),
                new Tuple<string,string>( "5327","思茅地区"),
                new Tuple<string,string>( "5328","西双版纳傣族自治州"),
                new Tuple<string,string>( "5329","大理白族自治州"),
                new Tuple<string,string>( "5330","保山地区"),
                new Tuple<string,string>( "5331","德宏傣族景颇族自治州"),
                new Tuple<string,string>( "5332","丽江地区"),
                new Tuple<string,string>( "5333","怒江傈僳族自治州"),
                new Tuple<string,string>( "5334","迪庆藏族自治州"),
                new Tuple<string,string>( "5335","临沧地区"),
                new Tuple<string,string>( "5401","拉萨市"),
                new Tuple<string,string>( "5421","昌都地区"),
                new Tuple<string,string>( "5422","山南地区"),
                new Tuple<string,string>( "5423","日喀则地区"),
                new Tuple<string,string>( "5424","那曲地区"),
                new Tuple<string,string>( "5425","阿里地区"),
                new Tuple<string,string>( "5426","林芝地区"),
                new Tuple<string,string>( "6101","西安市"),
                new Tuple<string,string>( "6102","铜川市"),
                new Tuple<string,string>( "6103","宝鸡市"),
                new Tuple<string,string>( "6104","咸阳市"),
                new Tuple<string,string>( "6105","渭南市"),
                new Tuple<string,string>( "6106","延安市"),
                new Tuple<string,string>( "6107","汉中市"),
                new Tuple<string,string>( "6108","榆林市"),
                new Tuple<string,string>( "6109","安康市"),
                new Tuple<string,string>( "6110","商洛市"),
                new Tuple<string,string>( "6124","安康地区"),
                new Tuple<string,string>( "6125","商洛地区"),
                new Tuple<string,string>( "6127","榆林地区"),
                new Tuple<string,string>( "6201","兰州市"),
                new Tuple<string,string>( "6202","嘉峪关市"),
                new Tuple<string,string>( "6203","嘉峪关市金昌市"),
                new Tuple<string,string>( "6204","白银市"),
                new Tuple<string,string>( "6205","天水市"),
                new Tuple<string,string>( "6206","武威市"),
                new Tuple<string,string>( "6207","张掖市"),
                new Tuple<string,string>( "6208","平凉市"),
                new Tuple<string,string>( "6209","酒泉市"),
                new Tuple<string,string>( "6210","庆阳市"),
                new Tuple<string,string>( "6211","定西市"),
                new Tuple<string,string>( "6212","陇南市"),
                new Tuple<string,string>( "6221","酒泉地区"),
                new Tuple<string,string>( "6222","张掖地区"),
                new Tuple<string,string>( "6223","武威地区"),
                new Tuple<string,string>( "6224","定西地区"),
                new Tuple<string,string>( "6226","陇南地区"),
                new Tuple<string,string>( "6227","平凉地区"),
                new Tuple<string,string>( "6228","庆阳地区"),
                new Tuple<string,string>( "6229","临夏回族自治州"),
                new Tuple<string,string>( "6230","甘南藏族自治州"),
                new Tuple<string,string>( "6301","西宁市"),
                new Tuple<string,string>( "6321","海东地区"),
                new Tuple<string,string>( "6322","海北藏族自治州"),
                new Tuple<string,string>( "6323","黄南藏族自治州"),
                new Tuple<string,string>( "6325","海南藏族自治州"),
                new Tuple<string,string>( "6326","果洛藏族自治州"),
                new Tuple<string,string>( "6327","玉树藏族自治州"),
                new Tuple<string,string>( "6328","海西蒙古族藏族自治州"),
                new Tuple<string,string>( "6401","银川市"),
                new Tuple<string,string>( "6402","石嘴山市"),
                new Tuple<string,string>( "6403","吴忠市"),
                new Tuple<string,string>( "6404","固原市"),
                new Tuple<string,string>( "6405","中卫市"),
                new Tuple<string,string>( "6422","固原地区"),
                new Tuple<string,string>( "6501","乌鲁木齐市"),
                new Tuple<string,string>( "6502","克拉玛依市"),
                new Tuple<string,string>( "6521","吐鲁番地区"),
                new Tuple<string,string>( "6522","哈密地区"),
                new Tuple<string,string>( "6523","昌吉回族自治州"),
                new Tuple<string,string>( "6527","博尔塔拉蒙古自治州"),
                new Tuple<string,string>( "6528","巴音郭楞蒙古自治州"),
                new Tuple<string,string>( "6529","阿克苏地区"),
                new Tuple<string,string>( "6530","克孜勒苏柯尔克孜自治州"),
                new Tuple<string,string>( "6531","喀什地区"),
                new Tuple<string,string>( "6532","和田地区"),
                new Tuple<string,string>( "6540","伊犁哈萨克自治州"),
                new Tuple<string,string>( "6541","伊犁哈萨克自治州伊犁地区"),
                new Tuple<string,string>( "6542","塔城地区"),
                new Tuple<string,string>( "6543","阿勒泰地区"),
                new Tuple<string, string>( "6590","直辖县级行政单位")
            };
        
        /// <summary>
        /// 根据身份证获取省市
        /// </summary>
        /// <param name="identityNumber">身份证</param>
        /// <returns></returns>
        public static string GetProvinceDicList(string identityNumber)
        {
            if (identityNumber.Length < 4) 
            {
                return "";
            }
            string sValue = "";
            if (ProvinceDic.TryGetValue(identityNumber.Substring(0, 2), out sValue))
            {
                var city=PityDic.Find(x => x.Item1 == identityNumber.Substring(0, 4));
                if (city != null)
                {
                    sValue += city.Item2;
                }
                else { }
                return sValue;
            }
            else 
            {
                return "";
            }
        }
        #endregion

        #region 身份证号判断性别、出生日期
        /// <summary>
        /// 身份证号判断性别、出生日期
        /// </summary>
        /// <param name="code">身份证号</param>
        /// <param name="type">type=0出生日期；=1获取性别</param>
        /// <returns></returns>
        public static string GetBirthdayAndSex(string code, int type)
        {
            try
            {
                code = code.Trim();
                if (code.Length != 15 && code.Length != 18)
                {
                    throw new Exception("身份证格式错误；身份证号：" + code);
                }
                if (type == 0)
                {
                    if (code.Length == 15)
                        return $"19{code.Substring(6, 2)}-{code.Substring(8, 2)}-{code.Substring(10, 2)}";
                    else
                        return $"{code.Substring(6, 4)}-{code.Substring(10, 2)}-{ code.Substring(12, 2)}";
                }
                else
                {
                    int sex;
                    if (code.Length == 15)
                        sex = Convert.ToInt32(code.Substring(12, 3));
                    else
                        sex = Convert.ToInt32(code.Substring(14, 3));
                    if (sex % 2 == 0)
                    {
                        return "女";
                    }
                    else
                    {
                        return "男";
                    }
                }
            }
            catch { return ""; }
        }
        #endregion
    }
}
