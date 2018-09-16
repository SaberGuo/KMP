using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
namespace KMP.Interface.Model.NitrogenSystem
{
    [DisplayName("泵区")]
  public  class ParPumpArea : ParameterBase
    {
        int pumpNum;
        int subCoolerNum;
        double distance;
        ObservableCollection<double> pumpOffsets = new ObservableCollection<double>();
        ObservableCollection<double> subCoolerOffsets = new ObservableCollection<double>();
        [DisplayName("液压泵数量")]
        [Description("泵区")]
        public int PumpNum
        {
            get
            {
                return pumpNum;
            }

            set
            {
                pumpNum = value;
                SetOffsetsNum(pumpNum,PumpOffsets,1000);
            }
        }
        [DisplayName("过冷器数量")]
        [Description("泵区")]
        public int SubCoolerNum
        {
            get
            {
                return subCoolerNum;
            }

            set
            {
                subCoolerNum = value;
                SetOffsetsNum(SubCoolerNum,SubCoolerOffsets,3000);
            }
        }

        //[ItemsSource(typeof(double))]
        [DisplayName("液压泵间距参数")]
        [Description("泵区")]
        public ObservableCollection<double> PumpOffsets
        {
            get
            {
                return pumpOffsets;
            }

            set
            {
                pumpOffsets = value;
            }
        }
        //[ItemsSource(typeof(double))]
        [DisplayName("过冷器间距参数")]
        [Description("泵区")]
        public ObservableCollection<double> SubCoolerOffsets
        {
            get
            {
                return subCoolerOffsets;
            }

            set
            {
                subCoolerOffsets = value;
            }
        }
        [DisplayName("液压泵与过冷器间距")]
        [Description("泵区")]
        public double Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }

        void SetOffsetsNum(int Num,ObservableCollection<double> sub,double value)
        {
            int i = Num;
            if (sub.Count > i - 1)
            {
                for (int h = i - 1; h < sub.Count; h++)
                {
                    sub.RemoveAt(h);
                }
            }
            if (sub.Count < i - 1)
            {
                for (int h = sub.Count; h < i - 1; h++)
                {
                    sub.Add(value);
                }
            }
        }
    }
}
