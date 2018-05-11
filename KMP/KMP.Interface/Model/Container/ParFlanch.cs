using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
   public class ParFlanch : ParameterBase
    {
        double dN;
        double d6;
        double d0;
        double d1;
        double d2;
        double h;
        double c;
        double x;
        double d;
        double n;
        /// <summary>
        /// 公称通径
        /// </summary>
        public double DN
        {
            get
            {
                return dN;
            }

            set
            {
                dN = value;
                this.RaisePropertyChanged(() => this.DN);
            }
        }
        /// <summary>
        /// 内径
        /// </summary>
        public double D6
        {
            get
            {
                return d6;
            }

            set
            {
                d6 = value;
                this.RaisePropertyChanged(() => this.D6);
            }
        }
        /// <summary>
        /// 螺栓孔中心距离直径
        /// </summary>
        public double D0
        {
            get
            {
                return d0;
            }

            set
            {
                d0 = value;
                this.RaisePropertyChanged(() => this.D0);
            }
        }
        /// <summary>
        /// 外径
        /// </summary>
        public double D1
        {
            get
            {
                return d1;
            }

            set
            {
                d1 = value;
                this.RaisePropertyChanged(() => this.D1);
            }
        }
        /// <summary>
        /// 凹陷直径
        /// </summary>
        public double D2
        {
            get
            {
                return d2;
            }

            set
            {
                d2 = value;
                this.RaisePropertyChanged(() => this.D2);
            }
        }
        /// <summary>
        /// 法兰厚度
        /// </summary>
        public double H
        {
            get
            {
                return h;
            }

            set
            {
                h = value;
                this.RaisePropertyChanged(() => this.H);
            }
        }
        /// <summary>
        /// 螺栓孔直径
        /// </summary>
        public double C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
                this.RaisePropertyChanged(() => this.C);
            }
        }
        /// <summary>
        /// 螺栓孔螺丝
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                this.RaisePropertyChanged(() => this.X);
            }
        }
        /// <summary>
        /// 螺栓直径
        /// </summary>
        public double D
        {
            get
            {
                return d;
            }

            set
            {
                d = value;
                this.RaisePropertyChanged(() => this.D);
            }
        }
        /// <summary>
        /// 螺栓数量
        /// </summary>
        public double N
        {
            get
            {
                return n;
            }

            set
            {
                n = value;
                this.RaisePropertyChanged(() => this.N);
            }
        }
    }
}
