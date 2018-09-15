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
    /// <summary>
    /// 低温泵
    /// </summary>
    [DisplayName("低温泵")]
   public class ParCoolVAC:ParameterBase
    {
        public ParCoolVAC()
        {
            ServiceLocator.Current.GetInstance<ParVACDictProxy>();
            
        }
        private ParVAC _vac=new ParVAC();
        private double _vacDN;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("低温泵")]
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
        [Description("低温泵")]
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
                Manfacturer = CoolVacManufacturer.德国莱宝,
                Height = 301,
                TotolHeight = 560,
                PumpingSpeedH2O = 7000,
                PumpingSpeedAr = 1600,
                PumpingSpeedN2 = 2100,
                PumpingSpeedH2 = 3200,
                CoolDownTime = 70,
                CrossoverValue = 250,
                Weight = 32,
                InN2DN = 16,
                OutN2DN = 16

            });
            VACDict.Add("DN320", new ParVAC
            {
                
                Flanch = ParFlanchDict.FlanchDict["DN320"],
                Manfacturer = CoolVacManufacturer.德国莱宝,
                Height = 343,
                TotolHeight = 662,
               PumpingSpeedH2O = 10500,
                PumpingSpeedAr = 2500,
                PumpingSpeedN2 = 3000,
                PumpingSpeedH2 = 6000,
                CoolDownTime = 100,
                CrossoverValue = 500,
                Weight = 46,
                InN2DN = 16,
                OutN2DN = 16
            });
            VACDict.Add("DN400", new ParVAC
            {
               
                Flanch = ParFlanchDict.FlanchDict["DN400"],
                Manfacturer = CoolVacManufacturer.德国莱宝,
                Height = 430,
                TotolHeight = 712,
                  PumpingSpeedH2O = 18000,
                PumpingSpeedAr = 4000,
                PumpingSpeedN2 = 5200,
                PumpingSpeedH2 = 6200,
                CoolDownTime = 100,
                CrossoverValue = 700,
                Weight = 54,
                InN2DN = 25,
                OutN2DN = 25
            });
            VACDict.Add("DN500", new ParVAC
            {
               
                Flanch = ParFlanchDict.FlanchDict["DN500"],
                Manfacturer = CoolVacManufacturer.德国莱宝,
                Height = 516,
                TotolHeight = 787,
                  PumpingSpeedH2O = 30000,
                PumpingSpeedAr = 8400,
                PumpingSpeedN2 = 10000,
                PumpingSpeedH2 = 12000,
                CoolDownTime = 150,
                CrossoverValue = 800,
                Weight = 70,
                InN2DN = 25,
                OutN2DN = 25
            });
            VACDict.Add("DN630", new ParVAC
            {

                Flanch = ParFlanchDict.FlanchDict["DN630"],
                Manfacturer = CoolVacManufacturer.德国莱宝,
                Height = 608,
                TotolHeight = 787,
                PumpingSpeedH2O=46000,
                PumpingSpeedAr=13500,
                PumpingSpeedN2=18000,
                PumpingSpeedH2=14000,
                PumpingSpeedHe=4000,
                CrossoverValue=850,
                CoolDownTime=180,
                Weight=65,
                InN2DN = 63,
                OutN2DN = 63

            });
            VACDict.Add("DN1000", new ParVAC
            {

                Flanch = ParFlanchDict.FlanchDict["DN1000"],
                Manfacturer = CoolVacManufacturer.自研低温泵,
                Height = 787,
                TotolHeight = 886,
                PumpingSpeedH2O = 93000,
                PumpingSpeedAr = 25000,
                PumpingSpeedN2 = 30000,
                PumpingSpeedH2 = 30000,
                PumpingSpeedHe = 7000,
                CrossoverValue = 1200,
                CoolDownTime = 260,
                Weight = 245,
                InN2DN = 63,
                OutN2DN = 63

            });
            VACDict.Add("DN1250", new ParVAC
            {

                Flanch = ParFlanchDict.FlanchDict["DN1250"],
                Manfacturer = CoolVacManufacturer.自研低温泵,
                Height = 829,
                TotolHeight = 950,
                PumpingSpeedH2O = 180000,
                PumpingSpeedAr = 47000,
                PumpingSpeedN2 = 57000,
                PumpingSpeedH2 = 60000,
                PumpingSpeedHe = 15000,
                CrossoverValue = 1000,
                CoolDownTime = 330,
                Weight = 450,
                InN2DN = 63,
                OutN2DN = 63

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

        [DisplayName("法兰参数")]
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


        [DisplayName("泵主体高度")]
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
        [DisplayName("泵总高度")]
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
        private double pumpingSpeedH2O;
        private double pumpingSpeedAr;
        private double pumpingSpeedN2;
        private double pumpingSpeedH2;
        private double pumpingSpeedHe;
        private double crossoverValue;
        private double coolDownTime;
        private ParFlanch inN2 = new ParFlanch();
        private ParFlanch outN2 = new ParFlanch();
        private double inN2DN=63;
        private double outN2DN=63;
  
        private double weight;
        [DisplayName("Weight(Kg)")]
        public double Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
                this.RaisePropertyChanged(() => this.Weight);
            }
        }
        [DisplayName("Pumping Speed H2O")]
        public double PumpingSpeedH2O
        {
            get
            {
                return pumpingSpeedH2O;
            }

            set
            {
                pumpingSpeedH2O = value;
                this.RaisePropertyChanged(() => this.PumpingSpeedH2O);
            }
        }
        [DisplayName("Pumping Speed Ar")]
        public double PumpingSpeedAr
        {
            get
            {
                return pumpingSpeedAr;
            }

            set
            {
                pumpingSpeedAr = value;
                this.RaisePropertyChanged(() => this.PumpingSpeedAr);
            }
        }
        [DisplayName("Pumping Speed N2")]
        public double PumpingSpeedN2
        {
            get
            {
                return pumpingSpeedN2;
            }

            set
            {
                pumpingSpeedN2 = value;
                this.RaisePropertyChanged(() => this.PumpingSpeedN2);
            }
        }
        [DisplayName("Pumping Speed H2")]
        public double PumpingSpeedH2
        {
            get
            {
                return pumpingSpeedH2;
            }

            set
            {
                pumpingSpeedH2 = value;
                this.RaisePropertyChanged(() => this.PumpingSpeedH2);
            }
        }
        [DisplayName("Pumping Speed He")]
        public double PumpingSpeedHe
        {
            get
            {
                return pumpingSpeedHe;
            }

            set
            {
                pumpingSpeedHe = value;
                this.RaisePropertyChanged(() => this.PumpingSpeedHe);
            }
        }
        [DisplayName("Crossover Value at 20K(mbar.I)")]
        public double CrossoverValue
        {
            get
            {
                return crossoverValue;
            }

            set
            {
                crossoverValue = value;
                this.RaisePropertyChanged(() => this.CrossoverValue);
            }
        }
        [DisplayName("Cool Down Time to 20K(min)")]
        public double CoolDownTime
        {
            get
            {
                return coolDownTime;
            }

            set
            {
                coolDownTime = value;
                this.RaisePropertyChanged(() => this.CoolDownTime);
            }
        }
        [ReadOnly(true)]
        [DisplayName("生产厂家")]
        public CoolVacManufacturer Manfacturer { get; set; }
        [DisplayName("液氮进口法兰尺寸")]
        public ParFlanch InN2
        {
            get
            {
                return inN2;
            }

            set
            {
                inN2 = value;
            }
        }
        [DisplayName("液氮出口法兰尺寸")]
        public ParFlanch OutN2
        {
            get
            {
                return outN2;
            }

            set
            {
                outN2 = value;
            }
        }
        [DisplayName("液氮进口法兰公径")]
        [ItemsSource(typeof(ParFlanchSource))]
        public double InN2DN
        {
            get
            {
                return inN2DN;
            }

            set
            {
                inN2DN = value;

                ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.inN2DN.ToString()];
                Type T = typeof(ParFlanch);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(franch, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.InN2, c, null);
                }
            }
        }
        [DisplayName("液氮出口法兰公径")]
        [ItemsSource(typeof(ParFlanchSource))]
        public double OutN2DN
        {
            get
            {
                return outN2DN;
            }

            set
            {
                outN2DN = value;
                ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.outN2DN.ToString()];
                Type T = typeof(ParFlanch);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(franch, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.OutN2, c, null);
                }
            }
        }
    }
    public enum CoolVacManufacturer
    {
        德国莱宝=0,
        自研低温泵=1
    }
}
