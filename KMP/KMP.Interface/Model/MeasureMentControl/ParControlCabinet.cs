using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Practices.Prism.ViewModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition;

namespace KMP.Interface.Model.MeasureMentControl
{
  public  class ParControlCabinet : ParameterBase
    {
        private double height;
        private double width;
        private double length;
        [DisplayName("控制柜高")]
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
        [DisplayName("控制柜宽")]
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
        [DisplayName("控制柜厚")]
        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
                this.RaisePropertyChanged(() => this.Length);
            }
        }
    }
}
