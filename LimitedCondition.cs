using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    public class LimitedCondition
    {
        public FlowInterval Flowinterval;            // 流水节拍,单位小时
        public double LiftingTime { get; set; }      // 构件吊装时间
        public string LiftingInstallPlan { get; set; }      // 构件吊装安装进度计划
        public double CacheTime { get; set; }      // 构件缓存时间
        public double StockTime { get; set; }      // 构件库存时间
        public double TransportTime { get; set; }           // 构件运输时间
        public double StrengthIncreaseTime { get; set; }      // 构件强度增长时间
        public double ProductionCycle { get; set; }           // 构件模具生产周期
        
        public double TimeSlot { get; set; }      // 时间间隙
        public double YardArea { get; set; }      // 堆场面积 

        public int SCXw { get; set; }//外墙流水线数
        public int SCXn { get; set; }//内墙流水线数
        public int SCXg { get; set; }//固定模台流水线数
        public int SCXlt { get; set; }//楼梯流水线数***

        public int wWorkPositionAmount { get; set; }        // 外墙流水线工位数量
        public int nWorkPositionAmount { get; set; }        // 内墙流水线工位数量
        public int gWorkPositionAmount { get; set; }        // 固定模台流水线工位数量
        public int ltWorkPositionAmount { get; set; }        // 楼梯流水线工位数量***
    }
}
