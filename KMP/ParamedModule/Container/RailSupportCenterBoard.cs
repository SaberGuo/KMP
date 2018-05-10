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

        public override void CreateModule()
        {
            init();
            CreateDoc();
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
          ExtrudeFeature box=  InventorTool.CreateBoxWithHole(Definition, osketch, UsMM(par.Width ), UsMM(par.Width ), UsMM(par.Thickness),
                 UsMM(par.HoleCenterDistance ), UsMM(par.HoleTopEdgeDistance ), UsMM(par.HoleSideEdgeDistance ), UsMM(par.HoleRadius ));
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
