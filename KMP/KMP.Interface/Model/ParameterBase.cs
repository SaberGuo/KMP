using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
namespace KMP.Interface.Model
{
 public   class ParameterBase:NotificationObject
    {
        string name;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                this.RaisePropertyChanged(() => this.name);
            }
        }

        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
                this.RaisePropertyChanged(()=>this.path);
            }
        }

        string path;
    }
}
