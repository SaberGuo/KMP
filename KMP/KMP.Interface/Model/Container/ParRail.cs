using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨参数
    /// </summary>
   public class ParRail:ParameterBase
    {
        double upBridgeHeight;
        double downBridgeHeight;
        double braceHeight;
        double upBridgeWidth;
        double downBridgeWidth;
        double braceWidth;
        double railLength;
        /// <summary>
        /// 上梁高度
        /// </summary>
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
            }
        }
        /// <summary>
        /// 下梁高度
        /// </summary>
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
            }
        }
        /// <summary>
        /// 中间支撑高度
        /// </summary>
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
