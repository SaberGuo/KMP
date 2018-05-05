using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using Infranstructure.Tool;
namespace ParamedModule
{
  public abstract  class AssembleModuleBase:ParamedModuleBase
    {
       protected AssemblyComponentDefinition assemblyDef;
       protected AssemblyDocument assembly;
        protected Matrix oPos;
        public AssembleModuleBase():base()
        {

        }
        protected void CreateDoc()
        {
             assembly = InventorTool.CreateAssembly();
            assemblyDef = assembly.ComponentDefinition;
            oPos = InventorTool.TranGeo.CreateMatrix();
        }
        protected void SaveDoc()
        {

        }
    }
}
