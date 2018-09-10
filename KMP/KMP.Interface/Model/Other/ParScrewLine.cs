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
    /// <summary>
    /// 干泵SP
    /// </summary>
    public class ParScrewLine:ParameterBase
    {
        public ParScrewLine()
        {
            ServiceLocator.Current.GetInstance<ParDryPumpSPDictProxy>();
        }
        private ParDryPumpSP par = new ParDryPumpSP();
        private string dn = "GXS 160";
        [DisplayName("干泵参数")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParDryPumpSP Par
        {
            get
            {
                return par;
            }

            set
            {
                par = value;
                this.RaisePropertyChanged(() => this.Par);
            }
        }
        [DisplayName("干泵型号")]
        [ItemsSource(typeof(ParDryPumpSPSource))]
        public string Dn
        {
            get
            {
                return dn;
            }

            set
            {
                dn = value;
                this.RaisePropertyChanged(() => this.Dn);


                ParDryPumpSP vac = ServiceLocator.Current.GetInstance<ParDryPumpSPDictProxy>().Dict[value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParDryPumpSP);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(vac, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.Par, c, null);
                }
            }
        }
    }
    public static class ParDryPumpSPDict
    {
        static Dictionary<string, ParDryPumpSP> _Dict = new Dictionary<string, ParDryPumpSP>();
        public static Dictionary<string, ParDryPumpSP> Dict
        {
            get
            {
                return _Dict;
            }
        }

    }
    [Export(typeof(ParDryPumpSPDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParDryPumpSPDictProxy
    {
        [ImportingConstructor]
        public ParDryPumpSPDictProxy()
        {
            Dict.Add("SP 250", new ParDryPumpSP()
            {
                PartType = "SP 250",
                Manfacturer="德国莱宝",
                EffectivePumpingSpeed = 270,
                UItimatePressure = "≤0.01",
                Mass = 450,
                NominalPowerRating = 7.5,
                Cooling = "Air",
                Width=450,
                Lenth=1348,
                Height=646,
                SideFlanchX = 525,
            SideFlanchY = 115,
            SideDN = 63,


            HandleWidth = 120,
            HandleLength = 65,
            HandleDepth = 20,

            BottomHeight = 166,


            TopHoleDepth = 173,
            TopHoleX = 442,
           // TopHoleY = Width / 2,
            FlanchDiameter = 190,
            FlanchIndiameter3 = 65,
            FlanchIndiameter2 = 70,
            FlanchInDiameter1 = 90,
            FlanchInDepth1 = 4,
            FlanchInDepth2 = 5,
            FlanchInDepth3 = 26,
            ScrewDiameter1 = 13.5,
            ScrewDiameter2 = 19,
            ScrewRangeDiameter1 = 130,
            ScrewRangeDiameter2 = 152,

            FlanchCYDiameter = 4,
            FlanchRactWith = 5,
            FlanchRactHeight = 1,

            AirHeight = 315,
            AirWidth = 243,
            ForX = 88,
            ForY = 435,
            SideY = 590,
        });
            Dict.Add("SP 630", new ParDryPumpSP()
            {
                PartType = "SP 630",
                Manfacturer = "德国莱宝",
                EffectivePumpingSpeed = 630,
                UItimatePressure = "≤0.01",
                Mass = 530,
                NominalPowerRating = 15,
                Cooling = "Air",
                Width = 550,
                Lenth = 1220,
                Height = 806,
                SideFlanchX = 525,
                SideFlanchY = 115,
                SideDN = 63,

                HandleWidth = 120,
                HandleLength = 65,
                HandleDepth = 20,

                BottomHeight = 166,


                TopHoleDepth = 173,
                TopHoleX = 442,
                // TopHoleY = Width / 2,
                FlanchDiameter = 190,
                FlanchIndiameter3 = 65,
                FlanchIndiameter2 = 70,
                FlanchInDiameter1 = 90,
                FlanchInDepth1 = 4,
                FlanchInDepth2 = 5,
                FlanchInDepth3 = 26,
                ScrewDiameter1 = 13.5,
                ScrewDiameter2 = 19,
                ScrewRangeDiameter1 = 130,
                ScrewRangeDiameter2 = 152,

                FlanchCYDiameter = 4,
                FlanchRactWith = 5,
                FlanchRactHeight = 1,

                AirHeight = 315,
                AirWidth = 243,
                ForX = 88,
                ForY = 435,
                SideY = 590,
            });
        }
        public Dictionary<string, ParDryPumpSP> Dict
        {
            get { return ParDryPumpSPDict.Dict; }
        }

    }
    public class ParDryPumpSPSource : IItemsSource
    {

        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParDryPumpSPDict.Dict)
            {
                VACs.Add(item.Value.PartType, item.Key);
            }
            return VACs;
        }
    }

    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("干泵参数")]
    public class ParDryPumpSP:ParameterBase
    {
        public ParDryPumpSP()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }
        #region
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
 
        [Category("干泵结构参数")]
        [DisplayName("干泵长度")]
        public double Lenth
        {
            get
            {
                return lenth;
            }

            set
            {
                lenth = value;
                this.RaisePropertyChanged(() => this.Lenth);
            }
        }
        [Category("干泵结构参数")]
        [DisplayName("干泵宽度")]
        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
                this.RaisePropertyChanged(() => this.Width);
            }
        }
        [Category("干泵结构参数")]
        [DisplayName("干泵高度")]
        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                this.RaisePropertyChanged(() => this.Height);
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
        #endregion
        #region 顶部孔参数
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("深度")]
        public double TopHoleDepth
        {
            get
            {
                return topHoleDepth;
            }

            set
            {
                topHoleDepth = value;
                this.RaisePropertyChanged(() => this.TopHoleDepth);
            }
        }
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("X位置")]
        public double TopHoleX
        {
            get
            {
                return topHoleX;
            }

            set
            {
                topHoleX = value;
                this.RaisePropertyChanged(() => this.TopHoleX);
            }
        }
        /// <summary>
        /// Y坐标，不显示
        /// </summary>
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("Y位置")]
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
        [Category("顶部孔")]
        [DisplayName("法兰直径")]
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
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("法兰厚度")]
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
        [Category("顶部孔")]
        [DisplayName("螺丝孔1直径")]
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
        [Category("顶部孔")]
        [DisplayName("螺丝孔2直径")]
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
        [Category("顶部孔")]
        [DisplayName("螺丝孔1环绕直径")]
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
        [Category("顶部孔")]
        [DisplayName("螺丝孔2环绕直径")]
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
        [Category("顶部孔")]
        [DisplayName("法兰内部孔1 直径")]
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
        [Category("顶部孔")]
        [DisplayName("法兰内部孔2 直径")]
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
        [Category("顶部孔")]
        [DisplayName("法兰内部孔3 直径")]
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
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("法兰内部孔1深度")]
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
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("法兰内部孔2 深度")]
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
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("法兰内部孔 3 深度")]
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
        [Category("顶部孔")]
        [DisplayName("法兰旋绕圆柱直径")]
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
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("法兰旋绕长方体宽度")]
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
        [Browsable(false)]
        [Category("顶部孔")]
        [DisplayName("法兰旋绕长方体高度")]
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
        [Category("侧边孔")]
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
                this.RaisePropertyChanged(() => this.SideFlanch);
            }
        }
        [Category("侧边孔")]
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
        [Browsable(false)]
        [Category("侧边孔")]
        [DisplayName("法兰X位置")]
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
        [Browsable(false)]
        [Category("侧边孔")]
        [DisplayName("法兰Y位置")]
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
        [Browsable(false)]
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
        /// <summary>
        /// 把手长度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 把手宽度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 把手深度
        /// </summary>
        [Browsable(false)]
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
    




        #endregion
        #region 透风窗口参数
        private double airWidth;
        private double airHeight;
        private double forX;
        private double forY;
        private double sideY;
        /// <summary>
        /// 窗口宽度
        /// </summary>
        [Browsable(false)]
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
        /// 窗口高度
        /// </summary>
        [Browsable(false)]
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
        /// 窗口前面水平位置
        /// </summary>
        [Browsable(false)]
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
        /// 窗口前面高位置
        /// </summary>
        [Browsable(false)]
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
        /// 窗口侧边高位置
        /// </summary>
        [Browsable(false)]
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

        #region 工艺参数
        private string manfacturer;
        private string partType;
        double mass;
        double effectivePumpingSpeed;
        string uItimatePressure;
        double nominalPowerRating;
        string cooling;
        [ReadOnly(true)]
        [DisplayName("生产厂家")]
        public string Manfacturer
        {
            get
            {
                return manfacturer;
            }

            set
            {
                manfacturer = value;
                this.RaisePropertyChanged(() => this.Manfacturer);
            }
        }
        [ReadOnly(true)]
        [DisplayName("产品型号")]
        public string PartType
        {
            get
            {
                return partType;
            }

            set
            {
                partType = value;
                this.RaisePropertyChanged(() => this.PartType);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("重量（Kg）")]
        public double Mass
        {
            get
            {
                return mass;
            }

            set
            {
                mass = value;
                this.RaisePropertyChanged(() => this.Mass);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Effective Pumping Speed（m3*h-1）")]
        public double EffectivePumpingSpeed
        {
            get
            {
                return effectivePumpingSpeed;
            }

            set
            {
                effectivePumpingSpeed = value;
                this.RaisePropertyChanged(() => this.EffectivePumpingSpeed);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("UItimate Pressure,total（mbar）")]
        public string UItimatePressure
        {
            get
            {
                return uItimatePressure;
            }

            set
            {
                uItimatePressure = value;
                this.RaisePropertyChanged(() => this.UItimatePressure);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Nominal Power Rating(KW)")]
        public double NominalPowerRating
        {
            get
            {
                return nominalPowerRating;
            }

            set
            {
                nominalPowerRating = value;
                this.RaisePropertyChanged(() => this.NominalPowerRating);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Cooling")]
        public string Cooling
        {
            get
            {
                return cooling;
            }

            set
            {
                cooling = value;
                this.RaisePropertyChanged(() => this.Cooling);
            }
        }
        #endregion
    }
}
