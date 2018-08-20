using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
namespace KMP.Interface.Model.NitrogenSystem
{
   public class ParElectricHeater : ParameterBase
    {
      
        double _dimension;
        double _height;

        double positionDistance;
        double pipeLenght;
        double pipeThickness;
        ParFlanch parFlanch = new ParFlanch();
        private double flanchDN = 10;
        /// <summary>
        /// 直径
        /// </summary>
        [DisplayName("直径")]
        public double Dimension
        {
            get
            {
                return _dimension;
            }

            set
            {
                _dimension = value;
                this.RaisePropertyChanged(() => this.Dimension);
            }
        }
        /// <summary>
        /// 高度
        /// </summary>
        [DisplayName("高度")]
        public double Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
                this.RaisePropertyChanged(() => this.Height);
            }
        }
        /// <summary>
        /// 上下位置
        /// </summary>
        [DisplayName("上下位置")]
        public double PositionDistance
        {
            get
            {
                return positionDistance;
            }

            set
            {
                positionDistance = value;
            }
        }
        /// <summary>
        /// 管道长度
        /// </summary>
        [DisplayName("管道长度")]
        public double PipeLenght
        {
            get
            {
                return pipeLenght;
            }

            set
            {
                pipeLenght = value;
            }
        }
        /// <summary>
        /// 管道厚度
        /// </summary>
        [DisplayName("管道厚度")]
        public double PipeThickness
        {
            get
            {
                return pipeThickness;
            }

            set
            {
                pipeThickness = value;
            }
        }
        /// <summary>
        /// 法兰参数
        /// </summary>
        public ParFlanch ParFlanch
        {
            get
            {
                return parFlanch;
            }

            set
            {
                parFlanch = value;
            }
        }
        [DisplayName("法兰公称通径")]
        [ItemsSource(typeof(ParFlanchSource))]
        public double FlanchDN
        {
            get
            {
                return this.flanchDN;
            }
            set
            {
                this.flanchDN = value;
                ParFlanch franch = ServiceLocator.Current.GetInstance<ParFlanchDictProxy>().FlanchDict["DN" + this.flanchDN.ToString()];
                Type T = typeof(ParFlanch);
                PropertyInfo[] propertys = T.GetProperties();
                foreach (var item in propertys)
                {
                    object c = item.GetValue(franch, null);
                    //object d = item.GetValue(this.ParFlanch, null);
                    item.SetValue(this.ParFlanch, c, null);
                }
            }
        }
    }
}
