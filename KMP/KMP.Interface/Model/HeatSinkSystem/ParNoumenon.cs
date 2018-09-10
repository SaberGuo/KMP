using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.HeatSinkSystem
{
    [DisplayName("热沉系统参数")]
    public  class ParNoumenon:ParameterBase
    {
        public override string ToString()
        {
            return "热沉系统参数";
        }
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
        [DisplayName("热沉罐长度（L）")]
        [Description("热沉罐-总汇管")]
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
        [Category("汇总管")]
        [DisplayName("管直径（d1）")]
        [Description("热沉罐-总汇管")]
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
        [Category("汇总管")]
        [DisplayName("管厚度（t）")]
        [Description("热沉罐-总汇管")]
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
        [Category("汇总管")]
        [DisplayName("管长度（L2）")]
        [Description("热沉罐-总汇管")]
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
        [Category("汇总管")]
        [DisplayName("管与罐口距离（L1）")]
        [Description("热沉罐-总汇管")]
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
        [Category("汇总管")]
        [DisplayName("管与罐中心距离（h1）")]
        [Description("热沉罐-总汇管")]
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
        #region 罐内支管
        [Category("支管")]
        [DisplayName("支管直径（d）")]
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
        [Category("支管")]
        [DisplayName("支管厚度（t）")]
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
        [Category("支管")]
        [DisplayName("支管与管口距离（L1）")]
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
        [Category("支管")]
        [DisplayName("支管水平距离（h1）")]
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
        [Category("支管")]
        [DisplayName("支管弯曲半径（d1）")]
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
        [Category("支管")]
        [DisplayName("支管数量")]
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
        double tAndCYDiatance;
        int endLongNumber;
        [Category("骨架")]
        [DisplayName("T字钢支撑高度（H2）")]
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
        [DisplayName("T字钢支撑宽度（L3）")]
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
        [DisplayName("T字钢顶部宽度（L2）")]
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
        [DisplayName("T字钢顶部厚度（H1）")]
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
        [DisplayName("环梁T字钢与两侧胀板距离（L1）")]
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
        [DisplayName("环梁T字钢数量")]
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
        [DisplayName("纵梁角度（a）")]
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

        [Category("骨架")]
        [DisplayName("T字钢到胀板外侧距离（H3）")]
        [Description("热沉罐-纵梁")]
        public double TAndCYDiatance
        {
            get
            {
                return tAndCYDiatance;
            }

            set
            {
                tAndCYDiatance = value;
            }
        }
        #endregion

    }
}
