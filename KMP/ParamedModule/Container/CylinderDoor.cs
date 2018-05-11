using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using KMP.Interface.Model.Container;
using Infranstructure.Tool;
using KMP.Interface;
using System.ComponentModel.Composition;
using Inventor;
namespace ParamedModule.Container
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CylinderDoor : PartModulebase
    {
        ParCylinderDoor par = new ParCylinderDoor();
        [ImportingConstructor]
        public CylinderDoor():base()
        {
            init();
            this.Parameter = par;
        }
        private void init()
        {
            par.InRadius = 1400;
            par.DoorRadius = 700;
            par.Thickness = 2;
            par.FlanchWidth = 4;
        }
        public override void CreateModule()
        {
        
           
            CreateDoc();
            RevolveFeature revolve = CreateDoor(UsMM(par.Thickness),UsMM(par.InRadius),UsMM(par.DoorRadius));
            List<Face> sideFace = InventorTool.GetCollectionFromIEnumerator<Face>(revolve.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(sideFace[4]);
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateH";
            Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[3], 0).Name = "mateK";
            SaveDoc();
        }

        private RevolveFeature CreateDoor(double thickness,double inRadius,double doorRadius)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchEllipticalArc Arc1 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, doorRadius, inRadius, 0, Math.PI / 2);
            SketchEllipticalArc Arc2 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, doorRadius + thickness, inRadius + thickness, 0, Math.PI / 2);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)Arc1, (SketchEntity)Arc2);
            SketchLine Line1 = osketch.SketchLines.AddByTwoPoints(Arc1.StartSketchPoint, Arc2.StartSketchPoint);
            SketchLine Line2 = osketch.SketchLines.AddByTwoPoints(Arc1.EndSketchPoint, Arc2.EndSketchPoint);

            osketch.GeometricConstraints.AddHorizontalAlign(Arc1.StartSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddVerticalAlign(Arc1.EndSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)Line1);
            osketch.GeometricConstraints.AddVertical((SketchEntity)Line2);
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, true, InventorTool.TranGeo.CreatePoint2d(-doorRadius / 2, 0));
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, false, InventorTool.TranGeo.CreatePoint2d(0, -inRadius / 2));
            Point2d p = InventorTool.TranGeo.CreatePoint2d((Line1.StartSketchPoint.Geometry.X + Line1.EndSketchPoint.Geometry.X) / 2 + 1, (Line1.StartSketchPoint.Geometry.Y + Line1.EndSketchPoint.Geometry.Y) / 2 + 1);
            osketch.DimensionConstraints.AddTwoPointDistance(Line1.StartSketchPoint, Line1.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);

            SketchEntitiesEnumerator entities = InventorTool.CreateRangle(osketch, thickness, thickness);
            SketchEntitiesEnumerator entities1 = InventorTool.CreateRangle(osketch, thickness,UsMM( par.FlanchWidth));
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            List<SketchLine> flanchLines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities1.GetEnumerator());

            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[3].EndSketchPoint, (SketchEntity)Line2);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[0], (SketchEntity)Line2.EndSketchPoint);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[1].EndSketchPoint, (SketchEntity)flanchLines[3]);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[2], (SketchEntity)flanchLines[3].StartSketchPoint);
            //ObjectCollection objc = InventorTool.CreateObjectCollection();
            // ObjectCollection flanchObjc = InventorTool.CreateObjectCollection();
            //lines.ForEach(a => objc.Add(a));
            //osketch.MoveSketchObjects(objc, lines[3].EndSketchPoint.Geometry.VectorTo(Line2.EndSketchPoint.Geometry));
            //flanchLines.ForEach(a => flanchObjc.Add(a));
            //osketch.MoveSketchObjects(flanchObjc, flanchLines[3].StartSketchPoint.Geometry.VectorTo(lines[2].StartSketchPoint.Geometry));
            Profile profile = osketch.Profiles.AddForSolid();
            RevolveFeature revolve = Definition.Features.RevolveFeatures.AddFull(profile, Line1, PartFeatureOperationEnum.kNewBodyOperation);
            return revolve;
        }

        public override bool CheckParamete()
        {
            if (!CommonTool.CheckParameterValue(par)) return false;
            return true;
        }
    }
}
