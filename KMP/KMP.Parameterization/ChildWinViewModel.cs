using Infranstructure.Events;
using KMP.Parameterization.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP.Parameterization
{
    [Export(typeof(ChildWinViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ChildWinViewModel: NotificationObject, IChildWinViewModel
    {
        private IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public ChildWinViewModel(IEventAggregator eventAggregator)
        {
            this.InitNewModelWin();
            _eventAggregator = eventAggregator;
            //_eventAggregator.GetEvent<GeneratorEvent>().Subscribe(OnGeneratorInfoChanged);

        }
        #region NewModel
        public void InitNewModelWin()
        {
            this.InitProjectTypes();
            this.LocationBrowseCommand = new DelegateCommand(this.LocationBrowseExecuted);
            this.NewModelOKCommand = new DelegateCommand(this.NewModelOKExecuted);
            this.NewModelCancelCommand = new DelegateCommand(this.NewModelCancelExecuted);
        }
        private string _newModelWinState = "Closed";
        public string NewModelWinState
        {
            get { return this._newModelWinState; }
            set { this._newModelWinState = value;
                RaisePropertyChanged("NewModelWinState"); }
        }
        private string _projectName;
        public string ProjectName
        {
            get
            {
                return this._projectName;
            }
            set
            {
                this._projectName = value;
                RaisePropertyChanged(() => ProjectName);
            }
        }

        public class ProjectT
        {
            public string Description { get; set; }
            public string TypeValue { get; set; }
        }

        public List<ProjectT> ProjectTypes { get; set; }
        private void InitProjectTypes()
        {
            ProjectTypes = new List<ProjectT>();
            ProjectTypes.Add(new ProjectT { Description = "容器系统", TypeValue = "ContainerSystem" });
            ProjectTypes.Add(new ProjectT { Description = "热沉系统", TypeValue = "HeaterSystem" });
            ProjectTypes.Add(new ProjectT { Description = "环境箱", TypeValue = "WareHouseEnvironment" });
            ProjectTypes.Add(new ProjectT { Description = "氮系统", TypeValue = "Nitrogen" });
            ProjectTypes.Add(new ProjectT { Description = "真空系统", TypeValue = "VacuoSystem" });
            //ProjectTypes.Add(new ProjectT { Description = "阀门", TypeValue = "Valve" });
            //ProjectTypes.Add(new ProjectT { Description = "低温泵", TypeValue = "CoolVAC" });
            //ProjectTypes.Add(new ProjectT { Description = "低温液体储槽阵列", TypeValue = "CryoLiquidTanks" });
        }
        private string _projectType ="ContainerSystem";
        public string ProjectType
        {
            get
            {
                return this._projectType;
            }
            set
            {
                this._projectType = value;
                RaisePropertyChanged(() => ProjectType);
            }
        }
        private string _projectLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"projects");
        public string ProjectLocation
        {
            get
            {
                return this._projectLocation;
            }
            set
            {
                this._projectLocation = value;
                RaisePropertyChanged(() => ProjectLocation);
            }
        }

        public DelegateCommand LocationBrowseCommand { get; set; }
        private void LocationBrowseExecuted()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.Description = "请选择工程目录";
            fbDlg.SelectedPath = (this.ProjectLocation == "") ? this.ProjectLocation : AppDomain.CurrentDomain.BaseDirectory;
            if(fbDlg.ShowDialog()== DialogResult.OK)
            {
                ProjectLocation = fbDlg.SelectedPath;
            }
        }

        public DelegateCommand NewModelOKCommand { get; set; }
        public DelegateCommand NewModelCancelCommand { get; set; }
        private void NewModelOKExecuted()
        {
            if (!System.IO.Directory.Exists(this.ProjectLocation))
            {
                return;
            }
            //check the filename
            try
            {
                System.IO.DirectoryInfo projectInfo = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(this.ProjectLocation, this.ProjectName));
            }
            catch (Exception)
            {

                return;
            }
            this.NewModelHandler(this, new ProjectEventArgs { ProjectDir = this.ProjectLocation, ProjectName = this.ProjectName, ProjectType = this.ProjectType });
            this.NewModelWinState = "Closed";
        }
        private void NewModelCancelExecuted()
        {
            this.NewModelWinState = "Closed";
        }

        public void CreateNewModelWindows()
        {
            this.NewModelWinState = "Open";
        }

        public event EventHandler<ProjectEventArgs> NewModelHandler;
        #endregion

        #region GeneratorInfo childwindow
        private string _generatorWinState = "Closed";
        public string GeneratorWinState
        {
            get { return this._generatorWinState; }
            set
            {
                this._generatorWinState = value;
                RaisePropertyChanged(() => this.GeneratorWinState);
            }
        }

        private int currentValue = 0;
        public int CurrentValue
        {
            get
            {
                return this.currentValue;
            }
            set
            {
                this.currentValue = value;
                RaisePropertyChanged(() => this.CurrentValue);
            }
        }
        private int maxValue = 100;
        public int MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
                RaisePropertyChanged(() => this.MaxValue);
            }
        }
        private string info;
        public string Info
        {
            get { return this.info; }
            set
            {
                this.info = value;
                RaisePropertyChanged(() => Info);
            }
        }

        private void OnGeneratorInfoChanged(string info)
        {
            if (info.Contains("start_generator"))
            {
                MaxValue = int.Parse(info.Split(',')[1]);
                CurrentValue = 0;
                this.GeneratorWinState = "Open";
            }

            if (info.Contains("generating"))
            {
                CurrentValue += 1;
                if (CurrentValue > MaxValue)
                {
                    this.CurrentValue = MaxValue;
                }
                Info = info.Split(',')[1];
            }
            if (info.Contains("end_generator"))
            {
                this.CurrentValue = MaxValue;
                this.GeneratorWinState = "Closed";
            }
        }
        #endregion
    }
}
