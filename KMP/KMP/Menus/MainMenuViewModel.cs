﻿using Infranstructure;
using Infranstructure.Commands;
using Infranstructure.Events;
using KMP.Interface;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace KMP.Menus
{
    [Export(typeof(MainMenuViewModel))]
    public class MainMenuViewModel: NotificationObject
    {
        private ILoggerFacade _logger;
        private IEventAggregator _eventAggregator;
        private DatabaseCommandProxy _dbCommandProxy;
        [ImportingConstructor]
        public MainMenuViewModel(ILoggerFacade logger, IEventAggregator eventAggregator, DatabaseCommandProxy dbCommandProxy)
        {
            this._logger = logger;
            this._eventAggregator = eventAggregator;
            this._dbCommandProxy = dbCommandProxy;
            _eventAggregator.GetEvent<ProjectChangedEvent>().Subscribe(OnProjectChanged);
            CommandInit();


        }

        #region Commands


        private void CommandInit()
        {
            _dbCommandProxy.BrowserCommand = new DelegateCommand(DBBrowserExecuted);
        }
        void DBBrowserExecuted()
        {
            Window t = ((Window)ServiceLocator.Current.GetInstance<IBrowserWindow>());
            t.Owner = System.Windows.Application.Current.MainWindow;
            t.ShowDialog();
            
        }

        void OnProjectChanged(string projectPath)
        {
            if(projectPath == "")
            {
                this.HasProject = false;
            }
            else
            {
                this.HasProject = true;
            }
        }
        #endregion

        private bool hasProject = false;
        public bool HasProject
        {
            get
            {
                return hasProject;
            }
            set
            {
                this.hasProject = value;
                this.RaisePropertyChanged(() => this.HasProject);
            }
        }
    }
}
