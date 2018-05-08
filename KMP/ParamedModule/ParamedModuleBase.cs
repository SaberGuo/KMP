using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using KMP.Interface;
using KMP.Interface.Model;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace ParamedModule
{
    public abstract class ParamedModuleBase :NotificationObject, IParamedModule
    {

        ParameterBase parameter;
        string modelPath;
        string modelName;
        ObservableCollection<IParamedModule> subParamModules = new ObservableCollection<IParamedModule>();
        /*public ComponentOccurrence Occurrence
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }*/
        public ComponentOccurrence Occurrence { get; set; }

        public string ModelName
        {
            get
            {
                return this.modelName;
            }
            set
            {
                this.modelName = value;
                RaisePropertyChanged(() => this.ModelName);
            }
        }
        public ParameterBase Parameter
        {
            get
            {
                return parameter;
            }

            set
            {
                parameter = value;
            }
        }

        public string ModelPath {
            get {
                return this.modelPath;
            }
            set {
                this.modelPath = value;
                RaisePropertyChanged(() => this.ModelPath);

            }
        }

        public ObservableCollection<IParamedModule> SubParamModules {
            get {
                return this.subParamModules;
            }
            set {
                this.subParamModules = value;
                RaisePropertyChanged(() => this.SubParamModules);
            }
        }

        public abstract void CreateModule(ParameterBase Parameter);
     
    }
}
