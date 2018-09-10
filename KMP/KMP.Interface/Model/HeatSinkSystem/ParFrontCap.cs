﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace KMP.Interface.Model.HeatSinkSystem
{
    [DisplayName("大门参数")]
    public class ParFrontCap : ParameterBase
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
        [DisplayName("门厚度T2")]
        [Description("热沉盖-汇总管")]
        [Category("热沉盖参数")]
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
        #endregion
        #region 骨架"
        [Category("骨架")]
        [DisplayName("槽厚度（T）")]
        [Description("热沉盖-槽")]
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
        [Category("骨架")]
        [DisplayName("槽钢高度（H）")]
        [Description("热沉盖-槽")]
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
        [DisplayName("槽宽度（D）")]
        [Description("热沉盖-槽")]
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
        [Category("骨架")]
        [DisplayName("圆槽与门边距离（L）")]
        [Description("热沉盖-槽")]
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
        [Category("骨架")]
        [DisplayName("内环槽钢直径")]
        [Description("热沉盖-槽")]
        public double InSlotDiameter { get; set; }
        #endregion
        #region 汇总管
        double pipeAngle;
        double pipeDiameter;
        double pipeThickness;
        double pipeYOffset;
        double pipeXOffset;
        [Category("汇总管")]
        [DisplayName("管总角度（a）")]
        [Description("热沉盖-汇总管")]
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
        [Category("汇总管")]
        [DisplayName("管内直径（d）")]
        [Description("热沉盖-汇总管")]
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
        [DisplayName("管厚度（T1）")]
        [Description("热沉盖-汇总管")]
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
        [DisplayName("管中心与门边距离（L）")]
        [Description("热沉盖-汇总管")]
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
        [Category("汇总管")]
        [DisplayName("管中心与门面距离（h）")]
        [Description("热沉盖-汇总管")]
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
        #region 支管

        [Category("支管")]
        [DisplayName("支管直径（d1）")]
        [Description("热沉盖-支撑管")]
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
        [Description("热沉盖-支撑管")]
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
        [DisplayName("首个支管与管口角度（a）")]
        [Description("热沉盖-支撑管")]
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
        [DisplayName("管支撑水平距离（h1）")]
        [Description("热沉盖-支撑管")]
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
        [DisplayName("支管弯曲半径（d2）")]
        [Description("热沉盖-支撑管")]
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
        [Description("热沉盖-支撑管")]
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
        #region 连接板
        double titleHeigh;
        double titleWidth;
        double titleOffset;
        double titleLength;
        [Category("连接板")]
        [DisplayName("厚度（T）")]
        [Description("热沉盖-独板")]
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
        [Category("连接板")]
        [DisplayName("宽度（W）")]
        [Description("热沉盖-独板")]
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
        [Category("连接板")]
        [DisplayName("与底板距离（T1）")]
        [Description("热沉盖-独板")]
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
        [Category("连接板")]
        [DisplayName("长度（L）")]
        [Description("热沉盖-独板")]
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
        #region 上吊板
        double plugWidth;
        double plugHeight;
        double plugLenght;
        double plugOffset;
        double plugHoleDiameter;
        double plugHoleDistance;
        [Category("上吊板")]
        [DisplayName("宽度（H）")]
        [Description("热沉盖-插销1")]
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
        [Category("上吊板")]
        [DisplayName("厚度（T）")]
        [Description("热沉盖-插销2")]
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
        [Category("上吊板")]
        [DisplayName("两片间距离（T1）")]
        [Description("热沉盖-插销2")]
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
        [Category("上吊板")]
        [DisplayName("孔直径（d）")]
        [Description("热沉盖-插销1")]
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
        [Category("上吊板")]
        [DisplayName("孔与上吊板边距离（L1）")]
        [Description("热沉盖-插销1")]
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
        [Category("上吊板")]
        [DisplayName("长度（L）")]
        [Description("热沉盖-插销1")]
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