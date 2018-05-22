using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.HeatSinkSystem
{
   public class ParCap:ParameterBase
    {
        #region
        PassedParameter inDiameter = new PassedParameter();
        PassedParameter thickness=new PassedParameter();
        double capThickness;
        double slotThickness;
        double slotHight;
        double slotWide;
        double slotOffset;
        [Browsable(false)]
        [DisplayName("热沉内直径")]
        public PassedParameter InDiameter
        {
            get
            {
                return inDiameter;
            }

            set
            {
                inDiameter = value;
            }
        }
        [Browsable(false)]
        [DisplayName("热沉罐厚度")]
        public PassedParameter Thickness
        {
            get
            {
                return thickness;
            }

            set
            {
                thickness = value;
            }
        }
        [DisplayName("门厚度")]
        public double CapThickness
        {
            get
            {
                return capThickness;
            }

            set
            {
                capThickness = value;
            }
        }
        [DisplayName("槽厚度")]
        public double SlotThickness
        {
            get
            {
                return slotThickness;
            }

            set
            {
                slotThickness = value;
            }
        }
        [DisplayName("槽高度")]
        public double SlotHight
        {
            get
            {
                return slotHight;
            }

            set
            {
                slotHight = value;
            }
        }
        [DisplayName("槽宽度")]
        public double SlotWide
        {
            get
            {
                return slotWide;
            }

            set
            {
                slotWide = value;
            }
        }
        [DisplayName("圆槽与门边距离")]
        public double SlotOffset
        {
            get
            {
                return slotOffset;
            }

            set
            {
                slotOffset = value;
            }
        }
        #endregion
        double pipeAngle;
        double pipeDiameter;
        double pipeThickness;
        double pipeYOffset;
        double pipeXOffset;
        [DisplayName("管总角度")]
        public double PipeAngle
        {
            get
            {
                return pipeAngle;
            }

            set
            {
                pipeAngle = value;
            }
        }
        [DisplayName("管内直径")]
        public double PipeDiameter
        {
            get
            {
                return pipeDiameter;
            }

            set
            {
                pipeDiameter = value;
            }
        }
        [DisplayName("管厚度")]
        public double PipeThickness
        {
            get
            {
                return pipeThickness;
            }

            set
            {
                pipeThickness = value;
            }
        }
        [DisplayName("管中心与门边距离")]
        public double PipeYOffset
        {
            get
            {
                return pipeYOffset;
            }

            set
            {
                pipeYOffset = value;
            }
        }
        [DisplayName("管中心与门面距离")]
        public double PipeXOffset
        {
            get
            {
                return pipeXOffset;
            }

            set
            {
                pipeXOffset = value;
            }
        }
        #region 罐内管支撑

        [Category("内管")]
        [DisplayName("管支撑直径")]
        public double PipeSurDiameter
        {
            get
            {
                return pipeSurDiameter;
            }

            set
            {
                pipeSurDiameter = value;
            }
        }
        [Category("内管")]
        [DisplayName("管支撑厚度")]
        public double PipeSurThickness
        {
            get
            {
                return pipeSurThickness;
            }

            set
            {
                pipeSurThickness = value;
            }
        }
        [Category("内管")]
        [DisplayName("首个支撑与管口角度")]
        public double PipeSurDistance
        {
            get
            {
                return pipeSurDistance;
            }

            set
            {
                pipeSurDistance = value;
            }
        }
        [Category("内管")]
        [DisplayName("管支撑水平距离")]
        public double PipeSurLength
        {
            get
            {
                return pipeSurLength;
            }

            set
            {
                pipeSurLength = value;
            }
        }
        [Category("内管")]
        [DisplayName("管支撑弯曲半径")]
        public double PipeSurCurveRadius
        {
            get
            {
                return pipeSurCurveRadius;
            }

            set
            {
                pipeSurCurveRadius = value;
            }
        }
        [Category("内管")]
        [DisplayName("管支撑数量")]
        public int PipeSurNum
        {
            get
            {
                return pipeSurNum;
            }

            set
            {
                pipeSurNum = value;
            }
        }

    

        double pipeSurDiameter;
        double pipeSurThickness;
        double pipeSurDistance;
        double pipeSurLength;
        double pipeSurCurveRadius;
        int pipeSurNum;
        #endregion
    }
}
