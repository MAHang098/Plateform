using HWb2bAccess.DAL;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.BLL
{
    public class PoBLL
    {
        PoDAL dal = new PoDAL();
        public string ErrorCode { get { return dal.ErrorCode; } }
        public string ErrorMsg { get { return dal.ErrorMsg; } }
        public PoBLL()
        {
              
        }
        /// <summary>
        /// 查询华为订单
        /// </summary>
        /// <param name="pageNum">第几页。由于总页数未知，可以从第1页遍历到第n页，当查不出数据时就是最后一页</param>
        /// <param name="poStatus">PO签返标志</param>
        /// <param name="poSubType">PO业务领域</param>
        /// <param name="shipmentStatus">订单状态</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>PoLineListOutput订单查询输出参数，其中PageVO包含由数据数量信息，Results包含返回的数据集</returns>
        public PoLineListOutput GetPoLineList(int pageNum,EPoStatus poStatus=EPoStatus.all, EPoSubType poSubType=EPoSubType.P, EShipmentStatus shipmentStatus=EShipmentStatus.all,  int pageSize = 100)
        {
            PoLineListInput param = new PoLineListInput
            {
                poStatus = poStatus.ToString(),
                poSubType = poSubType.ToString(),
                shipmentStatus = shipmentStatus.ToString()
            };

            PoLineListOutput output = dal.GetPoLineList(param, pageNum, pageSize);
            return output;
        }
        /// <summary>
        /// 下载订单的PDF文件
        /// </summary>
        /// <param name="outFile">输出文件名</param>
        /// <param name="showPrice">是否显示价格</param>
        /// <param name="lang">语言版本</param>
        /// <param name="erpId">ERPID,默认1</param>
        /// <param name="poNums">待输出的PO行 PoLineToDownload</param>
        /// <returns></returns>
        public bool DownloadPoPdf(string outFile,bool showPrice,ELang lang=ELang.zh_CN,EInstanceId erpId=EInstanceId.Huawei,params string[] poNums)
        {
            List<PoLineToDownload> poLines = new List<PoLineToDownload>();
            foreach (var p in poNums)
            {
                poLines.Add(new PoLineToDownload { instanceId = erpId, poNumber = p });
            }

            bool ret = false;
            GenPoPdfInput input = new GenPoPdfInput
            {
                lang = lang.ToString(),
                queryHistoryDB = 0,
                showPriceFlag = showPrice,
                lines = poLines.ToArray()
            };
            var res = dal.GenPoPdfDAL(input);
            if(res!=null)
            {
                if(res.Success)
                {
                    string fileKey = res.PreUrl;
                    DownloadBLL downloadBLL = new DownloadBLL();
                    FileDownloadInput downLoadParam = new FileDownloadInput
                    {
                        downloadType = "1",
                        downloadKey = res.PreUrl
                    };
                    ret = downloadBLL.DownloadToFile(outFile, downLoadParam);
                }
            }
            return ret;
        }

        public bool SignBackPoList(SignBackPoListInput input)
        {
            var res = dal.SignBackPoList(input);
            return res.Success;
        }
       


        #region Restsharp版
        /// <summary>
        /// 查询华为订单,Restsharp方式
        /// </summary>
        /// <param name="pageNum">第几页。由于总页数未知，可以从第1页遍历到第n页，当查不出数据时就是最后一页</param>
        /// <param name="poStatus">PO签返标志</param>
        /// <param name="poSubType">PO业务领域</param>
        /// <param name="shipmentStatus">订单状态</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>PoLineListOutput订单查询输出参数，其中PageVO包含由数据数量信息，Results包含返回的数据集</returns>
        public PoLineListOutput GetPoLineListRest(int pageNum, EPoStatus poStatus = EPoStatus.all, EPoSubType poSubType = EPoSubType.P, EShipmentStatus shipmentStatus = EShipmentStatus.all, int pageSize = 10)
        {
            PoLineListInput param = new PoLineListInput
            {
                poStatus = poStatus.ToString(),
                poSubType = poSubType.ToString(),
                shipmentStatus = shipmentStatus.ToString()
            };
            PoLineListOutput output = dal.GetPoLineListRest(param, pageNum, pageSize);
            return output;
        } 
        #endregion
    }
}
