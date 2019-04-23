using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model
{
    public class FileDownloadInput
    {
        /// <summary>
        /// 标识下载方式(当前只支持值为1：表示后台通过url方式下载)
        /// </summary>
        public string downloadType { get; set; }
        /// <summary>
        /// url参数(当downloadType为其他值时，该值代表其他系统中文件下载的参数，但目前只开放通过url方式下载)
        /// 固定为"downloadKey":"iscp/lpm/services/labelService/previewLabel/595defcce4b0fdc2fa03d984"
        /// </summary>
        public string downloadKey { get; set; }
        public FileDownloadInput()
        {
            downloadType = "1";
            downloadKey = "iscp/lpm/services/labelService/previewLabel/595defcce4b0fdc2fa03d984";
        }
        
    }

}
