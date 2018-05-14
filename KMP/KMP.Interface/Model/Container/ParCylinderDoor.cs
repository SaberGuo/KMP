using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
  public  class ParCylinderDoor:ParameterBase
    {
        PassedParameter inRadius;
        PassedParameter thickness;
      
        double doorRadius;
        double flanchWidth;
        ParCylinderHole topHole = new ParCylinderHole();
        ParCylinderHole sideHole = new ParCylinderHole();
        /// <summary>
        /// 容器内半径 不显示
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
        /// <summary>
        /// 容器门厚度 不显示
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
        /// <summary>
        /// 容器门深度内半径
        /// </summary>
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
        /// 容器门法兰宽度
        /// </summary>
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

        public ParCylinderHole TopHole
        {
            get
            {
                return topHole;
            }

            set
            {
                topHole = value;
                this.RaisePropertyChanged(() => this.TopHole);
            }
        }

        public ParCylinderHole SideHole
        {
            get
            {
                return sideHole;
            }

            set
            {
                sideHole = value;
                this.RaisePropertyChanged(() => this.SideHole);
            }
        }
    }
}
