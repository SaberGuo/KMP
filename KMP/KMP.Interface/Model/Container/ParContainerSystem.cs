using KMP.Interface.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
    [DisplayName("容器参数")]
    public class ParContainerSystem:ParameterBase
    {
        public ParContainerSystem()
        {
            
        }

        public override string ToString()
        {
            return "容器参数";
        }
        double pedestalNumber;
        PassedParameter inRadius=new PassedParameter();
        PassedParameter thickness = new PassedParameter();
        /// <summary>
        /// 底座数量
        /// </summary>
        [DisplayName("支座数量")]
        [Description("容器系统")]
        public double PedestalNumber
        {
            
            get
            {
                return pedestalNumber;
            }

            set
            {
                pedestalNumber = value;
                this.RaisePropertyChanged(() => this.PedestalNumber);
            }
        }
        PassedParameter inDiameter = new PassedParameter();
        [DisplayName("容器直径（d2）")]
        [Description("容器内直径")]
        public PassedParameter InDiameter
        {
            get
            {
                return inDiameter;
            }

            set
            {
                inDiameter = value;
            }
        }

        [Browsable(false)]
        /// <summary>
        /// 罐体半径
        /// </summary>
        public PassedParameter InRadius
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

        [DisplayName("容器壁厚（T1）")]
        [Description("容器内直径")]
        /// <summary>
        /// 罐体厚度
        /// </summary>
        public PassedParameter Thickness
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
        [DisplayName("踏板距罐口距离")]
        [Description("容器系统")]
        /// <summary>
        /// 罐体厚度
        /// </summary>
     public double PlaneOffset { get; set; }
        [DisplayName("导轨距罐口距离")]
        [Description("容器系统")]
        public double RailOffset { get; set; }
        [DisplayName("首个容器底座到罐口距离")]
        [Description("容器系统")]
        public double PedestalFirstOffset { get; set; }
        [DisplayName("容器底座间隔")]
        [Description("容器系统")]
        public double PedestalSpace { get; set; }

     
    }
}
