using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace KMP.Interface.Model
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
            FlanchDict.Add("DN63", new ParFlanch
            {
                DN = 63,
                D6 = 70,
                D0 = 110,
                D1 = 130,
                D2 = 95,
                H = 12,
                C = 9,
                X = 1,
                D = 8,
                N = 4
            });
            FlanchDict.Add("DN80", new ParFlanch
            {
                DN = 80,
                D6 = 83,
                D0 = 125,
                D1 = 145,
                D2 = 110,
                H = 12,
                C = 9,
                X = 1,
                D = 8,
                N = 8
            });
            FlanchDict.Add("DN100", new ParFlanch
            {
                DN = 100,
                D6 = 102,
                D0 = 145,
                D1 = 165,
                D2 = 130,
                H = 12,
                C = 9,
                X = 1,
                D = 8,
                N = 8
            });
            FlanchDict.Add("DN125", new ParFlanch
            {
                DN = 125,
                D6 = 127,
                D0 = 175,
                D1 = 200,
                D2 = 155,
                H = 16,
                C = 11,
                X = 1,
                D = 10,
                N = 8
            });
            FlanchDict.Add("DN160", new ParFlanch
            {
                DN = 160,
                D6 = 153,
                D0 = 200,
                D1 = 225,
                D2 = 180,
                H = 16,
                C = 11,
                X = 1,
                D = 10,
                N = 8
            });
            FlanchDict.Add("DN200", new ParFlanch
            {
                DN = 200,
                D6 = 213,
                D0 = 260,
                D1 = 285,
                D2 = 240,
                H = 16,
                C = 11,
                X = 1,
                D = 10,
                N = 12
            });
            FlanchDict.Add("DN250", new ParFlanch
            {
                DN = 250,
                D6 = 261,
                D0 = 310,
                D1 = 335,
                D2 = 290,
                H = 16,
                C = 11,
                X = 1,
                D = 10,
                N = 12
            });
            FlanchDict.Add("DN320", new ParFlanch
            {
                DN = 320,
                D6 = 318,
                D0 = 395,
                D1 = 425,
                D2 = 370,
                H = 20,
                C = 14,
                X = 2,
                D = 12,
                N = 12
            });
            FlanchDict.Add("DN400", new ParFlanch
            {
                DN = 400,
                D6 = 400,
                D0 = 480,
                D1 = 510,
                D2 = 450,
                H = 20,
                C = 14,
                X = 2,
                D = 12,
                N = 16
            });
            FlanchDict.Add("DN500", new ParFlanch
            {
                DN = 500,
                D6 = 501,
                D0 = 580,
                D1 = 610,
                D2 = 550,
                H = 20,
                C = 14,
                X = 2,
                D = 12,
                N = 16
            });
            FlanchDict.Add("DN630", new ParFlanch
            {
                DN = 630,
                D6 = 651,
                D0 = 720,
                D1 = 750,
                D2 = 690,
                H = 24,
                C = 14,
                X = 2,
                D = 12,
                N = 20
            });
            FlanchDict.Add("DN800", new ParFlanch
            {
                DN = 800,
                D6 = 800,
                D0 = 890,
                D1 = 920,
                D2 = 860,
                H = 24,
                C = 14,
                X = 2,
                D = 12,
                N = 24
            });
            FlanchDict.Add("DN1000", new ParFlanch
            {
                DN = 1000,
                D6 = 1000,
                D0 = 1090,
                D1 = 1120,
                D2 = 1060,
                H = 24,
                C = 14,
                X = 2,
                D = 12,
                N = 32
            });
            FlanchDict.Add("DN1250", new ParFlanch
            {
                DN = 1250,
                D6 = 1250,
                D0 = 1404,
                D1 = 1440,
                D2 = 1340,
                H = 28,
                C = 19,
                X = 2.5,
                D = 16,
                N = 32
            });
            FlanchDict.Add("DN1600", new ParFlanch
            {
                DN = 1600,
                D6 = 1600,
                D0 = 1755,
                D1 = 1790,
                D2 = 1705,
                H = 30,
                C = 19,
                X = 2.5,
                D = 16,
                N = 32
            });
            FlanchDict.Add("DN1800", new ParFlanch
            {
                DN = 1800,
                D6 = 1800,
                D0 = 1940,
                D1 = 1980,
                D2 = 1920,
                H = 32,
                C = 24,
                X = 2.5,
                D = 20,
                N = 32
            });
            FlanchDict.Add("DN2000", new ParFlanch
            {
                DN = 2000,
                D6 = 2000,
                D0 = 2205,
                D1 = 2245,
                D2 = 2140,
                H = 32,
                C = 24,
                X = 2.5,
                D = 20,
                N = 32
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
