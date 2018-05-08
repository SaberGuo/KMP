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
   
    public class RailSupportTopBoard : PartModulebase
    {
      internal  ParRailSupportTopBoard par = new ParRailSupportTopBoard();
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
      
        public override void CreateModule()
        {
            CreateDoc();
           PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            ExtrudeFeature box = InventorTool.CreateBoxWithHole(Definition,osketch,par.Width/10,par.Width / 10, par.Thickness,
                par.HoleCenterDistance / 10, par.HoleTopEdgeDistance / 10, par.HoleSideEdgeDistance / 10, par.HoleRadius / 10);
            Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            box.Name = "TopBoard";
            MateiMateDefinition mateD = Definition.iMateDefinitions.AddMateiMateDefinition(startFace, 0);
            mateD.Name = "mateD";
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
