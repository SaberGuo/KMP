using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace KMP.Interface.ComParam
{
    public interface ISubComParam
    {

    }
    public interface IComParam
    {
        ISubComParam inputs { get; set; }
        ISubComParam outputs { get; set; }

        ICommand ComputeCommand { get; set; }

        string DocPath { get; }
    }


}
