using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨支架支撑下边板
    /// </summary>
  public  class ParRailSupportCenterBoard : ParameterBase
    {
        double thickness;
        double width;

        double holeRadius;
        double holeCenterDistance;
        double holeSideEdgeDistance;
        double holeTopEdgeDistance;
        /// <summary>
        /// 厚度
        /// </summary>
        public double Thickness
        {
            get
            {
                return thickness;
            }

            set
            {
                thickness = value;
                this.RaisePropertyChanged(() => this.Thickness);
            }
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
                this.RaisePropertyChanged(() => this.Width);
            }
        }
        /// <summary>
        /// 螺丝孔半径
        /// </summary>
        public double HoleRadius
        {
            get
            {
                return holeRadius;
            }

            set
            {
                holeRadius = value;
                this.RaisePropertyChanged(() => this.HoleRadius);
            }
        }
        /// <summary>
        /// 螺丝孔中心之间距离
        /// </summary>
        public double HoleCenterDistance
        {
            get
            {
                return holeCenterDistance;
            }

            set
            {
                holeCenterDistance = value;
                this.RaisePropertyChanged(() => this.HoleCenterDistance);
            }
        }
        /// <summary>
        /// 螺丝孔中心到钣金侧边距离
        /// </summary>
        public double HoleSideEdgeDistance
        {
            get
            {
                return holeSideEdgeDistance;
            }

            set
            {
                holeSideEdgeDistance = value;
                this.RaisePropertyChanged(() => this.HoleSideEdgeDistance);
            }
        }
        /// <summary>
        /// 螺丝孔中心到钣金顶边距离
        /// </summary>
        public double HoleTopEdgeDistance
        {
            get
            {
                return holeTopEdgeDistance;
            }

            set
            {
                holeTopEdgeDistance = value;
                this.RaisePropertyChanged(() => this.HoleTopEdgeDistance);
            }
        }
    }
}
