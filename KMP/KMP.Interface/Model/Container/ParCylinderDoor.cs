using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using KMP.Interface.Model;
namespace KMP.Interface.Model.Container
{
  public  class ParCylinderDoor:ParameterBase
    {
        PassedParameter inRadius;
        PassedParameter thickness;
      
        double doorRadius;
        double flanchWidth;
        ParCylinderHole topHole = new ParCylinderHole();
        ObservableCollection<ParCylinderHole> sideHoles = new ObservableCollection<ParCylinderHole>();
       
        /// <summary>
        /// 容器内半径 不显示
        /// </summary>
        /// 
        [Browsable(false)]
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
        /// 容器门厚度 不显示
        /// </summary>
        /// 
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
        /// 容器门深度内半径
        /// </summary>
        [Category("容器大门")]
        [DisplayName("大门深度（d1）")]
        [Description("容器大门")]
        public double DoorRadius
        {
            get
            {
                return doorRadius;
            }

            set
            {
                doorRadius = value;
                this.RaisePropertyChanged(() => this.DoorRadius);
            }
        }
        /// <summary>
        /// 容器门法兰宽度
        /// </summary>
        [Category("容器大门")]
        [DisplayName("大门法兰宽度（H2）")]
        [Description("容器大门")]
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
        [Category("容器大门")]
        [DisplayName("大门法兰厚度（T2）")]
        [Description("容器大门")]
        public double FlanchThinkness { get; set; }
        [Category("容器大门")]
        [DisplayName("直筒段长度（T3）")]
        [Description("容器大门")]
        public double StraightLength { get; set; }
        [Category("开孔")]
        [DisplayName("大门轴向孔")]
        [Description("容器-后封头轴向孔")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ParCylinderHole TopHole
        {
            get
            {
                return topHole;
            }

            set
            {
                topHole = value;
                this.RaisePropertyChanged(() => this.TopHole);
            }
        }
        [Category("开孔")]
        [DisplayName("大门侧孔")]
        [Description("容器大门—侧孔")]
        public ObservableCollection<ParCylinderHole> SideHoles
        {
            get
            {
                return sideHoles;
            }

            set
            {
                sideHoles = value;
            }
        }
    }
}
