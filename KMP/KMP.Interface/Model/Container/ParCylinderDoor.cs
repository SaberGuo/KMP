using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KMP.Interface.Model.Container
{
  public  class ParCylinderDoor:ParameterBase
    {
        PassedParameter inRadius;
        PassedParameter thickness;
        double doorRadius;
        double flanchWidth;
        /// <summary>
        /// 容器内半径 不显示
        /// </summary>
        /// 
        [Browsable(false)]
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
        /// <summary>
        /// 容器门厚度 不显示
        /// </summary>
        /// 
        [Browsable(false)]
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
        /// <summary>
        /// 容器门深度内半径
        /// </summary>
        /// 
        [DisplayName("容器门深度内半径")]
        public double DoorRadius
        {
            get
            {
                return doorRadius;
            }

            set
            {
                doorRadius = value;
                this.RaisePropertyChanged(() => this.DoorRadius);
            }
        }
        /// <summary>
        /// 法兰宽度
        /// </summary>
        /// 
        [DisplayName("罐体法兰宽度")]
        public double FlanchWidth
        {
            get
            {
                return flanchWidth;
            }

            set
            {
                flanchWidth = value;
                this.RaisePropertyChanged(() => this.FlanchWidth);
            }
        }
    }
}
