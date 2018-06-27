using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Infranstructure.Commands
{
    public static class ViewCommands
    {
        static DelegateCommand<string> _OrientViewCommand;
        public static DelegateCommand<string> OrientViewCommand
        {
            get { return _OrientViewCommand; }
            set { _OrientViewCommand = value; }
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ViewCommandProxy
    {
        public virtual DelegateCommand<string> OrientViewCommand
        {
            get { return ViewCommands.OrientViewCommand; }
            set { ViewCommands.OrientViewCommand = value; }
        }

    }
}
