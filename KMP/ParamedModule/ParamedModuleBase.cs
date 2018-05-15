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
        ModuleCollection subParameModules =new ModuleCollection();
        string modelPath;
        string name;

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

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                RaisePropertyChanged(() => this.Name);
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
                parameter.PropertyChanged += Parameter_PropertyChanged;
                this.RaisePropertyChanged(() => this.Parameter);
            }
        }

        private void Parameter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(e.PropertyName);
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
                foreach (var item in subParameModules)
                {
                    item.ModelPath = modelPath;
                }
                this.RaisePropertyChanged(() => this.ModelPath);
            }
        }
        public double UsMM(double value)
        {
            return value / 10;
        }

       public ModuleCollection SubParamedModules
        {
            get
            {
                return subParameModules;
            }

            set
            {
                subParameModules = value;
                this.RaisePropertyChanged(() => this.SubParamedModules);
            }
        }

    
        public abstract void CreateModule();
        public abstract bool CheckParamete();
    }
}
