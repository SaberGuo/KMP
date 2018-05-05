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
using System.Reflection;
namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架顶板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RailSupportTopBoard : PartModulebase
    {
        ParRailSupportTopBoard par = new ParRailSupportTopBoard();
        public RailSupportTopBoard():base()
        {
            this.Parameter = par;
            init();
        }
        void init()
        {
            par.Width = 120;
            par.Thickness = 20;
            par.HoleCenterDistance = 10;
            par.HoleRadius = 5.5;
            par.HoleSideEdgeDistance =15;
            par.HoleTopEdgeDistance = 15;
        }
      
        public override void CreateModule(ParameterBase Parameter)
        {
            CreateDoc();
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
           InventorTool.CreateBoxWithHole(partDef,osketch,par.Width/10,par.Width / 10, par.Thickness,
                par.HoleCenterDistance / 10, par.HoleTopEdgeDistance / 10, par.HoleSideEdgeDistance / 10, par.HoleRadius / 10);
            SaveDoc();
        }

    

        public override bool CheckParamete()
        {
         
            if (!CommonTool.CheckParameterValue(this.Parameter)) return false;
            if (par.HoleTopEdgeDistance <= par.HoleRadius) return false;
            if (par.HoleSideEdgeDistance <= par.HoleRadius) return false;
            if (par.HoleCenterDistance + par.HoleRadius > par.Width / 2) return false;
            return true;
        }
    }
}
