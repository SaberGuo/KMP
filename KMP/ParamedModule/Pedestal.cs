using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
using Infranstructure.Tool;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Pedestal : ParamedModuleBase
    {
        ParPedestal parPedestal = new ParPedestal();
        [ImportingConstructor]
        public Pedestal():base()
        {
            this.Parameter = this.parPedestal;
        }
        void init()
        {
            parPedestal.InRadius = 100;
            parPedestal.Thickness = 2;
            parPedestal.PanelThickness = 3;
            parPedestal.UnderBoardingAngle = 120;
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
         SketchCircle inCircle= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, parPedestal.InRadius);
          SketchArc outArc=  osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.Origin, parPedestal.InRadius + parPedestal.Thickness,
                Math.PI, Math.PI);
           SketchArc underBoardArc= osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.Origin, parPedestal.InRadius + parPedestal.Thickness + parPedestal.PanelThickness,
               Math.PI *1.5 - parPedestal.UnderBoardingAngle / 360*Math.PI, parPedestal.UnderBoardingAngle / 180 * Math.PI);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)inCircle, (SketchEntity)outArc);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)inCircle, (SketchEntity)underBoardArc);
            osketch.DimensionConstraints.AddTangentDistance((SketchEntity)inCircle, (SketchEntity)outArc, inCircle., outArc.EndSketchPoint.Geometry, outArc.EndSketchPoint.Geometry, true);
        }
    }
}
