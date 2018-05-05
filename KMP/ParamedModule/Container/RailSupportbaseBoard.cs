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
    /// 导轨支架底板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
 public  class RailSupportbaseBoard : PartModulebase
    {
        ParRailSupportbaseBoard par = new ParRailSupportbaseBoard();
        [ImportingConstructor]
        public RailSupportbaseBoard():base()
        {
            this.Parameter = par;
        }
        void init()
        {
            par.Length = 260;
            par.Width = 220;
            par.Thickness = 15;
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            init();
            CreateDoc();
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            InventorTool.CreateBox(partDef, osketch, par.Length/10, par.Width/10, par.Thickness);
            SaveDoc();
        }
    }
}
