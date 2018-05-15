using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace KMP.Interface.Model.Container
{
    public static class ParFlanchDict
    {
        static Dictionary<string, ParFlanch> _flanchDict = new Dictionary<string, ParFlanch>();

        public static Dictionary<string, ParFlanch> FlanchDict
        {
            get
            {
                return _flanchDict;
            }
        }

        
    }
    [Export(typeof(ParFlanchDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParFlanchDictProxy
    {
        public ParFlanchDictProxy()
        {
            FlanchDict.Add("DN10", new ParFlanch
                {
                    DN = 10,
                    D6 = 12.2,
                    D0 = 40,
                    D1 = 55,
                    D2 = 30,
                    H = 8,
                    C = 6.6,
                    X = 0.6,
                    D = 6,
                    N = 4
                });

            FlanchDict.Add("DN16", new ParFlanch
                {
                    DN = 16,
                    D6 = 17.2,
                    D0 = 45,
                    D1 = 60,
                    D2 = 35,
                    H = 8,
                    C = 6.6,
                    X = 0.6,
                    D = 6,
                    N = 4
                });

            FlanchDict.Add("DN20", new ParFlanch
                {
                    DN = 20,
                    D6 = 22.2,
                    D0 = 50,
                    D1 = 65,
                    D2 = 40,
                    H = 8,
                    C = 6.6,
                    X = 0.6,
                    D = 6,
                    N = 4
                });
            FlanchDict.Add("DN25", new ParFlanch
                {
                    DN = 25,
                    D6 = 26.2,
                    D0 = 55,
                    D1 = 70,
                    D2 = 45,
                    H = 8,
                    C = 6.6,
                    X = 0.6,
                    D = 6,
                    N = 4
                });

            FlanchDict.Add("DN32", new ParFlanch
                {
                    DN = 32,
                    D6 = 34.2,
                    D0 = 70,
                    D1 = 90,
                    D2 = 55,
                    H = 8,
                    C = 9,
                    X = 1,
                    D = 8,
                    N = 4
                });
            FlanchDict.Add("DN40", new ParFlanch
            {
                DN = 40,
                D6 = 41.2,
                D0 = 80,
                D1 = 100,
                D2 = 65,
                H = 12,
                C = 9,
                X = 1,
                D = 8,
                N = 4
            });
            FlanchDict.Add("DN50", new ParFlanch
            {
                DN = 50,
                D6 = 52.2,
                D0 = 90,
                D1 = 110,
                D2 = 75,
                H = 12,
                C = 9,
                X = 1,
                D = 8,
                N = 4
            });




        }
        public Dictionary<string, ParFlanch> FlanchDict
        {
            get { return ParFlanchDict.FlanchDict; }
        }
    }
    public class ParFlanchSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection flanches = new ItemCollection();

            foreach (var item in ParFlanchDict.FlanchDict)
            {
                flanches.Add(item.Value.DN, item.Key);
            }
           
            return flanches;
        }
    }



    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("法兰参数")]
   public class ParFlanch : ParameterBase
    {
        double dN;
        double d6;
        double d0;
        double d1;
        double d2;
        double h;
        double c;
        double x;
        double d;
        double n;
        /// <summary>
        /// 公称通径
        /// </summary>
        /// 
        
        public double DN
        {
            get
            {
                return dN;
            }

            set
            {
                dN = value;
                
                this.RaisePropertyChanged(() => this.DN);
            }
        }
        /// <summary>
        /// 内径
        /// </summary>
        public double D6
        {
            get
            {
                return d6;
            }

            set
            {
                d6 = value;
                this.RaisePropertyChanged(() => this.D6);
            }
        }
        /// <summary>
        /// 螺栓孔中心距离直径
        /// </summary>
        public double D0
        {
            get
            {
                return d0;
            }

            set
            {
                d0 = value;
                this.RaisePropertyChanged(() => this.D0);
            }
        }
        /// <summary>
        /// 外径
        /// </summary>
        public double D1
        {
            get
            {
                return d1;
            }

            set
            {
                d1 = value;
                this.RaisePropertyChanged(() => this.D1);
            }
        }
        /// <summary>
        /// 凹陷直径
        /// </summary>
        public double D2
        {
            get
            {
                return d2;
            }

            set
            {
                d2 = value;
                this.RaisePropertyChanged(() => this.D2);
            }
        }
        /// <summary>
        /// 法兰厚度
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
        /// 螺栓孔直径
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
        /// 螺栓孔螺丝
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                this.RaisePropertyChanged(() => this.X);
            }
        }
        /// <summary>
        /// 螺栓直径
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
        /// 螺栓数量
        /// </summary>
        public double N
        {
            get
            {
                return n;
            }

            set
            {
                n = value;
                this.RaisePropertyChanged(() => this.N);
            }
        }
    }
}
