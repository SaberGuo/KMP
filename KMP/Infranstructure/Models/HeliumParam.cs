using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    public class HeliumParam : NotificationObject
    {
        //液氮系统热负荷
        private double _Q;
        public double Q
        {
            get
            {
                return this._Q;
            }
            set
            {
                this._Q = value;
                this.RaisePropertyChanged(() => this.Q);
            }
        }

        //液氮的定压比热
        private double _cp;
        public double cp
        {
            get
            {
                return this._cp;
            }
            set
            {
                this._cp = value;
                this.RaisePropertyChanged(() => this.cp);
            }
        }

        //液氮的密度
        private double _rou;
        public double rou
        {
            get
            {
                return this._rou;
            }
            set
            {
                this._rou = value;
                this.RaisePropertyChanged(() => this.rou);
            }
        }

        //液氮的体积流量
        private double _V;
        public double V
        {
            get
            {
                return this._V;
            }
            set
            {
                this._V = value;
                this.RaisePropertyChanged(() => this.V);
            }
        }

        //液氮过冷度
        private double _deltaT;
        public double deltaT
        {
            get
            {
                return this._deltaT;
            }
            set
            {
                this._deltaT = value;
                this.RaisePropertyChanged(() => this.deltaT);
            }
        }

        //液氮循环管道直径
        private double _D;
        public double D
        {
            get
            {
                return this._D;
            }
            set
            {
                this._D = value;
                this.RaisePropertyChanged(() => this.D);
            }

        }
        //液氮体积流量
        private double _Vspeed;
        public double Vspeed
        {
            get
            {
                return this._Vspeed;
            }
            set
            {
                this._Vspeed = value;
                this.RaisePropertyChanged(() => this.Vspeed);
            }
        }

        //液氮的流速
        private double _u;
        public double u
        {
            get
            {
                return this._u;
            }
            set
            {
                this._u = value;
                this.RaisePropertyChanged(() => this.u);
            }
        }

        //液氮循环压力损失
        private double _deltaP;
        public double deltaP
        {
            get
            {
                return this._deltaP;
            }
            set
            {
                this._deltaP = value;
                this.RaisePropertyChanged(() => this.deltaP);
            }
        }

        //沿程阻力损失
        private double _deltaP1;
        public double deltaP1
        {
            get
            {
                return this._deltaP1;
            }
            set
            {
                this._deltaP1 = value;
                this.RaisePropertyChanged(() => this.deltaP1);
            }
        }

        //局部阻力损失
        private double _deltaP2;
        public double deltaP2
        {
            get
            {
                return this._deltaP2;
            }
            set
            {
                this._deltaP2 = value;
                this.RaisePropertyChanged(() => this.deltaP2);
            }
        }
        //设备阻力损失
        private double _deltaP3;
        public double deltaP3
        {
            get
            {
                return this._deltaP3;
            }
            set
            {
                this._deltaP3 = value;
                this.RaisePropertyChanged(() => this.deltaP3);
            }
        }
    }
}
