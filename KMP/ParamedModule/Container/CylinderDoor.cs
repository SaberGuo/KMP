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
        public CylinderDoor(PassedParameter InRadius,PassedParameter Thickness):base()
        {
            this.Name = "罐门";
            init();
            this.Parameter = par;
            par.InRadius = InRadius;
            par.Thickness = Thickness;

        }
        private void init()
        {
           
            par.DoorRadius = 700;
            par.FlanchWidth = 40;
            ParCylinderHole hole = new ParCylinderHole() { HoleRadius = 200, PipeLenght = 300, PipeThickness = 4 };
            ParFlanch flanch = new ParFlanch() { D6 = 400,D1=520,H=20,D2=450,D0=480,D=10,N=6};
            par.TopHole = hole;
            par.TopHole.ParFlanch = flanch;
        }
        public override void CreateModule()
        {
        
           
            CreateDoc();
            SketchEllipticalArc OutArc;
            RevolveFeature revolve = CreateDoor(UsMM(par.Thickness.Value),UsMM(par.InRadius.Value),UsMM(par.DoorRadius),out OutArc);
            List<Face> sideFace = InventorTool.GetCollectionFromIEnumerator<Face>(revolve.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(sideFace[4],true);
           
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateH";
            Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[3], 0).Name = "mateK";
            WorkPlane topHolePlane;
            Face topHoleFace;
            SketchCircle topInCircle;
            CreateTopHole(revolve,UsMM(par.TopHole.ParFlanch.D6/2),out topHolePlane,out topHoleFace,out topInCircle);
          ExtrudeFeature topHolePipe=  CreateTopHolePipe(topHolePlane, topHoleFace, UsMM(par.TopHole.PipeLenght), UsMM(par.TopHole.PipeThickness));
           Face topHoleEndFace= InventorTool.GetCollectionFromIEnumerator<Face>( topHolePipe.Faces.GetEnumerator()).Where(a=>a.SurfaceType==SurfaceTypeEnum.kPlaneSurface).FirstOrDefault();
           ExtrudeFeature flach= CreateTopFlance(topHoleEndFace, topInCircle, UsMM(par.TopHole.ParFlanch.D1 / 2), UsMM(par.TopHole.ParFlanch.H));
            Face flachEndFace = InventorTool.GetFirstFromIEnumerator<Face>(flach.EndFaces.GetEnumerator());
            CreateFlanceGroove(flachEndFace, topInCircle, UsMM(par.TopHole.ParFlanch.D2 / 2));
            CreateFlanceScrew(flachEndFace, topInCircle,par.TopHole.ParFlanch.N, UsMM(par.TopHole.ParFlanch.D / 2), UsMM(par.TopHole.ParFlanch.D0 / 2), UsMM(par.TopHole.ParFlanch.H));
            SaveDoc();
        }

        private RevolveFeature CreateDoor(double thickness,double inRadius,double doorRadius,out SketchEllipticalArc Arc2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchEllipticalArc Arc1 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, doorRadius, inRadius, 0, Math.PI / 2);
             Arc2 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, doorRadius + thickness, inRadius + thickness, 0, Math.PI / 2);
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
        private void CreateTopHole(RevolveFeature door,double holeRadius,out WorkPlane topHolePlane,out Face topHoleFace,out SketchCircle topSketchCircle)
        {
            List<Face> faces = InventorTool.GetCollectionFromIEnumerator<Face>(door.Faces.GetEnumerator());
           Face cylinderFace=  faces.Where(a => a.SurfaceType == SurfaceTypeEnum.kCylinderSurface).FirstOrDefault();
            Edge cylinderEdge = InventorTool.GetFirstFromIEnumerator<Edge>(cylinderFace.Edges.GetEnumerator());


            topHolePlane = Definition.WorkPlanes.AddByPlaneAndOffset(InventorTool.GetFirstFromIEnumerator<WorkPlane>(Definition.WorkPlanes.GetEnumerator()),-UsMM(par.DoorRadius+par.Thickness.Value),true);
            PlanarSketch osketch = Definition.Sketches.Add(topHolePlane);
          SketchCircle circle=(SketchCircle) osketch.AddByProjectingEntity(cylinderEdge);
            circle.Construction = true;
            topSketchCircle= osketch.SketchCircles.AddByCenterRadius(circle.CenterSketchPoint, holeRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
           ExtrudeFeature feature= Definition.Features.ExtrudeFeatures.Add(ex);
            topHoleFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.Faces.GetEnumerator());

        }
        private ExtrudeFeature CreateTopHolePipe(WorkPlane topHolePlane,Face topHoleFace,double pipeLenght,double thickness)
        {
            Edge edge = InventorTool.GetFirstFromIEnumerator<Edge>(topHoleFace.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(topHolePlane);
            SketchEntity circle = osketch.AddByProjectingEntity(edge);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(circle);
            osketch.OffsetSketchEntitiesUsingDistance(objc, thickness, false);
           // SketchCircle circle2 = osketch.SketchCircles.AddByCenterRadius(circle.CenterSketchPoint, outRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if(item.Count>1)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            ex.SetDistanceExtentTwo(pipeLenght);
         return   Definition.Features.ExtrudeFeatures.Add(ex);
        }
        private ExtrudeFeature CreateTopFlance(Face plane,SketchCircle topInCircle, double flachOutRadius, double flachThickness)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
          SketchCircle circle=(SketchCircle)osketch.AddByProjectingEntity(topInCircle);
            SketchCircle outCircle = osketch.SketchCircles.AddByCenterRadius(circle.CenterSketchPoint, flachOutRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            ex.SetDistanceExtent(flachThickness, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(ex);
        }
        /// <summary>
        /// 创建法兰凹面
        /// </summary>
        private void CreateFlanceGroove(Face plane, SketchCircle sketchInCircle, double outRadius)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle inCircle = (SketchCircle)osketch.AddByProjectingEntity(sketchInCircle);
            SketchCircle outCircle = osketch.SketchCircles.AddByCenterRadius(inCircle.CenterSketchPoint, outRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            //foreach (ProfilePath item in pro)
            //{
            //    foreach (var sub in item)
            //    {
            //        if(sub==outCircle)
            //        {
            //            item.AddsMaterial = true;
            //            break;
            //        }
            //        else
            //        {
            //            item.AddsMaterial = false;
            //        }
            //    }
            //}
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(1 + "mm", PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(ex);
            // List<SketchCircle> circles = new List<SketchCircle>();
            // foreach (Edge item in plane.Edges)
            // {
            //     circles.Add((SketchCircle)osketch.AddByProjectingEntity(item));
            // }
            //SketchCircle circles
        }
        /// <summary>
        /// 创建法兰螺丝孔
        /// </summary>
        /// <param name="plane">定位面</param>
        /// <param name="inCircle">中心点定位圆</param>
        /// <param name="screwNumber">螺丝数量</param>
        /// <param name="ScrewRadius">孔半径</param>
        /// <param name="arrangeRadius">排版半径</param>
        private void CreateFlanceScrew(Face plane, SketchCircle inCircle, double screwNumber, double ScrewRadius, double arrangeRadius, double flanchThickness)
        {
            double angle = 360 / screwNumber / 180 * Math.PI;
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle flanceInCircle = (SketchCircle)osketch.AddByProjectingEntity(inCircle);
            SketchCircle screwCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(0, arrangeRadius), ScrewRadius);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(screwCircle);
            for (int i = 1; i < screwNumber; i++)
            {
                osketch.RotateSketchObjects(objc, flanceInCircle.CenterSketchPoint.Geometry, angle * i, true);
            }
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(flanchThickness, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(ex);
        }
        public override bool CheckParamete()
        {
            if (!CommonTool.CheckParameterValue(par)) return false;
            if (par.FlanchWidth < par.Thickness.Value) return false;
            return true;
        }
        
    }
}
