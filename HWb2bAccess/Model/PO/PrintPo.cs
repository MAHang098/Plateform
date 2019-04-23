using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model.PO
{
   
    public class GenPoPdfInput
    {
        /// <summary>
        /// 是否显示金额，默认：显示
        /// </summary>
        public bool showPriceFlag { get; set; }
        /// <summary>
        /// 是否查询归档PO，默认不查
        /// </summary>
        public int queryHistoryDB { get; set; }
        /// <summary>
        /// 文本语言： en_US,zhCN,ru_MO,It_IT. 默认：zh_CN
        /// </summary>
        public string lang { get; set; }

        public PoLineToDownload[] lines { get; set; }
        /// <summary>
        /// 用默认值创建输入参数
        /// </summary>
        public GenPoPdfInput()
        {
            showPriceFlag = true;
            queryHistoryDB = 0;
            lang = "zh_CN";            
        }
        public GenPoPdfInput(params PoLineToDownload[] poLineToGenPdfs)
        {
            showPriceFlag = true;
            queryHistoryDB = 0;
            lang = "zh_CN";
            lines = poLineToGenPdfs;
        }
    }

    public class PoLineToDownload
    {
        /// <summary>
        /// ERP服务器ID，系统帐套标识，1，华技，2，巴西，4，聚信
        /// </summary>
        public EInstanceId instanceId { get; set; }
        public string poNumber { get; set; }
        public PoLineToDownload()
        {
            instanceId = EInstanceId.Huawei;//默认使用华技ERP服务器
        }
    }

    public class GenPoPdfOutParameter
    {
        public string HttpCode { get; set; }
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// {success:true,message:"the file key"}   //根据key获得文件
        /// {success:false, message:"the failture message"}
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 文件下载地址 需要传入download服务的downloadKey参数进行下载
        /// </summary>
        public string PreUrl { get; set; }

    }
}
