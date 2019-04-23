using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.Model.PO
{
    public class SignBackPoListInput
    {
        /// <summary>
        /// 接受还是驳回,"accept"是接受；"reject"是驳回
        /// </summary>
        public string operateType { get; set; }
        public List<ColTaskQuery> colTaskQueries { get; set; }
        public SignBackPoListInput()
        {
            operateType = ESignBackOperation.accept.ToString();
            colTaskQueries = new List<ColTaskQuery>();
        }

    }
    public class SignBackPoListOutput
    {
        public string Code { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
        public SignBackOutData[] Data { get; set; }
        
        
    }

    public class SignBackOutData
    {
        /// <summary>
        /// PO号
        /// </summary>
        public string PoNum { get; set; }
        /// <summary>
        /// PO行号
        /// </summary>
        public string PoLineNum { get; set; }
        /// <summary>
        /// 任务单ID
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// PO发运行ID
        /// </summary>
        public long LineLocationId { get; set; }
        public EInstanceId InstanceId { get; set; }
    }

    public class ColTaskQuery
    {
        /// <summary>
        /// 必需，PO待办类型,固定未new_po
        /// </summary>
        public string businessType { get; set; }
        /// <summary>
        /// 必需，系统帐套标识
        /// </summary>
        public EInstanceId instanceId { get; set; }
        /// <summary>
        /// 必需，PO行ID,取FindPoLineList出参对应字段的值
        /// </summary>
        public int lineLocationId { get; set; }
        /// <summary>
        /// 任务单ID,取FindPoLineList出参对应字段的值
        /// </summary>
        public int? taskId { get; set; }
        /// <summary>
        /// 备注原因
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 用默认值新建实例
        /// </summary>
        public ColTaskQuery()
        {
            businessType = "new_po";
            instanceId = EInstanceId.Huawei;
        }
    }
}
