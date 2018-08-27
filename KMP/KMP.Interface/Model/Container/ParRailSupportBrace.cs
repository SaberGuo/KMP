using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨支架支撑参数
    /// </summary>
  public  class ParRailSupportBrace : ParameterBase
    {
        public override string ToString()
        {
            return "导轨-中部支撑参数";
        }
        double inRadius;
        double thickness;
        double height;
        [DisplayName("支撑内直径（d）")]
        [Description("导轨-中支持")]
        public double InDiameter
        {
            get
            {
                return inRadius;
            }

            set
            {
                inRadius = value;
                this.RaisePropertyChanged(() => this.InDiameter);
            }
        }
        [DisplayName("厚度（T）")]
        [Description("导轨-中支持")]
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
        [DisplayName("高度（h）")]
        [Description("导轨-中支持")]
        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                this.RaisePropertyChanged(() => this.Height);
            }
        }
    }
}
