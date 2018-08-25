using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨支架侧板参数
    /// </summary>
   public class ParRailSupportSidePlate : ParameterBase
    {
        double thickness;
        double width;
        double length;
        /// <summary>
        /// 板材厚度
        /// </summary>
        /// 
        [DisplayName("厚度（h）")]
        [Description("导轨-下支持立板")]
        [ReadOnly(true)]
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
        /// 板材宽度
        /// </summary>
        /// 
        [DisplayName("宽度（T）")]
        [Description("导轨-下支持立板")]
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
        /// 板材长度
        /// </summary>
        [DisplayName("长度（L)")]
        [Description("导轨-下支持立板")]
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
    }
}
