using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KMP.Interface.Model.Container
{
  public  class ParPedestal:ParameterBase
    {
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
        /// <summary>
        /// 板材厚度
        /// </summary>
        /// 
        [DisplayName("板材厚度")]
        public double PanelThickness
        {
            get
            {
                return panelThickness;
            }

            set
            {
                panelThickness = value;
                this.RaisePropertyChanged(() => this.PanelThickness);
            }
        }
        /// <summary>
        /// 竖板厚度
        /// </summary>
        /// 
        [DisplayName("竖板厚度")]
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
        /// 垫板角度
        /// </summary>
        /// 
        [DisplayName("垫板角度")]
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
        [DisplayName("垫板宽度")]
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
        /// <summary>
        /// 底板距离罐体中心距离
        /// </summary>
        /// 
        [DisplayName("底板距离罐体中心距离")]
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
        /// 竖板间隔
        /// </summary>
        /// 
        [DisplayName("竖板间隔")]
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
        /// 背板偏移距离
        /// </summary>
        /// 
        [DisplayName("背板偏移距离")]
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
        /// <summary>
        /// 竖板宽度
        /// </summary>
        /// 
        [DisplayName("竖板宽度")]
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
       [DisplayName("竖板数量")]
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
        /// <summary>
        /// 底板长度
        /// </summary>
        /// 
        [DisplayName("底板长度")]
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
    }
}
