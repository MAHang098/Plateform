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
        /// <returns>PoLineListOutputParameter订单查询输出参数，其中PageVO包含由数据数量信息，Results包含返回的数据集</returns>
        public PoLineListOutputParameter GetPoLineList(int pageNum,EPoStatus poStatus=EPoStatus.all, EPoSubType poSubType=EPoSubType.P, EShipmentStatus shipmentStatus=EShipmentStatus.all,  int pageSize = 100)
        {
            PoLineListInParameter param = new PoLineListInParameter
            {
                PoStatus = poStatus.ToString(),
                PoSubType = poSubType.ToString(),
                ShipmentStatus = shipmentStatus.ToString()
            };

            PoLineListOutputParameter output = dal.GetPoLineList(param, pageNum, pageSize);
            return output;
        }
        /// <summary>
        /// 查询华为订单,Restsharp方式
        /// </summary>
        /// <param name="page">第几页。由于总页数未知，可以从第1页遍历到第n页，当查不出数据时就是最后一页</param>
        /// <param name="poStatus">PO签返标志</param>
        /// <param name="poSubType">PO业务领域</param>
        /// <param name="shipmentStatus">订单状态</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>PoLineListOutputParameter订单查询输出参数，其中PageVO包含由数据数量信息，Results包含返回的数据集</returns>
        public PoLineListOutputParameter GetPoLineListRest(int page, EPoStatus poStatus = EPoStatus.all, EPoSubType poSubType = EPoSubType.P, EShipmentStatus shipmentStatus = EShipmentStatus.all, int pageSize = 10)
        {
            PoLineListInParameter param = new PoLineListInParameter
            {
                PoStatus = poStatus.ToString(),
                PoSubType = poSubType.ToString(),
                ShipmentStatus = shipmentStatus.ToString()
            };
            PoLineListOutputParameter output = dal.GetPoLineListRest(param, page, pageSize);
            return output;
        }
    }
}
