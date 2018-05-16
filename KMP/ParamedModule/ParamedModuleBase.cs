using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using KMP.Interface;
using KMP.Interface.Model;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using Infranstructure.Events;

namespace ParamedModule
{
    public abstract class ParamedModuleBase :NotificationObject, IParamedModule
    {

        ParameterBase parameter;
        ModuleCollection subParameModules =new ModuleCollection();
        string modelPath;
        string name;
        public ParamedModuleBase()
        {
            this.SubParamedModules.Root = this;
        }
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

        public event EventHandler<GeneratorEventArgs> GeneratorChanged;

        public void GeneratorProgress(object sender, string info)
        {
            this.GeneratorChanged(sender, new GeneratorEventArgs { ProgressInfo = info });
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
        public virtual string FullPath { get; }
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

        public int GetGeneratorCount()
        {
            int count = 0;
            foreach (var item in subParameModules)
            {
                count +=item.GetGeneratorCount();
            }
            count += 1;
            return count;
        }
        public abstract void CreateModule();
        public abstract bool CheckParamete();
    
    }
}
