using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
namespace KMP.Interface.Model.Container
{
   public class PassedParameter : NotificationObject
    {
        double value;

        public double Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
                this.RaisePropertyChanged(() => this.Value);
            }
        }
    }
}
