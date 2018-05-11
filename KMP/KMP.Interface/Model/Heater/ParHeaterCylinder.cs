using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Heater
{
    public class ParHeaterCylinder : ParameterBase
    {
        double exDiameter = 50;
        double thickness= 5;
        double length =100;

        
        double insulatorThickness = 5;
        double skeletonInnerDiameter;//=insulatorThickness*2+exDiameter

        double ribLength;//骨架T形梁长度
        double ribHeight;//骨架T形梁高度
        double ribThickness1;
        double ribThickness2;

        /// <summary>
        /// 筒体热沉胀板外径
        /// </summary>
        public double ExDiameter
        {
            get
            {
                return this.exDiameter;
            }
            set
            {
                this.exDiameter = value;
                RaisePropertyChanged(() => this.ExDiameter);
            }
        }

        /// <summary>
        /// 胀板厚度
        /// </summary>
        public double Thickness
        {
            get
            {
                return this.thickness;
            }
            set
            {
                this.thickness = value;
                RaisePropertyChanged(() => this.Thickness);
            }
        }
        /// <summary>
        /// 筒体热沉有效长度
        /// </summary>
        public double Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
                RaisePropertyChanged(() => this.Length);
            }
        }

        /// <summary>
        /// 筒体绝热条厚度
        /// </summary>
        public double InsulatorThickness
        {
            get
            {
                return this.insulatorThickness;
            }
            set
            {
                this.insulatorThickness = value;
                RaisePropertyChanged(() => this.InsulatorThickness);
            }
        }

        /// <summary>
        /// 骨架T形梁长度
        /// </summary>
        public double RibLength
        {
            get
            {
                return this.ribLength;
            }
            set
            {
                this.ribLength = value;
                RaisePropertyChanged(() => this.RibLength);
            }
        }

        /// <summary>
        /// 骨架T形梁高度
        /// </summary>
        public double RibHeight
        {
            get
            {
                return this.ribHeight;
            }
            set
            {
                this.ribHeight = value;
                RaisePropertyChanged(() => this.RibHeight);
            }
        }

        /// <summary>
        /// 骨架T形梁厚度1
        /// </summary>

        public double RibThickness1
        {
            get
            {
                return this.ribThickness1;
            }
            set
            {
                this.ribThickness1 = value;
                RaisePropertyChanged(() => this.RibThickness1);
            }
        }
        /// <summary>
        /// 骨架T形梁厚度2
        /// </summary>

        public double RibThickness2
        {
            get
            {
                return this.ribThickness2;
            }
            set
            {
                this.ribThickness2 = value;
                RaisePropertyChanged(() => this.RibThickness2);
            }
        }


        public double SkeletonInnerDiameter
        {
            get
            {
                return this.skeletonInnerDiameter;
            }
        }

    }
}
