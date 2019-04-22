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
        public bool ShowPriceFlag { get; set; }
        /// <summary>
        /// 是否查询归档PO，默认不查
        /// </summary>
        public bool QueryHistoryDB { get; set; }
        /// <summary>
        /// 文本语言： en_US,zhCN,ru_MO,It_IT. 默认：zh_CN
        /// </summary>
        public string Lang { get; set; }

        public PoLineToGenPdf[] Lines { get; set; }
        /// <summary>
        /// 用默认值创建输入参数
        /// </summary>
        public GenPoPdfInput()
        {
            ShowPriceFlag = true;
            QueryHistoryDB = false;
            Lang = "zh_CN";            
        }
        public GenPoPdfInput(params PoLineToGenPdf[] poLineToGenPdfs)
        {
            ShowPriceFlag = true;
            QueryHistoryDB = false;
            Lang = "zh_CN";
            Lines = poLineToGenPdfs;
        }
    }

    public class PoLineToGenPdf
    {
        /// <summary>
        /// ERP服务器ID，华技使用：1
        /// </summary>
        public int InstanceId { get; set; }
        public string PoNumber { get; set; }
        public PoLineToGenPdf()
        {
            InstanceId = 1;//默认使用华技ERP服务器
        }
    }

    public class GenPoPdfOutParameter
    {
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
        public string FileUrl { get; set; }

    }
}
