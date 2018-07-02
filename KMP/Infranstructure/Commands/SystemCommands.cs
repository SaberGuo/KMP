using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Infranstructure.Commands
{
    public static class SystemCommands
    {
        static DelegateCommand _ConfigCommand;
        public static DelegateCommand ConfigCommand
        {
            get { return _ConfigCommand; }
            set { _ConfigCommand = value; }
        }

        static DelegateCommand _ReportCommand;
        public static DelegateCommand ReportCommand
        {
            get { return _ReportCommand; }
            set { _ReportCommand = value; }
        }

        static DelegateCommand _AnlysisCommand;
        public static DelegateCommand AnlysisCommand
        {
            get { return _AnlysisCommand; }
            set { _AnlysisCommand = value; }
        }
    }
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SystemCommandProxy
    {
        public virtual DelegateCommand ConfigCommand
        {
            get { return SystemCommands.ConfigCommand; }
            set { SystemCommands.ConfigCommand = value; }
        }

        public virtual DelegateCommand ReportCommand
        {
            get { return SystemCommands.ReportCommand; }
            set { SystemCommands.ReportCommand = value; }
        }

        public virtual DelegateCommand AnlysisCommand
        {
            get { return SystemCommands.AnlysisCommand; }
            set { SystemCommands.AnlysisCommand = value; }
        }
    }
}
