
using Infranstructure;
using Infranstructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.StatusBar
{
    [Export(typeof(StatusBarViewModel))]
    class StatusBarViewModel: NotificationObject
    {
        IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public StatusBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<GeneratorEvent>().Subscribe(this.OnGeneratorChanged);
        }

        private void OnGeneratorChanged(string info)
        {
            if (info.Contains("start_generator"))
            {
                MaxValue = int.Parse(info.Split(',')[1]);
                CurrentValue = 0;
            }

            if (info.Contains("generating"))
            {
                CurrentValue += 1;
                if (CurrentValue > MaxValue)
                {
                    this.CurrentValue = MaxValue;
                }
                Info =info.Split(',')[1];
            }
            if (info.Contains("end_generator"))
            {
                this.CurrentValue = MaxValue;
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


    }
}
