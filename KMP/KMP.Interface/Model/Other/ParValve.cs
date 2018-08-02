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
   public class ParValve:ParameterBase
    {
        public ParValve()
        {
            ServiceLocator.Current.GetInstance<ParValveTypeProxy>();
        }
        double dn;
        ParValveType valveType = new ParValveType();
        [DisplayName("阀类型")]
        [ItemsSource(typeof(ParValveTypeSource))]
        public double DN
        {
            get
            {
                return dn;
            }

            set
            {
                dn = value;
                this.RaisePropertyChanged(() => this.DN);
                ParValveType vac = ServiceLocator.Current.GetInstance<ParValveTypeProxy>().ValveTypeDict["DN" + value.ToString()];
                // ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParValveType);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(vac, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.ValveType, c, null);
                }
            }
        }
        [DisplayName("阀参数")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParValveType ValveType
        {
            get
            {
                return valveType;
            }

            set
            {
                valveType = value;
                this.RaisePropertyChanged(() => this.ValveType);
            }
        }
    }



    public static class ParValveTypeDict
    {
        static Dictionary<string, ParValveType> _ValveTypeDict = new Dictionary<string, ParValveType>();
        public static Dictionary<string, ParValveType> ValveTypeDict
        {
            get
            {
                return _ValveTypeDict;
            }
        }
    }
    [Export(typeof(ParValveTypeProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParValveTypeProxy
    {
        [ImportingConstructor]
        public ParValveTypeProxy()
        {
            ValveTypeDict.Add("DN63", new ParValveType
            {
                A = 110,
                B = 130,
                C = 52,
                D = 173,
                F = 64,
                G = 34,
                H = 140.5,
                I = 64,
                Flanch = ParFlanchDict.FlanchDict["DN63"]
            });
            ValveTypeDict.Add("DN80", new ParValveType
            {
                A = 130,
                B = 140,
                C = 57,
                D = 228,
                F = 75,
                G = 36,
                H = 162,
                I = 77,
                Flanch = ParFlanchDict.FlanchDict["DN80"]
            });
            ValveTypeDict.Add("DN100", new ParValveType
            {
                A = 150,
                B = 178,
                C = 74,
                D = 228,
                F = 75,
                G = 35,
                H = 179,
                I = 77,
                Flanch = ParFlanchDict.FlanchDict["DN100"]
            });
            ValveTypeDict.Add("DN160", new ParValveType
            {
                A = 200,
                B = 228,
                C = 95,
                D = 302,
                F = 75,
                G = 40,
                H = 215,
                I = 77,
                Flanch = ParFlanchDict.FlanchDict["DN160"]
            });
            ValveTypeDict.Add("DN200", new ParValveType
            {
                A = 248,
                B = 276,
                C = 120,
                D = 380,
                F = 75,
                G = 42,
                H = 229.5,
                I = 98,
                Flanch = ParFlanchDict.FlanchDict["DN200"]
            });
            ValveTypeDict.Add("DN250", new ParValveType
            {
                A = 310,
                B = 342,
                C = 147,
                D = 464,
                F = 80,
                G = 48,
                H = 202.5,
                I = 117,
                Flanch = ParFlanchDict.FlanchDict["DN250"]
            });
            ValveTypeDict.Add("DN320", new ParValveType
            {
                A = 425,
                B = 449,
                C = 212.5,
                D = 669.5,
                F = 138,
                G = 100,
                H = 318,
                I = 117,
                Flanch = ParFlanchDict.FlanchDict["DN320"]
            });
            ValveTypeDict.Add("DN400", new ParValveType
            {
                A = 512,
                B = 536,
                C = 256,
                D = 810,
                F = 138,
                G = 100,
                H = 328,
                I = 117,
                Flanch = ParFlanchDict.FlanchDict["DN400"]
            });
        }
        public Dictionary<string,ParValveType> ValveTypeDict
        {
            get { return ParValveTypeDict.ValveTypeDict; }
        }
    }

    public class ParValveTypeSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParValveTypeDict.ValveTypeDict)
            {
                VACs.Add(item.Value.Flanch.DN, item.Key);
            }
            return VACs;
        }
    }
    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("阀参数")]
    public class ParValveType : ParameterBase
    {
        public ParValveType()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
        }
        ParFlanch _flanch = new ParFlanch();
        double a, b, g, c, d, h, i, f;
        public ParFlanch Flanch
        {
            get
            {
                return _flanch;
            }

            set
            {
                _flanch = value;
                this.RaisePropertyChanged(() => this.Flanch);
            }
        }
        /// <summary>
        /// 阀门面宽度
        /// </summary>
        public double A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
                this.RaisePropertyChanged(() => this.A);
            }
        }
        /// <summary>
        /// 阀门面上沿宽度
        /// </summary>
        public double B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
                this.RaisePropertyChanged(() => this.B);
            }
        }
        /// <summary>
        /// 阀门面厚度
        /// </summary>
        public double G
        {
            get
            {
                return g;
            }

            set
            {
                g = value;
                this.RaisePropertyChanged(() => this.G);
            }
        }
        /// <summary>
        /// 阀门圆心距底面距离
        /// </summary>
        public double C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
                this.RaisePropertyChanged(() => this.C);
            }
        }
        /// <summary>
        /// 阀门圆心距顶面距离
        /// </summary>
        public double D
        {
            get
            {
                return d;
            }

            set
            {
                d = value;
                this.RaisePropertyChanged(() => this.D);
            }
        }
        /// <summary>
        /// 阀门上部长度
        /// </summary>
        public double H
        {
            get
            {
                return h;
            }

            set
            {
                h = value;
                this.RaisePropertyChanged(() => this.H);
            }
        }
        /// <summary>
        /// 阀门上部直径
        /// </summary>
        public double I
        {
            get
            {
                return i;
            }

            set
            {
                i = value;
                this.RaisePropertyChanged(() => this.I);
            }
        }

        public double F
        {
            get
            {
                return f;
            }

            set
            {
                f = value;
                this.RaisePropertyChanged(() => this.F);
            }
        }
    }

}
