using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.Anlysis
{
    [Export(typeof(AnlysisViewModel))]
    class AnlysisViewModel: NotificationObject
    {
        private EventAggregator _eventAggregator;
        private VaccumViewModel _vaccumViewModel;

        [ImportingConstructor]
        AnlysisViewModel(EventAggregator eventAggregator)
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
    }
}
