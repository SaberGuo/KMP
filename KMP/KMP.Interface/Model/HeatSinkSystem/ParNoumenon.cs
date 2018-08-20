using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.HeatSinkSystem
{
  public  class ParNoumenon:ParameterBase
    {
        #region
        PassedParameter inDiameter ;
        PassedParameter thickness;
        double length;
        [Browsable(false)]
        [DisplayName("热沉罐内半径d")]
        [Description("热沉罐-支持管")]
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
        [DisplayName("热沉罐壁厚t1")]
        [Description("热沉罐-支持管")]
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
        [DisplayName("热沉罐长度L")]
        [Description("热沉罐-支持管")]
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
        [Category("管道")]
        [DisplayName("管直径d1")]
        [Description("热沉罐-支持管")]
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
        [DisplayName("管厚度t")]
        [Description("热沉罐-支持管")]
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
        [DisplayName("管长度L2")]
        [Description("热沉罐-支持管")]
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
        [Category("管道")]
        [DisplayName("管与罐口距离L1")]
        [Description("热沉罐-支持管")]
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
        [Category("管道")]
        [DisplayName("管与罐中心距离h1")]
        [Description("热沉罐-支持管")]
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
        [Category("管支撑")]
        [DisplayName("管支撑直径")]
        [Description("热沉罐-支持管")]
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
        [Category("管支撑")]
        [DisplayName("管支撑厚度t")]
        [Description("热沉罐-支持管")]
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
        [Category("管支撑")]
        [DisplayName("管支撑与管口距离L1")]
        [Description("热沉罐-支持管")]
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
        [Category("管支撑")]
        [DisplayName("管支撑水平距离h1")]
        [Description("热沉罐-支持管")]
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
        [Category("管支撑")]
        [DisplayName("管支撑弯曲半径d1")]
        [Description("热沉罐-支持管")]
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
        [Category("管支撑")]
        [DisplayName("管支撑数量")]
        [Description("热沉罐-支持管")]
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
        [DisplayName("T字钢支撑高度H2")]
        [Description("热沉罐-T字钢")]
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
        [DisplayName("T字钢支撑宽度L3")]
        [Description("热沉罐-T字钢")]
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
        [DisplayName("T字钢顶部宽度L2")]
        [Description("热沉罐-T字钢")]
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
        [DisplayName("T字钢顶部高度H1")]
        [Description("热沉罐-T字钢")]
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
        [DisplayName("T字钢与罐口距离L1")]
        [Description("热沉罐-T字钢")]
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
        [Description("热沉罐-T字钢")]
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
        [DisplayName("纵梁角度a")]
        [Description("热沉罐-纵梁")]
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
        [Description("热沉罐-纵梁")]
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
