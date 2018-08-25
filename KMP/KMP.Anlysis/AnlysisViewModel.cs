using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
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
        private VaccumViewModel _vaccumViewModel;
        private HeliumViewModel _heliumViewModel;

        [ImportingConstructor]
        public AnalysisViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            VaccumViewModel = new VaccumViewModel();
        }

        public VaccumViewModel VaccumViewModel
        {
            get
            {
                return this._vaccumViewModel;
            }
            set
            {
                this._vaccumViewModel = value;
            }
        }

        public HeliumViewModel HeliumViewModel
        {
            get
            {
                return this._heliumViewModel;
            }
            set
            {
                this._heliumViewModel = value;
            }
        }
    }
}
