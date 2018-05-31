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
        static DelegateCommand _SerializationCommand;
        public static DelegateCommand SerializationCommand
        {
            get { return _SerializationCommand; }
            set { _SerializationCommand = value; }
        }

        public static DelegateCommand DeSerializationCommand
        {
            get
            {
                return _DeSerializationCommand;
            }

            set
            {
                _DeSerializationCommand = value;
            }
        }

        static DelegateCommand _DeSerializationCommand;

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
        public virtual DelegateCommand SerializationCommand
        {
            get { return ModelCommands.SerializationCommand; }
            set { ModelCommands.SerializationCommand = value; }
        }
        public virtual DelegateCommand DeSerializationCommand
        {
            get { return ModelCommands.DeSerializationCommand; }
            set { ModelCommands.DeSerializationCommand = value; }
        }
    }
}
