using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Practices.Prism.ViewModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition;
namespace KMP.Interface.Model.MeasureMentControl
{
    [DisplayName("测控系统")]
  public  class ParCabinets:ParameterBase
    {
        int num;
        double distance;
        [DisplayName("控制柜数量")]
        [Description("测控系统")]
        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
                this.RaisePropertyChanged(() => this.Num);
            }
        }
        [DisplayName("控制柜间距")]
        [Description("测控系统")]
        public double Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
                this.RaisePropertyChanged(() => this.Distance);
            }
        }
    }
}
