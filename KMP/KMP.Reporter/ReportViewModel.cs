using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.Reporter
{
    [Export]
    public class ReportViewModel: NotificationObject
    {
        private ReportGenerator reportGen = new ReportGenerator();

        private string genPath = "";

        public string GenPath
        {
            get
            {
                return this.genPath;
            }
            set
            {
                this.genPath = value;
            }
        }
        [ImportingConstructor]
        public ReportViewModel()
        {
            this.DocGenerateCommand = new DelegateCommand(DocGenerateExecuted);

        }

        private string _DocPath = "";
        public string DocPath
        {
            get
            {
                return _DocPath;
            }
            set
            {
                _DocPath = value;
                RaisePropertyChanged(() => this.DocPath);
            }
        }

        private DelegateCommand _DocGenerateCommand;

        public DelegateCommand DocGenerateCommand
        {
            get
            {
                return this._DocGenerateCommand;
            }
            set
            {
                this._DocGenerateCommand = value;
            }
        }

        private void DocGenerateExecuted()
        {
            reportGen.Path = genPath;
            DocPath = reportGen.Generate();
            RaisePropertyChanged(() => this.DocPath);

        }
    }
}
