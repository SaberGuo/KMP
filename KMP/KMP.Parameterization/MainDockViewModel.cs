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
using System.Threading.Tasks;

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
            _childWindViewModel = new ChildWinViewModel(_eventAggregator);
            InitFileCommands();
            InitModelCommands();
            //this._eventAggregator.GetEvent<GeneratorEvent>().Subscribe(this.OnGenerateChanged);



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
        private IInvMonitorViewModel activeDocument;

        public IInvMonitorViewModel ActiveDocument
        {
            get
            {
                return activeDocument;
            }
            set
            {
                this.activeDocument = value;
                RaisePropertyChanged(() => ActiveDocument);
            }
        }

       

        public ModuleProject Modules { get; set; }
        #region test
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
            this.Modules.First().GeneratorChanged += OnGeneratorChanged;



        }

        #endregion
        private void OnGeneratorChanged(object sender, GeneratorEventArgs e)
        {
            this._eventAggregator.GetEvent<GeneratorEvent>().Publish("generating," + e.ProgressInfo);
        }
        #region modelcommands
        private void InitModelCommands()
        {
            _modelCommandProxy.GenModelCommand = new DelegateCommand(GenModelExecuted);
        }

        private void GenModelExecuted()
        {
            if(this.Modules.Count == 0)
            {

                return;
            }
            this._eventAggregator.GetEvent<GeneratorEvent>().Publish("start_generator,"+this.Modules.First().GetGeneratorCount().ToString());
            //GeneratorDelegate gDelegate = new GeneratorDelegate(GenerateModules);
            //gDelegate.BeginInvoke(GenerateCallback, null);
            Task generatortask = new Task(GenerateModules);
            generatortask.ContinueWith((result)=> {
                _invMonitorController.UpdateInvModel(this.Modules.First().FullPath);
                this._eventAggregator.GetEvent<GeneratorEvent>().Publish("end_generator");
            });
            generatortask.Start();
            //generatortask.Start();
            //this.Modules.Create();

        }

        public void OnGenerateChanged(string info)
        {
            if(info == "end_generator")
            {
                _invMonitorController.UpdateInvModel(this.Modules.First().FullPath);
            }
        }
        //public delegate void GeneratorDelegate();

        private void GenerateModules()
        {
            try
            {
                this.Modules.Create();
            }
            catch (Exception e)
            {

                this._eventAggregator.GetEvent<InfoEvent>().Publish(e);
            }
            
        }
        private void GenerateCallback()
        {
            if (this.Modules.Count > 0)
            {
                try
                {
                    _invMonitorController.UpdateInvModel(this.Modules.First().FullPath);
                }
                catch (Exception e)
                {

                    this._eventAggregator.GetEvent<InfoEvent>().Publish(e);
                }
               
                this._eventAggregator.GetEvent<GeneratorEvent>().Publish("end_generator");
            }
            
        }
        #endregion

        public void ShowModule(IParamedModule m)
        {
            if (System.IO.File.Exists(m.FullPath))
            {
                _invMonitorController.UpdateInvModel(m.FullPath);
            }
            
        }


    }
}
