using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨支架顶部平板
    /// </summary>
  public  class ParRailSupportTopBoard : ParameterBase
    {
        double thickness;
        double width;
       
        double holeRadius;
        double holeCenterDistance;
        double holeSideEdgeDistance;
        double holeTopEdgeDistance;

        [DisplayName("厚度")]
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
        [DisplayName("宽度")]
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
        [DisplayName("孔半径")]
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
        [DisplayName("孔中心距离")]
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
        [DisplayName("孔边距")]
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
