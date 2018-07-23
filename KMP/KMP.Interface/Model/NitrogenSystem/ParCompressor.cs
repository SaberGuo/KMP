using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace KMP.Interface.Model.NitrogenSystem
{
   public class ParCompressor : ParameterBase
    {
        double thinckness;
        double width;
        double height;
        double winHeight;
        double winWidth;
        double winDepth;
        double distanceSF;
        double distanceTop;
        [DisplayName("前后厚度")]
        public double ThinckNess
        {
            get
            {
                return thinckness;
            }

            set
            {
                thinckness = value;
            }
        }
        [DisplayName("宽度")]
        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
        [DisplayName("高度")]
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
        [DisplayName("窗口高度")]
        public double WinHeight
        {
            get
            {
                return winHeight;
            }

            set
            {
                winHeight = value;
            }
        }
        [DisplayName("窗口宽度")]
        public double WinWidth
        {
            get
            {
                return winWidth;
            }

            set
            {
                winWidth = value;
            }
        }
        [DisplayName("窗口深度")]
        public double WinDepth
        {
            get
            {
                return winDepth;
            }

            set
            {
                winDepth = value;
            }
        }
        [DisplayName("窗口与压缩机侧边距离")]
        public double DistanceSF
        {
            get
            {
                return distanceSF;
            }

            set
            {
                distanceSF = value;
            }
        }
        [DisplayName("窗口与压缩机顶部距离")]
        public double DistanceTop
        {
            get
            {
                return distanceTop;
            }

            set
            {
                distanceTop = value;
            }
        }
    }
}
