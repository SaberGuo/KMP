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

        static DelegateCommand _SaveCommand;
        public static DelegateCommand SaveCommand
        {
            get
            {
                return _SaveCommand;
            }
            set
            {
                _SaveCommand = value;
            }
        }

        static DelegateCommand _SaveAsCommand;
        public static DelegateCommand SaveAsCommand
        {
            get
            {
                return _SaveAsCommand;
            }
            set
            {
                _SaveAsCommand = value;
            }
        }

        static DelegateCommand _AddModelCommand;
        public static DelegateCommand AddModelCommand
        {
            get
            {
                return _AddModelCommand;
            }
            set
            {
                _AddModelCommand = value;
            }
        }

        static DelegateCommand _AddModelFromDatabaseCommand;
        public static DelegateCommand AddModelFromDatabaseCommand
        {
            get { return _AddModelFromDatabaseCommand; }
            set { _AddModelFromDatabaseCommand = value; }
        }

        static DelegateCommand _UploadCommand;
        public static DelegateCommand UploadCommand
        {
            get
            {
                return _UploadCommand;
            }
            set
            {
                _UploadCommand = value;
            }
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

        public virtual DelegateCommand SaveCommand
        {
            get { return ModelCommands.SaveCommand; }
            set { ModelCommands.SaveCommand = value; }
        }

        public virtual DelegateCommand SaveAsCommand
        {
            get { return ModelCommands.SaveAsCommand; }
            set { ModelCommands.SaveAsCommand = value; }
        }

        public virtual DelegateCommand AddModelCommand
        {
            get { return ModelCommands.AddModelCommand; }
            set { ModelCommands.AddModelCommand = value; }
        }

        public virtual DelegateCommand AddModelFromDatabaseCommand
        {
            get { return ModelCommands.AddModelFromDatabaseCommand; }
            set { ModelCommands.AddModelFromDatabaseCommand = value; }
        }

        public virtual DelegateCommand UploadCommand
        {
            get { return ModelCommands.UploadCommand; }
            set { ModelCommands.UploadCommand = value; }
        }
    }
}
