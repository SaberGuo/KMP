using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Other;
namespace ParamedModule.Other
{
    [Export("CoolVAC", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CoolVAC : PartModulebase
    {
        ParCoolVAC par = new ParCoolVAC();
        [ImportingConstructor]
        public CoolVAC() : base()
        {
            this.Name = "低温泵";
            par.Flanch.C = 6.2;
            par.Flanch.D = 6;
            par.Flanch.D0 = 320;
            par.Flanch.D1 = 350;
            par.Flanch.D2 = 300;
            par.Flanch.D6 = 290;
            par.Flanch.DN = 250;
            par.Flanch.H = 10;
            par.Flanch.N = 12;
            par.Flanch.X = 6;
            par.Height = 310;
            par.TotolHeight = 560;
            this.Parameter = par;
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            SketchCircle InCircle, rollCircle;
            ExtrudeFeature Cy = CreateCy(out InCircle);
            Face CyStartFace = InventorTool.GetFirstFromIEnumerator<Face>(Cy.EndFaces.GetEnumerator());
            Face CyEndFace = InventorTool.GetFirstFromIEnumerator<Face>(Cy.StartFaces.GetEnumerator());

            ExtrudeFeature roll = CreateRoll(CyEndFace, out rollCircle);

            RevolveFeature cat = CreateCat();

            ExtrudeFeature flanch = CreateFlance(CyStartFace, InCircle, par.Flanch.D1 / 2, par.Flanch.H);
            Face flanchEndFace = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
            CreateFlanceGroove(flanchEndFace, InCircle, par.Flanch.D2 / 2);
            CreateFlanceScrew(flanchEndFace, InCircle, par.Flanch.N, par.Flanch.D / 2, par.Flanch.D0 / 2, par.Flanch.H);
            CreateSur(Cy);
            Face rollEF = InventorTool.GetFirstFromIEnumerator<Face>(roll.EndFaces.GetEnumerator());
            CreateMote(rollCircle, rollEF);

        }

        private void CreateMote(SketchCircle rollCircle, Face rollEF)
        {
            PlanarSketch osketch = Definition.Sketches.Add(rollEF);
            SketchCircle cir = (SketchCircle)osketch.AddByProjectingEntity(rollCircle);
            osketch.SketchCircles.AddByCenterRadius(cir.CenterSketchPoint, cir.Radius + 10);
            cir.Construction = true;
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(40, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);
        }

        private ExtrudeFeature CreateRoll(Face CyEndFace,out SketchCircle cir)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(CyEndFace, par.Flanch.D6 / 4, true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            cir= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, 5);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(20, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           return Definition.Features.ExtrudeFeatures.Add(def);
        }
        #region
        private ExtrudeFeature CreateCy(out SketchCircle cir2)
        {
            double cyHeigh = par.Height - (par.Flanch.D6/2+par.Flanch.H) / 2;
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            SketchCircle cir1 = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, par.Flanch.H + par.Flanch.D6 / 2);
            cir2= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, par.Flanch.D6 / 2);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1)
                    item.AddsMaterial = true;
                else
                    item.AddsMaterial = false;
            }
            ExtrudeDefinition extruedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extruedef.SetDistanceExtent(cyHeigh, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrude = Definition.Features.ExtrudeFeatures.Add(extruedef);
            return extrude;
        }

