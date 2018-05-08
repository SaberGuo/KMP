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
      internal  PartComponentDefinition Definition;
      internal  PartDocument Doc;
        public PartModulebase():base()
        {
            this.ModelPath = AppDomain.CurrentDomain.BaseDirectory + "Project\\" + this.GetType().Name + ".ipt";
        }
       protected void CreateDoc()
        {
             Doc = InventorTool.CreatePart();
            Definition = Doc.ComponentDefinition;
        }
      
        protected void SaveDoc()
        {
           
            Doc.FullFileName = ModelPath;
            Doc.Save();
          //  Doc.Close();
          
        }
    }
}
