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
    [DisplayName("干泵DV")]
    public class ParDRYVAC : ParameterBase
    {
        public ParDRYVAC() : base()
        {
            ServiceLocator.Current.GetInstance<ParDryPumpDictProxy>();
        }
        private ParDRY par = new ParDRY();
        private string dn = "DV450-i";
        [DisplayName("干泵参数")]
        [Description("干泵GXS")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParDRY Par
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
        [Description("干泵GXS")]
        [ItemsSource(typeof(ParDRYSource))]
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


                ParDRY vac = ServiceLocator.Current.GetInstance<ParDRYDictProxy>().Dict[value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParDRY);
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
    public static class ParDRYDict
    {
        static Dictionary<string, ParDRY> _Dict = new Dictionary<string, ParDRY>();
        public static Dictionary<string, ParDRY> Dict
        {
            get
            {
                return _Dict;
            }
        }

    }
    [Export(typeof(ParDRYDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParDRYDictProxy
    {
        [ImportingConstructor]
        public ParDRYDictProxy()
        {
            Dict.Add("DV450-i", new ParDRY()
            {
                PartType = "DV450-i",
                Manfacturer="德国莱宝",
                Width=677,
                Length=1339,
                Height=600,
                MaxPumpingSeed=450,
                IntakeFlange=100,
                Mass=790,
                NominalFlow=6,

                TopDN = 250,
                TopHoleDepth = 47,
                TopHoleX = 442,
                TopHoleY = 395,

                ScreenX = 46,
                ScreenY = 25,
                ScreenLength = 160,
                ScreenHeight1 = 66,
                ScreenHeight2 = 82,
                ScreenWidth = 240,
                BottomHeight = 120,

                FanX = 112,
                FanY = 25,
                FanWidth = 116,

                PumpHeight = 107,
                PumpLenght = 131,
                PumpWidth = 55,
                PumpX = 32,
                PumpY = 214,

                SideFlanchX = 88,
                SideFlanchY = 190,
                SideDN = 63,

                SideHoleDia = 27,
                SideHoleThinkness = 2.4,
                SideHoleX1 = 165,
                SideHoleX2 = 215,
                SideHoleY1 = 100,
                SideHoleY2 = 100,

                ValveInDia = 17.5,
                ValveThinkness = 2.3,
                ValveX = 60,
                ValveY = 395,
            });
            Dict.Add("DV650-i", new ParDRY()
            {
                Width = 677,
                Length = 1339,
                Height = 600,
                   PartType = "DV650-i",
                Manfacturer = "德国莱宝",
                MaxPumpingSeed = 650,
                IntakeFlange = 100,
                Mass = 750,
                NominalFlow = 7.5,
                TopDN = 250,
                TopHoleDepth = 47,
                TopHoleX = 442,
                TopHoleY = 395,

                ScreenX = 46,
                ScreenY = 25,
                ScreenLength = 160,
                ScreenHeight1 = 66,
                ScreenHeight2 = 82,
                ScreenWidth = 240,
                BottomHeight = 120,

                FanX = 112,
                FanY = 25,
                FanWidth = 116,

                PumpHeight = 107,
                PumpLenght = 131,
                PumpWidth = 55,
                PumpX = 32,
                PumpY = 214,

                SideFlanchX = 88,
                SideFlanchY = 190,
                SideDN = 63,

                SideHoleDia = 27,
                SideHoleThinkness = 2.4,
                SideHoleX1 = 165,
                SideHoleX2 = 215,
                SideHoleY1 = 100,
                SideHoleY2 = 100,

                ValveInDia = 17.5,
                ValveThinkness = 2.3,
                ValveX = 60,
                ValveY = 395,
            });
            Dict.Add("DV1200-i", new ParDRY()
            {
                Width = 677,
                Length = 1339,
                Height = 1024,
                   PartType = "DV1200-i",
                Manfacturer = "德国莱宝",
                MaxPumpingSeed = 1250,
                IntakeFlange = 100,
                Mass = 1400,
                NominalFlow = 15,
                TopDN = 250,
                TopHoleDepth = 47,
                TopHoleX = 442,
                TopHoleY = 395,

                ScreenX = 46,
                ScreenY = 25,
                ScreenLength = 160,
                ScreenHeight1 = 66,
                ScreenHeight2 = 82,
                ScreenWidth = 240,
                BottomHeight = 120,

                FanX = 112,
                FanY = 25,
                FanWidth = 116,

                PumpHeight = 107,
                PumpLenght = 131,
                PumpWidth = 55,
                PumpX = 32,
                PumpY = 214,

                SideFlanchX = 88,
                SideFlanchY = 190,
                SideDN = 63,

                SideHoleDia = 27,
                SideHoleThinkness = 2.4,
                SideHoleX1 = 165,
                SideHoleX2 = 215,
                SideHoleY1 = 100,
                SideHoleY2 = 100,

                ValveInDia = 17.5,
                ValveThinkness = 2.3,
                ValveX = 60,
                ValveY = 395,
            });
            Dict.Add("DV5000-i", new ParDRY()
            {
                PartType = "DV5000-i",
                Manfacturer = "德国莱宝",
                Width = 677,
                Length = 1339,
                Height = 1024,
                MaxPumpingSeed = 3800,
                IntakeFlange = 250,
                Mass = 1200,
                NominalFlow = 11,


                TopDN = 250,
            TopHoleDepth = 47,
            TopHoleX = 442,
            TopHoleY = 395,

            ScreenX = 46,
            ScreenY = 25,
            ScreenLength = 160,
            ScreenHeight1 = 66,
            ScreenHeight2 = 82,
            ScreenWidth = 240,
            BottomHeight = 120,

            FanX = 112,
            FanY = 25,
            FanWidth = 116,

            PumpHeight = 107,
            PumpLenght = 131,
            PumpWidth = 55,
            PumpX = 32,
            PumpY = 214,

            SideFlanchX = 88,
            SideFlanchY = 190,
            SideDN = 63,

            SideHoleDia = 27,
            SideHoleThinkness = 2.4,
            SideHoleX1 = 165,
            SideHoleX2 = 215,
            SideHoleY1 = 100,
            SideHoleY2 = 100,

            ValveInDia = 17.5,
            ValveThinkness = 2.3,
            ValveX = 60,
            ValveY = 395,
        });
        }
        public Dictionary<string, ParDRY> Dict
        {
            get { return ParDRYDict.Dict; }
        }

    }

    public class ParDRYSource : IItemsSource
    {

        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParDRYDict.Dict)
            {
                VACs.Add(item.Value.PartType, item.Key);
            }
            return VACs;
        }
    }
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("干泵参数")]
    public class ParDRY : ParameterBase
    {
        public ParDRY()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }

        #region
        private double lenth;
        private double width;
        private double height;
        private double topDN;
        private ParFlanch topFlanch = new ParFlanch();
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
        [Category("干泵结构参数")]
        [DisplayName("泵长")]
        public double Length
        {
            get
            {
                return lenth;
            }

            set
            {
                lenth = value;
                this.RaisePropertyChanged(() => this.Length);
            }
        }
        [Category("干泵结构参数")]
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
                this.RaisePropertyChanged(() => this.Width);
            }
        }
        [Category("干泵结构参数")]
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
                this.RaisePropertyChanged(() => this.Height);
            }
        }
        #endregion
        #region 顶部接口法兰
        [Category("干泵结构参数")]
        [DisplayName("顶部接口法兰参数")]
        public ParFlanch TopFlanch
        {
            get
            {
                return topFlanch;
            }

            set
            {
                topFlanch = value;
                this.RaisePropertyChanged(() => this.TopFlanch);
            }
        }
        [Category("干泵结构参数")]
        [DisplayName("顶部接口法兰DN")]
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
        /// <summary>
        /// 顶部接口深度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 顶部接口X位置
        /// </summary>
        [Browsable(false)]
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
        /// 顶部接口Y位置
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 顶部触摸屏长度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 顶部触摸屏宽度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 顶部触摸屏左侧高
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 顶部触摸屏右侧高度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 顶部触摸屏x位置
        /// </summary>
        [Browsable(false)]

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
        /// <summary>
        /// 顶部触摸屏Y位置
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 风扇宽度
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 风扇x位置
        /// </summary>
        [Browsable(false)]
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
        /// <summary>
        /// 风扇Y位置
        /// </summary>
        [Browsable(false)]
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
                this.RaisePropertyChanged(() => this.ValveX);
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
                this.RaisePropertyChanged(() => this.ValveY);
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
                this.RaisePropertyChanged(() => this.ValveInDia);
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
                this.RaisePropertyChanged(() => this.ValveThinkness);
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
        [Browsable(false)]
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
        [Category("侧边接口")]
        [DisplayName("法兰DN")]
        [Browsable(false)]
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
        [Browsable(false)]
        public double SideFlanchX
        {
            get
            {
                return sideFlanchX;
            }

            set
            {
                sideFlanchX = value;
                this.RaisePropertyChanged(() => this.SideFlanchX);
            }
        }
        [Browsable(false)]
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
                this.RaisePropertyChanged(() => this.SideFlanchY);
            }
        }
        #endregion
        #region 侧边两个小孔接口参数
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Browsable(false)]
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
        /// <summary>
        /// 底部支撑高度
        /// </summary>
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


        #region 工艺参数
        double maxPumpingSeed;
        double mass;
        double intakeFlange;
        double nominalFlow;

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
        [DisplayName("Max pumping speed w/o gas ballast（m3/h）")]
        public double MaxPumpingSeed
        {
            get
            {
                return maxPumpingSeed;
            }

            set
            {
                maxPumpingSeed = value;
                this.RaisePropertyChanged(() => this.MaxPumpingSeed);
            }
        }


        [Category("干泵工艺参数")]
        [DisplayName("IntakeFlange（DN）")]
        public double IntakeFlange
        {
            get
            {
                return intakeFlange;
            }

            set
            {
                intakeFlange = value;
                this.RaisePropertyChanged(() => this.IntakeFlange);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Nominal Flow（I/min）")]
        public double NominalFlow
        {
            get
            {
                return nominalFlow;
            }

            set
            {
                nominalFlow = value;
                this.RaisePropertyChanged(() => this.NominalFlow);
            }
        }
        #endregion
        private string manfacturer;
        private string partType;
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


    }
}
