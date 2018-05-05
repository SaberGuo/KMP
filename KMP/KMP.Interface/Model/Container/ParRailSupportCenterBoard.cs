﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
    /// <summary>
    /// 导轨支架支撑下边板
    /// </summary>
  public  class ParRailSupportCenterBoard : ParameterBase
    {
        double thickness;
        double width;
        double length;

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