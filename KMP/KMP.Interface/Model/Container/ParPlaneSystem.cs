using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
  public  class ParPlaneSystem:ParameterBase
    {
        int planeNumber;

        public int PlaneNumber
        {
            get
            {
                return planeNumber;
            }

            set
            {
                planeNumber = value;
            }
        }
    }
}
