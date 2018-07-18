using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    // 构件  类
    public class Component
    {
        public int Floor { get; set; }         //   楼层
        public int Category { get; set; }        //  种类 1-10
        //分类：***
        //      外墙：1.三明治外墙。
        //      内墙：2.叠合板，3.叠合梁，4.空调板，5.单外墙，6.内墙，7.柱子。
        //      固定：8.阳台板，9.飘窗。
        //      楼梯：10.楼梯。
        public int Amount { get; set; }              //  数量
    }
}
