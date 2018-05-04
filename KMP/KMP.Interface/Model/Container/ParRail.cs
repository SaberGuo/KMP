using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
namespace KMP.Interface.Model.Container
{
   public class ParRail:ParameterBase
    {
        double upBridgeHeight;
        double downBridgeHeight;
        double braceHeight;
        double upBridgeWidth;
        double downBridgeWidth;
        double braceWidth;
        double railLength;

        public double UpBridgeHeight
        {
            get
            {
                return upBridgeHeight;
            }

            set
            {
                upBridgeHeight = value;
            }
        }

        public double DownBridgeHeight
        {
            get
            {
                return downBridgeHeight;
            }

            set
            {
                downBridgeHeight = value;
            }
        }

        public double BraceHeight
        {
            get
            {
                return braceHeight;
            }

            set
            {
                braceHeight = value;
            }
        }

        public double UpBridgeWidth
        {
            get
            {
                return upBridgeWidth;
            }

            set
            {
                upBridgeWidth = value;
            }
        }

        public double DownBridgeWidth
        {
            get
            {
                return downBridgeWidth;
            }

            set
            {
                downBridgeWidth = value;
            }
        }

        public double BraceWidth
        {
            get
            {
                return braceWidth;
            }

            set
            {
                braceWidth = value;
            }
        }

        public double RailLength
        {
            get
            {
                return railLength;
            }

            set
            {
                railLength = value;
            }
        }
    }
}
