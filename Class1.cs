using System;
using System.Collections.Generic;
using System.Text;

namespace text
{
    class Class1
    {
        static void Main(String[] args)//底层算法测试
        {
            //构件
            Component c1 = new Component();
            c1.Floor = 1;
            c1.Category = 1;
            c1.Amount = 10;
            Component c2 = new Component();
            c2.Floor = 1;
            c2.Category = 2;
            c2.Amount = 8;
            Component c3 = new Component();
            c3.Floor = 1;
            c3.Category = 3;
            c3.Amount = 2;
            Component c4 = new Component();
            c4.Floor = 1;
            c4.Category = 4;
            c4.Amount = 10;
            Component c11 = new Component();
            c11.Floor = 2;
            c11.Category = 5;
            c11.Amount = 10;
            Component c21 = new Component();
            c21.Floor = 2;
            c21.Category = 6;
            c21.Amount = 10;
            Component c31 = new Component();
            c31.Floor = 2;
            c31.Category = 7;
            c31.Amount = 5;
            Component c41 = new Component();
            c41.Floor = 2;
            c41.Category = 8;
            c41.Amount = 10;
            Component c111 = new Component();
            c111.Floor = 3;
            c111.Category = 9;
            c111.Amount = 5;
            Component c211 = new Component();
            c211.Floor = 3;
            c211.Category = 10;
            c211.Amount = 10;
            Component c311 = new Component();
            c311.Floor = 3;
            c311.Category = 2;
            c311.Amount = 10;
            Component c411 = new Component();
            c411.Floor = 3;
            c411.Category = 4;
            c411.Amount = 10;
            List<Component> c = new List<Component>();
            List<Component> c_2 = new List<Component>();
            c.Add(c1);
            c.Add(c2);
            c.Add(c3);
            c.Add(c4);
            c.Add(c11);
            c.Add(c21);
            c.Add(c31);
            c.Add(c41);
            c_2.Add(c111);
            c_2.Add(c211);
            c_2.Add(c311);
            c_2.Add(c411);
            //楼栋
            Building b1 = new Building();
            b1.ID = 1;
            b1.Name = "楼栋一";
            b1.Components = c;
            b1.CompletedTime = DateTime.Now;
            List<Building> b = new List<Building>();
            b.Add(b1);
            Building b2 = new Building();
            b2.ID = 2;
            b2.Name = "楼栋二";
            b2.Components = c_2;
            b2.CompletedTime = DateTime.Now;
            List<Building> b_2 = new List<Building>();
            b_2.Add(b2);

            //项目
            Project p1 = new Project();
            p1.ID = 1;
            p1.Name = "项目1";
            p1.Buildings = b;
            p1.CompletedTime = DateTime.Now;
            List<Project> p = new List<Project>();
            p.Add(p1);
            Project p2 = new Project();
            p2.ID = 2;
            p2.Name = "项目2";
            p2.Buildings = b_2;
            p2.CompletedTime = DateTime.Now;
            p.Add(p2);
            //批次
            BatchTask bt1 = new BatchTask();
            bt1.ID = 1;
            bt1.Name = "批次一";
            bt1.Projects = p;
            bt1.CompletedTime = DateTime.Now;

            //流水节拍
            FlowInterval F = new FlowInterval();
            F.wWallBeat = 0.7;
            F.wnWallBeat = 0.5;
            F.nWallBeat = 0.8;
            F.gwWallBeat = 0.5;
            F.gnWallBeat = 0.14;
            F.gWallBeat = 0.3;
            F.ltWallBeat = 0.1;

            //约束
            LimitedCondition L = new LimitedCondition();
            L.Flowinterval = F;
            L.SCXw = 1;
            L.SCXn = 1;
            L.SCXg = 1;
            L.SCXlt = 1;
            L.wWorkPositionAmount = 30;
            L.nWorkPositionAmount = 30;
            L.gWorkPositionAmount = 30;
            L.ltWorkPositionAmount = 30;

            //测试
            Class1 text1 = new Class1();
            DateTime now = DateTime.Now;
            int s = text1.MakePlan(L, now, bt1).Count;
            for (int i = 0; i < s; i++)
            {
                Console.WriteLine("生产开始时间：\t"+text1.MakePlan(L, now, bt1)[i].ActualStartTime);
                Console.WriteLine("生产结束：\t" + text1.MakePlan(L, now, bt1)[i].ActualEndTime);
                Console.WriteLine("批次id：\t" + text1.MakePlan(L, now, bt1)[i].BatchID);
                Console.WriteLine("批次名：\t" + text1.MakePlan(L, now, bt1)[i].BatchName);
                Console.WriteLine("楼栋：\t\t" + text1.MakePlan(L, now, bt1)[i].Building);
                Console.WriteLine("构件id：\t" + text1.MakePlan(L, now, bt1)[i].ComponentID);
                Console.WriteLine("构件种类：\t" + text1.MakePlan(L, now, bt1)[i].ComponentType);
                Console.WriteLine("楼层：\t\t" + text1.MakePlan(L, now, bt1)[i].Floor);
                Console.WriteLine("流水线号：\t" + text1.MakePlan(L, now, bt1)[i].FlowLineNumber);
                Console.WriteLine("项目名：\t" + text1.MakePlan(L, now, bt1)[i].ProjectName);
                Console.WriteLine("工位数量：\t" + text1.MakePlan(L, now, bt1)[i].WorkPositionAmount);
                Console.WriteLine();
            }
            Console.WriteLine(s);
            Console.ReadLine();
        }

