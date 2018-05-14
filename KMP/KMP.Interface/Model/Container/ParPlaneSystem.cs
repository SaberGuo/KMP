using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
  public  class ParPlaneSystem:ParameterBase
    {
        int planeNumber;
        double totalHeight;
        PassedParameter cylinderInRadius;
        //double offset;
        double heightOffset;
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
