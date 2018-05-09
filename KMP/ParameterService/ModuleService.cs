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
        IParamedModule assemsly;
        public void Create()
        {

            foreach (var item in list)
            {
                if (item.Parameter == null) continue;
                switch (item.Parameter.GetType().Name)
                {
                    case "ParCylinder":
                        item.CreateModule();
                        break;
                    case "ParCylinderDoor":
                       // item.CreateModule();
                        break;
                    case "ParPedestal":
                        //item.CreateModule();
                        break;
                    case "ParRail":
                       // item.CreateModule(new ParRail());
                        break;
                    //case "ParRailSupportBrace":
                    //    item.CreateModule(new ParRailSupportBrace());
                    //    break;
                    //case "ParRailSupportTopBoard":
                    //    item.CreateModule(new ParRailSupportTopBoard());
                    //    break;
                    //case "ParRailSupportCenterBoard":
                    //    item.CreateModule(new ParRailSupportCenterBoard());
                    //    break;
                    //case "ParRailSupportSidePlate":
                    //    item.CreateModule(new ParRailSupportSidePlate());
                    //    break;
                    //case "ParRailSupportbaseBoard":
                    //    item.CreateModule(new ParRailSupportbaseBoard());
                    //    break;
                    case "ParRailSupport":
                      assemsly=  item;
                        break;
                    case "ParContainerSystem":
                       // item.CreateModule();
                        break;
                    default:
                        break;
                }
               
            }
           // assemsly.CreateModule();
        }
    }
}
