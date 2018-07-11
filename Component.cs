using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    // 构件  类
    public class Component
    {
        public int Floor { get; set; }         //楼层
        public int Category { get; set; }        //  种类 外/内/固定/楼分别以1,2,3,4代替
        //分类：***
        //      1.外墙：三明治外墙。
        //      2.内墙：叠合板，叠合梁，空调板，单外墙，内墙，柱子。
        //      3.固定：阳台板，飘窗。
        //      4.楼梯：楼梯。
        public int Amount { get; set; }              //  数量
    }
}
