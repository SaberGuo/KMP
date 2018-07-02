using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;

namespace KMP.Interface.Model.Other
{
   public class ParValve:ParameterBase
    {
        public ParValve()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }
        ParFlanch _flanch = new ParFlanch();
        double a, b, g, c, d, h, i,f;
        public ParFlanch Flanch
        {
            get
            {
                return _flanch;
            }

            set
            {
                _flanch = value;
            }
        }
        /// <summary>
        /// 阀门面宽度
        /// </summary>
        public double A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }
        /// <summary>
        /// 阀门面上沿宽度
        /// </summary>
        public double B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }
        /// <summary>
        /// 阀门面厚度
        /// </summary>
        public double G
        {
            get
            {
                return g;
            }

            set
            {
                g = value;
            }
        }
        /// <summary>
        /// 阀门圆心距底面距离
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
            }
        }
        /// <summary>
        /// 阀门圆心距顶面距离
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
            }
        }
        /// <summary>
        /// 阀门上部长度
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
            }
        }
        /// <summary>
        /// 阀门上部直径
        /// </summary>
        public double I
        {
            get
            {
                return i;
            }

            set
            {
                i = value;
            }
        }

        public double F
        {
            get
            {
                return f;
            }

            set
            {
                f = value;
            }
        }
    }
}
