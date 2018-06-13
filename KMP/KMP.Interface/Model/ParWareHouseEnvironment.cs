using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KMP.Interface.Model
{
  public  class ParWareHouseEnvironment:ParameterBase
    {
        double offSet;
        [DisplayName("热沉偏移距离")]
        public double OffSet
        {
            get
            {
                return offSet;
            }

            set
            {
                offSet = value;
            }
        }
    }
}
