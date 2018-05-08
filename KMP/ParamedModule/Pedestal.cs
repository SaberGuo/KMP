using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
using Infranstructure.Tool;
namespace ParamedModule
{
    public class Pedestal : ParamedModuleBase
    {
        ParPedestal parPedestal = new ParPedestal();
        public Pedestal():base()
        {
            this.Parameter = this.parPedestal;
            this.ModelName = "Pedestal";
        }
        void init()
        {

        }
        PartDocument part;
        PartComponentDefinition partDef;
        public override void CreateModule(ParameterBase Parameter)
        {
            parPedestal = Parameter as ParPedestal;
            if (parPedestal == null) return;
            init();
             part = InventorTool.CreatePart();
             partDef = part.ComponentDefinition;
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
        }
    }
}
