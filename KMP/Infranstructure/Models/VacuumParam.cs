using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    public class VacuumParam
    {
        //粗抽结束压力
        [DisplayName("粗抽结束压力(P)")]
        public double PreEndPress { get; set; }

        //真空容器容积
        [DisplayName("真空容器容积(V)")]
        public double Volume { get; set; }

        //低温泵的渡越容积
        [DisplayName("低温泵的渡越容积(Q)")]
        public double CryopumpVolume { get; set; }

        //抽气时间
        [DisplayName("抽气时间(t)")]
        public double PumpingTime { get; set; }

        private double _PressT = 1;
        //经t时间抽气后的压力
        [DisplayName("经t时间抽气后的压力(Pt)")]
        public double PressT {
            get
            {
                return _PressT;
            }
            set
            {
                this._PressT = value;
                if(this._PressT>1 && this._PressT < 10)
                {
                    this.Kq = 4;
                }
                else if(this._PressT<100)
                {
                    this.Kq = 2;
                }
                else if (this._PressT < 1000)
                {
                    this.Kq = 1.5;
                }
                else if(this._PressT < 10000)
                {
                    this.Kq = 1.25;
                }
                else if(this._PressT< 100000)
                {
                    this.Kq = 1;
                }
            }
        }

        //设备开始抽气时的压力
        [DisplayName("设备开始抽气时的压力(Pi)")]
        public double PressS { get; set; }

        //粗抽泵的抽速
        [DisplayName("粗抽泵的抽速(Sp)")]
        public double PrePumpingSpeed { get; set; }

        //粗抽系统的极限压力
        [DisplayName("粗抽系统的极限压力(P0)")]
        public double PreUltimatePress { get; set; }

        //修正系数
        [DisplayName("修正系数(Kq)")]
        
        public double Kq { get; set; }

        //管道流导
        [DisplayName("管道流导(U)")]
        public double PipelineConductance { get; set; }

        //管道长度
        [DisplayName("管道长度(Le)")]
        public double PipeLength { get; set; }

        //管道直径
        [DisplayName("管道直径(d)")]
        public double PipeDiameter { get; set; }

        //平均压力
        [DisplayName("平均压力(P_b)")]
        public double AvgPress { get; set; }

        //粗抽泵的有效抽速
        [DisplayName("粗抽泵的有效抽速(S)")]
        public double PreAvalPumpingSpeed { get; set; }
    }
}
