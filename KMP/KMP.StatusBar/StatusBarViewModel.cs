
using Infranstructure;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.StatusBar
{
    [Export(typeof(StatusBarViewModel))]
    class StatusBarViewModel: NotificationObject
    {
        [ImportingConstructor]
        public StatusBarViewModel()
        {

        }
    }
}
