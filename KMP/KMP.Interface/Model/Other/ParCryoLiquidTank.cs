using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.ComponentModel.Composition;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;

namespace KMP.Interface.Model.Other
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
                flanches.Add(item.Value.Capacity.ToString(), item.Key);
            }

            return flanches;
        }
    }
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("液体储槽参数")]
    public class ParCryoLiquidTank:ParameterBase
    {
       
        ParTankCapacity capacity=new ParTankCapacity();
        double capacityDN;
        [DisplayName("容积参数")]
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
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("储槽罐参数")]
    public class ParTankCapacity
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
            }
        }
    }


}
