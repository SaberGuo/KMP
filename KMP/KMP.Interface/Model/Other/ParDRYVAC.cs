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
