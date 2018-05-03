using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Infranstructure.Commands
{
    public static class FileCommands
    {
        //TODO: 03 - The application defines a global SaveAll command which invokes the Save command on all registered Orders. It is enabled only when all orders can be saved.
        public static DelegateCommand _OpenFileCommand;
        public static DelegateCommand _NewFileCommand;
        public static CompositeCommand _SaveAllFileCommand = new CompositeCommand();

        public static DelegateCommand OpenFileCommand
        {
            get { return _OpenFileCommand; }
            set { _OpenFileCommand = value; }
        }

        public static DelegateCommand NewFileCommand
        {
            get { return _NewFileCommand; }
            set { _NewFileCommand = value; }
        }

        public static CompositeCommand SaveAllFileCommand
        {
            get { return _SaveAllFileCommand; }
        }

    }

    /// <summary>
    /// Provides a class wrapper around the static SaveAll command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileCommandProxy
    {
        public virtual DelegateCommand OpenFileCommand
        {
            get { return FileCommands.OpenFileCommand; }
            set { FileCommands.OpenFileCommand = value; }
        }
        public virtual CompositeCommand SaveAllFileCommand
        {
            get { return FileCommands.SaveAllFileCommand; }
        }
    }
}
