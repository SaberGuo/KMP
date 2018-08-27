using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨面距中心高度 总高度修改
    /// </summary>
  public  class ParRailSystem:ParameterBase
    {
        public override string ToString()
        {
            return "导轨系统参数";
        }
        double supportNum;
        double railTotalHeight=1;
        PassedParameter cylinderInRadius;
        double offset;
        double heightOffset;
        double railToCenterDistance;
        /// <summary>
        /// 导轨支架数量
        /// </summary>
        /// 
        [DisplayName("导轨支座数量")]
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
        [Browsable(false)]
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
        [DisplayName("距罐体中心线平移(R1)")]
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
        /// <summary>
        /// 导轨到罐体中心高度
        /// </summary>
        [DisplayName("导轨到罐体中心高度(h1)")]
        [Description("导轨-支持")]
        public double RailToCenterDistance
        {
            get
            {
                return railToCenterDistance;
            }

            set
            {
                railToCenterDistance = value;
                if(cylinderInRadius!=null)
               RailTotalHeight= CylinderInRadius.Value - railToCenterDistance;
            }
        }
    }
}
