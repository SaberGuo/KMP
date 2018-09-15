using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.NitrogenSystem
{
    /// <summary>
    /// 汽化器参数
    /// </summary>
    [DisplayName("汽化器")]
  public  class ParVaporizer : ParameterBase
    {
        #region 汽化器主体参数
       
        double height;
        double width;
        double length;
        double grooveWidth;
        double grooveDepth;
        double grooveStartWidth;
        double wGrooveNum;
        double hGrooveNum;
        double grooveBetween;
        [Category("汽化器参数")]
        [DisplayName("高度(H1)")]
        [Description("汽化器")]
        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }
        [Category("汽化器参数")]
        [DisplayName("宽度(W)")]
        [Description("汽化器")]
        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
        [Category("汽化器参数")]
        [DisplayName("长度(T)")]
        [Description("汽化器")]
        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }
        [Category("支撑")]
        [DisplayName("高度(H2)")]
        [Description("汽化器")]
        public double SurHeight
        {
            get
            {
                return surHeight;
            }

            set
            {
                surHeight = value;
            }
        }
        #endregion
        #region 槽参数
        [Category("槽参数")]
        [DisplayName("宽度")]
        [Browsable(false)]
        public double GrooveWidth
        {
            get
            {
                return grooveWidth;
            }

            set
            {
                grooveWidth = value;
            }
        }
        [Category("槽参数")]
        [DisplayName("深度")]
        [Browsable(false)]
        public double GrooveDepth
        {
            get
            {
                return grooveDepth;
            }

            set
            {
                grooveDepth = value;
            }
        }
        [Category("槽参数")]
        [DisplayName("起始宽度")]
        [Browsable(false)]
        public double GrooveStartWidth
        {
            get
            {
                return grooveStartWidth;
            }

            set
            {
                grooveStartWidth = value;
            }
        }
        [Category("槽参数")]
        [DisplayName("宽面阵列数量")]
        [Browsable(false)]
        public double WGrooveNum
        {
            get
            {
                return wGrooveNum;
            }

            set
            {
                wGrooveNum = value;
            }
        }
        [Category("槽参数")]
        [DisplayName("长面阵列数量")]
        [Browsable(false)]
        public double HGrooveNum
        {
            get
            {
                return hGrooveNum;
            }

            set
            {
                hGrooveNum = value;
            }
        }
        [Category("槽参数")]
        [DisplayName("间距")]
        [Browsable(false)]
        public double GrooveBetween
        {
            get
            {
                return grooveBetween;
            }

            set
            {
                grooveBetween = value;
            }
        }

        #endregion
        #region  支撑参数
        double surWidth;
        double surWidth2;
        double surDistanceEdge;
        double surHeight;
        [Category("支撑参数")]
        [DisplayName("宽度")]
        [Browsable(false)]
        public double SurWidth
        {
            get
            {
                return surWidth;
            }

            set
            {
                surWidth = value;
            }
        }
        [Category("支撑参数")]
        [DisplayName("支撑底面宽度")]
        [Browsable(false)]
        public double SurWidth2
        {
            get
            {
                return surWidth2;
            }

            set
            {
                surWidth2 = value;
            }
        }
        [Category("支撑参数")]
        [DisplayName("支撑与侧面距离")]
        [Browsable(false)]
        public double SurDistanceEdge
        {
            get
            {
                return surDistanceEdge;
            }

            set
            {
                surDistanceEdge = value;
            }
        }
     

   


        #endregion
    }
}
