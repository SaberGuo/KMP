using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
namespace KMP.Interface
{
  public  interface IParamedModule
    {
        ParameterBase Parameter { get; set; }
       
        ComponentOccurrence Occurrence { get; set; }
        void CreateModule(ParameterBase Parameter);
    }
}
