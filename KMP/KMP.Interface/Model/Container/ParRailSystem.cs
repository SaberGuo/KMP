using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
  public  class ParRailSystem:ParameterBase
    {
        double supportNum;

        public double SupportNum
        {
            get
            {
                return supportNum;
            }

            set
            {
                supportNum = value;
            }
        }
    }
}
