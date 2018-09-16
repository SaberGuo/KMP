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

namespace KMP.Interface.Model.NitrogenSystem
{
    public static class ParTankCapacityDict
    {
        static Dictionary<string, ParTankCapacity> _Dict = new Dictionary<string, ParTankCapacity>();

        public static Dictionary<string, ParTankCapacity> TankCapacityDict
        {
            get
            {
                return _Dict;
            }
        }


    }

    [Export(typeof(ParTankCapacityDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParTankCapacityDictProxy 
    {
        public ParTankCapacityDictProxy()
        {
            TankCapacityDict.Add("1200", new ParTankCapacity()
            {
                Capacity = 1200,
                Dimension = 1420,
                Height = 3340
            });
            TankCapacityDict.Add("2000", new ParTankCapacity()
            {
                Capacity = 2000,
                Dimension = 1712,
                Height = 3450
            });

            TankCapacityDict.Add("3500", new ParTankCapacity()
            {
                Capacity = 3500,
                Dimension = 2016,
                Height = 4025
            });
            TankCapacityDict.Add("4000", new ParTankCapacity()
            {
                Capacity = 4000,
                Dimension = 2024,
                Height = 5000
            });
            TankCapacityDict.Add("5000", new ParTankCapacity()
            {
                Capacity = 5000,
                Dimension = 2016,
                Height = 5180
            });
            TankCapacityDict.Add("6000", new ParTankCapacity()
            {
                Capacity = 6000,
                Dimension = 2016,
                Height = 5915
            });
            TankCapacityDict.Add("10000", new ParTankCapacity()
            {
                Capacity = 10000,
                Dimension = 2418,
                Height = 6100
            });
            TankCapacityDict.Add("15000", new ParTankCapacity()
            {
                Capacity = 15000,
                Dimension = 2420,
                Height = 8195
            });
            TankCapacityDict.Add("20000", new ParTankCapacity()
            {
                Capacity = 20000,
                Dimension = 2620,
                Height = 8600
            });
            TankCapacityDict.Add("25000", new ParTankCapacity()
            {
                Capacity = 25000,
                Dimension = 2418,
                Height = 6100
            });
            TankCapacityDict.Add("30000", new ParTankCapacity()
            {
                Capacity = 30000,
                Dimension = 2820,
                Height = 10356
            });
            TankCapacityDict.Add("33000", new ParTankCapacity()
            {
                Capacity = 33000,
                Dimension = 2820,
                Height = 11166
            });
            TankCapacityDict.Add("35000", new ParTankCapacity()
            {
                Capacity = 35000,
                Dimension = 3024,
                Height = 10850
            });
            TankCapacityDict.Add("40000", new ParTankCapacity()
            {
                Capacity = 40000,
                Dimension = 3024,
                Height = 11324
            });
            TankCapacityDict.Add("50000", new ParTankCapacity()
            {
                Capacity = 50000,
                Dimension = 3224,
                Height = 12155
            });
            TankCapacityDict.Add("60000", new ParTankCapacity()
            {
                Capacity = 60000,
                Dimension = 3032,
                Height = 17525
            });
            TankCapacityDict.Add("75000", new ParTankCapacity()
            {
                Capacity = 75000,
                Dimension = 3228,
                Height = 17320
            });
            TankCapacityDict.Add("100000", new ParTankCapacity()
            {
                Capacity = 100000,
                Dimension = 3640,
                Height = 17050
            });
        }
        public Dictionary<string, ParTankCapacity> TankCapacityDict
        {
            get { return ParTankCapacityDict.TankCapacityDict; }
        }
    }

    public class ParTankCapacitySource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection flanches = new ItemCollection();

            foreach (var item in ParTankCapacityDict.TankCapacityDict)
            {
                flanches.Add(item.Value.Capacity, item.Key);
            }

            return flanches;
        }
    }

    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("液体储槽参数")]
    public class ParCryoLiquidTank:ParameterBase
    {
        public ParCryoLiquidTank()
        {
            ServiceLocator.Current.GetInstance<ParTankCapacityDictProxy>();
        }
        ParTankCapacity capacity=new ParTankCapacity();
        double capacityDN = 1200;
        [DisplayName("容积参数")]
        [Description("储槽")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParTankCapacity Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                capacity = value;
            }
        }
        [DisplayName("有效容积")]
        [Description("储槽")]
        [ItemsSource(typeof(ParTankCapacitySource))]
        public double CapacityDN
        {
            get
            {
                return capacityDN;
            }

            set
            {
                capacityDN = value;
                ParTankCapacity tankCapacity = ServiceLocator.Current.GetInstance<ParTankCapacityDictProxy>().TankCapacityDict[CapacityDN.ToString()];
                Type T = typeof(ParTankCapacity);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(tankCapacity, null);
                    item.SetValue(this.Capacity, c, null);
                }
            }
        }
    }
    /// <summary>
    /// 储槽罐参数
    /// </summary>
    [DisplayName("储槽罐")]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("储槽罐参数")]
    public class ParTankCapacity:ParameterBase
    {
        double _capacity;
        double _dimension;
        double _height;
        [DisplayName("储槽体积")]
        public double Capacity
        {
            get
            {
                return _capacity;
            }

            set
            {
                _capacity = value;
                this.RaisePropertyChanged(() => this.Capacity);
            }
        }
       [DisplayName("储槽直径")]
        public double Dimension
        {
            get
            {
                return _dimension;
            }

            set
            {
                _dimension = value;
                this.RaisePropertyChanged(() => this.Dimension);
            }
        }
        [DisplayName("储槽高度")]
        public double Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
                this.RaisePropertyChanged(() => this.Height);
            }
        }
    }


}
