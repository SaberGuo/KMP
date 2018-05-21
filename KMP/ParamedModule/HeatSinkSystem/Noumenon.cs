using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.HeatSinkSystem;
using Inventor;
using Infranstructure.Tool;
using System.ComponentModel.Composition;
using KMP.Interface;
namespace ParamedModule.HeatSinkSystem
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Noumenon : PartModulebase
    {
        internal ParNoumenon par = new ParNoumenon();
        public Noumenon() : base()
        {
            this.Parameter = par;
            init();
        }
        void init()
        {
            par.InRadius.Value = 1000;
            par.Thickness.Value = 24;
            par.Length = 4000;
            par.PipeDistance = 200;
            par.PipeOffset = 700;
            par.PipeDiameter = 200;
            par.PipeThickness = 10;
            par.PipeLength = 3000;
            par.PipeSurLength = 20;
            par.PipeSurDistance = 100;
            par.PipeSurCurveRadius = 40;
            par.PipeSurThickness = 2;
            par.PipeSurDiameter = 10;
            par.PipeSurNum = 10;
            par.TBrachHeight = 8;
            par.TBrachWidth = 5;
            par.TTopHeight = 3;
            par.TTopWidth = 12;
            par.THoopOffset = 100;
            par.THoopNumber = 5;
        }

        private void Parameter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule()
        {
            SketchCircle inCircle, outCircle,pipeOutCircle; ///罐内外草图圆、罐外圆草图
            ExtrudeFeature cylinder = CreateCylinder(UsMM(par.InRadius.Value),UsMM(par.Thickness.Value),UsMM(par.Length),out inCircle,out outCircle);
            Face CStartFace = InventorTool.GetFirstFromIEnumerator<Face>(cylinder.StartFaces.GetEnumerator());
            Face CEndFace= InventorTool.GetFirstFromIEnumerator<Face>(cylinder.EndFaces.GetEnumerator());
            List<Face> CSideFace= InventorTool.GetCollectionFromIEnumerator<Face>(cylinder.SideFaces.GetEnumerator());
            WorkAxis axis = Definition.WorkAxes.AddByRevolvedFace(CSideFace[0]);
          ExtrudeFeature pipe=  CreatePipe(CStartFace, inCircle, UsMM(par.PipeLength), UsMM(par.PipeDiameter/2), UsMM(par.PipeThickness), UsMM(par.PipeDistance), UsMM(par.PipeOffset),out pipeOutCircle);
            Face pipeStartFace = InventorTool.GetFirstFromIEnumerator<Face>(pipe.StartFaces.GetEnumerator());
            SweepFeature pipeSur = CreatePipeSurp(CSideFace[0],pipeStartFace, inCircle, pipeOutCircle,UsMM( par.PipeSurDistance), UsMM(par.PipeSurCurveRadius), UsMM(par.PipeSurLength), UsMM(par.PipeSurDiameter/2), UsMM(par.PipeSurThickness));
            CreatePipeSurMid(pipe,pipeSur, axis, UsMM(par.PipeLength), par.PipeSurNum, UsMM(par.PipeSurDistance), Definition.WorkPlanes[3],Definition.WorkPlanes[2]);

            CreateEndLong(CSideFace[1],axis, UsMM(par.TBrachWidth), UsMM(par.TBrachHeight), UsMM(par.TTopWidth), UsMM(par.TTopHeight), UsMM(par.THoopOffset),par.THoopNumber,UsMM(par.Length));
        }
        #region
        ExtrudeFeature CreateCylinder(double radius,double thickness,double length,out SketchCircle inCircle,out SketchCircle outCircle)
        {
            CreateDoc();
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            inCircle= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, radius);
           AddDiameter(osketch, (SketchEntity)inCircle, radius * 2);
             outCircle = osketch.SketchCircles.AddByCenterRadius(inCircle.CenterSketchPoint, radius + thickness);
            Profile pro = osketch.Profiles.AddForSolid();
       
           ExtrudeDefinition def= Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
          return  Definition.Features.ExtrudeFeatures.Add(def);
        }
        ExtrudeFeature CreatePipe(Face CylinderStartFace,SketchCircle inCircle,double pipeLength,double pipeRadius,double pipeThickness,double distance,double offset,out SketchCircle pipeOutCirce)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(CylinderStartFace, -distance, true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle BigCircle = (SketchCircle)osketch.AddByProjectingEntity(inCircle);
            BigCircle.Construction = true;
            Point2d temp = BigCircle.CenterSketchPoint.Geometry;
            Point2d center = InventorTool.CreatePoint2d(temp.X, temp.Y-offset);
          
          SketchCircle pipeCircle=  osketch.SketchCircles.AddByCenterRadius(center, pipeRadius);
            InventorTool.AddTwoPointDistance(osketch, BigCircle.CenterSketchPoint, pipeCircle.CenterSketchPoint,0,DimensionOrientationEnum.kAlignedDim).Parameter.Value=offset;
            pipeOutCirce = osketch.SketchCircles.AddByCenterRadius(pipeCircle.CenterSketchPoint, pipeRadius + pipeThickness);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {

                foreach (ProfileEntity sub in item)
                {
                    if(sub.SketchEntity!=pipeCircle)
                    {
                        item.AddsMaterial = true;
                        break;
                    }
                    else
                    {
                        item.AddsMaterial = false;
                    }
                }
            }
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(pipeLength, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        SweepFeature CreatePipeSurp(Face CSideFace,Face pipeStartFace,SketchCircle cylinderInCircle,SketchCircle pipeOutCircle,double pipeSurHDistance,
            double pipeSurHRaidus,double pipeSurHLength,double pipeSurRadius,double pipeSurThickness)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(pipeStartFace, -pipeSurHDistance, true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle BigCircle = (SketchCircle)osketch.AddByProjectingEntity(cylinderInCircle);
            SketchCircle pipeCircle = (SketchCircle)osketch.AddByProjectingEntity(pipeOutCircle);
            Point2d center = pipeCircle.CenterSketchPoint.Geometry;
            Point2d lineStart = InventorTool.CreatePoint2d(center.X-pipeCircle.Radius, center.Y);
            Point2d lineEnd = InventorTool.CreatePoint2d(lineStart.X - pipeSurHLength, center.Y);

            SketchLine line = osketch.SketchLines.AddByTwoPoints(lineStart, lineEnd);
            Point2d arcCente = InventorTool.CreatePoint2d(lineEnd.X, lineEnd.Y - pipeSurHRaidus);
            SketchArc arc = osketch.SketchArcs.AddByCenterStartSweepAngle(arcCente, pipeSurHRaidus, Math.PI / 2, Math.PI/2);
            Point2d VlineEnd = InventorTool.CreatePoint2d(arc.EndSketchPoint.Geometry.X, arc.EndSketchPoint.Geometry.Y-1);
            SketchLine VLine = osketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, VlineEnd);
            InventorTool.CreateTwoPointCoinCident(osketch, arc, line);
            //InventorTool.CreateTwoPointCoinCident(osketch, arc, VLine);
            osketch.GeometricConstraints.AddVertical((SketchEntity)VLine);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)VLine.EndSketchPoint, (SketchEntity)BigCircle);
            //SketchPoint SurCenter = osketch.SketchPoints.Add(InventorTool.Origin);
            //osketch.GeometricConstraints.AddCoincident((SketchEntity)SurCenter, (SketchEntity)BigCircle);
            //osketch.GeometricConstraints.AddCoincident((SketchEntity)SurCenter, (SketchEntity)BigCircle);
            WorkPlane PipePlane = Definition.WorkPlanes.AddByPointAndTangent(VLine.EndSketchPoint, CSideFace, true);
            PlanarSketch pipeSketch = Definition.Sketches.Add(PipePlane);
            SketchPoint pipeCenter = (SketchPoint)pipeSketch.AddByProjectingEntity(VLine.EndSketchPoint);
            pipeSketch.SketchCircles.AddByCenterRadius(pipeCenter, pipeSurRadius);
            pipeSketch.SketchCircles.AddByCenterRadius(pipeCenter, pipeSurRadius + pipeSurThickness);
            Profile pipePro = pipeSketch.Profiles.AddForSolid();
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(line);
            objc.Add(arc);
            objc.Add(VLine);
            Path pPath= Definition.Features.CreatePath(line);
          SweepDefinition def=  Definition.Features.SweepFeatures.CreateSweepDefinition(SweepTypeEnum.kPathSweepType, pipePro,pPath, PartFeatureOperationEnum.kJoinOperation);
        return    Definition.Features.SweepFeatures.Add(def);
        }
        void CreatePipeSurMid(ExtrudeFeature pipe,SweepFeature pipeSur,WorkAxis axis,double pipeLength,int pipeSurNum,double pipeSurDistance,WorkPlane pipeSurMirPlane,WorkPlane pipeMirPlane)
        {
            double temp = (pipeLength - pipeSurDistance * 2) / (pipeSurNum - 1);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(pipeSur);
          RectangularPatternFeatureDefinition def=  Definition.Features.RectangularPatternFeatures.CreateDefinition(objc, axis, true, pipeSurNum, temp);
          RectangularPatternFeature feature=  Definition.Features.RectangularPatternFeatures.AddByDefinition(def);
            ObjectCollection MirObj = InventorTool.CreateObjectCollection();
            MirObj.Add(pipeSur);
            MirObj.Add(feature);
          MirrorFeatureDefinition mirr=  Definition.Features.MirrorFeatures.CreateDefinition(MirObj, pipeSurMirPlane,PatternComputeTypeEnum.kAdjustToModelCompute);
          MirrorFeature pipeSurs=  Definition.Features.MirrorFeatures.AddByDefinition(mirr);
            ObjectCollection pipeMirObj = InventorTool.CreateObjectCollection();
            pipeMirObj.Add(pipeSur);
            pipeMirObj.Add(feature);
            pipeMirObj.Add(pipeSurs);
            pipeMirObj.Add(pipe);

            MirrorFeatureDefinition PipeDef = Definition.Features.MirrorFeatures.CreateDefinition(pipeMirObj, pipeMirPlane,PatternComputeTypeEnum.kAdjustToModelCompute);
            Definition.Features.MirrorFeatures.AddByDefinition(PipeDef);
        }
        #endregion
        void CreateEndLong(Face COutSF, WorkAxis axis, double braceWidth, double braceHeight, double topWidth, double topHeight,
            double TOffset, int THoopNum, double CLength)
        {
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(COutSF.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2],true);
          SketchLine line1=(SketchLine)  osketch.AddByProjectingEntity(edges[0]);
            SketchLine line2 = (SketchLine)osketch.AddByProjectingEntity(edges[1]);
            SketchLine line3 = osketch.SketchLines.AddByTwoPoints(line1.EndSketchPoint, line2.StartSketchPoint);
          List<SketchLine> lines=  CreateTSteel(osketch, braceWidth, braceHeight, topWidth, topHeight);
            osketch.GeometricConstraints.AddCollinear((SketchEntity)line3, (SketchEntity)lines[2]);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            foreach (var item in lines)
            {
                objc.Add(item);
            }
            osketch.MoveSketchObjects(objc, InventorTool.TranGeo.CreateVector2d(-100, -100));
            InventorTool.AddTwoPointDistance(osketch, line3.EndSketchPoint, lines[2].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = TOffset;
            Profile pro = osketch.Profiles.AddForSolid();
            RevolveFeature T = Definition.Features.RevolveFeatures.AddFull(pro, axis, PartFeatureOperationEnum.kNewBodyOperation);
            ObjectCollection RectObjc = InventorTool.CreateObjectCollection();
            RectObjc.Add(T);
          RectangularPatternFeatureDefinition def=  Definition.Features.RectangularPatternFeatures.CreateDefinition(RectObjc, axis, true, THoopNum,(CLength - TOffset * 2) / (THoopNum - 1));
            Definition.Features.RectangularPatternFeatures.AddByDefinition(def);
        }
        List<SketchLine> CreateTSteel(PlanarSketch osketch,double braceWidth,double braceHeight,double topWidth,double topHeight)
        {
          SketchEntitiesEnumerator entitys1=  InventorTool.CreateRangle(osketch, braceWidth, braceHeight);
            SketchEntitiesEnumerator entitys2 = InventorTool.CreateRangle(osketch, topWidth, topHeight);
            List<SketchLine> lines1 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entitys1.GetEnumerator());
            List<SketchLine> lines2 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entitys2.GetEnumerator());
            osketch.GeometricConstraints.AddCollinear((SketchEntity)lines1[0], (SketchEntity)lines2[2]);
            InventorTool.AddTwoPointDistance(osketch, lines1[0].StartSketchPoint, lines2[2].EndSketchPoint,0,DimensionOrientationEnum.kAlignedDim ).Parameter.Value= (topWidth - braceWidth) / 2;
          lines1.AddRange(lines2);
            return lines1;
        }
    }
}
