using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
   public class ParContainerSystem:ParameterBase
    {
        double pedestalNumber;

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
    }
}
