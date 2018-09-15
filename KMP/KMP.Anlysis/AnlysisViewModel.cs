using KMP.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using ParamedModule.Container;
using ParamedModule.HeatSinkSystem;
using ParamedModule.NitrogenSystem;
using ParamedModule.Other;
using ParamedModule.MeasureMentControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.Anlysis
{
    [Export(typeof(AnalysisViewModel))]
    public class AnalysisViewModel: NotificationObject
    {
        private IEventAggregator _eventAggregator;


        private IParamedModule _baseModule;
        [ImportingConstructor]
        public AnalysisViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

        }

        public IParamedModule BaseModule
        {
            get
            {
                return _baseModule;
            }
            set
            {
                this._baseModule = value;
                this.ContainerSys = this.GetSubModule(typeof(ContainerSystem), this._baseModule) as ContainerSystem;
            }
        }

        private ContainerSystem _ContainerSys;
        public ContainerSystem ContainerSys
        {
            get
            {
                return this._ContainerSys;
            }
            set
            {
                this._ContainerSys = value;
                RaisePropertyChanged(() => ContainerSys);
            }
        }

        public HeatSink HeateSinkSys
        {
            get
            {
                return this.GetSubModule(typeof(HeatSink), this._baseModule) as HeatSink;
            }
        }

        //
        public Nitrogen NitrogenSys
        {
            get
            {
                return this.GetSubModule(typeof(Nitrogen), this._baseModule) as Nitrogen;
            }
        }

        public VacuoSystem VacuoSys
        {
            get
            {
                return this.GetSubModule(typeof(VacuoSystem), this._baseModule) as VacuoSystem;
            }
        }
        public Cabinets CabinetSys
        {
            get
            {
                return this.GetSubModule(typeof(Cabinets), this._baseModule) as Cabinets;
            }
        }
        private IParamedModule GetSubModule(Type t, IParamedModule parent)
        {
            foreach (var item in _baseModule.SubParamedModules)
            {
                if(item.GetType() == t)
                {
                    return item;
                }
                IParamedModule sub = GetSubModule(t, item);
                if (sub != null)
                {
                    return sub;
                }
            }
            return null;
        }

    
    }
}
