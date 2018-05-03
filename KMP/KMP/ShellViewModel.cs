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
        FileCommandProxy _fileCommandProxy;
        IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, FileCommandProxy fileCommandProxy)
        {
            _eventAggregator = eventAggregator;
            _fileCommandProxy = fileCommandProxy;
            InitCommands();
        }

        private void InitCommands()
        {
            _fileCommandProxy.OpenFileCommand = new Microsoft.Practices.Prism.Commands.DelegateCommand(OpenFileExecute);
        }
        private void OpenFileExecute()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Inventor Open File Dialog";
            fdlg.InitialDirectory = @"c:\program files\autodesk\inventor 2013\samples\models\";
            fdlg.Filter = "Inventor files (*.ipt; *.iam; *.idw)|*.ipt;*.iam;*.idw";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _eventAggregator.GetEvent<UpdateModelEvent>().Publish(fdlg.FileName);
            }
        }
    }
}
