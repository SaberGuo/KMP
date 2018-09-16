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
    [DisplayName("干泵GXS")]
    public class ParGXS : ParameterBase
    {
        public ParGXS() : base()
        {
            ServiceLocator.Current.GetInstance<ParDryPumpDictProxy>();
        }
        private ParDryPump par = new ParDryPump();
        private string dn = "GXS 160";
        [DisplayName("干泵参数")]
        [Description("干泵GXS")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParDryPump Par
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
        [ItemsSource(typeof(ParDryPumpSource))]
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


                ParDryPump vac = ServiceLocator.Current.GetInstance<ParDryPumpDictProxy>().Dict[value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParDryPump);
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
    public static class ParDryPumpDict
    {
        static Dictionary<string, ParDryPump> _Dict = new Dictionary<string, ParDryPump>();
        public static Dictionary<string, ParDryPump> Dict
        {
            get
            {
                return _Dict;
            }
        }

    }
    [Export(typeof(ParDryPumpDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParDryPumpDictProxy
    {
        [ImportingConstructor]
        public ParDryPumpDictProxy()
        {
            Dict.Add("GXS 160", new ParDryPump()
            {
                PartType = "GXS 160",
               
                Length = 1111,
                Width = 390,
                Height = 568,
                
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 305,
                PumpInletFlange=63,
                ExhaustGasOutlet=40,
                TypicalPeakPumpingSpeed=160,
                UItimate= "<1*10﹣²",
                DryPumpMotorRating=7.5,
  
                MinimumFlowRateRequired=4,
                MinimumRequiredPressure=1
            });
            Dict.Add("GXS 160/1750", new ParDryPump()
            {
                PartType = "GXS 160/1750",
                Length = 1111,
                Width = 390,
                Height = 829.5,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 475,
                PumpInletFlange =100,
                ExhaustGasOutlet =40,
                TypicalPeakPumpingSpeed =1160,
                UItimate = "<1*10﹣³",
                DryPumpMotorRating =7.5,
                MechanicalBoosterMotorRating =4.5,
                MinimumFlowRateRequired =7,
                MinimumRequiredPressure =1
            });
            Dict.Add("GXS 250", new ParDryPump()
            {
                PartType = "GXS 250",
                Length = 1111,
                Width = 390,
                Height = 565,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 305,
                PumpInletFlange =63,
                ExhaustGasOutlet =40,
                TypicalPeakPumpingSpeed =250,
                UItimate ="< 1 * 10﹣²",
                DryPumpMotorRating =7.5,
                
                MinimumFlowRateRequired =4,
                MinimumRequiredPressure =1
            });
            Dict.Add("GXS 250/2600", new ParDryPump()
            {
                PartType = "GXS 250/2600",
                Length = 1111,
                Width = 390,
                Height = 829.5,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 515,
                PumpInletFlange =160,
                ExhaustGasOutlet =40,
                TypicalPeakPumpingSpeed =1900,
                UItimate = "<1*10﹣³",
                DryPumpMotorRating =7.5,
                MechanicalBoosterMotorRating =4.5,
                MinimumFlowRateRequired =7,
                MinimumRequiredPressure =1
            });
            Dict.Add("GXS 450", new ParDryPump()
            {
                PartType = "GXS 450",
                Length = 1205,
                Width = 517,
                Height = 717,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 546,
                PumpInletFlange =100,
                ExhaustGasOutlet =50,
                TypicalPeakPumpingSpeed =450,
                UItimate = "<1*10﹣²",
                DryPumpMotorRating =11,
                
                MinimumFlowRateRequired =6,
                MinimumRequiredPressure =1
            });
            Dict.Add("GXS 450/2600", new ParDryPump()
            {
                PartType = "GXS 450/2600",
                Length = 1205,
                Width = 517,
                Height = 1030.5,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 760,
                PumpInletFlange =160,
                ExhaustGasOutlet =50,
                TypicalPeakPumpingSpeed =2200,
                UItimate = "<1*10﹣³",
                DryPumpMotorRating =11,
                MechanicalBoosterMotorRating =7.5,
                MinimumFlowRateRequired =12,
                MinimumRequiredPressure =1.5
            });
            Dict.Add("GXS 450/4200", new ParDryPump()
            {
                PartType = "GXS 450/4200",
                Length = 1205,
                Width = 517,
                Height = 1030.5,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 818,
                PumpInletFlange =160,
                ExhaustGasOutlet =50,
                TypicalPeakPumpingSpeed =3026,
                UItimate = "<1*10﹣³",
                DryPumpMotorRating =11,
                MechanicalBoosterMotorRating =7.5,
                MinimumFlowRateRequired =12,
                MinimumRequiredPressure =1.5
            });
            Dict.Add("GXS 750", new ParDryPump()
            {
                PartType = "GXS 750",
                Length = 1641,
                Width = 517,
                Height = 717,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 679,
                PumpInletFlange =100,
                ExhaustGasOutlet =50,
                TypicalPeakPumpingSpeed =740,
                UItimate = "<1*10﹣²",
                
                MinimumFlowRateRequired =10,
                MinimumRequiredPressure =2
            });
            Dict.Add("GXS 750/2600", new ParDryPump()
            {
                PartType = "GXS 750/2600",
                Length = 1641,
                Width = 517,
                Height = 1030.5,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 918,
                PumpInletFlange =160,
                ExhaustGasOutlet =50,
                TypicalPeakPumpingSpeed =2300,
                UItimate = "<1*10﹣³",
                DryPumpMotorRating =22,
                MechanicalBoosterMotorRating =7.5,
                MinimumFlowRateRequired =12,
                MinimumRequiredPressure =2.5
            });
            Dict.Add("GXS 750/4200", new ParDryPump()
            {
                PartType = "GXS 750/4200",
                Length = 1641,
                Width = 517,
                Height = 1030.5,
                #region
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
                #endregion
                Manfacturer = "爱德华",
                Mass = 976,
                PumpInletFlange =160,
                ExhaustGasOutlet =50,
                TypicalPeakPumpingSpeed =3450,
                UItimate = "<1*10﹣³",
                DryPumpMotorRating =22,
                MechanicalBoosterMotorRating =7.5,
                MinimumFlowRateRequired =12,
                MinimumRequiredPressure =2.5
            });

        }
        public Dictionary<string, ParDryPump> Dict
        {
            get { return ParDryPumpDict.Dict; }
        }

    }

    public class ParDryPumpSource : IItemsSource
    {

        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParDryPumpDict.Dict)
            {
                VACs.Add(item.Value.PartType, item.Key);
            }
            return VACs;
        }
    }
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("干泵参数")]
    public class ParDryPump:ParameterBase
    {
        public ParDryPump()
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
        double mass;
        double pumpInletFlange;
        double exhaustGasOutlet;
        double typicalPeakPumpingSpeed;
        string uItimate;
        double dryPumpMotorRating;
        double mechanicalBoosterMotorRating;
        double minimumFlowRateRequired;
        double minimumRequiredPressure;
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
        [DisplayName("Pump inlet flange（ISO）")]

        public double PumpInletFlange
        {
            get
            {
                return pumpInletFlange;
            }

            set
            {
                pumpInletFlange = value;
                this.RaisePropertyChanged(() => this.PumpInletFlange);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Exhaustgas outlet(NW)")]
        public double ExhaustGasOutlet
        {
            get
            {
                return exhaustGasOutlet;
            }

            set
            {
                exhaustGasOutlet = value;
                this.RaisePropertyChanged(() => this.ExhaustGasOutlet);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Typical peak pumping speed（m3/h）")]
        public double TypicalPeakPumpingSpeed
        {
            get
            {
                return typicalPeakPumpingSpeed;
            }

            set
            {
                typicalPeakPumpingSpeed = value;
                this.RaisePropertyChanged(() => this.TypicalPeakPumpingSpeed);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("UItimate（shaft seal purge only）")]
        public string UItimate
        {
            get
            {
                return uItimate;
            }

            set
            {
                uItimate = value;
                this.RaisePropertyChanged(() => this.UItimate);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Dry pump motor rating(KW)")]
        public double DryPumpMotorRating
        {
            get
            {
                return dryPumpMotorRating;
            }

            set
            {
                dryPumpMotorRating = value;
                this.RaisePropertyChanged(() => this.DryPumpMotorRating);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Mechanical booster motor rating（KW）")]
        public double MechanicalBoosterMotorRating
        {
            get
            {
                return mechanicalBoosterMotorRating;
            }

            set
            {
                mechanicalBoosterMotorRating = value;
                this.RaisePropertyChanged(() => this.MechanicalBoosterMotorRating);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Minimum flow rate required（I/min）")]
        public double MinimumFlowRateRequired
        {
            get
            {
                return minimumFlowRateRequired;
            }

            set
            {
                minimumFlowRateRequired = value;
                this.RaisePropertyChanged(() => this.MinimumFlowRateRequired);
            }
        }
        [Category("干泵工艺参数")]
        [DisplayName("Minimum required pressure differential across supply and return（bar）")]
        public double MinimumRequiredPressure
        {
            get
            {
                return minimumRequiredPressure;
            }

            set
            {
                minimumRequiredPressure = value;
                this.RaisePropertyChanged(() => this.MinimumRequiredPressure);
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
