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
        double inRadius;
        double thickness;
        double height;
        [DisplayName("半径")]
        public double InRadius
        {
            get
            {
                return inRadius;
            }

            set
            {
                inRadius = value;
                this.RaisePropertyChanged(() => this.InRadius);
            }
        }
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
        [DisplayName("高度")]
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
