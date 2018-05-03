using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using KMP.Interface.Model;
namespace ParameterService
{
    [Export(typeof(IModuleService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
  public  class ModuleService:IModuleService
    {
        IParamedModule _paramedModule;
        [ImportingConstructor]
        public ModuleService()
        {
            _paramedModule = ServiceLocator.Current.GetInstance<IParamedModule>();
         
        }
        public void Create()
        {
            _paramedModule.CreateModule(new ParCylinder());
        }
    }
}
