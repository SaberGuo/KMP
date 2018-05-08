using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
using System.Collections.ObjectModel;

namespace KMP.Interface
{
  public  interface IParamedModule
    {
        ParameterBase Parameter { get; set; }
        string ModelPath { get; set; }

        string ModelName { get; set; }
        ObservableCollection<IParamedModule> SubParamModules { get; set; }
        ComponentOccurrence Occurrence { get; set; }
        void CreateModule(ParameterBase Parameter);
    }
}
