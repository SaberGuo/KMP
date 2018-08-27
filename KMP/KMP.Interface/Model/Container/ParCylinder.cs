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
    /// <summary>
    /// 大门法兰厚度T2 增加 后封头直边长度L4 增加
    /// </summary>
  public  class ParCylinder:ParameterBase
    {

        PassedParameter inRadius;
        PassedParameter thickness;
        double length;
        double capRadius;
      
        double flanchWidth;
        public override string ToString()
        {
            return "容器筒体参数";
        }
        #region 容器筒体
        [Category("a容器筒体")]
        [DisplayName("大门法兰宽度（H2）")]
        [Description("容器系统")]
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
        [Category("a容器筒体")]
        [DisplayName("大门法兰厚度（T2）")]
        [Description("容器系统")]
        public double FlanchThinckness { get; set; }
        /// <summary>
        /// 罐长度
        /// </summary>
        [Category("a容器筒体")]
        [DisplayName("直筒段长度（L1）")]
        [Description("容器系统")]
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
        [Category("a容器筒体")]
        [DisplayName("后封头深度（d1）")]
        [Description("容器系统")]
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
        /// 后封头直边长度L4
        /// </summary>
        [Category("a容器筒体")]
        [DisplayName("后封头直边长度（L4）")]
        [Description("容器系统")]
        public double CapLineLength { get; set; }
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
        #endregion
        #region 加强筋

        double ribWidth;
        double ribNumber;
        double ribHeight;
        double ribFirstDistance;
        double ribBraceWidth;
        double ribBraceHeight;

        [Category("加强筋")]
        [DisplayName("加强筋宽度（L3）")]
        [Description("容器系统")]
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
        [DisplayName("加强筋数量")]
        [Description("容器系统")]
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
        [DisplayName("加强筋总高度（H1）")]
        [Description("容器系统")]
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
        [DisplayName("初始定位（L2）")]
        [Description("容器系统")]
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
        [Category("加强筋")]
        [DisplayName("腹板宽度（L5）")]
        [Description("容器系统")]
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
        [Category("加强筋")]
        [DisplayName("腹板高度（H3）")]
        [Description("容器系统")]
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
        #endregion
        #region 开孔
        ParCylinderHole capTopHole = new ParCylinderHole();
        ObservableCollection<ParCylinderHole> capSideHoles = new ObservableCollection<ParCylinderHole>();
        ObservableCollection<ParCylinderHole> parHoles = new ObservableCollection<ParCylinderHole>();

        [Category("开孔")]
        [DisplayName("筒体开孔")]
        [Description("容器-孔")]
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
        [Category("开孔")]
        [DisplayName("后封头轴向孔")]
        [Description("容器-后封头轴向孔")]
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
        [Category("开孔")]
        [DisplayName("后封头侧孔")]
        [Description("容器-后封头侧孔")]   
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
        #endregion
    }
}
