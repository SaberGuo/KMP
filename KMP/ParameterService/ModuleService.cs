using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface;
using KMP.Interface.Model.Container;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using Infranstructure.Tool;
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
            assemsly= list.Where(a => a.Parameter.GetType().Name == "ParContainerSystem").FirstOrDefault();
         
        }
        IParamedModule assemsly;
        public void Create()
        {
            IParamedModule SS = ServiceLocator.Current.GetInstance<IParamedModule>("WareHouseEnvironment");//HeaterSystem
            SS.CreateModule();
            //foreach (var item in list)
            //{
            //    switch (item.Parameter.GetType().Name)
            //    {
            //        case "ParNoumenon":
            //            item.CreateModule();
            //            break;
            //        case "ParCap":
            //            item.CreateModule();
            //            break;
            //        case "ParHeatSink":
            //           // assemsly = item;
            //            break;
            //        case "ParContainerSystem":
            //            item.CreateModule();
            //            break;
            //        case "ParCylinder":
            //             item.CreateModule();
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //foreach (var item in list)
            //{
            //    if (item.Parameter == null) continue;
            //    switch (item.Parameter.GetType().Name)
            //    {
            //        case "ParCylinder":
            //           // item.CreateModule();
            //            break;
            //        case "ParCylinderDoor":
            //           // item.CreateModule();
            //            break;
            //        case "ParPedestal":
            //            //item.CreateModule();
            //            break;
            //        case "ParRail":
            //           // item.CreateModule();
            //            break;
            //        //case "ParRailSupportBrace":
            //        //    item.CreateModule(new ParRailSupportBrace());
            //        //    break;
            //        case "ParRailSupportTopBoard":
            //          // item.CreateModule();
            //            break;
            //        //case "ParRailSupportCenterBoard":
            //        //    item.CreateModule(new ParRailSupportCenterBoard());
            //        //    break;
            //        //case "ParRailSupportSidePlate":
            //        //    item.CreateModule(new ParRailSupportSidePlate());
            //        //    break;
            //        //case "ParRailSupportbaseBoard":
            //        //    item.CreateModule(new ParRailSupportbaseBoard());
            //        //    break;
            //        case "ParRailSupport":
            //          assemsly=  item;
            //            break;
            //        case "ParContainerSystem":
            //            item.CreateModule();
            //            break;
            //        case "ParRailSystem":
            //             //item.CreateModule();
            //            break;
            //        case "ParPlaneSupport":
            //           // item.CreateModule();
            //            break;
            //        case "ParPlaneSystem":
            //           // item.CreateModule();
            //            break;
            //        default:
            //            break;
            //    }
               
            //}
           // assemsly.CreateModule();
        }

        public IParamedModule CreateProject(string projType, string projPath)
        {
            IParamedModule module = ServiceLocator.Current.GetInstance<IParamedModule>(projType);
            module.ModelPath = projPath;
            return module;
        }

        public IParamedModule OpenProject(string projPath)
        {
            string projType = XMLDeserializerHelper.GetProjType(projPath);
            if (projType != "")
            {
                IParamedModule module = ServiceLocator.Current.GetInstance<IParamedModule>(projType);
                module.DeSerialization(projPath);
                module.ModelPath = System.IO.Path.GetDirectoryName(projPath);
                return module;
            }
            return null;
        }
        public void Serialization()
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + "aa.xml";
            //Type t = assemsly.GetType();
            //XMLDeserializerHelper.Serialization<ParamedModuleBase>(assemsly, path);
            assemsly.Serialization();
           
        }
        public void DeSerialization()
        {
            //assemsly.DeSerialization();
        }
    }
}

