using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    public class VacuumParam: NotificationObject
    {
        private double _PreEndPress = 100;
        //粗抽结束压力
        [DisplayName("粗抽结束压力(P)")]
        public double PreEndPress {
            get
            {
                return _PreEndPress;
            }
            set
            {
                this._PreEndPress = value;
                this.RaisePropertyChanged(() => this.PreEndPress);
            }
        }

        private double _Volume = 100;
        //真空容器容积
        [DisplayName("真空容器容积(V)")]
        public double Volume
        {
            get { return this._Volume; }
            set { this._Volume = value; this.RaisePropertyChanged(() => this.Volume); }
        }

        private double _CryopumpVolume = 10;
        //低温泵的渡越容积
        [DisplayName("低温泵的渡越容积(Q)")]
        public double CryopumpVolume
        {
            get { return this._CryopumpVolume; }
            set { this._CryopumpVolume = value; this.RaisePropertyChanged(() => this.CryopumpVolume); }
        }

        private double _PumpingTime = 50;
        //抽气时间
        [DisplayName("抽气时间(t)")]
        public double PumpingTime
        {
            get { return this._PumpingTime; }
            set { this._PumpingTime = value; this.RaisePropertyChanged(() => this.PumpingTime); }
        }

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
                    this._Kq = 4;
                }
                else if(this._PressT<100)
                {
                    this._Kq = 2;
                }
                else if (this._PressT < 1000)
                {
                    this._Kq = 1.5;
                }
                else if(this._PressT < 10000)
                {
                    this._Kq = 1.25;
                }
                else if(this._PressT< 100000)
                {
                    this._Kq = 1;
                }
                this.RaisePropertyChanged(() => this.Kq);
                this.RaisePropertyChanged(() => this.PressT);
            }
        }
        private double _PressS = 200;
        //设备开始抽气时的压力
        [DisplayName("设备开始抽气时的压力(Pi)")]
        public double PressS
        {
            get { return this._PressS; }
            set { this._PressS = value; this.RaisePropertyChanged(() => this.PressS); }
        }


        private double _PrePumpingSpeed = 2;
        //粗抽泵的抽速
        [DisplayName("粗抽泵的抽速(Sp)")]
        public double PrePumpingSpeed
        {
            get { return this._PrePumpingSpeed; }
            set { this._PrePumpingSpeed = value; this.RaisePropertyChanged(() => this.PrePumpingSpeed); }

        }

        private double _PreUltimatePress = 0.01;
        //粗抽系统的极限压力
        [DisplayName("粗抽系统的极限压力(P0)")]
        public double PreUltimatePress
        {
            get { return this._PreUltimatePress; }
            set { this._PreUltimatePress = value; this.RaisePropertyChanged(() => this.PreUltimatePress); }
        }

        private double _Kq = 1;
        //修正系数
        [DisplayName("修正系数(Kq)")]
        
        public double Kq {
            get {
                return _Kq;
            }
        }
        private double _PipelineConductance = 10;
        //管道流导
        [DisplayName("管道流导(U)")]
        public double PipelineConductance
        {
            get { return this._PipelineConductance; }
            set { this._PipelineConductance = value; this.RaisePropertyChanged(() => this.PipelineConductance); }
           
        }
        private double _PipeLength=20;
        //管道长度
        [DisplayName("管道长度(Le)")]
        public double PipeLength
        {
            get { return this._PipeLength; }
            set
            {
                this._PipeLength = value;
                this.RaisePropertyChanged(() => this.PipeLength);
            }
        }
        private double _PipeDiameter = 30;
        //管道直径
        [DisplayName("管道直径(d)")]
        public double PipeDiameter
        {
            get { return this._PipeDiameter; }
            set
            {
                this._PipeDiameter = value;
                this.RaisePropertyChanged(() => this.PipeDiameter);
            }
        }
        private double _AvgPress = 5;
        //平均压力
        [DisplayName("平均压力(P_b)")]
        public double AvgPress
        {
            get { return this._AvgPress; }
            set
            {
                this._AvgPress = value;
                this.RaisePropertyChanged(() => this.AvgPress);
            }
        }
        private double _PreAvalPumpingSpeed = 0;
        //粗抽泵的有效抽速
        [DisplayName("粗抽泵的有效抽速(S)")]
        public double PreAvalPumpingSpeed
        {
            get { return this._PreAvalPumpingSpeed; }
            set
            {
                this._PreAvalPumpingSpeed = value;
                this.RaisePropertyChanged(() => this.PreAvalPumpingSpeed);
            }

        }

        public void ParametersAnalysed(double prePumpingSpeed, double pipelineConductance, double preAvalPumpingSpeed)
        {
            this._PrePumpingSpeed = prePumpingSpeed;
            this._PreAvalPumpingSpeed = preAvalPumpingSpeed;
            this._PipelineConductance = pipelineConductance;
            this.RaisePropertyChanged(() => this.PipelineConductance);
            this.RaisePropertyChanged(() => this.PrePumpingSpeed);
            this.RaisePropertyChanged(() => this.PreAvalPumpingSpeed);
        }
    }
}
