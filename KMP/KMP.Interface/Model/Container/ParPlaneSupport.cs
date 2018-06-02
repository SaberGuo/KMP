using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Interface.Model.Container
{
  public  class ParPlaneSupport:ParameterBase
    {
        PassedParameter inRadius = new PassedParameter();
        double offset;
        double brachHeight1;
        double brachHeight2;
        double brachDiameter1;
        double brachDiameter2;
        double topBoardThickness;
        double topBoardWidth;
        /// <summary>
        /// 安装距离罐中心
        /// </summary>
        /// 
        [DisplayName("安装距离罐中心")]
        public double Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }
        /// <summary>
        /// 底部支撑高度
        /// </summary>
        /// 
        [DisplayName("底部支撑高度")]
        public double BrachHeight1
        {
            get
            {
                return brachHeight1;
            }

            set
            {
                brachHeight1 = value;
            }
        }
        /// <summary>
        /// 中部支撑高度
        /// </summary>
        /// 
        [DisplayName("中部支撑高度")]
        public double BrachHeight2
        {
            get
            {
                return brachHeight2;
            }

            set
            {
                brachHeight2 = value;
            }
        }
        /// <summary>
        /// 底部支撑半径
        /// </summary>
        /// 
        [DisplayName("底部支撑直径")]
        public double BrachDiameter1
        {
            get
            {
                return brachDiameter1;
            }

            set
            {
                brachDiameter1 = value;
            }
        }
        /// <summary>
        /// 中部支撑半径
        /// </summary>
        /// 
        [DisplayName("中部支撑直径")]
        public double BrachDiameter2
        {
            get
            {
                return brachDiameter2;
            }

            set
            {
                brachDiameter2 = value;
            }
        }
        /// <summary>
        /// 顶部厚度
        /// </summary>
        /// 
        [DisplayName("顶部厚度")]
        public double TopBoardThickness
        {
            get
            {
                return topBoardThickness;
            }

            set
            {
                topBoardThickness = value;
            }
        }
        /// <summary>
        /// 顶部宽度
        /// </summary>
        /// 
        [DisplayName("顶部宽度")]
        public double TopBoardWidth
        {
            get
            {
                return topBoardWidth;
            }

            set
            {
                topBoardWidth = value;
            }
        }
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
            }
        }
    }
}
