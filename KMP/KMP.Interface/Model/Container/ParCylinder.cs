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
using System.Collections.ObjectModel;
namespace KMP.Interface.Model.Container
{
  public  class ParCylinder:ParameterBase
    {
       
        double inRadius;
        double thickness;
        double length;
        double capRadius;
        double ribWidth;
        double ribNumber;
        double ribHeight;
        double ribFirstDistance;
        double ribBraceWidth;
        double ribBraceHeight;
        double flanchWidth;
        ObservableCollection<ParCylinderHole> parHoles = new ObservableCollection<ParCylinderHole>();
        /// <summary>
        /// 罐内半径
        /// </summary>
        public double InRadius
        {
            get
            {
                return inRadius;
            }

            set
            {
                inRadius = value;
                this.RaisePropertyChanged(() => this.InRadius);
            }
        }
        /// <summary>
        /// 罐厚度
        /// </summary>

        public double Thickness
        {
            get
            {
                return thickness;
            }

            set
            {
                thickness = value;
                this.RaisePropertyChanged(() => this.Thickness);
            }
        }
        /// <summary>
        /// 罐长度
        /// </summary>

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
        /// <summary>
        /// 罐封头长度内半径
        /// </summary>
        public double CapRadius
        {
            get
            {
                return capRadius;
            }

            set
            {
                capRadius = value;
                this.RaisePropertyChanged(() => this.CapRadius);
            }
        }
     
        /// <summary>
        /// 加强筋的宽度
        /// </summary>
        public double RibWidth
        {
            get
            {
                return ribWidth;
            }

            set
            {
                ribWidth = value;
                this.RaisePropertyChanged(() => this.RibWidth);
            }
        }
        /// <summary>
        ///加强筋数量
        /// </summary>

        public double RibNumber
        {
            get
            {
                return ribNumber;
            }

            set
            {
                ribNumber = value;
                this.RaisePropertyChanged(() => this.RibNumber);
            }
        }
        /// <summary>
        /// 加强筋的高度
        /// </summary>
        public double RibHeight
        {
            get
            {
                return ribHeight;
            }

            set
            {
                ribHeight = value;
                this.RaisePropertyChanged(() => this.RibHeight);
            }
        }
        /// <summary>
        /// 第一个加强筋距离罐口的距离
        /// </summary>
        public double RibFirstDistance
        {
            get
            {
                return ribFirstDistance;
            }

            set
            {
                ribFirstDistance = value;
                this.RaisePropertyChanged(() => this.RibFirstDistance);
            }
        }
        /// <summary>
        /// 加强筋支柱的宽度
        /// </summary>
        public double RibBraceWidth
        {
            get
            {
                return ribBraceWidth;
            }

            set
            {
                ribBraceWidth = value;
                this.RaisePropertyChanged(() => this.RibBraceWidth);
            }
        }
        /// <summary>
        /// 加强筋支柱的高度
        /// </summary>
        public double RibBraceHeight
        {
            get
            {
                return ribBraceHeight;
            }

            set
            {
                ribBraceHeight = value;
                this.RaisePropertyChanged(() => this.RibBraceHeight);
            }
        }
        /// <summary>
        /// 罐体法兰宽度
        /// </summary>
        public double FlanchWidth
        {
            get
            {
                return flanchWidth;
            }

            set
            {
                flanchWidth = value;
                this.RaisePropertyChanged(() => this.FlanchWidth);
            }
        }

        public ObservableCollection<ParCylinderHole> ParHoles
        {
            get
            {
                return parHoles;
            }

            set
            {
                parHoles = value;
                this.RaisePropertyChanged(() => this.ParHoles);
            }
        }
    }
}
