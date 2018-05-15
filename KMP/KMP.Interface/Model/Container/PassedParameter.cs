using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using KMP.Interface.Tools;

namespace KMP.Interface.Model.Container
{
    [TypeConverterAttribute(typeof(PassedParamConverter)),Description("系统参数")]
    public class PassedParameter : NotificationObject
    {
        public override string ToString()
        {
            return this.value.ToString();
        }
        double value;

        [DisplayName("数值")]
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
