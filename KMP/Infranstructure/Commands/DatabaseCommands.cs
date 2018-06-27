using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Infranstructure.Commands
{
    public static class DatabaseCommands
    {
        static DelegateCommand _BrowserCommand;
        static DelegateCommand _ConfigCommand;
        static DelegateCommand _UploadCommand;
        static DelegateCommand _DownloadCommand;

        public static DelegateCommand BrowserCommand
        {
            get { return _BrowserCommand; }
            set { _BrowserCommand = value; }
        }

        public static DelegateCommand ConfigCommand
        {
            get { return _ConfigCommand; }
            set { _ConfigCommand = value; }
        }

        public static DelegateCommand UploadCommand
        {
            get { return _UploadCommand; }
            set { _UploadCommand = value; }
        }

        public static DelegateCommand DownloadCommand
        {
            get { return _DownloadCommand; }
            set { _DownloadCommand = value; }
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DatabaseCommandProxy
    {
        public virtual DelegateCommand DownloadCommand
        {
            get { return DatabaseCommands.DownloadCommand; }
            set { DatabaseCommands.DownloadCommand = value; }
        }

        public virtual DelegateCommand UploadCommand
        {
            get { return DatabaseCommands.UploadCommand; }
            set { DatabaseCommands.UploadCommand = value; }
        }

        public virtual DelegateCommand ConfigCommand
        {
            get { return DatabaseCommands.ConfigCommand; }
            set { DatabaseCommands.ConfigCommand = value; }
        }

        public virtual DelegateCommand BrowserCommand
        {
            get { return DatabaseCommands.BrowserCommand; }
            set { DatabaseCommands.BrowserCommand = value; }
        }
    }
}
