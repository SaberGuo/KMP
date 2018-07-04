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
namespace KMP.Interface.Model.Other
{
   public class ParCoolVAC:ParameterBase
    {
        public ParCoolVAC()
        {
            ServiceLocator.Current.GetInstance<ParVACDictProxy>();
            
        }
        private ParVAC _vac=new ParVAC();
        private double _vacDN;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParVAC VAC
        {
            get
            {
                return _vac;
            }

            set
            {
                _vac = value;
                this.RaisePropertyChanged(() => this.VAC);
            }
        }
        [DisplayName("泵类型")]
        [ItemsSource(typeof(ParVACSource))]
        public double VacDN
        {
            get
            {
                return _vacDN;
            }

            set
            {
                this._vacDN = value;
                this.RaisePropertyChanged(() => this.VacDN);
                ParVAC vac = ServiceLocator.Current.GetInstance<ParVACDictProxy>().VACDict["DN" + value.ToString()];
               // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParVAC);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(vac, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.VAC, c, null);
                }
            }
        }

    
    }
    public static class ParVACDict
    {
        static Dictionary<string, ParVAC> _VACDict = new Dictionary<string, ParVAC>();
        public static Dictionary<string, ParVAC> VACDict
        {
            get
            {
                return _VACDict;
            }
        }

    }
    [Export(typeof(ParVACDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParVACDictProxy
    {
        [ImportingConstructor]
         public ParVACDictProxy()
        {
            VACDict.Add("DN250", new ParVAC
            {
          
                Flanch = ParFlanchDict.FlanchDict["DN250"],
                Height = 301,
                TotolHeight = 560
              
            });
            VACDict.Add("DN320", new ParVAC
            {
                
                Flanch = ParFlanchDict.FlanchDict["DN320"],
                Height = 343,
                TotolHeight = 662

            });
            VACDict.Add("DN400", new ParVAC
            {
               
                Flanch = ParFlanchDict.FlanchDict["DN400"],
                Height = 430,
                TotolHeight = 712

            });
            VACDict.Add("DN500", new ParVAC
            {
               
                Flanch = ParFlanchDict.FlanchDict["DN500"],
                Height = 516,
                TotolHeight = 787

            });
        }
        public Dictionary<string, ParVAC> VACDict
        {
            get { return ParVACDict.VACDict; }
        }

    }
    public class ParVACSource : IItemsSource
    {

        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParVACDict.VACDict)
            {
                VACs.Add(item.Value.Flanch.DN, item.Key);
            }
            return VACs;
        }
    }
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("泵参数")]
    public class ParVAC : ParameterBase
    {
        public ParVAC()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }
        ParFlanch _flanch = new ParFlanch();
        double height;
        double totolHeight;
       
      
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
        

  
        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                this.RaisePropertyChanged(() => this.Height);
            }
        }
      
        public double TotolHeight
        {
            get
            {
                return totolHeight;
            }

            set
            {
                totolHeight = value;
                this.RaisePropertyChanged(() => this.TotolHeight);
            }
        }
    }

}
