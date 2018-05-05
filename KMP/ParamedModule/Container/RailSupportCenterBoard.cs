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
    /// 导轨支架立柱下钣金
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class RailSupportCenterBoard : PartModulebase
    {
        ParRailSupportCenterBoard par = new ParRailSupportCenterBoard();
        public RailSupportCenterBoard():base()
        {
            this.Parameter = par;
        }
        void init()
        {
            par.Width = 180;
            par.Thickness = 15;
            par.HoleCenterDistance = 10;
            par.HoleRadius = 6.5;
            par.HoleSideEdgeDistance = 30;
            par.HoleTopEdgeDistance = 25;
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            init();
            CreateDoc();
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            InventorTool.CreateBoxWithHole(partDef, osketch, par.Width / 10, par.Width / 10, par.Thickness,
                 par.HoleCenterDistance / 10, par.HoleTopEdgeDistance / 10, par.HoleSideEdgeDistance / 10, par.HoleRadius / 10);
            SaveDoc();
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }
    }
}
