using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using KMP.Interface.Model;
using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class RailSupport : ParamedModuleBase
    {
        public RailSupport():base()
        {

        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            AssemblyDocument assembly = InventorTool.CreateAssembly();
            assemblyDef = assembly.ComponentDefinition;
        }
    }
}
