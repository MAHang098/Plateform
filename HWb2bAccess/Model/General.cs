using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model
{
    /// <summary>
    /// 系统帐套标识，1，华技，2，巴西，4，聚信
    /// </summary>
    public enum EInstanceId
    {
        /// <summary>
        /// 华技=1
        /// </summary>
        Huawei=1,
        /// <summary>
        /// 巴西=2
        /// </summary>
        Huawei_Brazil=2,
        /// <summary>
        /// 聚信=4
        /// </summary>
        Huawei_Juxin=4
    }
    /// <summary>
    /// 语言版本
    /// </summary>
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
        before_signe_back,
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

    /// <summary>
    /// 操作类型.”0“就是还未签返的,”1“是所有的，包括已经签返的和没有签返的；”2“表示已签返的。
    /// </summary>
    public enum EOperationFlag
    {
        NotSignBack =0,
        All=1,
        SignedBack=2
    }
    /// <summary>
    /// 订单签返意见。 "accept"是接受；"reject"是驳回
    /// </summary>
    public enum ESignBackOperation
    {
        accept, reject
    }
    /// <summary>
    /// PO代办类型
    /// </summary>
    public enum EBusinessType
    {
        /// <summary>
        /// 新订单
        /// </summary>
        new_po,//：“新订单”, sign back new PO tasks.（或者传huaweiPublishOrder）
        /// <summary>
        /// 待供应商确认交期修改
        /// </summary>
        po_promise_date_change,//：“待供应商确认交期修改”, sign back promise date changes.Production&General.（或者传huaweiApplyRequredDateChange）
        /// <summary>
        /// PO取消待供应商确认
        /// </summary>
        po_cancel_confirm,//：“PO取消待供应商确认”, sign back will cancel POs.Production&General.（或者传huaweiApplyCancelOrder）
        /// <summary>
        /// PO取消通知
        /// </summary>
        po_cancel_notice,//：“PO取消通知”, sign back cancelled POs.（或者传huaweiNotifyCancelOrder）
        /// <summary>
        /// PO内容变更通知
        /// </summary>
        po_content_change_notice,//：“PO内容变更通知”, sign back PO chagnes.（或者传huaweiNotifyOrderChange）
        /// <summary>
        /// RMA换货通知
        /// </summary>
        po_RMA_replacement_notice//：“RMA换货通知”, sign back the RMA POs.Production&General.（或者传huaweiNotifyRMAChange）
    }
}
