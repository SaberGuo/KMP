using Infranstructure.Commands;
using Infranstructure.Events;
using KMP.Interface;
using KMP.Interface.Model;
using KMP.Parameterization.Events;
using KMP.Parameterization.InventorMonitor;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP.Parameterization
{
    [Export(typeof(MainDockViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class MainDockViewModel: NotificationObject
    {
        IInvMonitorController _invMonitorController;
        IEventAggregator _eventAggregator;
        IModuleService _moduleService;
        FileCommandProxy _fileCommandProxy;
        ModelCommandProxy _modelCommandProxy;
        ChildWinViewModel _childWindViewModel;

        [ImportingConstructor]
        public MainDockViewModel(IEventAggregator eventAggregator,
            IInvMonitorController invMonitorController,
            IModuleService moduleService,
            FileCommandProxy fileCommandProxy,
            ModelCommandProxy modelCommandProxy)
        {
            _eventAggregator = eventAggregator;
            _invMonitorController = invMonitorController;
            _moduleService = moduleService;
            _fileCommandProxy = fileCommandProxy;
            _modelCommandProxy = modelCommandProxy;
            Modules = new ModuleProject();
            _childWindViewModel = new ChildWinViewModel();
            InitFileCommands();
            InitModelCommands();



        }
        #region childwindow

        private string _newModelWinState = "Closed";
        public string NewModelWinState
        {
            get { return this._newModelWinState; }
            set
            {
                this._newModelWinState = value;
                RaisePropertyChanged("NewModelWinState");
            }
        }

        public ChildWinViewModel ChildWinViewModel
        {
            get { return this._childWindViewModel; }
        }
        #endregion
        private void OnUpdateModel(string filePath)
        {
            OnParamedModuleDisplayTest();
            //this._invMonitorController.UpdateInvModel(filePath);
        }
        private void OnNewModel(ProjectInfo info)
        {
            IParamedModule model = ServiceLocator.Current.GetInstance<IParamedModule>(info.ProjectType);
            Modules.Add(model);
        }

        
        public ObservableCollection<IInvMonitorViewModel> Documents {
            get
            {
                return this._invMonitorController.Documents;
            }
        }

        #region test

        public ModuleProject Modules { get; set; }
        private void OnParamedModuleDisplayTest()
        {
            
        }
        #endregion

        #region filecommands
        private void InitFileCommands()
        {
            this._fileCommandProxy.OpenFileCommand = new DelegateCommand(this.OpenFileExecuted);
            this._fileCommandProxy.NewFileCommand = new DelegateCommand(this.NewFileExecuted);

            this._childWindViewModel.NewModelHandler += OnNewModelHandler;
        }
        private void OpenFileExecuted()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Inventor Open File Dialog";
            fdlg.InitialDirectory = @"c:\program files\autodesk\inventor 2013\samples\models\";
            fdlg.Filter = "Inventor files (*.ipt; *.iam; *.idw)|*.ipt;*.iam;*.idw";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //_eventAggregator.GetEvent<UpdateModelEvent>().Publish(fdlg.FileName);
            }
        }
        private void NewFileExecuted()
        {
            this._childWindViewModel.CreateNewModelWindows();
            //NewModelWinState = "Open";
            
         
        }

        private void OnNewModelHandler(object sender, ProjectEventArgs e)
        {
            //save current module

            //create model
            IParamedModule module = _moduleService.CreateProject(e.ProjectType, System.IO.Path.Combine(e.ProjectDir, e.ProjectName));
            this.Modules.AddModule(module);
            
        }

        #endregion

        #region modelcommands
        private void InitModelCommands()
        {
            _modelCommandProxy.GenModelCommand = new DelegateCommand(GenModelExecuted);
        }

        private void GenModelExecuted()
        {
            this.Modules.Create();
        }
        #endregion
    }
}
