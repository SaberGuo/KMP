﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KMP.Interface.Model.HeatSinkSystem
{
  public class ParHeatSink:ParameterBase
    {
        PassedParameter inDiameter = new PassedParameter();
        PassedParameter thickness = new PassedParameter();
        [DisplayName("热沉内直径")]
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
        [DisplayName("热沉罐厚度")]
        public PassedParameter Thickness
        {
            get
            {
                return thickness;
            }

            set
            {
                thickness = value;
            }
        }
    }
}
