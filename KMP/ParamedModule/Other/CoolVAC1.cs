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
    /// 低温泵
    /// </summary>
    [Export("CoolVAC1", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CoolVAC1 : PartModulebase
    {
        public ParCoolVAC1 par = new ParCoolVAC1();
        [ImportingConstructor]
        public CoolVAC1() : base()
        {
            this.Name = "凹面低温泵";

            InitModule();
        }
        public override void InitModule()
        {
            this.Parameter = par;
            par.VacDN =1000;
            base.InitModule();
        }
        public override bool CheckParamete()
        {
            //if (par.VAC.TotolHeight <= par.VAC.Height)
            //{
            //    ParErrorChanged(this, "泵主体高度不能大于总高度！");
            //    return false;
            //}
            return true;
        }

        public override void CreateSub()
        {
            CreateConcave();

        }
  
        private ExtrudeFeature CreateCy(out SketchCircle cir2)
        {
            double cyHeigh = UsMM(par.VAC.Height - (par.VAC.Flanch.D6 / 2 + par.VAC.Flanch.H) / 2);
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            SketchCircle cir1 = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(par.VAC.Flanch.H + par.VAC.Flanch.D6 / 2));
            cir2 = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(par.VAC.Flanch.D6 / 2));
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1)
                    item.AddsMaterial = true;
                else
                    item.AddsMaterial = true;
            }
            ExtrudeDefinition extruedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extruedef.SetDistanceExtent(cyHeigh, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrude = Definition.Features.ExtrudeFeatures.Add(extruedef);
            return extrude;
        }

        private RevolveFeature CreateCat()
        {
            PlanarSketch sketch1 = Definition.Sketches.Add(Definition.WorkPlanes[2]);
           // SketchEllipticalArc arc1 = sketch1.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Down, par.VAC.Flanch.D6 / 2 + par.VAC.Flanch.H, par.VAC.Flanch.D6 / 4 + par.VAC.Flanch.H, Math.PI*3 / 2,Math.PI/2);
            SketchEllipticalArc arc2 = sketch1.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Down, UsMM(par.VAC.Flanch.D6 / 2),UsMM( par.VAC.Flanch.D6 / 4) , Math.PI * 3 / 2, Math.PI / 2);
            SketchLine line1 = sketch1.SketchLines.AddByTwoPoints(arc2.CenterSketchPoint, arc2.EndSketchPoint);
            SketchLine line2 = sketch1.SketchLines.AddByTwoPoints(arc2.CenterSketchPoint, arc2.StartSketchPoint);

            Profile pro1 = sketch1.Profiles.AddForSolid();
            RevolveFeature cat = Definition.Features.RevolveFeatures.AddFull(pro1, line2, PartFeatureOperationEnum.kCutOperation);
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
            for (int i = 0; i < screwNumber; i++)
            {
                osketch.RotateSketchObjects(objc, flanceInCircle.CenterSketchPoint.Geometry, angle * i, true);
            }
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(flanchThickness, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(ex);
        }
        /// <summary>
        /// 创建进出液氮法兰
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="offset"></param>
        /// <param name="flanch"></param>
        /// <param name="plane"></param>
        private void CreateTail(double x,double y,double offset,ParFlanch flanch,WorkPlane plane)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle cir1 = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(x, y), UsMM(flanch.D6 / 2));
            SketchCircle cir2 = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(x, y), UsMM(flanch.D6 / 2+2));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(offset, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature cy = Definition.Features.ExtrudeFeatures.Add(def);
            Face EF = InventorTool.GetFirstFromIEnumerator<Face>(cy.EndFaces.GetEnumerator());
          ExtrudeFeature extrude=  CreateFlance(EF, cir1, UsMM(flanch.D1 / 2), UsMM(flanch.H));
            Face flanchEndFace = InventorTool.GetFirstFromIEnumerator<Face>(extrude.EndFaces.GetEnumerator());
            CreateFlanceGroove(flanchEndFace, cir1, UsMM(flanch.D2 / 2));
            CreateFlanceScrew(flanchEndFace, cir1, flanch.N, UsMM(flanch.D / 2), UsMM(flanch.D0 / 2), UsMM(flanch.H));
        }
       private void CreateCenter(WorkPlane plane,SketchCircle cir)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
           SketchCircle cir1=(SketchCircle) osketch.AddByProjectingEntity(cir);
            cir1.Construction = true;
          osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, 3);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(10, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature cy = Definition.Features.ExtrudeFeatures.Add(def);


            Face EF = InventorTool.GetFirstFromIEnumerator<Face>(cy.EndFaces.GetEnumerator());
            Point2d center = cir.CenterSketchPoint.Geometry;
            Point2d p1 = InventorTool.CreatePoint2d(center.X-3, center.Y-3);
            Point2d p2 = InventorTool.CreatePoint2d(center.X + 3, center.Y + 3);
            PlanarSketch tsketch = Definition.Sketches.Add(EF);
            tsketch.SketchLines.AddAsTwoPointRectangle(p1, p2);
            Profile tpro = tsketch.Profiles.AddForSolid();
            ExtrudeDefinition tdef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(tpro, PartFeatureOperationEnum.kJoinOperation);
            tdef.SetDistanceExtent(10, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature box = Definition.Features.ExtrudeFeatures.Add(tdef);

            List<Face> faces = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            PlanarSketch hsktch = Definition.Sketches.Add(faces[0]);
          
            hsktch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(1.5, 3), 1.5);
            Profile hpro = hsktch.Profiles.AddForSolid();
            ExtrudeDefinition hdef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(hpro, PartFeatureOperationEnum.kJoinOperation);
            hdef.SetDistanceExtent(5, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(hdef);
        }
        private void CreateSur(Face face1,Face face2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            Point2d p1 = InventorTool.CreatePoint2d(UsMM(par.VAC.Height / 8),UsMM( par.VAC.Flanch.D6 / 3));
            Point2d p2 = InventorTool.CreatePoint2d(p1.X + 2, p1.Y + 2);
            Point2d p3 = InventorTool.CreatePoint2d(p1.X - 2, p1.Y - 2);
            Point2d p4 = InventorTool.CreatePoint2d(p2.X + 2, p2.Y + 2);
            osketch.SketchLines.AddAsTwoPointRectangle(p1, p2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.VAC.Flanch.D6 / 2 + par.VAC.Flanch.H + 200), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature box1 = Definition.Features.ExtrudeFeatures.Add(def);
            Face box1EF = InventorTool.GetFirstFromIEnumerator<Face>(box1.EndFaces.GetEnumerator());


            PlanarSketch tsketch = Definition.Sketches.Add(box1EF);
            tsketch.SketchLines.AddAsTwoPointRectangle(p3, p4);
            Profile tpro = tsketch.Profiles.AddForSolid();
            ExtrudeDefinition tdef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(tpro, PartFeatureOperationEnum.kJoinOperation);
            tdef.SetDistanceExtent(UsMM( 20), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature box2 = Definition.Features.ExtrudeFeatures.Add(tdef);
            Face box2EF = InventorTool.GetFirstFromIEnumerator<Face>(box2.EndFaces.GetEnumerator());


            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(box2EF, 2,true);
            PlanarSketch Hsketch = Definition.Sketches.Add(plane);
          SketchEntitiesEnumerator entities=  Hsketch.SketchLines.AddAsTwoPointRectangle(p1, p2);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            Profile Hpro = Hsketch.Profiles.AddForSolid();
          RevolveFeature revolve=  Definition.Features.RevolveFeatures.AddFull(Hpro, lines[1], PartFeatureOperationEnum.kJoinOperation);

            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(box1);
            objc.Add(box2);
            objc.Add(revolve);
         MirrorFeature mirror=   Definition.Features.MirrorFeatures.Add(objc, Definition.WorkPlanes[2],false,PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Add(mirror);
            WorkPlane mirrorPlane = Definition.WorkPlanes.AddByTwoPlanes(face1, face2,true);
            Definition.Features.MirrorFeatures.Add(objc, mirrorPlane,false,PatternComputeTypeEnum.kAdjustToModelCompute);
        }
       
        #region 生成尾部凹进去的水平低温泵
        private void CreateConcave()
        {
            SketchCircle InCircle, rollCircle;
            ExtrudeFeature Cy = CreateCy(out InCircle);
            Face CyStartFace = InventorTool.GetFirstFromIEnumerator<Face>(Cy.EndFaces.GetEnumerator());
            Face CyEndFace = InventorTool.GetFirstFromIEnumerator<Face>(Cy.StartFaces.GetEnumerator());

            //  ExtrudeFeature roll = CreateRoll(CyEndFace, out rollCircle);
            CreateSur(CyStartFace, CyEndFace);
            RevolveFeature cat = CreateCat();

            ExtrudeFeature flanch = CreateFlance(CyStartFace, InCircle, UsMM(par.VAC.Flanch.D1 / 2),UsMM( par.VAC.Flanch.H));
            Face flanchEndFace = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
            CreateFlanceGroove(flanchEndFace, InCircle, UsMM(par.VAC.Flanch.D2 / 2));
            CreateFlanceScrew(flanchEndFace, InCircle, par.VAC.Flanch.N, UsMM(par.VAC.Flanch.D / 2), UsMM(par.VAC.Flanch.D0 / 2), UsMM(par.VAC.Flanch.H));

            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[1], UsMM(par.VAC.Flanch.D6 / 4),true);
            CreateTail( 0, -UsMM(par.VAC.Flanch.D6 / 4),UsMM(par.VAC.Flanch.D6/4), par.VAC.InN2,plane);
            CreateTail(UsMM(par.VAC.Flanch.D6 / 4), 0, UsMM(par.VAC.Flanch.D6 / 4), par.VAC.OutN2, plane);
            //  Face rollEF = InventorTool.GetFirstFromIEnumerator<Face>(roll.EndFaces.GetEnumerator());
            //  CreateMote(rollCircle, rollEF);
            CreateCenter(plane, InCircle);
        }
        #endregion
    }
}
