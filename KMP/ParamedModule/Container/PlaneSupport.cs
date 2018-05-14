using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
namespace ParamedModule.Container
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlaneSupport : PartModulebase
    {
      internal  ParPlaneSupport par = new ParPlaneSupport();
        [ImportingConstructor]
        public PlaneSupport():base()
        {
            this.Parameter = par;
            init();
        }
        public override bool CheckParamete()
        {
            if (!CommonTool.CheckParameterValue(par)) return false;
            return true;
        }
        void init()
        {
            par.InRadius.Value = 1400;
            par.BrachHeight1 = 100;
            par.BrachHeight2 = 200;
            par.BrachRadius1 = 100;
            par.BrachRadius2 = 50;
            par.Offset = 100;
            par.TopBoardThickness = 30;
            par.TopBoardWidth = 300;
        }
        public override void CreateModule()
        {
            CreateDoc();
            SketchArc arc;
            SketchLine line1, line2;
            List<SketchLine> lines1, lines2;
            CreateDownCyling(UsMM(par.InRadius.Value), UsMM(par.BrachRadius1), UsMM(par.BrachRadius2),
                UsMM(par.BrachHeight1), UsMM(par.BrachHeight2),UsMM(par.Offset),out arc,
                out line1,out line2,out lines1,out lines2);
         RevolveFeature UpCyling=   CreateUpCyling(lines2);
            CreateTopBox(UpCyling,UsMM(par.TopBoardWidth),UsMM(par.TopBoardWidth),UsMM(par.TopBoardThickness));
            CreateClear(arc, line1, line2);
         
        }
        void CreateDownCyling(double inRadius,double braceRadius1,double braceRadius2,double braceLength1,
            double braceLength2,double offset,out SketchArc arc,out SketchLine line1,out SketchLine line2,
           out List<SketchLine> lines1,out List<SketchLine> lines2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
           arc=  osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.Origin, inRadius, -Math.PI/2, Math.PI / 2);
           line1=  osketch.SketchLines.AddByTwoPoints(arc.CenterSketchPoint, arc.StartSketchPoint);
            line1.Construction = true;
             line2=  osketch.SketchLines.AddByTwoPoints(arc.CenterSketchPoint, arc.EndSketchPoint);
            line2.Construction = true;
            osketch.GeometricConstraints.AddPerpendicular((SketchEntity)line1, (SketchEntity)line2);
            osketch.GeometricConstraints.AddVertical((SketchEntity)line1);
            //SketchEntitiesEnumerator entities1= InventorTool.CreateRangle(osketch, braceLength1, braceRadius1);
            //List<SketchLine> lines1 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities1.GetEnumerator());
            SketchEntitiesEnumerator entities1 = osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.Origin, InventorTool.CreatePoint2d(1000, 2000));
            lines1 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities1.GetEnumerator());
            osketch.DimensionConstraints.AddTwoPointDistance(lines1[1].StartSketchPoint, lines1[1].EndSketchPoint,
                DimensionOrientationEnum.kAlignedDim, lines1[1].EndSketchPoint.Geometry).Parameter.Value=braceRadius1;
         
            SketchEntitiesEnumerator entities2=  InventorTool.CreateRangle(osketch, braceLength2, braceRadius2);
             lines2 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities2.GetEnumerator());
            osketch.GeometricConstraints.AddCollinear((SketchEntity)lines1[3], (SketchEntity)lines2[1]);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines1[3].EndSketchPoint, (SketchEntity)lines2[0]);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            foreach (var item in lines1)
            {
                objc.Add(item);
            }
            foreach (var item in lines2)
            {
                objc.Add(item);
            }
            osketch.MoveSketchObjects(objc, InventorTool.TranGeo.CreateVector2d(inRadius, -inRadius));

           
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines1[1].EndSketchPoint, (SketchEntity)arc);
            InventorTool.AddTwoPointDistance(osketch, arc.CenterSketchPoint, lines1[0].EndSketchPoint, 0, DimensionOrientationEnum.kVerticalDim)
                .Parameter.Value = offset;

            SketchPoint p = osketch.SketchPoints.Add(InventorTool.CreatePoint2d(300,-300));
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines1[0], (SketchEntity)p);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)arc, (SketchEntity)p);
            osketch.DimensionConstraints.AddTwoPointDistance(p, lines1[0].StartSketchPoint, DimensionOrientationEnum.kAlignedDim, p.Geometry)
                .Parameter.Value = braceLength1;
            Profile pro = osketch.Profiles.AddForSolid();
            RevolveFeature revolve = Definition.Features.RevolveFeatures.AddFull(pro, lines1[0], PartFeatureOperationEnum.kNewBodyOperation);
         
        }
        RevolveFeature CreateUpCyling(List<SketchLine> lines)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            List<SketchLine> results = new List<SketchLine>();
            foreach (var item in lines)
            {
              results.Add( (SketchLine) osketch.AddByProjectingEntity(item));
            }
            Profile pro = osketch.Profiles.AddForSolid();
          return  Definition.Features.RevolveFeatures.AddFull(pro, results[0], PartFeatureOperationEnum.kJoinOperation);
        }
        void CreateClear(SketchArc arc,SketchLine line1,SketchLine line2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchArc arcP=(SketchArc) osketch.AddByProjectingEntity(arc);
           SketchLine lineP1=(SketchLine) osketch.AddByProjectingEntity(line1);
            lineP1.Construction = false;
          SketchLine lineP2=(SketchLine)  osketch.AddByProjectingEntity(line2);
            lineP2.Construction = false;
            Profile pro = osketch.Profiles.AddForSolid();
            Definition.Features.RevolveFeatures.AddFull(pro, lineP2, PartFeatureOperationEnum.kIntersectOperation);
        }
        void CreateTopBox(RevolveFeature upCyling,double wide,double length,double height)
        {
            List<Face> SFS = InventorTool.GetCollectionFromIEnumerator<Face>(upCyling.SideFaces.GetEnumerator());
           Face face= SFS.Where(a => a.SurfaceType == SurfaceTypeEnum.kPlaneSurface).FirstOrDefault();
            if (face == null) return;
            PlanarSketch osketch = Definition.Sketches.Add(face, true);
            SketchCircle circle = InventorTool.GetFirstFromIEnumerator<SketchCircle>(osketch.SketchCircles.GetEnumerator());
            circle.Construction = true;
            //  InventorTool.CreateBox(Definition, osketch, length, wide, height);
           osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(-wide / 2,- length / 2), InventorTool.CreatePoint2d(wide / 2, length / 2));
            //InventorTool.AddTwoPointDistance(osketch,circle.CenterSketchPoint,)
            Profile pro = osketch.Profiles.AddForSolid();
        
            ExtrudeDefinition boxDef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            boxDef.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(boxDef).Name="PlaneSupTop";
          


        }
    }
}
