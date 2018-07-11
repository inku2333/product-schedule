using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    public class Building
    {
        public int ID { get; set; }   //  楼栋 ID
        public string Name { get; set; }     // 楼栋名称

        public List<Component> Components;         // 一个楼栋中会含有多种构件 
        public DateTime CompletedTime { get; set; }       //  期望完成的时间
    }
}