        //代码风格：暴力流

        /*约束组合，流水节拍，开始时间，每次处理楼层，批次*/
        public List<PlanObject> MakePlan(LimitedCondition L, DateTime starttime, BatchTask B)//makeplan函数
        {
            //创建list
            List<PlanObject> list = new List<PlanObject>();

            /*获取产能*/
            CNSF(L);//产能算法获得产能
            double[] PP = CNSF(L);
            DateTime wtime = starttime;
            DateTime ntime = starttime;
            DateTime gtime = starttime;
            DateTime lttime = starttime;
            DateTime wtemptime;
            DateTime ntemptime;
            DateTime gtemptime;
            DateTime lttemptime;

            /*核心算法函数本体*/
            int pi = B.Projects.Count;
            for (int i = 0; i < pi; i++)
            {
                int bi = B.Projects[i].Buildings.Count;
                for (int j = 0; j < bi; j++)
                {
                    int maxfloor = Maxfloor(B.Projects[i].Buildings[j]);
                    int pid = i;
                    int bid = j;
                    for (int downf = 1; downf <= maxfloor; downf++)//简单易懂
                    {
                        int[] GJ = GJHQ(downf, B, pid, bid);      //分次获取构件

                        double[] gp = new double[13];
                        gp = CoreFunc(GJ, PP, L);              //底层算法，获得生产排序

                        //重置时间，除第一次循环外，wtime为上一轮循环中的重置后的值，下同
                        wtemptime = wtime;
                        ntemptime = ntime;
                        gtemptime = gtime;
                        lttemptime = lttime;

                        //添加list成员


                        for (int k = 0; k < gp[2]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("内墙");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("内墙生产线");
                            plan.WorkPositionAmount = L.nWorkPositionAmount;
                            DateTime d = ntemptime;
                            ntemptime = ntemptime.AddHours(gp[10]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = ntemptime;
                            list.Add(plan);
                        }
                        for (int k = 0; k < gp[1]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("内墙");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("外墙生产线");
                            plan.WorkPositionAmount = L.wWorkPositionAmount;
                            DateTime d = wtemptime;
                            wtemptime = wtemptime.AddHours(gp[9]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = wtemptime;
                            list.Add(plan);
                        }
                        for (int k = 0; k < gp[4]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("内墙");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("固定模台生产线");
                            plan.WorkPositionAmount = L.gWorkPositionAmount;
                            DateTime d = gtemptime;
                            gtemptime = gtemptime.AddHours(gp[12]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = gtemptime;
                            list.Add(plan);
                        }
                        int ci = 0;
                        for (; ci < GJ[4]; ci++)
                        {
                            list[ci].ComponentType = ("叠合板");
                        }
                        for (; ci < GJ[4] + GJ[7]; ci++)
                        {
                            list[ci].ComponentType = ("单外墙");
                        }
                        for (; ci < GJ[4] + GJ[7] + GJ[8]; ci++)
                        {
                            list[ci].ComponentType = ("内墙");
                        }
                        for (; ci < GJ[4] + GJ[7] + GJ[8] + GJ[5]; ci++)
                        {
                            list[ci].ComponentType = ("叠合梁");
                        }
                        for (; ci < GJ[4] + GJ[7] + GJ[8] + GJ[5] + GJ[6]; ci++)
                        {
                            list[ci].ComponentType = ("空调板");
                        }
                        for (; ci < GJ[4] + GJ[7] + GJ[8] + GJ[5] + GJ[6] + GJ[9]; ci++)
                        {
                            list[ci].ComponentType = ("柱子");
                        }


                        for (int k = 0; k < gp[0]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("三明治外墙");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("外墙生产线");
                            plan.WorkPositionAmount = L.wWorkPositionAmount;
                            DateTime d = wtemptime;
                            wtemptime = wtemptime.AddHours(gp[8]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = wtemptime;
                            list.Add(plan);
                        }

                        int tempg = (int)gp[5] - GJ[10];
                        for (int k = 0; k < GJ[10]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("阳台板");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("固定模台生产线");
                            plan.WorkPositionAmount = L.gWorkPositionAmount;
                            DateTime d = gtemptime;
                            gtemptime = gtemptime.AddHours(gp[13]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = gtemptime;
                            list.Add(plan);
                        }
                        for (int k = 0; k < tempg; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("飘窗");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("固定模台生产线");
                            plan.WorkPositionAmount = L.gWorkPositionAmount;
                            DateTime d = gtemptime;
                            gtemptime = gtemptime.AddHours(gp[13]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = gtemptime;
                            list.Add(plan);
                        }

                        for (int k = 0; k < gp[3]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("三明治外墙");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("固定模台生产线");
                            plan.WorkPositionAmount = L.gWorkPositionAmount;
                            DateTime d = gtemptime;
                            gtemptime = gtemptime.AddHours(gp[11]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = gtemptime;
                            list.Add(plan);
                        }

                        for (int k = 0; k < gp[6]; k++)
                        {
                            //生成planobject
                            PlanObject plan = new PlanObject();
                            plan.BatchID = B.ID.ToString();
                            plan.BatchName = B.Name;
                            plan.ProjectName = B.Projects[i].Name;
                            plan.Building = B.Projects[i].Buildings[j].ID.ToString();
                            plan.Floor = downf.ToString();
                            plan.ComponentType = ("楼梯");
                            plan.ComponentID = (k + 1).ToString();
                            plan.FlowLineNumber = ("楼梯生产线");
                            plan.WorkPositionAmount = L.ltWorkPositionAmount;
                            DateTime d = lttemptime;
                            lttemptime = lttemptime.AddHours(gp[14]);
                            plan.ActualStartTime = d;
                            plan.ActualEndTime = lttemptime;
                            list.Add(plan);
                        }

                        ///循环每一轮后将时间置于各生产线生产的末位
                        wtime = wtemptime;
                        ntime = ntemptime;
                        gtime = gtemptime;
                        lttime = lttemptime;
                    }
                }
            }
            return list;
        }
        public double[] CNSF(LimitedCondition L)//产能算法 /*测试完毕无误*/
        {

            double wjp = L.Flowinterval.wWallBeat;//节拍，单位小时
            double wnjp = L.Flowinterval.wnWallBeat;
            double njp = L.Flowinterval.nWallBeat;
            double gjp = L.Flowinterval.gWallBeat;
            double gwjp = L.Flowinterval.gwWallBeat;
            double gnjp = L.Flowinterval.gnWallBeat;
            double ltjp = L.Flowinterval.ltWallBeat;
            int wnsum = L.wWorkPositionAmount;//w工位数量
            int nnsum = L.nWorkPositionAmount;
            int gnsum = L.gWorkPositionAmount;
            int ltnsum = L.ltWorkPositionAmount;
            double ts = L.TimeSlot;//时间间隙，单位小时

            double[] jp = new double[7];//节拍
            jp[0] = wjp;
            jp[1] = wnjp;
            jp[2] = njp;
            jp[3] = gjp;
            jp[4] = gwjp;
            jp[5] = gnjp;
            jp[6] = ltjp;

            int[] gsum = new int[7];//工位数量
            gsum[0] = wnsum;
            gsum[1] = wnsum;
            gsum[2] = nnsum;
            gsum[3] = gnsum;
            gsum[4] = gnsum;
            gsum[5] = gnsum;
            gsum[6] = ltnsum;

            double[] PP = new double[7];

            int sfi = 0;
            for (; sfi < 7; sfi++)
            {
                PP[sfi] = 1 / (jp[sfi] + ts);//时产能 
            }
            return PP;
        }
        public int[] GJHQ(int downf, BatchTask B, int pid, int bid)//获取构件算法 /*测试完毕无误*/
        {
            int[] GJ = new int[12]; //构件数组
            GJ[0] = 0;//1.外墙
            GJ[1] = 0;//内墙
            GJ[2] = 0;//固定模台
            GJ[3] = 0;//10.楼梯
            GJ[4] = 0;//2
            GJ[5] = 0;//3
            GJ[6] = 0;//4
            GJ[7] = 0;//5
            GJ[8] = 0;//6
            GJ[9] = 0;//7
            GJ[10] = 0;//8
            GJ[11] = 0;//9

            int ci = B.Projects[pid].Buildings[bid].Components.Count;
            for (int i = 0; i < ci; i++)
            {
                if (B.Projects[pid].Buildings[bid].Components[i].Floor == downf)
                {
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 1)
                    {
                        GJ[0] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 2)
                    {
                        GJ[4] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 3)
                    {
                        GJ[5] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 10)
                    {
                        GJ[3] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 4)
                    {
                        GJ[6] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 5)
                    {
                        GJ[7] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 6)
                    {
                        GJ[8] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 7)
                    {
                        GJ[9] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 8)
                    {
                        GJ[10] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    if (B.Projects[pid].Buildings[bid].Components[i].Category == 9)
                    {
                        GJ[11] = B.Projects[pid].Buildings[bid].Components[i].Amount;
                    }
                    GJ[1] = GJ[4] + GJ[5] + GJ[6] + GJ[7] + GJ[8] + GJ[9];
                    GJ[2] = GJ[10] + GJ[11];
                }
            }
            return GJ;
        }
        public double min3(double T1, double T2, double T3)//比较最小时间 /*测试完毕无误*/
        {
            double[] score = new double[3];
            score[0] = T1;
            score[1] = T2;
            score[2] = T3;
            double min = T1;
            for (int i = 1; i < 3; i++)
            {
                if (score[i] < min)
                    min = score[i];
            }
            return min;
        }
        public double[] CoreFunc(int[] GJ, double[] PP, LimitedCondition L)//底层算法
        {
            /*四种生产线对应一小时的产能*/
            double P11, P12;
            double P22;
            double P31, P32, P33;
            double P44;

            P11 = PP[0];
            P12 = PP[1];
            P22 = PP[2];
            P31 = PP[3];
            P32 = PP[4];
            P33 = PP[5];
            P44 = PP[6];

            double T1 = GJ[0] / (P11 * L.SCXw); //外墙生产线生产外墙的产能*生产线条数=外墙生产线生产外墙的时间
            double T2 = GJ[1] / (P22 * L.SCXn); //内墙生产线生产内墙的时间
            double T3 = GJ[2] / (P33 * L.SCXg); //固定模台生产线生产异形构件的时间
            double T4 = GJ[3] / (P44 * L.SCXlt); //楼梯生产线生产楼梯的时间

            double T11 = 1 / (P11 * L.SCXw); //外墙生产线生产一个外墙的时间
            double T12 = 1 / (P12 * L.SCXn); //外墙生产线生产一个内墙的时间
            double T22 = 1 / (P22 * L.SCXn); //内墙生产线生产一个内墙的时间
            double T31 = 1 / (P31 * L.SCXg); //固定模台生产线生产一个外墙的时间
            double T32 = 1 / (P32 * L.SCXg); //固定模台生产线生产一个内墙的时间
            double T33 = 1 / (P33 * L.SCXg); //固定模台生产线生产一个异形构件的时间
            double T44 = 1 / (P44 * L.SCXlt); //固定模台生产线生产一个异形构件的时间
            //显示 Console.WriteLine("T11-T44 {0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",T11,T12,T22,T31,T32,T33,T44);

            double Tm;
            double Tmin = min3(T1, T2, T3);

            //各条生产线生产构件的数量;
            double[] gp = new double[15];
            //下15行仅用于参考 以免遗忘意义
            //double g1p1 = gp[0];
            //double g2p1 = gp[1];
            //double g2p2 = gp[2];
            //double g1p3 = gp[3];
            //double g2p3 = gp[4];
            //double g3p3 = gp[5];
            //double g4p4 = gp[6];
            //double shijian = gp[7];
            //double t11 = gp[8];
            //double t12 = gp[9];
            //double t22 = gp[10];
            //double t31 = gp[11];
            //double t32 = gp[12];
            //double t33 = gp[13];
            //double t44 = gp[14];
            //判断与输出
            if (T1 == Tmin)
            {
                if (T2 >= T3)
                {
                    int numtemp1 = (int)((T3 - T1) * (P12 * L.SCXw) + T3 * (P22 * L.SCXn));

                    if (numtemp1 > GJ[1])//GJ[1]是内墙总数;
                    {
                        Tm = T3;
                        gp[0] = GJ[0];
                        gp[1] = (int)((T3 - T1) / T12)+1;
                        gp[2] = GJ[1] - gp[1];
                        gp[3] = 0;
                        gp[4] = 0;
                        gp[5] = GJ[2];
                        gp[6] = GJ[3];
                        gp[7] = Tm;
                        gp[8] = T11;
                        gp[9] = T12;
                        gp[10] = T22;
                        gp[11] = T31;
                        gp[12] = T32;
                        gp[13] = T33;
                        gp[14] = T44;
                        return gp;

                    }
                    else
                    {
                        Tm = T3 + (GJ[1] - numtemp1) / (P12 * L.SCXw + P22 * L.SCXn + P32 * L.SCXg);
                        gp[0] = GJ[0];
                        gp[1] = (int)((Tm - T1) / T12);
                        gp[2] = (int)(Tm / T22);
                        gp[3] = 0;
                        gp[4] = GJ[1]-gp[1]-gp[2];
                        gp[5] = GJ[2];
                        gp[6] = GJ[3];
                        gp[7] = Tm;
                        gp[8] = T11;
                        gp[9] = T12;
                        gp[10] = T22;
                        gp[11] = T31;
                        gp[12] = T32;
                        gp[13] = T33;
                        gp[14] = T44;
                        return gp;
                    }

                }
                else
                {
                    Tm = T3;
                    gp[0] = GJ[0];
                    gp[1] = 0;
                    gp[2] = GJ[1];
                    gp[3] = 0;
                    gp[4] = 0;
                    gp[5] = GJ[2];
                    gp[6] = GJ[3];
                    gp[7] = Tm;
                    gp[8] = T11;
                    gp[9] = T12;
                    gp[10] = T22;
                    gp[11] = T31;
                    gp[12] = T32;
                    gp[13] = T33;
                    gp[14] = T44;
                    return gp;
                }
            }
            if (T2 == Tmin)
            {
                if (T1 >= T3)
                {
                    Tm = T3 + (GJ[0] - T3 * P11 * L.SCXw) / (P11 * L.SCXw + P31 * L.SCXg);
                    gp[0] = (int)(Tm / T11)+1;
                    gp[1] = 0;
                    gp[2] = GJ[1];
                    gp[3] = GJ[0]-gp[0];
                    gp[4] = 0;
                    gp[5] = GJ[2];
                    gp[6] = GJ[3];
                    gp[7] = Tm;
                    gp[8] = T11;
                    gp[9] = T12;
                    gp[10] = T22;
                    gp[11] = T31;
                    gp[12] = T32;
                    gp[13] = T33;
                    gp[14] = T44;
                    return gp;
                }
                else
                {
                    Tm = T3;
                    gp[0] = GJ[0];
                    gp[1] = 0;
                    gp[2] = GJ[1];
                    gp[3] = 0;
                    gp[4] = 0;
                    gp[5] = GJ[2];
                    gp[6] = GJ[3];
                    gp[7] = Tm;
                    gp[8] = T11;
                    gp[9] = T12;
                    gp[10] = T22;
                    gp[11] = T31;
                    gp[12] = T32;
                    gp[13] = T33;
                    gp[14] = T44;
                    return gp;
                }
            }
            if (T3 == Tmin)
            {
                if (T1 <= T2)
                {
                    double numtemp2 = GJ[1] - T3 * P22 * L.SCXn;
                    double Tt = numtemp2 / (P22 * L.SCXn + P32 * L.SCXg);
                    if (Tt <= T1 - T3)
                    {
                        double T5 = T1 - T3 - Tt;
                        Tm = T3 + Tt + (T5 * P11 * L.SCXw) / (P11 * L.SCXw + P31 * L.SCXg);
                        gp[0] = (int)(Tm / T11)+1;
                        gp[1] = 0;
                        gp[2] = (int)((T3 + Tt) / T22)+1;
                        gp[3] = GJ[0]-gp[0];
                        gp[4] = GJ[1]-gp[2];
                        gp[5] = GJ[2];
                        gp[6] = GJ[3];
                        gp[7] = Tm;
                        gp[8] = T11;
                        gp[9] = T12;
                        gp[10] = T22;
                        gp[11] = T31;
                        gp[12] = T32;
                        gp[13] = T33;
                        gp[14] = T44;
                        return gp;
                    }
                    else
                    {
                        Tm = Tt + T3;
                        gp[0] = GJ[0];
                        gp[1] = 0;
                        gp[2] = (int)((T3 + Tt) / T22)+1;
                        gp[3] = 0;
                        gp[4] = GJ[1] - gp[2];
                        gp[5] = GJ[2];
                        gp[6] = GJ[3];
                        gp[7] = Tm;
                        gp[8] = T11;
                        gp[9] = T12;
                        gp[10] = T22;
                        gp[11] = T31;
                        gp[12] = T32;
                        gp[13] = T33;
                        gp[14] = T44;
                        return gp;
                    }
                }
                if (T1 >= T2)
                {
                    double numtemp2 = GJ[0] - T3 * P11 * L.SCXw;
                    double Tt = numtemp2 / (P11 * L.SCXw + P31 * L.SCXg);
                    if (Tt <= T2 - T3)
                    {
                        double T5 = T2 - T3 - Tt;
                        Tm = T3 + Tt + (T5 * P22 * L.SCXn) / (P12 * L.SCXw + P22 * L.SCXn + P32 * L.SCXg);
                        gp[0] = (int)((T3 + Tt) / T11)+1;
                        gp[1] = (int)((Tm - T3 - Tt) / T12);
                        gp[2] = (int)(Tm / T22);
                        gp[3] = GJ[0] - gp[0];
                        gp[4] = GJ[1] - gp[1] - gp[2];
                        gp[5] = GJ[2];
                        gp[6] = GJ[3];
                        gp[7] = Tm;
                        gp[8] = T11;
                        gp[9] = T12;
                        gp[10] = T22;
                        gp[11] = T31;
                        gp[12] = T32;
                        gp[13] = T33;
                        gp[14] = T44;
                        return gp;
                    }
                    else
                    {
                        Tm = Tt + T3;
                        gp[0] = (int)((T3 + Tt) / T11)+1;
                        gp[1] = 0;
                        gp[2] = GJ[1];
                        gp[3] = GJ[0] - gp[0];
                        gp[4] = 0;
                        gp[5] = GJ[2];
                        gp[6] = GJ[3];
                        gp[7] = Tm;
                        gp[8] = T11;
                        gp[9] = T12;
                        gp[10] = T22;
                        gp[11] = T31;
                        gp[12] = T32;
                        gp[13] = T33;
                        gp[14] = T44;
                        return gp;
                    }
                }
            }
            return gp;
        }
        public int Maxfloor(Building b)//求楼层最值  /*测试完毕无误*/
        {
            int csum = b.Components.Count;
            int max = 1;
            for (int i = 0; i < csum; i++)
            {
                if (b.Components[i].Floor > max)
                    max = b.Components[i].Floor;
            }
            return max;
        }
    }
}
