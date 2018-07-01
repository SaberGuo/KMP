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
namespace KMP.Interface.Model.Other
{
   public class ParCoolVAC:ParameterBase
    {
        ParFlanch _flanch = new ParFlanch();
        double height;
        double totolHeight;
        private double flanchDN = 50;
        [DisplayName("法兰")]
        public ParFlanch Flanch
        {
            get
            {
                return _flanch;
            }

            set
            {
                _flanch = value;
                this.RaisePropertyChanged(() => this.Flanch);
            }
        }
        [DisplayName("法兰公称通径")]
        [ItemsSource(typeof(ParFlanchSource))]
        public double FlanchDN
        {
            get
            {
                return this.flanchDN;
            }
            set
            {
                this.flanchDN = value;
                ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParFlanch);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(franch, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.Flanch, c, null);
                }
            }
        }
        [DisplayName("泵体高度")]
        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }
        [DisplayName("总高度")]
        public double TotolHeight
        {
            get
            {
                return totolHeight;
            }

            set
            {
                totolHeight = value;
            }
        }
    }
}
