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
        ObservableCollection<IParamedModule> subParamedModules=new ObservableCollection<IParamedModule>();
        string modelPath;
        string name;

    
        public ComponentOccurrence Occurrence { get; set; }

        public ParameterBase Parameter
        {
            get
            {
                return parameter;
            }

            set
            {
                parameter = value;
                this.RaisePropertyChanged(() => this.Parameter);
            }
        }

        public string ModelPath
        {
            get
            {
                return modelPath;
            }

            set
            {
                modelPath = value;
                this.RaisePropertyChanged(() => this.ModelPath);
            }
        }

       public ObservableCollection<IParamedModule> SubParamedModules
        {
            get
            {
                return subParamedModules;
            }

            set
            {
                subParamedModules = value;
                this.RaisePropertyChanged(() => this.SubParamedModules);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        public abstract void CreateModule();
        public abstract bool CheckParamete();
    }
}
