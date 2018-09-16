using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KMP.Parameterization.InventorMonitor
{
    public interface IInvMonitorController
    {

        void UpdateInvModel(string filePath);
        void UpdateAll();

        ObservableCollection<IInvMonitorViewModel> Documents { get; }

        void captureImages();
    }
}
