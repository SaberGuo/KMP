using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
    [DisplayName("平板参数")]
    public  class ParPlaneSystem:ParameterBase
    {
        public override string ToString()
        {
            return "平板参数";
        }
        int planeNumber;
        double totalHeight;
        PassedParameter cylinderInRadius;
        double planeToCenterDistance;
        //double offset;
        double heightOffset;

        [DisplayName("平板数量")]
        public int PlaneNumber
        {
            get
            {
                return planeNumber;
            }

            set
            {
                planeNumber = value;
            }
        }
        /// <summary>
        /// 平板系统总高度 不显示
        /// </summary>
        /// 
        [DisplayName("内部踏板总高度")]
        [Browsable(false)]
        public double TotalHeight
        {
            get
            {
                return totalHeight;
            }

            set
            {
                totalHeight = value;
            }
        }
        /// <summary>
        /// 罐体半径 不显示
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
        ///// <summary>
        ///// 距罐体中心线平移  
        ///// </summary>
        //public double Offset
        //{
        //    get
        //    {
        //        return offset;
        //    }

        //    set
        //    {
        //        offset = value;
        //    }
        //}
        /// <summary>
        /// 高度上平移，不显示
        /// </summary>
        /// 
      //  [DisplayName("高度上平移"), ReadOnly(true)]
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
        [Description("平板系统")]
        [DisplayName("平板到罐体中心高度（H4）")]
        public double PlaneToCenterDistance
        {
            get
            {
                return planeToCenterDistance;
            }

            set
            {
                planeToCenterDistance = value;
            }
        }
    }
}
