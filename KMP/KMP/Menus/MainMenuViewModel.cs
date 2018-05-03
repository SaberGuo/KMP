using Infranstructure;
using Microsoft.Practices.Prism.Commands;
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
        [ImportingConstructor]
        public MainMenuViewModel(ILoggerFacade logger)
        {
            this._logger = logger;
            CommandInit();


        }

        #region Commands

        private void CommandInit()
        {
            //this.OpenFileCommand = new DelegateCommand(this.OpenFileExecute);
        }
       
        


        #endregion
    }
}
