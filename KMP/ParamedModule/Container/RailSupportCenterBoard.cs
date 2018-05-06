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
        internal ParRailSupportCenterBoard par = new ParRailSupportCenterBoard();
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
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
          ExtrudeFeature box=  InventorTool.CreateBoxWithHole(Definition, osketch, par.Width / 10, par.Width / 10, par.Thickness,
                 par.HoleCenterDistance / 10, par.HoleTopEdgeDistance / 10, par.HoleSideEdgeDistance / 10, par.HoleRadius / 10);
            box.Name = "CenterBoard";
            List<Face> sideFaces= InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            
            MateiMateDefinition mateB = Definition.iMateDefinitions.AddMateiMateDefinition(startFace, 0);
            mateB.Name = "mateB";
            MateiMateDefinition mateC = Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0);
            mateC.Name = "mateC";
            Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[2], 0).Name="flushC";
          
            SaveDoc();
           
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }
    }
}
