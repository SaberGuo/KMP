using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace KMP.Interface.Model.NitrogenSystem
{
    [DisplayName("低温液体储槽阵列")]
    public  class ParCryoLiquidTanks:ParameterBase
    {
        //int _number=1;
        //double _offset;
        int liquidTankNum=1;
        int nitrogenTankNum=1;
        int vaporizerNum=1;
        ObservableCollection<double> offsets = new ObservableCollection<double>();
        //[DisplayName("阵列数量")]
        //public int Number
        //{
        //    get
        //    {
        //        return _number;
        //    }

        //    set
        //    {
        //        _number = value;
        //    }
        //}
        //[DisplayName("间距")]
        //public double Offset
        //{
        //    get
        //    {
        //        return _offset;
        //    }

        //    set
        //    {
        //        _offset = value;
        //    }
        //}
        [DisplayName("阵列距离集合")]
        [Description("低温液体储槽阵列")]
        public ObservableCollection<double> Offsets
        {
            get
            {
                return offsets;
            }

            set
            {
                offsets = value;
            }
        }
        /// <summary>
        /// 液氮储槽数量
        /// </summary>
        [DisplayName("液氮储槽数量")]
        [Description("低温液体储槽阵列")]
        public int LiquidTankNum
        {
            get
            {
                return liquidTankNum;
            }

            set
            {
                liquidTankNum = value;
                SetOffsetsNum();
            }
        }
        /// <summary>
        /// 氮气储罐数量
        /// </summary>
        [DisplayName("氮气储罐数量")]
        [Description("低温液体储槽阵列")]
        public int NitrogenTankNum
        {
            get
            {
                return nitrogenTankNum;
            }

            set
            {
                nitrogenTankNum = value;
                SetOffsetsNum();
            }
        }
        /// <summary>
        /// 汽化器数量
        /// </summary>
        [DisplayName("汽化器数量")]
        [Description("低温液体储槽阵列")]
        public int VaporizerNum
        {
            get
            {
                return vaporizerNum;
            }

            set
            {
                vaporizerNum = value;
                SetOffsetsNum();
            }
        }
        void  SetOffsetsNum()
        {
            int i = VaporizerNum + NitrogenTankNum + LiquidTankNum;
            if(Offsets.Count>i-1)
            {
               for(int h=i-1;h<offsets.Count;h++)
                {
                    Offsets.RemoveAt(h);
                }
            }
            if(offsets.Count<i-1)
            {
               for(int h=offsets.Count;h<i-1;h++)
                {
                    offsets.Add(3500);
                }
            }
        }
    }
}
