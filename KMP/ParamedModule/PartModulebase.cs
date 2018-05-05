using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using Infranstructure.Tool;
namespace ParamedModule
{
  public abstract  class PartModulebase:ParamedModuleBase
    {
      protected  PartComponentDefinition partDef;
        PartDocument part;
        public PartModulebase():base()
        {

        }
       protected void CreateDoc()
        {
             part = InventorTool.CreatePart();
            partDef = part.ComponentDefinition;
        }
      
        protected void SaveDoc()
        {

            part.FullFileName = AppDomain.CurrentDomain.BaseDirectory + "Project\\" + this.GetType().Name+ ".ipt";
            part.Save();
            part.Close();
            part = null;
            partDef = null;
        }
    }
}
