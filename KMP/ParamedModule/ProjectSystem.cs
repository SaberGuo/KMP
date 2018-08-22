using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Container;
namespace ParamedModule
{
    [Export("ProjectSystem", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectSystem : ParamedModuleBase
    {
        [ImportingConstructor]
        public ProjectSystem():base()
        {

        }
        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule()
        {
            foreach (var item in this.SubParamedModules)
            {
                item.CreateModule();
            }
        }

        internal override void CloseSameNameDocment()
        {
            throw new NotImplementedException();
        }
    }
}
