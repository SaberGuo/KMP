using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Other;
using KMP.Interface.Model;
namespace ParamedModule.Other
{
    /// <summary>
    /// 分子泵
    /// </summary>
    [Export("MolecularPump", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MolecularPump : PartModulebase
    {
        ParMolecularPump par = new ParMolecularPump();
        public MolecularPump():base()
        {
            this.Parameter = par;
            this.Name = "分子泵";
            init();
        }
        private void init()
        {
            par.MAGW = 2200;
            double h = par.Molecular.H - par.Molecular.Flanch.H;
            length1 = h * length1 / length;
            length2 = h * length2 / length;
            length3 = h * length3 / length;
            length4 = h * length4 / length;
            length5 = h * length5 / length;
            length6 = h * length6 / length;
            length = h;
        }
        double length = 339;
        double length1=68;
        double length2=10;
        double length3=84;
        double length4=122;
        double length5=41;
        double length6=14;
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            SketchCircle cir=null;
          ExtrudeFeature cy1=  CreateCy(Definition.WorkPlanes[2],ref cir, UsMM(110 ), UsMM(length1));

            Face cy1EF = InventorTool.GetFirstFromIEnumerator<Face>(cy1.EndFaces.GetEnumerator());
            Face cy1StartF= InventorTool.GetFirstFromIEnumerator<Face>(cy1.StartFaces.GetEnumerator());
            CreateHoleCir(cy1StartF, cir, UsMM(66), UsMM(2));
            CreateScrew(Definition.WorkPlanes[3], cir, UsMM(110), UsMM(3),UsMM(6), true,(int)((length1-12)/9));
          ExtrudeFeature cut=  CreateCut(-90, cir, cir.Radius, UsMM(66 + 1), UsMM(length1 - 1));
            Face CutStartF = InventorTool.GetFirstFromIEnumerator<Face>(cut.StartFaces.GetEnumerator());
            CreateSidePart(CutStartF);

            ExtrudeFeature cy2 = CreateCy(cy1EF, ref cir, UsMM(123), UsMM(length2));
            Face cy2EF = InventorTool.GetFirstFromIEnumerator<Face>(cy2.EndFaces.GetEnumerator());
            ExtrudeFeature cy3 = CreateCy(cy2EF, ref cir, UsMM(130), UsMM(length3));
            Face cy3EF = InventorTool.GetFirstFromIEnumerator<Face>(cy3.EndFaces.GetEnumerator());
            CreateCut(-17, cir, cir.Radius, UsMM(123 + 1), UsMM(length3));
            CreateCut(17, cir, cir.Radius, UsMM(123 + 1), UsMM(length3));
            ExtrudeFeature cy4 = CreateCy(cy3EF, ref cir, UsMM(par.Molecular.D1/2), UsMM(length4));
            Face cy4EF = InventorTool.GetFirstFromIEnumerator<Face>(cy4.EndFaces.GetEnumerator());
            CreateScrew(Definition.WorkPlanes[3], cir, cir.Radius, UsMM(3), UsMM(length4-10), false, (int)((length4 - 16) / 9));
            LoftFeature cy5=  CreateLoft(cy4EF, cir, UsMM(par.Molecular.Flanch.D6/2), UsMM(length5));
            Face cy5EF = cy5.EndFace;
            ExtrudeFeature cy6 = CreateCy(cy5EF, ref cir, UsMM(par.Molecular.Flanch.D6 / 2), UsMM(length6));
            Face cy6EF = InventorTool.GetFirstFromIEnumerator<Face>(cy6.EndFaces.GetEnumerator());
            ParFlanch flanch = par.Molecular.Flanch;
          ExtrudeFeature flanchPart=  CreateFlance(cy6EF, cir, UsMM(flanch.D6/2), UsMM(flanch.D1/2), UsMM(flanch.H));
            Face flanchEF = InventorTool.GetFirstFromIEnumerator<Face>(flanchPart.EndFaces.GetEnumerator());
           CreateFlanceGroove(flanchEF, cir, UsMM(flanch.D6 / 2), UsMM(flanch.D2 / 2), Definition);
            InventorTool.CreateFlanceScrew(flanchEF, cir, flanch.N, UsMM(flanch.C/2), UsMM(flanch.D0 / 2), UsMM(flanch.H), Definition);
        }
        private ExtrudeFeature CreateCy(object face,ref SketchCircle cir,double radius,double length)
        {
            PlanarSketch osketch = Definition.Sketches.Add(face);
           cir=  DrawCir(osketch, cir, radius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private SketchCircle DrawCir(PlanarSketch osketch, SketchCircle cir,double radius)
        {
            if(cir!=null)
            {
                SketchCircle circle = (SketchCircle)osketch.AddByProjectingEntity(cir);
                circle.Construction = true;
                return osketch.SketchCircles.AddByCenterRadius(circle.CenterSketchPoint, radius);
            }
            else
            {
                return osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, radius);
            }
           
            
        }
        private LoftFeature CreateLoft(Face plane,SketchCircle cir,double radius,double distance)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
           SketchCircle cir1=(SketchCircle) osketch.AddByProjectingEntity(cir);
            Profile pro1 = osketch.Profiles.AddForSolid();
            WorkPlane plane1 = Definition.WorkPlanes.AddByPlaneAndOffset(plane, distance,true);
            PlanarSketch tsketch = Definition.Sketches.Add(plane1);
            SketchCircle cir2 = (SketchCircle)tsketch.AddByProjectingEntity(cir);
            cir2.Construction = true;
            SketchCircle cir3 = tsketch.SketchCircles.AddByCenterRadius(cir2.CenterSketchPoint, radius);
            Profile pro2 = tsketch.Profiles.AddForSolid();
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(pro1);
            objc.Add(pro2);
          LoftDefinition def=  Definition.Features.LoftFeatures.CreateLoftDefinition(objc, PartFeatureOperationEnum.kJoinOperation);
            return Definition.Features.LoftFeatures.Add(def);
            
        }
        private ExtrudeFeature CreateFlance(object plane,SketchCircle cir,double inRadius,double outRadius,double thinkness)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle cir1 = (SketchCircle)osketch.AddByProjectingEntity(cir);
            cir1.Construction = true;
            osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, inRadius);
            osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, outRadius);
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
            ex.SetDistanceExtent(thinkness, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(ex);
        }
        /// <summary>
        /// 创建半圆槽
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="cir"></param>
        /// <param name="distance"></param>
        /// <param name="radius"></param>
        /// <param name="FirstSpace"></param>
        /// <param name="direct"></param>
        /// <param name="num"></param>
        private void CreateScrew(object plane,SketchCircle cir,double distance,double radius,double FirstSpace,bool direct,int num)
        {
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, Definition.WorkAxes[1], true, true, cir.CenterSketchPoint);
           SketchEllipticalArc arc= osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d( distance, FirstSpace), InventorTool.Top, radius, radius,0,  Math.PI);
           osketch.SketchLines.AddByTwoPoints(arc.StartSketchPoint, arc.EndSketchPoint);
          SketchLine line=  osketch.SketchLines.AddByTwoPoints(InventorTool.Origin, InventorTool.CreatePoint2d(0, 10));
            Profile pro = osketch.Profiles.AddForSolid();
          RevolveFeature slot=  Definition.Features.RevolveFeatures.AddFull(pro, line, PartFeatureOperationEnum.kCutOperation);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(slot);
            Definition.Features.RectangularPatternFeatures.Add(objc, Definition.WorkAxes[2], direct, num, radius*3, PatternSpacingTypeEnum.kDefault, null, null, true, null, null, PatternSpacingTypeEnum.kDefault, null, PatternComputeTypeEnum.kAdjustToModelCompute);
        }
        private void CreateHoleCir(object face,  SketchCircle cir, double radius, double length)
        {
            PlanarSketch osketch = Definition.Sketches.Add(face);
            cir = DrawCir(osketch, cir, radius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateCut(double angle,SketchCircle cir,double radius,double offset,double height)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByLinePlaneAndAngle(Definition.WorkAxes[2], Definition.WorkPlanes[1], angle * Math.PI / 180,true);
            WorkPlane plane1 = Definition.WorkPlanes.AddByPlaneAndOffset(plane, offset,true);
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane1, Definition.WorkAxes[2], true, false, cir.CenterSketchPoint,true);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(-radius,0), InventorTool.CreatePoint2d(radius,height));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(radius, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private void  CreateSidePart(Face plane)
        {
            double width = Math.Pow(Math.Pow(110, 2) - Math.Pow(67, 2), 0.5) * 2;
          ExtrudeFeature ract1=  CreateRect(plane,UsMM(width),UsMM(length1), UsMM(9));
            Face ract1EF = InventorTool.GetFirstFromIEnumerator<Face>(ract1.EndFaces.GetEnumerator());
          ExtrudeFeature ract2=  CreateRect(ract1EF, UsMM(width - 29), UsMM(length1), UsMM(60));
            Face ract2EF = InventorTool.GetFirstFromIEnumerator<Face>(ract2.EndFaces.GetEnumerator());
          ExtrudeFeature ract3=  CreateRect(ract2EF, UsMM(width - 29), UsMM(134), UsMM(25));
            Face ract3EF = InventorTool.GetFirstFromIEnumerator<Face>(ract3.EndFaces.GetEnumerator());
            ExtrudeFeature ract4 = CreateRect(ract3EF, UsMM(width), UsMM(134), UsMM(9));
            Face ract4EF = InventorTool.GetFirstFromIEnumerator<Face>(ract4.EndFaces.GetEnumerator());
            ExtrudeFeature ract5 = CreateRect(ract4EF, UsMM(width+18), UsMM(260), UsMM(98));
            List<Face> ract5SF = InventorTool.GetCollectionFromIEnumerator<Face>(ract5.SideFaces.GetEnumerator());
            CreateRect(ract5SF[2], UsMM(50), UsMM(60), UsMM(27), UsMM(3), UsMM(57));
            CreateRect(ract5SF[2], UsMM(50), UsMM(60), UsMM(27), UsMM(3), UsMM(122));
        }
        private ExtrudeFeature CreateRect(Face plane,double width,double length,double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(-width / 2, 0), InventorTool.CreatePoint2d(width / 2, length));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateRect(Face plane, double width, double length, double height,double positionX,double postionY)
        {
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane,edges[1],false,true,edges[1].StopVertex);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(positionX, postionY), InventorTool.CreatePoint2d(positionX + width, postionY+length));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        /// <summary>
        /// 创建法兰凹面
        /// </summary>
        public static ExtrudeFeature CreateFlanceGroove(Face plane, SketchCircle sketchInCircle,double inRadius, double outRadius, PartComponentDefinition Definition)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle cir = (SketchCircle)osketch.AddByProjectingEntity(sketchInCircle);
            cir.Construction = true;
            osketch.SketchCircles.AddByCenterRadius(cir.CenterSketchPoint, inRadius);
            SketchCircle outCircle = osketch.SketchCircles.AddByCenterRadius(cir.CenterSketchPoint, outRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(1 + "mm", PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(ex);

        }
    }
}
