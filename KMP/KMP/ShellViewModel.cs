using Infranstructure.Commands;
using Infranstructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP
{

    [Export]
    class ShellViewModel: NotificationObject
    {
        IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
        
        }

       
        
    }
}
