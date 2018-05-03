using Infranstructure.Events;
using KMP.Parameterization.InventorMonitor;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.Parameterization
{
    [Export]
    class MainDockViewModel: NotificationObject
    {
        IInvMonitorController _invMonitorController;
        IEventAggregator _eventAggregator;

        
        [ImportingConstructor]
        public MainDockViewModel(IEventAggregator eventAggregator,IInvMonitorController invMonitorController)
        {
            _eventAggregator = eventAggregator;
            _invMonitorController = invMonitorController;

            _eventAggregator.GetEvent<UpdateModelEvent>().Subscribe(OnUpdateModel);
        }

        private void OnUpdateModel(string filePath)
        {
            this._invMonitorController.UpdateInvModel(filePath);
        }

        public ObservableCollection<IInvMonitorViewModel> Documents {
            get
            {
                return this._invMonitorController.Documents;
            }
        }

    }
}
