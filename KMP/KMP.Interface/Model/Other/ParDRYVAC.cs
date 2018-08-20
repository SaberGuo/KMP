using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Practices.Prism.ViewModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
namespace KMP.Interface.Model.Other
{
   public class ParDRYVAC:ParameterBase
    {
        public ParDRYVAC():base()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }
        #region
        private double lenth;
        private double width;
        private double height;
        private double topDN;
        private ParFlanch topFlanch=new ParFlanch();
        private double topHoleDepth;
        private double topHoleX;
        private double topHoleY;
        private double screenLength;
        private double screenWidth;
        private double screenHeight1;
        private double screenHeight2;
        private double screenX;
        private double screenY;
        private double fanWidth;
        private double fanX;
        private double fanY;
        private double valveX;
        private double valveY;
        private double valveInDia;
        private double valveThinkness;
        private double pumpHeight;
        private double pumpLenght;
        private double pumpWidth;
        private double pumpX;
        private double pumpY;
        private double sideDN;
        private ParFlanch sideFlanch = new ParFlanch();
        private double sideFlanchX;
        private double sideFlanchY;
        private double sideHoleDia;
        private double sideHoleThinkness;
        private double sideHoleX1;
        private double sideHoleX2;
        private double sideHoleY1;
        private double sideHoleY2;
        private double bottomHeight;
        #endregion
        #region 干泵长宽高
        [Category("干泵参数")]
        [DisplayName("泵长")]
        public double Lenth
        {
            get
            {
                return lenth;
            }

            set
            {
                lenth = value;
            }
        }
        [Category("干泵参数")]
        [DisplayName("泵宽")]
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
        [Category("干泵参数")]
        [DisplayName("泵高")]
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
        #endregion
        #region 顶部接口法兰
        [Category("顶部接口")]
        [DisplayName("法兰参数")]
        public ParFlanch TopFlanch
        {
            get
            {
                return topFlanch;
            }

            set
            {
                topFlanch = value;
            }
        }
        [Category("顶部接口")]
        [DisplayName("法兰DN")]
        public double TopDN
        {
            get
            {
                return topDN;
            }

            set
            {
                topDN = value;
                this.RaisePropertyChanged(() => this.TopDN);
                ParFlanch vac = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParFlanch);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(vac, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.TopFlanch, c, null);
                }
            }
        }
        [Category("顶部接口")]
        [DisplayName("接口深度")]
        public double TopHoleDepth
        {
            get
            {
                return topHoleDepth;
            }

            set
            {
                topHoleDepth = value;
            }
        }
        [Category("顶部接口")]
        [DisplayName("接口X位置")]
        public double TopHoleX
        {
            get
            {
                return topHoleX;
            }

            set
            {
                topHoleX = value;
            }
        }
        [Category("顶部接口")]
        [DisplayName("接口Y位置")]
        public double TopHoleY
        {
            get
            {
                return topHoleY;
            }

            set
            {
                topHoleY = value;
            }
        }
        #endregion
        #region 顶部触摸屏
        [Category("顶部触摸屏")]
        [DisplayName("触摸屏长度")]
        public double ScreenLength
        {
            get
            {
                return screenLength;
            }

            set
            {
                screenLength = value;
            }
        }
        [Category("顶部触摸屏")]
        [DisplayName("触摸屏宽度")]
        public double ScreenWidth
        {
            get
            {
                return screenWidth;
            }

            set
            {
                screenWidth = value;
            }
        }
        [Category("顶部触摸屏")]
        [DisplayName("触摸屏左侧高")]
        public double ScreenHeight1
        {
            get
            {
                return screenHeight1;
            }

            set
            {
                screenHeight1 = value;
            }
        }
        [Category("顶部触摸屏")]
        [DisplayName("触摸屏右侧高")]
        public double ScreenHeight2
        {
            get
            {
                return screenHeight2;
            }

            set
            {
                screenHeight2 = value;
            }
        }
        [Category("顶部触摸屏")]
        [DisplayName("触摸屏X位置")]
        public double ScreenX
        {
            get
            {
                return screenX;
            }

            set
            {
                screenX = value;
            }
        }
        [Category("顶部触摸屏")]
        [DisplayName("触摸屏Y位置")]
        public double ScreenY
        {
            get
            {
                return screenY;
            }

            set
            {
                screenY = value;
            }
        }
        #endregion
        #region 风扇参数
        [Category("风扇")]
        [DisplayName("宽度")]
        public double FanWidth
        {
            get
            {
                return fanWidth;
            }

            set
            {
                fanWidth = value;
            }
        }
        [Category("风扇")]
        [DisplayName("X位置")]
        public double FanX
        {
            get
            {
                return fanX;
            }

            set
            {
                fanX = value;
            }
        }
        [Category("风扇")]
        [DisplayName("Y位置")]
        public double FanY
        {
            get
            {
                return fanY;
            }

            set
            {
                fanY = value;
            }
        }
        #endregion
        #region 侧边阀门参数
        [Category("侧边阀门")]
        [DisplayName("X位置")]
        public double ValveX
        {
            get
            {
                return valveX;
            }

            set
            {
                valveX = value;
            }
        }
        [Category("侧边阀门")]
        [DisplayName("Y位置")]
        public double ValveY
        {
            get
            {
                return valveY;
            }

            set
            {
                valveY = value;
            }
        }
        [Category("侧边阀门")]
        [DisplayName("内直径")]
        public double ValveInDia
        {
            get
            {
                return valveInDia;
            }

            set
            {
                valveInDia = value;
            }
        }
        [Category("侧边阀门")]
        [DisplayName("阀壁厚度")]
        public double ValveThinkness
        {
            get
            {
                return valveThinkness;
            }

            set
            {
                valveThinkness = value;
            }
        }
        #endregion
        #region 泵代替物参数
        [Browsable(false)]
        public double PumpHeight
        {
            get
            {
                return pumpHeight;
            }

            set
            {
                pumpHeight = value;
            }
        }
        [Browsable(false)]
        public double PumpLenght
        {
            get
            {
                return pumpLenght;
            }

            set
            {
                pumpLenght = value;
            }
        }
        [Browsable(false)]
        public double PumpWidth
        {
            get
            {
                return pumpWidth;
            }

            set
            {
                pumpWidth = value;
            }
        }
        [Browsable(false)]
        public double PumpX
        {
            get
            {
                return pumpX;
            }

            set
            {
                pumpX = value;
            }
        }
        [Browsable(false)]
        public double PumpY
        {
            get
            {
                return pumpY;
            }

            set
            {
                pumpY = value;
            }
        }
        #endregion
        #region 侧边接口参数
        [Category("侧边接口")]
        [DisplayName("法兰参数")]
        public ParFlanch SideFlanch
        {
            get
            {
                return sideFlanch;
            }

            set
            {
                sideFlanch = value;
            }
        }
        [Category("侧边接口")]
        [DisplayName("法兰DN")]
        public double SideDN
        {
            get
            {
                return sideDN;
            }

            set
            {
                sideDN = value;
                this.RaisePropertyChanged(() => this.SideDN);
                ParFlanch vac = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParFlanch);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(vac, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.SideFlanch, c, null);
                }
            }
        }
        [Category("侧边接口")]
        [DisplayName("X位置")]
        public double SideFlanchX
        {
            get
            {
                return sideFlanchX;
            }

            set
            {
                sideFlanchX = value;
            }
        }
        [Category("侧边接口")]
        [DisplayName("Y位置")]
        public double SideFlanchY
        {
            get
            {
                return sideFlanchY;
            }

            set
            {
                sideFlanchY = value;
            }
        }
        #endregion
        #region 侧边两个小孔接口参数
        [Category("侧边孔")]
        [DisplayName("孔直径")]
        public double SideHoleDia
        {
            get
            {
                return sideHoleDia;
            }

            set
            {
                sideHoleDia = value;
            }
        }
        [Category("侧边孔")]
        [DisplayName("孔壁厚度")]
        public double SideHoleThinkness
        {
            get
            {
                return sideHoleThinkness;
            }

            set
            {
                sideHoleThinkness = value;
            }
        }
        [Category("侧边孔")]
        [DisplayName("孔1X位置")]
        public double SideHoleX1
        {
            get
            {
                return sideHoleX1;
            }

            set
            {
                sideHoleX1 = value;
            }
        }
        [Category("侧边孔")]
        [DisplayName("孔2X位置")]
        public double SideHoleX2
        {
            get
            {
                return sideHoleX2;
            }

            set
            {
                sideHoleX2 = value;
            }
        }
        [Category("侧边孔")]
        [DisplayName("孔1Y位置")]
        public double SideHoleY1
        {
            get
            {
                return sideHoleY1;
            }

            set
            {
                sideHoleY1 = value;
            }
        }
        [Category("侧边孔")]
        [DisplayName("孔2Y位置")]
        public double SideHoleY2
        {
            get
            {
                return sideHoleY2;
            }

            set
            {
                sideHoleY2 = value;
            }
        }


        #endregion
        [DisplayName("底部支撑高度")]
        public double BottomHeight
        {
            get
            {
                return bottomHeight;
            }

            set
            {
                bottomHeight = value;
            }
        }
    }
}
