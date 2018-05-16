using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using Infranstructure.Tool;
using System.IO;
namespace ParamedModule
{
  public abstract  class PartModulebase:ParamedModuleBase
    {
      internal  PartComponentDefinition Definition;
      internal  PartDocument Doc;
        public PartModulebase():base()
        {
            
        }
        public override string FullPath
        {
            get
            {
                return System.IO.Path.Combine(ModelPath, this.GetType().Name + ".ipt");
            }
        }
        protected void CreateDoc()
        {
             Doc = InventorTool.CreatePart();
            Definition = Doc.ComponentDefinition;
        }
        
        protected void SaveDoc()
        {
            Doc.FullFileName = this.FullPath;
            if (System.IO.File.Exists(Doc.FullFileName))
            {
                System.IO.File.Delete(Doc.FullFileName);
            }
            Doc.Save2();
           
          //  Doc.Close();
          
        }
    }
}
