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
    }
}
