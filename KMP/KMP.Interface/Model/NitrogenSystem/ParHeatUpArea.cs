using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace KMP.Interface.Model.NitrogenSystem
{
    
   public class ParHeatUpArea : ParameterBase
    {
        int electricHeaterNum;
        int compressorNum;
        ObservableCollection<double> offsets = new ObservableCollection<double>();
        [DisplayName("模型间距离")]
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
        [DisplayName("电加热器数量")]
        public int ElectricHeaterNum
        {
            get
            {
                return electricHeaterNum;
            }

            set
            {
                electricHeaterNum = value;
                SetOffsetsNum();
            }
        }
        [DisplayName("压缩机数量")]
        public int CompressorNum
        {
            get
            {
                return compressorNum;
            }

            set
            {
                compressorNum = value;
                SetOffsetsNum();
            }
        }
        void SetOffsetsNum()
        {
            int i =electricHeaterNum+compressorNum;
            if (Offsets.Count > i - 1)
            {
                for (int h = i - 1; h < offsets.Count; h++)
                {
                    Offsets.RemoveAt(h);
                }
            }
            if (offsets.Count < i - 1)
            {
                for (int h = offsets.Count; h < i - 1; h++)
                {
                    offsets.Add(3000);
                }
            }
        }
    }
}
