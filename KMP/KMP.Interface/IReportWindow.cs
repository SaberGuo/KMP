using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface
{
    public interface IReportWindow
    {
        string Path { get; set; }
        IParamedModule Root { get; set; }
    }
}
