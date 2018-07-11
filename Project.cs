using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    // 项目  类
    public class Project
    {
        public int ID { get; set; }   //  项目 ID
        public string Name { get; set; }     // 项目名称
       
        public List<Building> Buildings;         // 一个项目中会含有多种楼栋      
        public DateTime CompletedTime { get; set; }       //  期望完成的时间
    }
}
