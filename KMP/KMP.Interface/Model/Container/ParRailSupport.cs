using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KMP.Interface.Model.Container
{
    [DisplayName("导轨支架参数")]
    /// <summary>
    /// 导轨支架参数
    /// </summary>
    public  class ParRailSupport : ParameterBase
    {
        public override string ToString()
        {
            return "导轨支架参数";
        }
    }
}
