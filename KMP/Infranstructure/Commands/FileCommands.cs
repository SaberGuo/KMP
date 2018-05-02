using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Commands
{
    public static class FileCommands
    {
        //TODO: 03 - The application defines a global SaveAll command which invokes the Save command on all registered Orders. It is enabled only when all orders can be saved.
        public static CompositeCommand OpenFileCommand = new CompositeCommand();
        public static CompositeCommand NewFileCommand = new CompositeCommand();
        public static CompositeCommand SaveAllFileCommand = new CompositeCommand();
    }

    /// <summary>
    /// Provides a class wrapper around the static SaveAll command.
    /// </summary>
    public class FileCommandProxy
    {
        public virtual CompositeCommand OpenFileCommand
        {
            get { return FileCommands.OpenFileCommand; }
        }

        public virtual CompositeCommand SaveAllFileCommand
        {
            get { return FileCommands.SaveAllFileCommand; }
        }
    }
}
