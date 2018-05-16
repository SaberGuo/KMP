using Infranstructure.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Interface
{
    public class ModuleCollection : ObservableCollection<IParamedModule>
    {
        public ModuleCollection()
        {

        }
        
        public ModuleCollection(IParamedModule root)
        {
            this.Add(root);
            root.PropertyChanged += ModulePropertyChanged;
        }

        public IParamedModule Root { get; set; }
        public void AddModule(IParamedModule module)
        {
            this.Add(module);
            module.PropertyChanged += ModulePropertyChanged;
            module.GeneratorChanged += OnGeneratorChanged;
        }
        public virtual void ModulePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e);
        }

        public void OnGeneratorChanged(object sender, GeneratorEventArgs e)
        {
            if(Root != null)
            {
                Root.GeneratorProgress(sender, e.ProgressInfo);
            }
            
        }
    }
}
