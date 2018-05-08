using Infranstructure.Events;
using KMP.Interface;
using KMP.Interface.Model;
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
            OnParamedModuleDisplayTest();
            //this._invMonitorController.UpdateInvModel(filePath);
        }

        
        public ObservableCollection<IInvMonitorViewModel> Documents {
            get
            {
                return this._invMonitorController.Documents;
            }
        }

        #region test
        public ParameterBase SelectedModule { get; set; }
        public List<IParamedModule> Modules { get; set; }
        private void OnParamedModuleDisplayTest()
        {
            Modules = new List<IParamedModule>();
            IParamedModule rootModule = new ParamedModule.Container.Cylinder();
            Modules.Add(rootModule);

            rootModule.SubParamedModules.Add(new ParamedModule.Container.CylinderDoor());
            rootModule.SubParamedModules.Add(new ParamedModule.Container.Pedestal());
            RaisePropertyChanged("Modules");
        }
        #endregion
    }
}
