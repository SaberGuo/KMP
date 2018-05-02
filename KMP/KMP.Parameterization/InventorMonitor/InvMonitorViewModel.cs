using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Parameterization.InventorMonitor
{
    class InvMonitorViewModel: INotifyPropertyChanged
    {
        public InvMonitorViewModel(string filePath)
        {

        }
        
        public string FileName { get; set; }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
