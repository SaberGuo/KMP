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
    /// 导轨支架侧板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
  public  class RailSupportSidePlate : PartModulebase
    {
        ParRailSupportSidePlate par = new ParRailSupportSidePlate();
        public RailSupportSidePlate():base()
        {
            this.Parameter = par;
            init();
        }
        void init()
        {
            par.Width = 40;
            par.Thickness = 20;
            par.Length = 240;
        }
        public override bool CheckParamete()
        {
            throw new NotImplementedException();
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
