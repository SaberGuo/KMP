﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.HeatSinkSystem
{
  public  class ParNoumenon:ParameterBase
    {
        #region
        PassedParameter inDiameter = new PassedParameter();
        PassedParameter thickness=new PassedParameter();
        double length;
        [Browsable(false)]
        [DisplayName("热沉罐内半径")]
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
        [DisplayName("热沉罐壁厚")]
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
        [DisplayName("热沉罐长度")]
        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }
        #endregion
        #region 罐内管
        double pipeDiameter;
        double pipeThickness;
        double pipeLength;
        double pipeDistance;
        double pipeOffset;
    
        [DisplayName("管值径")]
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
        [DisplayName("管长度")]
        public double PipeLength
        {
            get
            {
                return pipeLength;
            }

            set
            {
                pipeLength = value;
            }
        }
        [DisplayName("管与罐口距离")]
        public double PipeDistance
        {
            get
            {
                return pipeDistance;
            }

            set
            {
                pipeDistance = value;
            }
        }
        [DisplayName("罐与罐中心距离")]
        public double PipeOffset
        {
            get
            {
                return pipeOffset;
            }

            set
            {
                pipeOffset = value;
            }
        }
        #endregion
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
        [DisplayName("管支撑与管口距离")]
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
        #region
        double tBrachHeight;
        double tBrachWidth;
        double tTopWidth;
        double tTopHeight;
        double tHoopOffset;
        int tHoopNumber;
        double endLongAngle;
        int endLongNumber;
        [Category("骨架")]
        [DisplayName("T字钢支撑高度")]
        public double TBrachHeight
        {
            get
            {
                return tBrachHeight;
            }

            set
            {
                tBrachHeight = value;
            }
        }
        [Category("骨架")]
        [DisplayName("T字钢支撑宽度")]
        public double TBrachWidth
        {
            get
            {
                return tBrachWidth;
            }

            set
            {
                tBrachWidth = value;
            }
        }
        [Category("骨架")]
        [DisplayName("T字钢顶部宽度")]
        public double TTopWidth
        {
            get
            {
                return tTopWidth;
            }

            set
            {
                tTopWidth = value;
            }
        }
        [Category("骨架")]
        [DisplayName("T字钢顶部高度")]
        public double TTopHeight
        {
            get
            {
                return tTopHeight;
            }

            set
            {
                tTopHeight = value;
            }
        }
        [Category("骨架")]
        [DisplayName("T字钢与罐口距离")]
        public double THoopOffset
        {
            get
            {
                return tHoopOffset;
            }

            set
            {
                tHoopOffset = value;
            }
        }
        [Category("骨架")]
        [DisplayName("T字钢箍数量")]
        public int THoopNumber
        {
            get
            {
                return tHoopNumber;
            }

            set
            {
                tHoopNumber = value;
            }
        }
        [Category("骨架")]
        [DisplayName("纵梁角度")]
        public double EndLongAngle
        {
            get
            {
                return endLongAngle;
            }

            set
            {
                endLongAngle = value;
            }
        }
        [Category("骨架")]
        [DisplayName("纵梁数量")]
        public int EndLongNumber
        {
            get
            {
                return endLongNumber;
            }

            set
            {
                endLongNumber = value;
            }
        }
        #endregion
     
    }
}
