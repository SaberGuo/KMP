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

  public  class PlaneTopPlate : PartModulebase
    {
        public ParPlaneTopPlate par = new ParPlaneTopPlate();
        public PlaneTopPlate():base()
        {
            this.Parameter = par;
            init();

            this.Name = "踏板";
        }
        void init()
        {
            par.Width = 40;
            par.Thickness = 20;
            par.Length = 240;
        }
        public override bool CheckParamete()
        {
            if (!CheckParZero()) return false;
            return true;
        }

     
        public override void CreateSub()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            ExtrudeFeature box = InventorTool.CreateBox(Definition, osketch, UsMM(par.Length), UsMM(par.Width), UsMM(par.Thickness));
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            box.Name = "RailSidePlate";
            List<Face> sideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            MateiMateDefinition mateA = Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0);
            mateA.Name = "mateA";
            // Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[0], 0).Name="flushA";
            Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[1], 0).Name = "flushB";
        }
    }
}
