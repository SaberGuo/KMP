using KMP.Parameterization.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Parameterization
{
    public interface IChildWinViewModel
    {
        void CreateNewModelWindows();

        event EventHandler<ProjectEventArgs> NewModelHandler;
    }
}
