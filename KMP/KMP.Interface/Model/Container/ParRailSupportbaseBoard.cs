using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Container
{
    [DisplayName("导轨-下底板参数")]
    /// <summary>
    /// 底板参数
    /// </summary>
    public  class ParRailSupportbaseBoard : ParameterBase
    {
        public override string ToString()
        {
            return "导轨-下底板参数";
        }
        double thickness;
        double width;
        double length;

        [DisplayName("厚度（T）")]
        [Description("导轨-下支持横板")]
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
        [DisplayName("宽度（W）")]
        [Description("导轨-下支持横板")]
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
        [DisplayName("长度（L）")]
        [Description("导轨-下支持横板")]
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
