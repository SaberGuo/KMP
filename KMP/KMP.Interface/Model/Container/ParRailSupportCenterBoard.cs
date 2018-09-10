﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    [DisplayName("导轨-下部支撑参数")]
    /// <summary>
    /// 导轨支架支撑下边板
    /// </summary>
    public  class ParRailSupportCenterBoard : ParameterBase
    {
        public override string ToString()
        {
            return "导轨-下部支撑参数";
        }
        double thickness;
        double width;
        double length;
        double holeRadius;
        double holeCenterDistance;
        double holeSideEdgeDistance;
        double holeTopEdgeDistance;
        /// <summary>
        /// 厚度
        /// </summary>
        /// 
        [DisplayName("厚度")]
        [Description("导轨-下底板")]
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
        /// 长度
        /// </summary>
        /// 
        [DisplayName("长度（L1）")]
        [Description("导轨-下底板")]
        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
                this.RaisePropertyChanged(() => this.Length);
            }
        }
        [DisplayName("宽度（W1）")]
        [Description("导轨-下底板")]
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
        /// 
        [DisplayName("螺丝孔直径(d1)")]
        [Description("导轨-下底板")]
        public double HoleDiameter
        {
            get
            {
                return holeRadius;
            }

            set
            {
                holeRadius = value;
                this.RaisePropertyChanged(() => this.HoleDiameter);
            }
        }
        /// <summary>
        /// 螺丝孔中心之间距离
        /// </summary>
        /// 
        [DisplayName("孔两个中心距离(L2)")]
        [Description("导轨-下底板")]
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
        /// 
        [DisplayName("螺丝孔边距(h1)")]
        [Description("导轨-下底板")]
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
        /// 
        [DisplayName("螺丝孔中心到顶边距离(L3)")]
        [Description("导轨-下底板")]
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
