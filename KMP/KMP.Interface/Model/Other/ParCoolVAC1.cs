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
    public class ParCoolVAC1 : ParameterBase
    {
        public ParCoolVAC1()
        {
            ServiceLocator.Current.GetInstance<ParVACDictProxy1>();

        }
        private ParVAC1 _vac = new ParVAC1();
        private double _vacDN;
        [Description("凹面低温泵")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParVAC1 VAC
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
        [Description("凹面低温泵")]
        [ItemsSource(typeof(ParVACSource1))]
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
                ParVAC1 vac = ServiceLocator.Current.GetInstance<ParVACDictProxy1>().VACDict["DN" + value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParVAC1);
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
    public static class ParVACDict1
    {
        static Dictionary<string, ParVAC1> _VACDict = new Dictionary<string, ParVAC1>();
        public static Dictionary<string, ParVAC1> VACDict
        {
            get
            {
                return _VACDict;
            }
        }

    }
    [Export(typeof(ParVACDictProxy1))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParVACDictProxy1
    {
        [ImportingConstructor]
        public ParVACDictProxy1()
        {

            VACDict.Add("DN1000", new ParVAC1
            {

                Flanch = ParFlanchDict.FlanchDict["DN1000"],
                Manfacturer = CoolVacManufacturer.自研低温泵,
                Height = 879,
              
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
            VACDict.Add("DN1250", new ParVAC1
            {

                Flanch = ParFlanchDict.FlanchDict["DN1250"],
                Manfacturer = CoolVacManufacturer.自研低温泵,
                Height = 1000,

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
        public Dictionary<string, ParVAC1> VACDict
        {
            get { return ParVACDict1.VACDict; }
        }

    }
    public class ParVACSource1 : IItemsSource
    {

        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParVACDict1.VACDict)
            {
                VACs.Add(item.Value.Flanch.DN, item.Key);
            }
            return VACs;
        }
    }
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("泵参数")]
    public class ParVAC1 : ParameterBase
    {
        public ParVAC1()
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


        [DisplayName("泵主体长度")]
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
        //[DisplayName("泵总高度")]
        //public double TotolHeight
        //{
        //    get
        //    {
        //        return totolHeight;
        //    }

        //    set
        //    {
        //        totolHeight = value;
        //        this.RaisePropertyChanged(() => this.TotolHeight);
        //    }
        //}
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
        [DisplayName("生产厂家")]
        [ReadOnly(true)]
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
 
}
