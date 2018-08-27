using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;

namespace KMP.Interface.Model.Container
{
    
    /// <summary>
    /// 导轨参数
    /// </summary>
    public class ParRail:ParameterBase
    {
        public override string ToString()
        {
            return "导轨参数";
        }

        double upBridgeHeight;
        double downBridgeHeight;
        double braceHeight;
        double upBridgeWidth;
        double downBridgeWidth;
        double braceWidth;
        double railLength;
        double totalHeight;
        [DisplayName("导轨总高度（h1）")]
        [Description("导轨")]
        public double TotalHeight
        {
            get
            {
                return totalHeight;
            }

            set
            {
                totalHeight = value;
                BraceHeight = TotalHeight - UpBridgeHeight - downBridgeHeight;
            }
        }
        /// <summary>
        /// 上梁高度
        /// </summary>
        /// 
        [DisplayName("导轨上板厚（d1）")]
        [Description("导轨")]
        public double UpBridgeHeight
        {
            get
            {
                return upBridgeHeight;
            }

            set
            {
                upBridgeHeight = value;
                this.RaisePropertyChanged(() => this.UpBridgeHeight);
                BraceHeight = TotalHeight - UpBridgeHeight - downBridgeHeight;
            }
        }
        /// <summary>
        /// 下梁高度
        /// </summary>
        /// 
        [DisplayName("导轨下板厚（d2）")]
        [Description("导轨")]
        public double DownBridgeHeight
        {
            get
            {
                return downBridgeHeight;
            }

            set
            {
                downBridgeHeight = value;
                this.RaisePropertyChanged(() => this.DownBridgeHeight);
                BraceHeight = TotalHeight - UpBridgeHeight - downBridgeHeight;
            }
        }
        /// <summary>
        /// 中间支撑高度
        /// </summary>
        /// 
        [DisplayName("中间支撑高度(h2)")]
        [Description("导轨")]
        [Browsable(false)]
        public double BraceHeight
        {
            get
            {
                return braceHeight;
            }

            set
            {
                braceHeight = value;
                this.RaisePropertyChanged(() => this.BraceHeight);
            }
        }
        /// <summary>
        /// 上梁宽度
        /// </summary>
        /// 
        [DisplayName("导轨上宽（L1）")]
        [Description("导轨")]
        public double UpBridgeWidth
        {
            get
            {
                return upBridgeWidth;
            }

            set
            {
                upBridgeWidth = value;
                this.RaisePropertyChanged(() => this.UpBridgeWidth);
            }
        }
        /// <summary>
        /// 下梁宽度
        /// </summary>
        /// 
        [DisplayName("导轨下宽（L3）")]
        [Description("导轨")]
        public double DownBridgeWidth
        {
            get
            {
                return downBridgeWidth;
            }

            set
            {
                downBridgeWidth = value;
                this.RaisePropertyChanged(() => this.DownBridgeWidth);
            }
        }
        /// <summary>
        /// 中间支撑宽度
        /// </summary>
        /// 
        [DisplayName("导轨腹板厚（d3）")]
        [Description("导轨")]
        public double BraceWidth
        {
            get
            {
                return braceWidth;
            }

            set
            {
                braceWidth = value;
                this.RaisePropertyChanged(() => this.BraceWidth);
            }
        }
        /// <summary>
        /// 导轨长度
        /// </summary>
        /// 
        [DisplayName("导轨长度")]
        [Description("导轨")]
        public double RailLength
        {
            get
            {
                return railLength;
            }

            set
            {
                railLength = value;
                this.RaisePropertyChanged(() => this.RailLength);
            }
        }

      
    }
}
