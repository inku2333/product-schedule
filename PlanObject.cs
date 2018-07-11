using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    //  算法返回的对象
    public class PlanObject
    {
        public string BatchID { get; set; }    // 批次号
        public string BatchName { get; set; }    // 批次名称
        public string ProjectName { get; set; }    // 项目名称
        public string Building { get; set; }      // 楼栋
        public string Floor { get; set; }    // 楼层***
        public string ComponentType { get; set; }      // 构件种类
        public string ComponentID { get; set; }        // 构件ID号
        public string FlowLineNumber { get; set; }     // 流水线号
        public DateTime ActualStartTime { get; set; }  // 排产实际开始时间
        public DateTime ActualEndTime { get; set; }    // 排产实际完成时间
    }
}
