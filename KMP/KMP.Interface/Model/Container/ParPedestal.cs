using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KMP.Interface.Model.Container
{
    [DisplayName("底座参数")]
    public  class ParPedestal:ParameterBase
    {
        public override string ToString()
        {
            return "底座参数";
        }
        #region
        PassedParameter inRadius;
        PassedParameter thickness;
        double panelThickness;
        double footBoardThickness;
        double underBoardingAngle;
        double underBoardWidth;
        double pedestalCenterDistance;
        double footBoardBetween;
        double backBoardMoveDistance;
        double footBoardWidth;
        double pedestalLength;
        double footBoardNum;
        /// <summary>
        /// 罐体内半径
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
        /// 罐体厚度
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
        #endregion
        ///// <summary>
        ///// 板材厚度
        ///// </summary>
        ///// 
        //[DisplayName("板材厚度（T3）")]
        //[Description("容器底座")]
        //public double PanelThickness
        //{
        //    get
        //    {
        //        return panelThickness;
        //    }

        //    set
        //    {
        //        panelThickness = value;
        //        this.RaisePropertyChanged(() => this.PanelThickness);
        //    }
        //}
        #region 垫板
        /// <summary>
        /// 垫板角度
        /// </summary>
        /// 
        [Category("底座垫板")]
        [DisplayName("垫板角度（a）")]
        [Description("容器底座")]
        public double UnderBoardingAngle
        {
            get
            {
                return underBoardingAngle;
            }

            set
            {
                underBoardingAngle = value;
                this.RaisePropertyChanged(() => this.UnderBoardingAngle);
            }
        }
        /// <summary>
        /// 垫板宽度
        /// </summary>
        /// 
        [Category("底座垫板")]
        [DisplayName("垫板宽度（d5）")]
        [Description("容器底座 - 背板")]
        public double UnderBoardWidth
        {
            get
            {
                return underBoardWidth;
            }

            set
            {
                underBoardWidth = value;
                this.RaisePropertyChanged(() => this.UnderBoardWidth);
            }
        }
        [Category("底座垫板")]
        [DisplayName("垫板厚度（T1）")]
        [Description("容器底座")]
        public double UnderBoardThinkness { get; set; }
        #endregion
        #region 背板
        /// <summary>
        /// 背板偏移距离
        /// </summary>
        /// 
        [Category("底座背板")]
        [DisplayName("背板偏移距离（d2）")]
        [Description("容器底座 - 背板")]
        public double BackBoardMoveDistance
        {
            get
            {
                return backBoardMoveDistance;
            }

            set
            {
                backBoardMoveDistance = value;
                this.RaisePropertyChanged(() => this.BackBoardMoveDistance);
            }
        }
        [Category("底座背板")]
        [DisplayName("背板厚度（T2）")]
        [Description("容器底座 - 背板")]
        public double BackBoardThinkness { get; set; }
        #endregion
        #region 竖板
        /// <summary>
        /// 竖板厚度
        /// </summary>
        /// 
        [Category("底座竖板")]
        [DisplayName("竖板厚度（T4）")]
        [Description("容器底座")]
        public double FootBoardThickness
        {
            get
            {
                return footBoardThickness;
            }

            set
            {
                footBoardThickness = value;
                this.RaisePropertyChanged(() => this.FootBoardThickness);
            }
        }
        /// <summary>
        /// 竖板间隔
        /// </summary>
        /// 
        [Category("底座竖板")]
        [DisplayName("竖板间隔（d3）")]
        [Description("容器底座")]
        public double FootBoardBetween
        {
            get
            {
                return footBoardBetween;
            }

            set
            {
                footBoardBetween = value;
                this.RaisePropertyChanged(() => this.FootBoardBetween);
            }
        }

        /// <summary>
        /// 竖板宽度
        /// </summary>
        /// 
        [Category("底座竖板")]
        [DisplayName("竖板宽度(d4)")]
        [Description("容器底座 - 背板")]
        public double FootBoardWidth
        {
            get
            {
                return footBoardWidth;
            }

            set
            {
                footBoardWidth = value;
                this.RaisePropertyChanged(() => this.FootBoardWidth);
            }
        }
        /// <summary>
        /// 竖板数量
        /// </summary>
        /// 
        [Category("底座竖板")]
        [DisplayName("竖板数量")]
       [Description("容器底座")]
        public double FootBoardNum
        {
            get
            {
                return footBoardNum;
            }

            set
            {
                footBoardNum = value;
                this.RaisePropertyChanged(() => this.FootBoardNum);
            }
        }
        #endregion
        #region 底板
        /// <summary>
        /// 底板距离罐体中心距离
        /// </summary>
        /// 
        [Category("底座底板")]
        [DisplayName("底板距离罐体中心距离（h1）")]
        [Description("容器底座")]
        public double PedestalCenterDistance
        {
            get
            {
                return pedestalCenterDistance;
            }

            set
            {
                pedestalCenterDistance = value;
                this.RaisePropertyChanged(() => this.PedestalCenterDistance);
            }
        }
        /// <summary>
        /// 底板长度
        /// </summary>
        /// 
        [Category("底座底板")]
        [DisplayName("底板长度（L1）")]
        [Description("容器底座")]
        public double PedestalLength
        {
            get
            {
                return pedestalLength;
            }

            set
            {
                pedestalLength = value;
                this.RaisePropertyChanged(() => this.PedestalLength);
            }
        }
        [Category("底座底板")]
        [DisplayName("底板厚度（T3）")]
        [Description("容器底座 - 背板")]
        public double PedestalThinkness { get; set; }
        #endregion
    }
}
