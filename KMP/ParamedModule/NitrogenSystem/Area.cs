using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
namespace ParamedModule.NitrogenSystem
{
   public  class Area:PartModulebase
    {
        public double Length = 1000;
        public double width = 0;
        public Area():base()
        {
            this.Name = "地面";
        }

        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
           ExtrudeFeature box;
            if(width>0)
            {
                box = InventorTool.CreateBox(Definition, osketch, Length, width, 10);
            }
            else
            {
                box = InventorTool.CreateBox(Definition, osketch, Length, Length / 2, 10);
            }
            box.Name = "Area";
        }
    }
}
