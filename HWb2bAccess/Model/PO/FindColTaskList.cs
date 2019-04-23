using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model.PO
{
    public class FindColTaskListInput
    {
        /// <summary>
        /// 必需，PO发运行ID
        /// </summary>
        public string lineLocationId { get; set; }
        /// <summary>
        /// 必需，系统帐套标识，1，华技，2，巴西，4，聚信
        /// </summary>
        public EInstanceId instanceId { get; set; }
        /// <summary>
        /// 选择类型，”0“就是还未签返的,”1“是所有的，包括已经签返的和没有签返的；”2“表示已签返的。
        /// </summary>
        public string selectType { get; set; }
        
        /// <summary>
        /// 任务单ID
        /// </summary>
        public string taskId { get; set; }

    }
    public class FindColTaskListOutput
    {
        /// <summary>
        /// 任务单ID，主键
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// PO号
        /// </summary>
        public string PoNumber { get; set; }
        /// <summary>
        /// 采购员工号
        /// </summary>
        public string AgentEmployeeNumber { get; set; }
        /// <summary>
        /// PO发运行ID
        /// </summary>
        public long LineLocationId { get; set; }
        /// <summary>
        /// PO行号
        /// </summary>
        public string PoLineNum { get; set; }
        /// <summary>
        /// 系统帐套标识，1，华技，2，巴西，4，聚信
        /// </summary>
        public EInstanceId InstanceId { get; set; }
        /// <summary>
        /// 当前处理人
        /// </summary>
        public string CurrentHandler { get; set; }
        /// <summary>
        /// 最后处理人
        /// </summary>
        public string LastHandler { get; set; }
        /// <summary>
        /// 变更字段
        /// </summary>
        public string ChangeColumnName { get; set; }
        /// <summary>
        /// 变更前内容
        /// </summary>
        public string ChangePreContent { get; set; }
        /// <summary>
        /// 变更后内容
        /// </summary>
        public string ChangeAfterContent { get; set; }
        /// <summary>
        /// 变更理由
        /// </summary>
        public string OpenRemark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string CloseRemark { get; set; }
        /// <summary>
        /// 任务单状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 操作类型.”0“就是还未签返的,”1“是所有的，包括已经签返的和没有签返的；”2“表示已签返的。
        /// </summary>
        public EOperationFlag OperationFlag { get; set; }

    }
}
