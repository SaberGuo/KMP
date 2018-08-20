using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 底板参数
    /// </summary>
  public  class ParRailSupportbaseBoard : ParameterBase
    {
        double thickness;
        double width;
        double length;

        [DisplayName("厚度T")]
        [Description("导轨下支撑横板")]
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
        [DisplayName("宽度W")]
        [Description("导轨下支撑横板")]
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
        [DisplayName("长度L")]
        [Description("导轨下支撑横板")]
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
