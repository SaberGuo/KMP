using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Interactivity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.Prism.ViewModel;
namespace KMP.Interface.Model
{
  public  class ParCylinder:ParameterBase
    {
        double inRadius;
        double thickness;
        double length;
        double capRadius;
        double ribThickness;
        double ribWidth;
        double ribNumber;

        public double InRadius
        {
            get
            {
                return inRadius;
            }

            set
            {
                inRadius = value;
                this.RaisePropertyChanged(() => this.inRadius);
            }
        }

        public double Thickness
        {
            get
            {
                return thickness;
            }

            set
            {
                thickness = value;
                this.RaisePropertyChanged(() => this.thickness);
            }
        }

        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
                this.RaisePropertyChanged(() => this.length);
            }
        }

        public double CapRadius
        {
            get
            {
                return capRadius;
            }

            set
            {
                capRadius = value;
                this.RaisePropertyChanged(() => this.capRadius);
            }
        }

        public double RibThickness
        {
            get
            {
                return ribThickness;
            }

            set
            {
                ribThickness = value;
                this.RaisePropertyChanged(() => this.ribThickness);
            }
        }

        public double RibWidth
        {
            get
            {
                return ribWidth;
            }

            set
            {
                ribWidth = value;
                this.RaisePropertyChanged(() => this.ribWidth);
            }
        }

        public double RibNumber
        {
            get
            {
                return ribNumber;
            }

            set
            {
                ribNumber = value;
                this.RaisePropertyChanged(() => this.ribNumber);
            }
        }
    }
}
