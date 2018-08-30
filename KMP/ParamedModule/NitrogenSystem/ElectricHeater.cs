using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
using KMP.Interface.Model;
namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 电加热器
    /// </summary>
    [Export("ElectricHeater", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ElectricHeater : PartModulebase
    {
        public ParElectricHeater par = new ParElectricHeater();
        [ImportingConstructor]
        public ElectricHeater():base()
        {
           
            this.Name = "电加热器";
            init();
            this.Parameter = par;
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        void init()
        {
            par.Dimension = 300;
            par.Height = 1300;
            par.PipeLenght = 50;
            par.PipeThickness = 2;
            par.PositionDistance = 350;

            ParFlanch parflanch1 = new ParFlanch()
            {
                DN = 10,
                D6 = 12.2,
                D0 = 40,
                D1 = 55,
                D2 = 30,
                H = 8,
                C = 6.6,
                X = 0.6,
                D = 6,
                N = 4
            };
            par.ParFlanch = parflanch1;
            par.FlanchDN = 80;

        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {

            double shortDimen = UsMM(par.Dimension)*0.9 ;
            double height = UsMM(par.Height) - shortDimen;
            SketchEllipticalArc arc2;
            SketchLine line2;
           RevolveFeature tank= CreateTank(shortDimen, height, out arc2, out line2);
            CreateSur(shortDimen, arc2, line2);
            List<Face> tankSFs = InventorTool.GetCollectionFromIEnumerator<Face>(tank.Faces.GetEnumerator());
            // for(int i=0;i<tankSFs.Count;i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(tankSFs[i], 0).Name = "a" + i;
            //}
            CreateHole(tankSFs[0],1);
            CreateHole(tankSFs[0], -1);
        }

        private void CreateSur(double shortDimen, SketchEllipticalArc arc2, SketchLine line2)
        {
            PlanarSketch SupSketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            SketchEllipticalArc SurArc = (SketchEllipticalArc)SupSketch.AddByProjectingEntity(arc2);
            Point2d ArcEndPoint = SurArc.EndSketchPoint.Geometry;
            SketchLine SurLine1 = SupSketch.SketchLines.AddByTwoPoints(SurArc.EndSketchPoint, InventorTool.CreatePoint2d(ArcEndPoint.X, ArcEndPoint.Y - shortDimen));
            InventorTool.AddTwoPointDistance(SupSketch, SurLine1.StartSketchPoint, SurLine1.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = shortDimen;
            SketchLine SurLine2 = SupSketch.SketchLines.AddByTwoPoints(SurLine1.EndSketchPoint, InventorTool.CreatePoint2d(SurLine1.EndSketchPoint.Geometry.X - shortDimen / 10, SurLine1.EndSketchPoint.Geometry.Y));
            SketchLine SurLine3 = SupSketch.SketchLines.AddByTwoPoints(SurLine2.EndSketchPoint, InventorTool.CreatePoint2d(SurLine2.EndSketchPoint.Geometry.X, SurLine2.EndSketchPoint.Geometry.Y + 5));
            SupSketch.GeometricConstraints.AddPerpendicular((SketchEntity)SurLine1, (SketchEntity)SurLine2);
            SupSketch.GeometricConstraints.AddParallel((SketchEntity)SurLine1, (SketchEntity)SurLine3);
            SupSketch.GeometricConstraints.AddCoincident((SketchEntity)SurLine3.EndSketchPoint, (SketchEntity)SurArc);
            Profile SurPro = SupSketch.Profiles.AddForSolid();
            ExtrudeDefinition SurExtrude = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(SurPro, PartFeatureOperationEnum.kJoinOperation);
            SurExtrude.SetDistanceExtent(shortDimen / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            ExtrudeFeature SurExtrudeFeature = Definition.Features.ExtrudeFeatures.Add(SurExtrude);
            SurExtrudeFeature.Name = "Sur";
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(SurExtrudeFeature);
            WorkAxis axis = Definition.WorkAxes.AddByLine(line2, false);
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[2], 0);
            plane.Visible = false;
            plane.Name = "Flush";
            WorkPlane plane1 = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[1], 0);
            plane1.Visible = false;
            plane1.Name = "Mate";
            axis.Visible = false;
            axis.Name = "Axis";
            Definition.Features.CircularPatternFeatures.Add(objc, axis, false, 3, Math.PI * 2, true, PatternComputeTypeEnum.kAdjustToModelCompute);
        }

        private RevolveFeature CreateTank(double shortDimen, double height, out SketchEllipticalArc arc2, out SketchLine line2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            // SketchLine line=  osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(shortDimen / 2, height / 2), InventorTool.CreatePoint2d(shortDimen / 2, -height / 2));
            SketchEllipticalArc arc1 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, height / 2), InventorTool.Right, UsMM(par.Dimension) / 2, shortDimen / 2, 0, Math.PI / 2);
            arc2 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, -height / 2), InventorTool.Right, UsMM(par.Dimension) / 2, shortDimen / 2, 1.5 * Math.PI, Math.PI / 2);
            SketchEllipticalArc arc3 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, height / 2), InventorTool.Right, UsMM(par.Dimension) / 2 - 2, shortDimen / 2 - 2, 0, Math.PI / 2);
            SketchEllipticalArc arc4 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, -height / 2), InventorTool.Right, UsMM(par.Dimension) / 2 - 2, shortDimen / 2 - 2, 1.5 * Math.PI, Math.PI / 2);
            // SketchLine CenterLine = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc2.EndSketchPoint);
            //CenterLine.Construction = true;
            SketchLine line = osketch.SketchLines.AddByTwoPoints(arc1.StartSketchPoint, arc2.EndSketchPoint);
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(arc3.StartSketchPoint, arc4.EndSketchPoint);
            line2 = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc3.EndSketchPoint);
            SketchLine line3 = osketch.SketchLines.AddByTwoPoints(arc2.StartSketchPoint, arc4.StartSketchPoint);
            Profile pro = osketch.Profiles.AddForSolid();
          return  Definition.Features.RevolveFeatures.AddFull(pro, line2, PartFeatureOperationEnum.kNewBodyOperation);
        }
        private void CreateHole(Face TankFace,int Direct)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[3], Direct* UsMM(par.PositionDistance),true);
            PlanarSketch osketch = Definition.Sketches.Add(plane,true);
            osketch.Visible = false;
          SketchPoint p=  osketch.SketchPoints.Add(InventorTool.CreatePoint2d(Direct * UsMM(par.Dimension / 2), 0));
            WorkPlane plane1 = Definition.WorkPlanes.AddByPointAndTangent(p, TankFace,true);
            PlanarSketch Wsketch = Definition.Sketches.Add(plane1);
            SketchPoint Center = (SketchPoint)Wsketch.AddByProjectingEntity(p);
           SketchCircle cir=  Wsketch.SketchCircles.AddByCenterRadius(Center, UsMM(par.ParFlanch.D6 / 2));
            Profile pro = Wsketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(UsMM(par.Dimension / 2), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature hole = Definition.Features.ExtrudeFeatures.Add(def);
            ExtrudeFeature pipe= CreatePipe(plane1, cir);
            Face pipeFace = InventorTool.GetFirstFromIEnumerator<Face>(pipe.EndFaces.GetEnumerator());
           ExtrudeFeature flanch= InventorTool.CreateFlance(pipeFace, cir, UsMM(par.ParFlanch.D1/2), UsMM(par.ParFlanch.H), Definition);
            Face flanchFace = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
           ExtrudeFeature Groove= InventorTool.CreateFlanceGroove(flanchFace, cir, UsMM(par.ParFlanch.D2 / 2), Definition);
           ExtrudeFeature Screw= InventorTool.CreateFlanceScrew(flanchFace, cir, par.ParFlanch.N, UsMM(par.ParFlanch.C / 2), UsMM(par.ParFlanch.D0 / 2), UsMM(par.ParFlanch.H), Definition);
           // ObjectCollection objc = InventorTool.CreateObjectCollection();
           // objc.Add(hole);
           // objc.Add(pipe);
           // objc.Add(flanch);
           // objc.Add(Groove);
           // objc.Add(Screw);
           //MirrorFeature mirror= Definition.Features.MirrorFeatures.Add(objc, Definition.WorkPlanes[3],false,PatternComputeTypeEnum.kAdjustToModelCompute);
           // objc.Clear();
           // objc.Add(mirror);
           // Definition.Features.MirrorFeatures.Add(objc, Definition.WorkPlanes[1], true, PatternComputeTypeEnum.kAdjustToModelCompute);
        }
        private ExtrudeFeature CreatePipe(WorkPlane plane,SketchCircle cir)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle cir1 = (SketchCircle)osketch.AddByProjectingEntity(cir);
            SketchCircle cir2 = osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, cir1.Radius + UsMM(par.PipeThickness));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.PipeLenght), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            def.SetDistanceExtentTwo(UsMM(par.PipeLenght)/2);
            ExtrudeFeature pipe = Definition.Features.ExtrudeFeatures.Add(def);
            return pipe;
        }
    }
}
