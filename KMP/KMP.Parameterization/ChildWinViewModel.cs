using KMP.Parameterization.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP.Parameterization
{
    class ChildWinViewModel: NotificationObject, IChildWinViewModel
    {
        public ChildWinViewModel()
        {
            this.InitNewModelWin();
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
    }
}
