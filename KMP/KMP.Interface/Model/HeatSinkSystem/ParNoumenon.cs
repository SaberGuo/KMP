using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.HeatSinkSystem
{
  public  class ParNoumenon:ParameterBase
    {
        PassedParameter inRadius=new PassedParameter();
        PassedParameter thickness=new PassedParameter();

        public PassedParameter InRadius
        {
            get
            {
                return inRadius;
            }

            set
            {
                inRadius = value;
            }
        }

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
