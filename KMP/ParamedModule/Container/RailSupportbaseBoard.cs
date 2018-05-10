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

 public  class RailSupportbaseBoard : PartModulebase
    {
        internal ParRailSupportbaseBoard par = new ParRailSupportbaseBoard();
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

        public override void CreateModule()
        {
            init();
            CreateDoc();
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
          ExtrudeFeature box=   InventorTool.CreateBox(Definition, osketch, UsMM(par.Length), UsMM(par.Width), UsMM(par.Thickness));
            List<Face> sideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            box.Name = "RailBaseBoard";
          
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            List<Edge> startEdges = InventorTool.GetCollectionFromIEnumerator<Edge>(startFace.Edges.GetEnumerator());
            List<Edge> endEdges = InventorTool.GetCollectionFromIEnumerator<Edge>(endFace.Edges.GetEnumerator());
            MateiMateDefinition mateA = Definition.iMateDefinitions.AddMateiMateDefinition(startFace, 0);
            mateA.Name = "mateA";
            MateiMateDefinition mateB = Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0);
            mateB.Name = "mateB";
           // partDef.iMateDefinitions.AddAngleiMateDefinition(sideFaces[0], false, Math.PI/2);
            //Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[0], 0 ).Name= "flushA";
            Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[1], 0).Name="flushB";
            Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[3], 0).Name="flushC";
           // Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[0], 0).Name="flushD";
            SaveDoc();
        }
    }
}
