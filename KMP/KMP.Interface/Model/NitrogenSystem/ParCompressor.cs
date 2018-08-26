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
        [DisplayName("厚度（T）")]
        [Description("压缩机")]
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
        [DisplayName("宽度（W）")]
        [Description("压缩机")]
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
        [DisplayName("高度（h）")]
        [Description("压缩机")]
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
        #region 面板
        [DisplayName("窗口高度")]
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        #endregion
    }
}
