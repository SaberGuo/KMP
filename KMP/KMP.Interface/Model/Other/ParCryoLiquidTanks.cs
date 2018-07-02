using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.Other
{

  public  class ParCryoLiquidTanks:ParameterBase
    {
        int _number=1;
        double _offset;
        [DisplayName("阵列数量")]
        public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }
        [DisplayName("间距")]
        public double Offset
        {
            get
            {
                return _offset;
            }

            set
            {
                _offset = value;
            }
        }
    }
}
