using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Infranstructure.Tool;
using KMP.Interface;
using System.ComponentModel.Composition;
using Inventor;
namespace ParamedModule
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CylinderDoor : ParamedModuleBase
    {
        ParCylinderDoor parCylineDoor = new ParCylinderDoor();
        [ImportingConstructor]
        public CylinderDoor():base()
        {
            this.Parameter = parCylineDoor;
        }
        private void init()
        {
            parCylineDoor.InRadius = 100;
            parCylineDoor.DoorRadius = 70;
            parCylineDoor.Thickness = 2;
        }
        public override void CreateModule(ParameterBase Parameter)
        {
            parCylineDoor = Parameter as ParCylinderDoor;
            if (parCylineDoor == null) return;
            init();
            PartDocument part = InventorTool.CreatePart();
            PartComponentDefinition partDef = part.ComponentDefinition;
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            SketchEllipticalArc Arc1 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, parCylineDoor.DoorRadius, parCylineDoor.InRadius,  0, Math.PI / 2);
            SketchEllipticalArc Arc2 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, parCylineDoor.DoorRadius + parCylineDoor.Thickness, parCylineDoor.InRadius+parCylineDoor.Thickness,  0, Math.PI / 2);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)Arc1, (SketchEntity)Arc2);
            SketchLine Line1 = osketch.SketchLines.AddByTwoPoints(Arc1.StartSketchPoint, Arc2.StartSketchPoint);
            SketchLine Line2 = osketch.SketchLines.AddByTwoPoints(Arc1.EndSketchPoint, Arc2.EndSketchPoint);

            osketch.GeometricConstraints.AddHorizontalAlign(Arc1.StartSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddVerticalAlign(Arc1.EndSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)Line1);
            osketch.GeometricConstraints.AddVertical((SketchEntity)Line2);
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, true, InventorTool.TranGeo.CreatePoint2d(-parCylineDoor.DoorRadius / 2, 0));
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, false, InventorTool.TranGeo.CreatePoint2d(0, -parCylineDoor.InRadius / 2));
           
            Point2d p = InventorTool.TranGeo.CreatePoint2d((Line1.StartSketchPoint.Geometry.X + Line1.EndSketchPoint.Geometry.X) / 2 + 1, (Line1.StartSketchPoint.Geometry.Y + Line1.EndSketchPoint.Geometry.Y) / 2+1);
            osketch.DimensionConstraints.AddTwoPointDistance(Line1.StartSketchPoint, Line1.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);
            Profile profile = osketch.Profiles.AddForSolid();
            RevolveFeature revolve = partDef.Features.RevolveFeatures.AddFull(profile, Line1, PartFeatureOperationEnum.kNewBodyOperation);
        }
    }
}
