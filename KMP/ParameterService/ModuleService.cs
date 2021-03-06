﻿using System;
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
        List<IParamedModule> list;
        [ImportingConstructor]
        public ModuleService()
        {
            list = ServiceLocator.Current.GetAllInstances<IParamedModule>().ToList();
         
        }
        public void Create()
        {
            foreach (var item in list)
            {
                switch (item.Parameter.GetType().Name)
                {
                    case "ParCylinder":
                        item.CreateModule(new ParCylinder());
                        break;
                    case "ParCylinderDoor":
                        item.CreateModule(new ParCylinderDoor());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
