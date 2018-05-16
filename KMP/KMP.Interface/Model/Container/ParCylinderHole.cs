using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
namespace KMP.Interface.Model.Container
{
  public  class ParCylinderHole:NotificationObject
    {
        double positionAngle;
        double positionDistance;
        double holeRadius;
        double pipeLenght;
        double holeOffset;
        double pipeThickness;
        ParFlanch parFlanch=new ParFlanch();
        /// <summary>
        /// 定位角度
        /// </summary>
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
        /// <summary>
        /// 孔半径
        /// </summary>

        public double HoleRadius
        {
            get
            {
                return holeRadius;
            }

            set
            {
                holeRadius = value;
                this.RaisePropertyChanged(() => this.HoleRadius);
            }
        }
        /// <summary>
        /// 短管长度
        /// </summary>

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
    }
}
