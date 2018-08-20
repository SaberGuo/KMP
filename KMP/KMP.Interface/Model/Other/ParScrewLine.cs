using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
namespace KMP.Interface.Model.Other
{
   public class ParScrewLine:ParameterBase
    {
        public ParScrewLine()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }
        private double lenth;
        private double width;
        private double height;

    

        private double sideDN;
        private ParFlanch sideFlanch = new ParFlanch();
        private double sideFlanchX;
        private double sideFlanchY;

        private double bottomHeight;

        private double handleLength;
        private double handleWidth;
        private double handleDepth;
        private double handleX1;
        private double handleX2;
        private double handleY1;
        private double handleY2;

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


     
        private double topHoleDepth;
        private double topHoleX;
        private double topHoleY;
        private double flanchDiameter;
        private double flanchThinkness;
        private double screwDiameter1;
        private double screwDiameter2;
        private double screwRangeDiameter1;
        private double screwRangeDiameter2;
        private double flanchInDiameter1;
        private double flanchIndiameter2;
        private double flanchIndiameter3;
        private double flanchInDepth1;
        private double flanchInDepth2;
        private double flanchInDepth3;
        private double flanchCYDiameter;
        private double flanchRactWith;
        private double flanchRactHeight;

        #region 顶部孔参数
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
        /// <summary>
        /// Y坐标，不显示
        /// </summary>
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

        /// <summary>
        /// 法兰直径
        /// </summary>
        public double FlanchDiameter
        {
            get
            {
                return flanchDiameter;
            }

            set
            {
                flanchDiameter = value;
            }
        }
        /// <summary>
        /// 法兰厚度
        /// </summary>
        public double FlanchThinkness
        {
            get
            {
                return flanchThinkness;
            }

            set
            {
                flanchThinkness = value;
            }
        }
        /// <summary>
        /// 螺丝孔直径1
        /// </summary>
        public double ScrewDiameter1
        {
            get
            {
                return screwDiameter1;
            }

            set
            {
                screwDiameter1 = value;
            }
        }
        /// <summary>
        /// 螺丝孔直径2
        /// </summary>
        public double ScrewDiameter2
        {
            get
            {
                return screwDiameter2;
            }

            set
            {
                screwDiameter2 = value;
            }
        }
        /// <summary>
        /// 螺丝孔旋转直径1
        /// </summary>
        public double ScrewRangeDiameter1
        {
            get
            {
                return screwRangeDiameter1;
            }

            set
            {
                screwRangeDiameter1 = value;
            }
        }
        /// <summary>
        /// 螺丝孔旋转2
        /// </summary>
        public double ScrewRangeDiameter2
        {
            get
            {
                return screwRangeDiameter2;
            }

            set
            {
                screwRangeDiameter2 = value;
            }
        }
        /// <summary>
        /// 法兰内部孔1直径
        /// </summary>
        public double FlanchInDiameter1
        {
            get
            {
                return flanchInDiameter1;
            }

            set
            {
                flanchInDiameter1 = value;
            }
        }
       /// <summary>
       /// 法兰内部孔2 直径
       /// </summary>
        public double FlanchIndiameter2
        {
            get
            {
                return flanchIndiameter2;
            }

            set
            {
                flanchIndiameter2 = value;
            }
        }
        /// <summary>
        /// 法兰内部孔3 直径
        /// </summary>
        public double FlanchIndiameter3
        {
            get
            {
                return flanchIndiameter3;
            }

            set
            {
                flanchIndiameter3 = value;
            }
        }
        /// <summary>
        /// 法兰内部孔1深度
        /// </summary>
        public double FlanchInDepth1
        {
            get
            {
                return flanchInDepth1;
            }

            set
            {
                flanchInDepth1 = value;
            }
        }
        /// <summary>
        /// 法兰内部孔2 深度
        /// </summary>
        public double FlanchInDepth2
        {
            get
            {
                return flanchInDepth2;
            }

            set
            {
                flanchInDepth2 = value;
            }
        }
        /// <summary>
        /// 法兰内部孔 3 深度
        /// </summary>
        public double FlanchInDepth3
        {
            get
            {
                return flanchInDepth3;
            }

            set
            {
                flanchInDepth3 = value;
            }
        }
        /// <summary>
        /// 法兰旋绕圆柱直径
        /// </summary>
        public double FlanchCYDiameter
        {
            get
            {
                return flanchCYDiameter;
            }

            set
            {
                flanchCYDiameter = value;
            }
        }
        /// <summary>
        /// 法兰旋绕长方体宽度
        /// </summary>
        public double FlanchRactWith
        {
            get
            {
                return flanchRactWith;
            }

            set
            {
                flanchRactWith = value;
            }
        }
        /// <summary>
        /// 法兰旋绕长方体高度
        /// </summary>
        public double FlanchRactHeight
        {
            get
            {
                return flanchRactHeight;
            }

            set
            {
                flanchRactHeight = value;
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
        #region 把手参数
        public double HandleLength
        {
            get
            {
                return handleLength;
            }

            set
            {
                handleLength = value;
            }
        }

        public double HandleWidth
        {
            get
            {
                return handleWidth;
            }

            set
            {
                handleWidth = value;
            }
        }

        public double HandleDepth
        {
            get
            {
                return handleDepth;
            }

            set
            {
                handleDepth = value;
            }
        }

        public double HandleX1
        {
            get
            {
                return handleX1;
            }

            set
            {
                handleX1 = value;
            }
        }

        public double HandleX2
        {
            get
            {
                return handleX2;
            }

            set
            {
                handleX2 = value;
            }
        }

        public double HandleY1
        {
            get
            {
                return handleY1;
            }

            set
            {
                handleY1 = value;
            }
        }

        public double HandleY2
        {
            get
            {
                return handleY2;
            }

            set
            {
                handleY2 = value;
            }
        }




        #endregion

        #region 透风窗口参数
        private double airWidth;
        private double airHeight;
        private double forX;
        private double forY;
        private double sideY;
        /// <summary>
        /// 宽
        /// </summary>
        public double AirWidth
        {
            get
            {
                return airWidth;
            }

            set
            {
                airWidth = value;
            }
        }
        /// <summary>
        /// 高
        /// </summary>
        public double AirHeight
        {
            get
            {
                return airHeight;
            }

            set
            {
                airHeight = value;
            }
        }
        /// <summary>
        /// 前面水平位置
        /// </summary>
        public double ForX
        {
            get
            {
                return forX;
            }

            set
            {
                forX = value;
            }
        }
        /// <summary>
        /// 前面高位置
        /// </summary>
        public double ForY
        {
            get
            {
                return forY;
            }

            set
            {
                forY = value;
            }
        }
        /// <summary>
        /// 侧边高位置
        /// </summary>
        public double SideY
        {
            get
            {
                return sideY;
            }

            set
            {
                sideY = value;
            }
        }
        #endregion
    }
}
