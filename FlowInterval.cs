using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    // 流水节拍 类
    public class FlowInterval
    {
        public double wWallBeat { get; set; }    //  外墙节拍,单位小时
        public double wnWallBeat { get; set; }    //  外墙生产内墙节拍,单位小时
        public double nWallBeat { get; set; }    //  内墙节拍,单位小时
        public double gWallBeat { get; set; }    //  固定模台节拍,单位小时
        public double gwWallBeat { get; set; }    //  固定模台生产外墙节拍,单位小时
        public double gnWallBeat { get; set; }    //  固定模台生产内墙节拍,单位小时
        public double ltWallBeat { get; set; }    //  楼梯节拍，单位小时***

    }
}
