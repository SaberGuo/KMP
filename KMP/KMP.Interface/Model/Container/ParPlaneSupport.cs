﻿using System;
using System.Collections.Generic;
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
        double brachRadius1;
        double brachRadius2;
        double topBoardThickness;
        double topBoardWidth;
        /// <summary>
        /// 安装距离罐中心
        /// </summary>
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
        public double BrachRadius1
        {
            get
            {
                return brachRadius1;
            }

            set
            {
                brachRadius1 = value;
            }
        }
        /// <summary>
        /// 中部支撑半径
        /// </summary>
        public double BrachRadius2
        {
            get
            {
                return brachRadius2;
            }

            set
            {
                brachRadius2 = value;
            }
        }
        /// <summary>
        /// 顶部厚度
        /// </summary>
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