using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    // 批次 类
    public class BatchTask
    {
        public int  ID { get; set; }   // 批次号
        public  string Name { get; set; }  // 批次名
        public List<Project> Projects;     // 一个批次包含的多个项目信息， 一个项目中会含有多种构件 批次的其他信息
        public DateTime CompletedTime { get; set; }       //  期望完成的时间
    }
}
