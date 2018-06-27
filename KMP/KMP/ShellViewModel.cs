using Infranstructure.Commands;
using Infranstructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP
{

    [Export]
    class ShellViewModel: NotificationObject
    {
        IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProjectChangedEvent>().Subscribe(OnProjectChanged);
            _eventAggregator.GetEvent<GeneratorEvent>().Subscribe(this.OnGeneratorChanged);
        }

        private void OnProjectChanged(string projectPath)
        {
            this.Title = "KMP-" + projectPath;
        }
        private string title = "KMP";
        private bool _isGenerating = false;
        public bool IsGenerating
        {
            get { return this._isGenerating; }
            set { this._isGenerating = value; this.RaisePropertyChanged(() => this.IsGenerating); }
        }
        private string _generatorInfo = "";
        public string GeneratorInfo
        {
            get
            {
                return this._generatorInfo;
            }
            set
            {
                this._generatorInfo = value;
                RaisePropertyChanged(() => this.GeneratorInfo);
            }
        }
        private void OnGeneratorChanged(string info)
        {
            if (info.Contains("start_generator"))
            {
                this.IsGenerating = true;
                return;
            }

            if (info.Contains("generating"))
            {

                this.GeneratorInfo = info.Split(',')[1];
            }
            if (info.Contains("end_generator"))
            {
                this.IsGenerating = false;
                return;
            }
        }
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }

       
        
    }
}
