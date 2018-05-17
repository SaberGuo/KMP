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
using System.ComponentModel;

namespace KMP.Interface.Model.Container
{
  public  class ParCylinder:ParameterBase
    {

        PassedParameter inRadius;
        PassedParameter thickness;
        double length;
        double capRadius;
        double ribWidth;
        double ribNumber;
        double ribHeight;
        double ribFirstDistance;
        double ribBraceWidth;
        double ribBraceHeight;
        double flanchWidth;
        ParCylinderHole capTopHole = new ParCylinderHole();
        ObservableCollection<ParCylinderHole> capSideHoles = new ObservableCollection<ParCylinderHole>();
        ObservableCollection<ParCylinderHole> parHoles = new ObservableCollection<ParCylinderHole>();
        [Browsable(false)]
        /// <summary>
        /// 罐内半径
        /// </summary>
        public PassedParameter InRadius
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
        [Browsable(false)]
        public PassedParameter Thickness
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
        [Category("罐体")]
        [DisplayName("长度")]
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
        [Category("罐体")]
        [DisplayName("封头长度内半径")]
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
        [Category("加强筋")]
        [DisplayName("宽度")]
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
        [Category("加强筋")]
        [DisplayName("数量")]
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
        [Category("加强筋")]
        [DisplayName("高度")]
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
        [Category("加强筋")]
        [DisplayName("第一个离罐口的距离")]
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
        [Category("加强筋支柱")]
        [DisplayName("宽度")]
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
        [Category("加强筋支柱")]
        [DisplayName("高度")]
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
        [Category("罐体")]
        [DisplayName("法兰宽度")]
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
        [Category("罐体")]
        [DisplayName("开孔")]
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
        /// <summary>
        /// 堵头顶孔
        /// </summary>
        /// 
        [Category("罐体")]
        [DisplayName("堵头顶孔")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParCylinderHole CapTopHole
        {
            get
            {
                return capTopHole;
            }

            set
            {
                capTopHole = value;
                this.RaisePropertyChanged(() => this.CapTopHole);
            }
        }
        /// <summary>
        /// 堵头侧边孔集合
        /// </summary>
        /// 
        [Category("罐体")]
        [DisplayName("堵头侧边孔")]
        public ObservableCollection<ParCylinderHole> CapSideHoles
        {
            get
            {
                return capSideHoles;
            }

            set
            {
                capSideHoles = value;
                this.RaisePropertyChanged(() => this.CapSideHoles);
            }
        }
    }
}
