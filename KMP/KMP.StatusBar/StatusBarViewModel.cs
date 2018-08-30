
using Infranstructure;
using Infranstructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
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
        private ILoggerFacade _logger;
        private string[] ImageUris = new string[3] { @"Resources\info.png", @"Resources\warning.png", @"Resources\error.png" };
        [ImportingConstructor]
        public StatusBarViewModel(IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _eventAggregator.GetEvent<GeneratorEvent>().Subscribe(this.OnGeneratorChanged,ThreadOption.BackgroundThread);
            _eventAggregator.GetEvent<InfoEvent>().Subscribe(this.OnInfoExecuted, ThreadOption.BackgroundThread);
        }

        virtual protected void OnInfoExecuted(Exception e)
        {
            MyException info = e as MyException;
            //LoggerInfo linfo = new LoggerInfo();
            if (info == null)
            {
                InfoImage = ImageUris[2];
                InfoContent = e.Message;
                TimerTick = DateTime.Now.ToString();
                _logger.Log(e.Message, Category.Exception, Priority.High);
                //linfo.InfoContent = InfoContent;
                //linfo.InfoImage = InfoImage;
                //linfo.TimerTick = DateTime.Now.ToString();
            }
            else
            {
                int type = (int)(info.InfoType) - 1;
                InfoImage = ImageUris[type];
                InfoContent = info.Message;
                TimerTick = DateTime.Now.ToString();
                //linfo.InfoContent = InfoContent;
                //linfo.InfoImage = InfoImage;
                //linfo.TimerTick = DateTime.Now.ToString();

                switch (info.InfoType)
                {
                    case ExceptionType.INFO:
                        _logger.Log(info.Message, Category.Info, Priority.Low);
                        break;
                    case ExceptionType.WARNING:
                        _logger.Log(info.Message, Category.Warn, Priority.Low);
                        break;
                    case ExceptionType.ERROR:
                        _logger.Log(info.Message, Category.Exception, Priority.High);
                        break;
                    default:
                        break;
                }
            }
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

        private string _infoImage;
        private string _TimerTick;
        private string _infoContent;
        public string InfoImage
        {
            get { return this._infoImage; }
            set
            {
                this._infoImage = value;
                RaisePropertyChanged(() => this.InfoImage);
            }
        }



        public string InfoContent
        {
            get { return _infoContent; }
            set
            {
                this._infoContent = value;
                RaisePropertyChanged(() => this.InfoContent);
            }
        }
        public string TimerTick
        {
            get
            {
                return this._TimerTick;
            }
            set
            {
                this._TimerTick = value;
                RaisePropertyChanged(() => this.TimerTick);
            }
        }


    }
}
