using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model
{
   
    public enum ELang
    {
        /// <summary>
        /// English
        /// </summary>
        en_US,
        /// <summary>
        /// 中文
        /// </summary>
        zh_CN,
        /// <summary>
        /// Russian
        /// </summary>
        ru_MO,
        /// <summary>
        /// Italian
        /// </summary>
        it_IT

    }
    /// <summary>
    /// PO签返标志
    /// </summary>
    public enum EPoStatus
    {
        all,
        /// <summary>
        /// 未签返
        /// </summary>
        before_signed_back,
        /// <summary>
        /// 已签返
        /// </summary>
        signed_back,
        WARN
    }
    /// <summary>
    /// PO业务领域
    /// </summary>
    public enum EPoSubType
    {
        /// <summary>
        /// 工程领域Engineering
        /// </summary>
        E,
        /// <summary>
        /// 生产领域Production
        /// </summary>
        P,
        /// <summary>
        /// 综合领域General
        /// </summary>
        G
    }
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum EShipmentStatus
    {
        all,
        /// <summary>
        /// 新下订单
        /// </summary>
        NEW,
        /// <summary>
        /// 在途订单
        /// </summary>
        OPEN,
        /// <summary>
        /// 取消的订单
        /// </summary>
        CANCELLED,
        /// <summary>
        /// 关闭的订单
        /// </summary>
        CLOSED,
        /// <summary>
        /// 已交货未关闭的订单
        /// </summary>
        CLOSED_FOR_RECEVING

    }

    public enum EBussinessMode
    {
        all,
        T_DUN,
        DUN,
        Normal,
        PO_Consignment,
        VMI_Consignment,
        VCI_CA,
        VCI_PO,
        VMI_TSN,
        VMI_VRN,
        Candyman
    }
}
