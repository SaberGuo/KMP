using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Practices.Prism.ViewModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition;
namespace KMP.Interface.Model.Other
{
    /// <summary>
    /// 分子泵参数
    /// </summary>
   public class ParMolecularPump:ParameterBase
    {
        public ParMolecularPump():base()
        {
            ServiceLocator.Current.GetInstance<ParMolecularDictProxy>();
        }
        private double mAGW;

        [DisplayName("分子泵型号选择")]
        [ItemsSource(typeof(ParMolecularSource))]
        public double MAGW
        {
            get
            {
                return mAGW;
            }

            set
            {
                mAGW = value;
                this.RaisePropertyChanged(() => this.MAGW);
                ParMolecular molecular = ServiceLocator.Current.GetInstance<ParMolecularDictProxy>().MolecularDict[value.ToString()];
                Type T = typeof(ParMolecular);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(molecular, null);

                    item.SetValue(this.Molecular, c, null);
                }
            }
        }
        [DisplayName("分子泵参数")]
        public ParMolecular Molecular
        {
            get
            {
                return _molecular;
            }

            set
            {
                _molecular = value;
            }
        }

        private ParMolecular _molecular=new ParMolecular();
   
    }

    public static class ParMolecularDict
    {
        static Dictionary<string, ParMolecular> _VACDict = new Dictionary<string, ParMolecular>();
        public static Dictionary<string, ParMolecular> MolecularDict
        {
            get
            {
                return _VACDict;
            }
        }

    }
    [Export(typeof(ParMolecularDictProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ParMolecularDictProxy
    {
        [ImportingConstructor]
        public ParMolecularDictProxy()
        {
            ServiceLocator.Current.GetInstance<ParFlanchDictProxy>();
            MolecularDict.Add("1300", new ParMolecular()
            {
                MAGW = 1300,
                Flanch = ParFlanchDict.FlanchDict["DN200"],
                D1 = 285,
                H = 305

            });
            MolecularDict.Add("1600", new ParMolecular()
            {
                MAGW=1600,
                Flanch = ParFlanchDict.FlanchDict["DN250"],
                D1 = 317,
                H = 325

            });
            MolecularDict.Add("1700", new ParMolecular()
            {
                MAGW = 1700,
                Flanch = ParFlanchDict.FlanchDict["DN250"],
                D1 = 317,
                H = 325

            });
            MolecularDict.Add("2200", new ParMolecular()
            {
                MAGW=2200,
                Flanch = ParFlanchDict.FlanchDict["DN250"],
                D1 = 349,
                H = 355

            });
        }
        public Dictionary<string, ParMolecular> MolecularDict
        {
            get { return ParMolecularDict.MolecularDict; }
        }

    }

    public class ParMolecularSource : IItemsSource
    {

        public ItemCollection GetValues()
        {
            ItemCollection VACs = new ItemCollection();
            foreach (var item in ParMolecularDict.MolecularDict)
            {
                VACs.Add(item.Value.MAGW, item.Key);
            }
            return VACs;
        }
    }

    [TypeConverterAttribute(typeof(ExpandableObjectConverter)), Description("分子泵参数")]
    public class ParMolecular : ParameterBase
    {
        private ParFlanch flanch = new ParFlanch();
        public ParFlanch Flanch
        {
            get
            {
                return flanch;
            }

            set
            {
                flanch = value;
                this.RaisePropertyChanged(() => this.Flanch);
            }
        }
        /// <summary>
        /// 主题直径
        /// </summary>
        [DisplayName("主体直径")]
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
        /// 总长度
        /// </summary>
        [DisplayName("总长度")]
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
        [Browsable(false)]
        public double MAGW
        {
            get
            {
                return mAGW;
            }

            set
            {
                mAGW = value;
            }
        }

        private double d1;
        private double h;
        private double mAGW;
    }

}
