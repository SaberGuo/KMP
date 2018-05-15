using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Infranstructure.Commands
{
    public static class ModelCommands
    {
        static DelegateCommand _GenModelCommand;
        public static DelegateCommand GenModelCommand
        {
            get { return _GenModelCommand; }
            set { _GenModelCommand = value; }
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ModelCommandProxy
    {
        public virtual DelegateCommand GenModelCommand
        {
            get { return ModelCommands.GenModelCommand; }
            set { ModelCommands.GenModelCommand = value; }
        }
    }
}
