using KMP.Interface.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
   public class ParContainerSystem:ParameterBase
    {
        double pedestalNumber;
        PassedParameter inRadius=new PassedParameter();
        PassedParameter thickness = new PassedParameter();
        /// <summary>
        /// 底座数量
        /// </summary>
        [DisplayName("底座数量")]
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

        [DisplayName("罐体半径")]
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

        [DisplayName("罐体厚度")]
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
    }
}