        private RevolveFeature CreateCat()
        {
            PlanarSketch sketch1 = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            SketchEllipticalArc arc1 = sketch1.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Down, par.Flanch.D6 / 2 + par.Flanch.H, par.Flanch.D6 / 4 + par.Flanch.H, 0, Math.PI / 2);
            SketchEllipticalArc arc2 = sketch1.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Down, par.Flanch.D6 / 2 , par.Flanch.D6 / 4 , 0, Math.PI / 2);
            SketchLine line1 = sketch1.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc2.EndSketchPoint);
            sketch1.SketchLines.AddByTwoPoints(arc1.StartSketchPoint, arc2.StartSketchPoint);
            Profile pro1 = sketch1.Profiles.AddForSolid();
            RevolveFeature cat = Definition.Features.RevolveFeatures.AddFull(pro1, line1, PartFeatureOperationEnum.kJoinOperation);
            return cat;
        }
        private ExtrudeFeature CreateFlance(Face plane, SketchCircle topInCircle, double flachOutRadius, double flachThickness)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle circle = (SketchCircle)osketch.AddByProjectingEntity(topInCircle);
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
       
        #endregion
        private ExtrudeFeature CreateSur(ExtrudeFeature cy)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
           List<Face> CySFs= InventorTool.GetCollectionFromIEnumerator<Face>(cy.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(CySFs[1], true);
            List<Edge> CyEdges = InventorTool.GetCollectionFromIEnumerator<Edge>(CySFs[1].Edges.GetEnumerator());
           SketchLine CyLine=(SketchLine) osketch.AddByProjectingEntity(CyEdges[1]);
            Point2d p = CyLine.StartSketchPoint.Geometry;
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(CyLine.StartSketchPoint, InventorTool.CreatePoint2d(p.X-20, p.Y));
            SketchLine line2 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(p.X,p.Y-20), InventorTool.CreatePoint2d(p.X - 20, p.Y-20));
            SketchLine line3 = osketch.SketchLines.AddByTwoPoints(line1.EndSketchPoint, line2.EndSketchPoint);
            double x = par.TotolHeight - par.Height;
            double y = p.Y - x / 4;
            SketchLine line4 = osketch.SketchLines.AddByTwoPoints(CyLine.StartSketchPoint, InventorTool.CreatePoint2d(x, y));
            SketchLine line5 = osketch.SketchLines.AddByTwoPoints(line2.StartSketchPoint, InventorTool.CreatePoint2d(x-20, y-20));
            osketch.GeometricConstraints.AddParallel((SketchEntity)line4, (SketchEntity)line5);
            Point2d p4 = line4.EndSketchPoint.Geometry;
            Point2d p5 = line5.EndSketchPoint.Geometry;
            SketchLine line6 = osketch.SketchLines.AddByTwoPoints(line4.EndSketchPoint, InventorTool.CreatePoint2d(p4.X, p4.Y-40));
            Point2d p6 = line6.EndSketchPoint.Geometry;
            SketchLine line7 = osketch.SketchLines.AddByTwoPoints(line5.EndSketchPoint, InventorTool.CreatePoint2d(p6.X-20, p6.Y));
           SketchLine line8=  osketch.SketchLines.AddByTwoPoints(line6.EndSketchPoint, line7.EndSketchPoint);
           
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)line8);
            osketch.GeometricConstraints.AddVertical((SketchEntity)line7);
            osketch.SketchArcs.AddByFillet((SketchEntity)line4, (SketchEntity)line6, 3, line4.StartSketchPoint.Geometry, line6.EndSketchPoint.Geometry);
            osketch.SketchArcs.AddByFillet((SketchEntity)line5, (SketchEntity)line7, 1, line5.StartSketchPoint.Geometry, line7.EndSketchPoint.Geometry);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(20, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);

            ExtrudeFeature Sur= Definition.Features.ExtrudeFeatures.Add(def);
            List<Face> SSFs = InventorTool.GetCollectionFromIEnumerator<Face>(Sur.SideFaces.GetEnumerator());
           // List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(SSFs[2].Edges.GetEnumerator());
           // List<Edge> edges1 = InventorTool.GetCollectionFromIEnumerator<Edge>(SSFs[1].Edges.GetEnumerator());
           // EdgeCollection deges = InventorTool.CreateEdgeCollection();
           // for (int i=0;i< edges.Count;i++)
           // {
           //     deges.Add(edges[i]);
           // }
           // for (int i = 0; i < edges1.Count; i++)
           // {
           //     deges.Add(edges1[i]);
           // }
           
           //// deges.Add(edges[3]);
           // Definition.Features.FilletFeatures.AddSimple(deges, 4);
                 
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(Sur);
            Definition.Features.CircularPatternFeatures.Add(objc, Axis, false, 3, Math.PI * 2, true, PatternComputeTypeEnum.kAdjustToModelCompute);
            return Sur;
        }
    }
}
