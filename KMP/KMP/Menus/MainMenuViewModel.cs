using Infranstructure;
using Infranstructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP.Menus
{
    [Export(typeof(MainMenuViewModel))]
    public class MainMenuViewModel: NotificationObject
    {
        private ILoggerFacade _logger;
        private IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public MainMenuViewModel(ILoggerFacade logger, IEventAggregator eventAggregator)
        {
            this._logger = logger;
            this._eventAggregator = eventAggregator;
            CommandInit();


        }

        #region Commands


        private void CommandInit()
        {
            
        }
       
       
        #endregion
    }
}
