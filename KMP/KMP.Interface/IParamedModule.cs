using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace KMP.Interface
{
  public  interface IParamedModule
    {
        ParameterBase Parameter { get; set; }
        string ModelPath { get; set; }
        string Name { get; set; }
        ComponentOccurrence Occurrence { get; set; }
        ModuleCollection SubParamedModules { get; set; }
        void CreateModule();
        bool CheckParamete();

        event PropertyChangedEventHandler PropertyChanged;
    }
}
