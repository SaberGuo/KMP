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
        PassedParameter inDiameter ;
        PassedParameter thickness;
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
        [Category("槽")]
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
        [Category("槽")]
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
        [Category("槽")]
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
        [Category("槽")]
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
        #region
        double pipeAngle;
        double pipeDiameter;
        double pipeThickness;
        double pipeYOffset;
        double pipeXOffset;
        [Category("管道")]
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
        [Category("管道")]
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
        [Category("管道")]
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
        [Category("管道")]
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
        [Category("管道")]
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
        #endregion
        #region 罐内管支撑

        [Category("管支架")]
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
        [Category("管支架")]
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
        [Category("管支架")]
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
        [Category("管支架")]
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
        [Category("管支架")]
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
        [Category("管支架")]
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
        #region
        double titleHeigh;
        double titleWidth;
        double titleOffset;
        double titleLength;
        [Category("独板")]
        [DisplayName("厚度")]
        public double TitleHeigh
        {
            get
            {
                return titleHeigh;
            }

            set
            {
                titleHeigh = value;
            }
        }
        [Category("独板")]
        [DisplayName("宽度")]
        public double TitleWidth
        {
            get
            {
                return titleWidth;
            }

            set
            {
                titleWidth = value;
            }
        }
        [Category("独板")]
        [DisplayName("与底板距离")]
        public double TitleOffset
        {
            get
            {
                return titleOffset;
            }

            set
            {
                titleOffset = value;
            }
        }
        [Category("独板")]
        [DisplayName("长度")]
        public double TitleLength
        {
            get
            {
                return titleLength;
            }

            set
            {
                titleLength = value;
            }
        }


        #endregion
        #region 插头
        double plugWidth;
        double plugHeight;
        double plugLenght;
        double plugOffset;
        double plugHoleDiameter;
        double plugHoleDistance;
        [Category("插头")]
        [DisplayName("宽度")]
        public double PlugWidth
        {
            get
            {
                return plugWidth;
            }

            set
            {
                plugWidth = value;
            }
        }
        [Category("插头")]
        [DisplayName("厚度")]
        public double PlugHeight
        {
            get
            {
                return plugHeight;
            }

            set
            {
                plugHeight = value;
            }
        }
        [Category("插头")]
        [DisplayName("两片间距离")]
        public double PlugOffset
        {
            get
            {
                return plugOffset;
            }

            set
            {
                plugOffset = value;
            }
        }
        [Category("插头")]
        [DisplayName("孔直径")]
        public double PlugHoleDiameter
        {
            get
            {
                return plugHoleDiameter;
            }

            set
            {
                plugHoleDiameter = value;
            }
        }
        [Category("插头")]
        [DisplayName("孔与插头边距离")]
        public double PlugHoleDistance
        {
            get
            {
                return plugHoleDistance;
            }

            set
            {
                plugHoleDistance = value;
            }
        }
        [Category("插头")]
        [DisplayName("长度")]
        public double PlugLenght
        {
            get
            {
                return plugLenght;
            }

            set
            {
                plugLenght = value;
            }
        }
        #endregion
    }
}
