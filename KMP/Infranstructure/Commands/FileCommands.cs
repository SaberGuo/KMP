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
        static DelegateCommand _OpenFileCommand;
        static DelegateCommand _NewFileCommand;
        static DelegateCommand _SaveAsFileCommand;
        static DelegateCommand _ExitCommand;
        static DelegateCommand _CloseCommand;
        static CompositeCommand _SaveAllFileCommand = new CompositeCommand();

        public static DelegateCommand CloseCommand
        {
            get { return _CloseCommand; }
            set { _CloseCommand = value; }
        }
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

        public static DelegateCommand SaveAsFileCommand
        {
            get { return _SaveAsFileCommand; }
            set { _SaveAsFileCommand = value; }
        }
        public static CompositeCommand SaveAllFileCommand
        {
            get { return _SaveAllFileCommand; }
        }

        public static DelegateCommand ExitCommand
        {
            get { return _ExitCommand; }
            set { _ExitCommand = value; }
        }


    }

    /// <summary>
    /// Provides a class wrapper around the static SaveAll command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FileCommandProxy
    {
        public virtual DelegateCommand OpenFileCommand
        {
            get { return FileCommands.OpenFileCommand; }
            set { FileCommands.OpenFileCommand = value; }
        }

        public virtual DelegateCommand CloseCommand
        {
            get { return FileCommands.CloseCommand; }
            set { FileCommands.CloseCommand = value; }
        }

        public virtual DelegateCommand NewFileCommand
        {
            get { return FileCommands.NewFileCommand; }
            set { FileCommands.NewFileCommand = value; }
        }

        public virtual DelegateCommand SaveAsFileCommand
        {
            get { return FileCommands.SaveAsFileCommand; }
            set { FileCommands.SaveAsFileCommand = value; }
        }
        public virtual CompositeCommand SaveAllFileCommand
        {
            get { return FileCommands.SaveAllFileCommand; }
        }

        public virtual DelegateCommand ExitCommand
        {
            get { return FileCommands.ExitCommand; }
            set { FileCommands.ExitCommand = value; }
        }
    }
}
