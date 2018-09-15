﻿using System;
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
    [DisplayName("电加热器")]
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
        [Category("电加热器参数")]
        [DisplayName("直径")]
        [Description("电加热器1")]
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
        [Category("电加热器参数")]
        [DisplayName("高度")]
        [Description("电加热器1")]
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
        #region 管道
        /// <summary>
        /// 上下位置
        /// </summary>
        [DisplayName("垂直与中心距离")]
        [Category("管道")]
        [Description("电加热器2")]
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
        [Category("管道")]
        [Description("电加热器2")]
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
        [Category("管道")]
        [Description("电加热器2")]
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
        [DisplayName("法兰参数")]
        [Category("管道")]
        [Description("电加热器2")]
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
        [Category("管道")]
        [Description("电加热器2")]
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
        #endregion
    }
}
