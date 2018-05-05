using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface;
using KMP.Interface.Model.Container;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;

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
                if (item.Parameter == null) continue;
                switch (item.Parameter.GetType().Name)
                {
                    case "ParCylinder":
                        //item.CreateModule(new ParCylinder());
                        break;
                    case "ParCylinderDoor":
                       // item.CreateModule(new ParCylinderDoor());
                        break;
                    case "ParPedestal":
                      //  item.CreateModule(new ParPedestal());
                        break;
                    case "ParRail":
                       // item.CreateModule(new ParRail());
                        break;
                    case "ParRailSupportBrace":
                       // item.CreateModule(new ParRailSupportBrace());
                        break;
                    case "ParRailSupportTopBoard":
                        item.CreateModule(new ParRailSupportTopBoard());
                        bool isaa= item.CheckParamete();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
