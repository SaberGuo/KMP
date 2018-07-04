﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
namespace KMP.Interface.Model
{
  [DisplayName("容器开孔")]
  public  class ParCylinderHole:NotificationObject
    {
        double positionAngle;
        double positionDistance;
       // double holeRadius;
        double pipeLenght;
        double holeOffset;
        double pipeThickness;
        bool isHeatSinkHole;
        ParFlanch parFlanch=new ParFlanch();
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
        /// <summary>
        /// 定位角度
        /// </summary>
        [DisplayName("定位角度")]
        public double PositionAngle
        {
            get
            {
                return positionAngle;
            }

            set
            {
                positionAngle = value;
                this.RaisePropertyChanged(() => this.PositionAngle);
            }
        }
        /// <summary>
        /// 定位距离
        /// </summary>
        [DisplayName("定位距离")]
        public double PositionDistance
        {
            get
            {
                return positionDistance;
            }

            set
            {
                positionDistance = value;
                this.RaisePropertyChanged(() => this.PositionDistance);
            }
        }
        ///// <summary>
        ///// 孔半径
        ///// </summary>
        //[DisplayName("孔半径")]
        //public double HoleRadius
        //{
        //    get
        //    {
        //        return holeRadius;
        //    }

        //    set
        //    {
        //        holeRadius = value;
        //        this.RaisePropertyChanged(() => this.HoleRadius);
        //    }
        //}
        /// <summary>
        /// 短管长度
        /// </summary>
        [DisplayName("短管长度")]
        public double PipeLenght
        {
            get
            {
                return pipeLenght;
            }

            set
            {
                pipeLenght = value;
                this.RaisePropertyChanged(() => this.PipeLenght);
            }
        }
        /// <summary>
        /// 孔偏移量
        /// </summary>
        /// 
        [DisplayName("孔偏移量")]
        public double HoleOffset
        {
            get
            {
                return holeOffset;
            }

            set
            {
                holeOffset = value;
                this.RaisePropertyChanged(() => this.HoleOffset);
            }
        }

        private double flanchDN = 10;

        [DisplayName("法兰")]
        //[ItemsSource(typeof(ParFlanchSource))]
        public ParFlanch ParFlanch
        {
            get
            {
                return parFlanch;
            }

            set
            {
                parFlanch = value;
                this.RaisePropertyChanged(() => this.ParFlanch);
            }
        }
        /// <summary>
        /// 短管厚度
        /// </summary>
        /// 
        [DisplayName("短管厚度")]
        public double PipeThickness
        {
            get
            {
                return pipeThickness;
            }

            set
            {
                pipeThickness = value;
                this.RaisePropertyChanged(() => this.PipeThickness);
            }
        }
        [DisplayName("是否热沉系统孔")]
        public bool IsHeatSinkHole
        {
            get
            {
                return isHeatSinkHole;
            }

            set
            {
                isHeatSinkHole = value;
            }
        }
    }
}