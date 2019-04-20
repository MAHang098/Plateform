﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model.PO
{

    public class PoLineListInParameter
    {
        /// <summary>
        /// 必需。PO业务领域：E-工程领域 P-生产领域 G-综合领域
        /// </summary>
        public string PoSubType { get; set; }
        /// 必须。状态类别，固定为COL_TASK_STATUS
        /// </summary>
        public string StatusType { get; set; }
        /// <summary>
        /// 非必须。订单状态，all-全部，NEW-新下订单，OPEN-在途订单，CANCELLED-取消的订单，CLOSED-关闭的订单，CLOSED_FOR_RECEVING-已交货未关闭的订单
        /// </summary>
        public string ShipmentStatus { get; set; }
        /// <summary>
        /// 非必需。订单签返状态。"before_signed_back：表示未签返， signed_back：表示已签返，   WARN：表示警告
        /// </summary>
        public string PoStatus { get; set; }

        /// <summary>
        /// 任务单状态,非必需
        /// </summary>
        public string ColTaskOrPoStatus { get; set; }

        /// <summary>
        /// 是否只看交期变更，非必需
        /// </summary>
        public bool? HasChangeDelivery { get; set; }
        /// <summary>
        /// Item编码，非必需。最多10个，多个时用半角逗号隔开
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 华为子公司ID，非必需。最多10个，多个时用半角逗号隔开
        /// </summary>
        public string OrgId { get; set; }
        /// <summary>
        /// PO号，非必需
        /// </summary>
        public string PoNumber { get; set; }
        /// <summary>
        /// 承诺开始日期,非必需,Data Format：YYYY-MM-dd
        /// </summary>
        public string PromiseDateStart { get; set; }
        /// <summary>
        /// 承诺结束日期,非必需,Data Format：YYYY-MM-dd
        /// </summary>
        public string PromiseDateEnd { get; set; }
        /// <summary>
        /// 发布开始日期,非必需,Data Format：YYYY-MM-dd
        /// </summary>
        public string PublishDateStart { get; set; }
        /// <summary>
        /// 发布结束日期,非必需,Data Format：YYYY-MM-dd
        /// </summary>
        public string PublishDateEnd { get; set; }

        /// <summary>
        /// 采购模式，非必需.取值为：all, T_DUN,DUN, Normal, PO_Consignment,VMI_Consignment,  VCI_CA, VCI_PO,  VMI_TSN,   VMI_VRN,   Candyman
        /// </summary>
        public string BusinessMode { get; set; }
        /// <summary>
        /// 厂家型号，非必需
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// 采购协议号，非必需
        /// </summary>
        public string SubcontractNo { get; set; }
        /// <summary>
        /// 站点信息，非必需
        /// </summary>
        public string SiteInfo { get; set; }
        /// <summary>
        /// 工程号，非必需
        /// </summary>
        public string EngineeringNoLike { get; set; }
        /// <summary>
        /// 项目编码，非必需
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 工程名称，非必需
        /// </summary>
        public string EngName { get; set; }
        /// <summary>
        /// 项目名称，非必需
        /// </summary>
        public string EngInfoEngineeringName { get; set; }
        /// <summary>
        /// 超过X天没处理，非必需
        /// </summary>
        public int? MoreXDaysUndeal { get; set; }
        /// <summary>
        /// 生产厂家，非必需
        /// </summary>
        public string ManufactureSiteInfo { get; set; }
        /// <summary>
        /// 采购员，非必需
        /// </summary>
        public string AgentEmployeeNumber { get; set; }
        /// <summary>
        /// 是否包含VCICA，非必需 ，生产专用
        ///'新订单：includeVCICA=-1； vci-ca订单 :includeVCICA=1;不传PO和CA都查出来。
        /// </summary>
        public int? IncludeVCICA { get; set; }
        /// <summary>
        /// lineLocationId集合，非必需
        /// </summary>
        public List<long> LineLocationIdList { get; set; }
        /// <summary>
        /// instanceId 集合，非必需
        /// </summary>
        public List<int> InstanceIdList { get; set; }
        /// <summary>
        /// 最后更新时间-开始，非必需，Data Format：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string LastUpdateDateStart { get; set; }
        /// <summary>
        /// 最后更新时间-结束，非必需
        /// </summary>
        public string LastUpdateDateEnd { get; set; }
        /// <summary>
        /// 构造函数，提供默认值
        /// </summary>
        public PoLineListInParameter()
        {
            PoSubType = "P";
            ShipmentStatus = "all";
            PoStatus = "all";
            StatusType = "COL_TASK_STATUS";
            HasChangeDelivery = null;
            MoreXDaysUndeal = null;
            IncludeVCICA = null;
        }

    }


    public class PoLineListOutputParameter
    {
        public FindPoLineListPageVO PageVO { get; set; }
        public FindPoLineListResult[] Results { get; set; }
       
    }

    public class FindPoLineListPageVO
    {
        public int TotalRows { get; set; }
        public int CurPage { get; set; }
        public int PageSize { get; set; }
        public int ResultMode { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int MysqlStartIndex { get; set; }
        public int MysqlEndIndex { get; set; }
        public int TotalPages { get; set; }
    }

    public class FindPoLineListResult
    {
        /// <summary>
        /// PO发运行ID
        /// </summary>
        public int LineLocationId { get; set; }
        /// <summary>
        /// ERP服务器ID
        /// </summary>
        public int InstanceId { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 附件数量
        /// </summary>
        public float AttachmentQty { get; set; }
        /// <summary>
        /// 招标区域
        /// </summary>
        public string BiddingArea { get; set; }
        /// <summary>
        /// 开票地址
        /// </summary>
        public string BillToLocation { get; set; }
        /// <summary>
        /// 采购模式
        /// </summary>
        public EBussinessMode BusinessMode { get; set; }
        /// <summary>
        /// 发运方式
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// 品类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// 未交付数量
        /// </summary>
        public float DueQty { get; set; }
        /// <summary>
        /// 工程号
        /// </summary>
        public string EngineeringNo { get; set; }
        /// <summary>
        /// 产品大类
        /// </summary>
        public string EngInfo { get; set; }
        /// <summary>
        /// 母局
        /// </summary>
        public string EngInfoCentralZone { get; set; }
        /// <summary>
        /// 项目名称或工程名称
        /// </summary>
        public string EngInfoEngineeringName { get; set; }
        /// <summary>
        /// 销售合同号
        /// </summary>
        public string EngInfoSalesContractNo { get; set; }
        /// <summary>
        /// 例外信息释放时间
        /// </summary>
        public DateTime ExceptionUpdatedDate { get; set; }
        /// <summary>
        /// 订单有效日期
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// 首次承诺日期
        /// </summary>
        public DateTime FirstPromiseDate { get; set; }
        /// <summary>
        /// 开票单位
        /// </summary>
        public string IssuOffice { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        /// <summary>
        /// 物料/服务编码版本
        /// </summary>
        public string ItemRevision { get; set; }
        /// <summary>
        /// 生产：生产厂家， 工程：站点信息，格式：站点ID<!>站点编码<!>站点名称
        /// </summary>
        public string ManufactureSiteInfo { get; set; }
        /// <summary>
        /// 服务结束日期或完工日期
        /// </summary>
        public DateTime NeedByDate { get; set; }
        /// <summary>
        /// 接受时间
        /// </summary>
        public DateTime OpenDate { get; set; }
        /// <summary>
        /// 待签返任务单数量
        /// </summary>
        public float OpenTaskQuantity { get; set; }
        /// <summary>
        /// 总任务单数量
        /// </summary>
        public float TaskQuantity { get; set; }
        /// <summary>
        /// 华为子公司
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 厂家型号
        /// </summary>
        public string partNumber { get; set; }
        /// <summary>
        /// 支付条款
        /// </summary>
        public string PaymentTerms { get; set; }
        /// <summary>
        /// 给验收人备注
        /// </summary>
        public string PllaNoteToReceiver { get; set; }
        /// <summary>
        /// 订单行号
        /// </summary>
        public string PoLineNum { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string PoNumber { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string PriceOverride { get; set; }
        /// <summary>
        /// PR号
        /// </summary>
        public string PrNumber { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 承诺日期(页面显示为需求日期)
        /// </summary>
        public DateTime PromiseDate { get; set; }
        /// <summary>
        /// 订单下发日期
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 代表处
        /// </summary>
        public string RepOfficeName { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string RevisionNum { get; set; }
        /// <summary>
        /// 发运行号
        /// </summary>
        public string ShipmentNum { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EShipmentStatus ShipmentStatus { get; set; }
        /// <summary>
        /// 收货地点
        /// </summary>
        public string ShipToLocation { get; set; }
        /// <summary>
        /// 收货地点code
        /// </summary>
        public string ShipToLocationCode { get; set; }
        /// <summary>
        /// 开工日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 采购协议号
        /// </summary>
        public string SubcontractNo { get; set; }
        /// <summary>
        /// 子项目编码
        /// </summary>
        public string SubProjectCode { get; set; }
        /// <summary>
        /// 任务令
        /// </summary>
        public string TaskNum { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public string TaxRate { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string TermsMode { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfMeasure { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// 给供应商备注（特殊备注）
        /// </summary>
        public string VendorShortText { get; set; }
        /// <summary>
        /// 华为型号
        /// </summary>
        public string Hwm { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 取消数量
        /// </summary>
        public string QuantityCancelled { get; set; }
        /// <summary>
        /// 已收到数量
        /// </summary>
        public string QuantityReceived { get; set; }
        /// <summary>
        /// 变更List
        /// </summary>
        public string ObjectChangeContext { get; set; }
    }


    #region Enums
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

    #endregion
}

