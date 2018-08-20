using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
  public  class ParRailSystem:ParameterBase
    {
        double supportNum;
        double railTotalHeight;
        PassedParameter cylinderInRadius;
        double offset;
        double heightOffset;
        /// <summary>
        /// 导轨支架数量
        /// </summary>
        /// 
        [DisplayName("导轨支架数量")]
        public double SupportNum
        {
            get
            {
                return supportNum;
            }

            set
            {
                supportNum = value;
            }
        }
        /// <summary>
        /// 导轨系统总高度
        /// </summary>
        /// 
        [DisplayName("导轨系统总高度")]
        public double RailTotalHeight
        {
            get
            {
                return railTotalHeight;
            }

            set
            {
                railTotalHeight = value;
            }
        }
        /// <summary>
        /// 罐体半径
        /// </summary>
        /// 
        [Browsable(false)]
        public PassedParameter CylinderInRadius
        {
            get
            {
                return cylinderInRadius;
            }

            set
            {
                cylinderInRadius = value;
            }
        }
        /// <summary>
        /// 距罐体中心线平移
        /// </summary>
        /// 
        [DisplayName("距罐体中心线平移R")]
        [Description("导轨-支持")]
        public double Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }
        /// <summary>
        /// 高度上平移，不显示
        /// </summary>
        /// 
        [Browsable(false)]
        public double HeightOffset
        {
            get
            {
                return heightOffset;
            }

            set
            {
                heightOffset = value;
            }
        }
    }
}
