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
    /// 干泵SP
    /// </summary>
    [Export("ScrewLine", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScrewLine : PartModulebase
    {
        ParScrewLine par = new ParScrewLine();
        public ScrewLine() : base()
        {
            this.Parameter = par;
            this.Name = "干泵SP";
            init();
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        void init()
        {
            par.Lenth = 1300;
            par.Width = 442;
            par.Height = 627;

            par.SideFlanchX = 525;
            par.SideFlanchY = 115;
            par.SideDN = 63;

            par.HandleX1 = 490;
            par.HandleY1 = 44;
            par.HandleY2 = 44;
            par.HandleX2 = 1158;

            par.HandleWidth = 120;
            par.HandleLength = 65;
            par.HandleDepth = 20;

            par.BottomHeight = 166;


            par.TopHoleDepth = 173;
            par.TopHoleX = 442;
            par.TopHoleY = par.Width / 2;
            par.FlanchDiameter = 190;
            par.FlanchIndiameter3 = 65;
            par.FlanchIndiameter2 = 70;
            par.FlanchInDiameter1 = 90;
            par.FlanchInDepth1 = 4;
            par.FlanchInDepth2 = 5;
            par.FlanchInDepth3 = 26;
            par.ScrewDiameter1 = 13.5;
            par.ScrewDiameter2 = 19;
            par.ScrewRangeDiameter1 = 130;
            par.ScrewRangeDiameter2 = 152;

            par.FlanchCYDiameter = 4;
            par.FlanchRactWith = 5;
            par.FlanchRactHeight = 1;

            par.AirHeight = 315;
            par.AirWidth = 243;
            par.ForX = 88;
            par.ForY = 435;
            par.SideY = 590;

        }
        public override bool CheckParamete()
        {
            if (par.SideFlanchX <= par.SideFlanch.D6 || par.SideFlanchY <= par.SideFlanch.D6 || par.Height - par.SideFlanch.D6 <= par.SideFlanchY
                || par.Lenth - par.SideFlanch.D6 <= par.SideFlanchX)
            {
                ParErrorChanged(this, "侧边孔位置超出范围！");
                return false;
            }
               

            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch BoxSketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            ExtrudeFeature box = CreateBox(BoxSketch, UsMM(par.Lenth), UsMM(par.Width), UsMM(par.Height));
            Face BoxEF = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            List<Face> BoxSF = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            Face BoxStartF = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            WorkPlane PlaneX = Definition.WorkPlanes.AddByTwoPlanes(BoxSF[0], BoxSF[2], true);
            WorkPlane PlaneY = Definition.WorkPlanes.AddByTwoPlanes(BoxSF[1], BoxSF[3], true);
            //  CreateTopFlanch(BoxEF);
            CreateSidePart(BoxSF[1],PlaneY);
            CreateBottom(BoxStartF, PlaneX, PlaneY);
            CreateTopFlanch(BoxEF,PlaneY);
            CreateAir(BoxSF[3], BoxSF[2], PlaneY);
        }
        private ExtrudeFeature CreateBox(PlanarSketch osketch, double length, double width, double height)
        {
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(0, 0), InventorTool.CreatePoint2d(-length, width));

            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition extrudedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extrudedef.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(extrudedef);
        }
        #region 顶部孔接口
        private void CreateTopFlanch(Face plane,WorkPlane planeY)
        {
            SketchCircle cir;
            WorkPlane holePlane = Definition.WorkPlanes.AddByPlaneAndOffset(plane, -UsMM(par.TopHoleDepth), true);
            CreateTopHole(holePlane, UsMM(par.TopHoleX), -UsMM(par.TopHoleY), UsMM(par.FlanchDiameter + 50), UsMM(par.TopHoleDepth - 3), out cir);
            ExtrudeFeature hole = CreateTopHole(holePlane, UsMM(par.TopHoleX), -UsMM(par.TopHoleY), UsMM(par.FlanchDiameter), UsMM(par.TopHoleDepth), out cir);
            //  Face holeStartF = InventorTool.GetFirstFromIEnumerator<Face>(hole.Faces.GetEnumerator());
            ExtrudeFeature flanchConcave = CreateTopCY(holePlane, cir, UsMM(par.FlanchIndiameter3), UsMM(par.FlanchInDepth3));
            Face flanchConcaveEF = InventorTool.GetFirstFromIEnumerator<Face>(flanchConcave.EndFaces.GetEnumerator());
            ExtrudeFeature flanch1 = CreateTopCY(flanchConcaveEF, cir, UsMM(par.FlanchIndiameter2), UsMM(par.FlanchInDepth2));
            Face flanch1EF = InventorTool.GetFirstFromIEnumerator<Face>(flanch1.EndFaces.GetEnumerator());
            ExtrudeFeature flanch2 = CreateTopCY(flanch1EF, cir, UsMM(par.FlanchInDiameter1), UsMM(par.FlanchInDepth1 ));
            Face flanch2EF = InventorTool.GetFirstFromIEnumerator<Face>(flanch2.EndFaces.GetEnumerator());
            // Face flanchEF = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
              CreateFlanceScrew(flanch2EF, cir, 4, UsMM(par.ScrewDiameter1/2), UsMM(par.ScrewRangeDiameter1/2), UsMM(par.FlanchInDepth1+par.FlanchInDepth2+par.FlanchInDepth3),0);
            CreateFlanceScrew(flanch2EF, cir, 4, UsMM(par.ScrewDiameter2 / 2), UsMM(par.ScrewRangeDiameter2 / 2), UsMM(par.FlanchInDepth1 + par.FlanchInDepth2 + par.FlanchInDepth3),Math.PI/4);
            CreateCyCircle(planeY, cir,UsMM(par.FlanchInDepth2+par.FlanchInDepth3),UsMM(par.FlanchInDiameter1/2-par.FlanchCYDiameter/2),UsMM(par.FlanchCYDiameter),
                UsMM(par.FlanchInDiameter1/2-par.FlanchCYDiameter-1-par.FlanchRactWith),UsMM(par.FlanchRactWith),UsMM(par.FlanchRactHeight));
        }
        private void CreateCyCircle(WorkPlane plane,SketchCircle cir,double Y,double cirX,double Dia,double ractX,double ractWidth,double ractHeight)
        {
         
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, Definition.WorkAxes[1], true, true, cir.CenterSketchPoint);
            SketchLine mirLine = osketch.SketchLines.AddByTwoPoints(InventorTool.Origin, InventorTool.CreatePoint2d(0, 1));
            mirLine.Construction = true;
            SketchCircle circle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(cirX, Y+Dia/2), Dia / 2);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(ractX, Y+Dia/2), InventorTool.CreatePoint2d(ractX + ractWidth, Y + ractHeight+Dia/2));
            Profile pro = osketch.Profiles.AddForSolid();
            Definition.Features.RevolveFeatures.AddFull(pro, mirLine, PartFeatureOperationEnum.kJoinOperation);
        }
        private ExtrudeFeature CreateTopHole(WorkPlane plane, double x, double y, double diameter, double depth, out SketchCircle cir)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            cir = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(x, y), diameter / 2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(depth, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateTopCY(object plane, SketchCircle cir, double diameter, double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle cir1 = (SketchCircle)osketch.AddByProjectingEntity(cir);
            cir = osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, diameter / 2);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count == 2)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateFlanceScrew(Face plane, SketchCircle inCircle, double screwNumber, double ScrewRadius, double arrangeRadius, double flanchThickness,double startAngle)
        {
            double angle = 360 / screwNumber / 180 * Math.PI;
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle flanceInCircle = (SketchCircle)osketch.AddByProjectingEntity(inCircle);
            flanceInCircle.Construction = true;
            SketchCircle screwCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(Math.Sin(startAngle)*arrangeRadius, Math.Cos(startAngle)*arrangeRadius), ScrewRadius);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(screwCircle);
            for (int i = 1; i < screwNumber; i++)
            {
                osketch.RotateSketchObjects(objc, flanceInCircle.CenterSketchPoint.Geometry, angle * i, true);
            }
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count == 1)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(flanchThickness, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(ex);
        }
        #endregion
        #region 创建底部支撑
        private void CreateBottom(Face plane, WorkPlane planeX, WorkPlane planeY)
        {
          
            ExtrudeFeature Beam = CreateBottomBeam(plane, par.Width - 37 * 2, 58, 34, 190, 37);
            Face BeamEF = InventorTool.GetFirstFromIEnumerator<Face>(Beam.EndFaces.GetEnumerator());
            ExtrudeFeature BeamSur = CreateBeamSur(BeamEF, 30, 29, 15, 78);
            Face BeamSurEF = InventorTool.GetFirstFromIEnumerator<Face>(BeamSur.EndFaces.GetEnumerator());
            ExtrudeFeature BeamSurPlane = CreateBeamSurPlane(BeamSurEF, 30, 5);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(BeamSur);
            objc.Add(BeamSurPlane);
            MirrorFeature BeamSurMirror = Definition.Features.MirrorFeatures.Add(objc, planeY);
            objc.Add(Beam);
            objc.Add(BeamSurMirror);
            Definition.Features.MirrorFeatures.Add(objc, planeX);
            objc.Clear();
      
        }

 
        private ExtrudeFeature CreateBottomBeam(Face plane, double length, double width, double height, double sideX, double sideY)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)), InventorTool.CreatePoint2d(UsMM(sideX + width), UsMM(sideY + length)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateBeamSur(Face plane, double sideX, double sideY, double Radius, double height)
        {
            Edge e = InventorTool.GetFirstFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, e, true, true, e.StartVertex);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX), -UsMM(sideY)), UsMM(Radius));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateBeamSurPlane(Face plane, double radius, double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(radius));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }

        #endregion
        #region 创建侧边部件
        private void CreateSidePart(Face plane,WorkPlane midir)
        {
            CreateHandle(plane, midir);
            ExtrudeFeature cyling = CreateHole(plane, par.SideFlanchX, par.SideFlanchY, par.SideFlanch.D6, 5, 47);
            Face CyEF = InventorTool.GetFirstFromIEnumerator<Face>(cyling.EndFaces.GetEnumerator());
           
            CreateSideFlanch(CyEF, par.SideFlanch);
           
        }
        private ExtrudeFeature CreateHole(Face plane, double sideX, double sideY, double diameter, double thinkness, double Height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);

            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)), UsMM(diameter / 2));
            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)), UsMM(diameter / 2 + thinkness));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(Height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature pit = Definition.Features.ExtrudeFeatures.Add(def);
            return pit;
        }
        private void CreateSideFlanch(Face plane, ParFlanch flanch)
        {
            PlanarSketch Mysketch = Definition.Sketches.Add(plane);

            Mysketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(flanch.D1 / 2));
            Profile myPro = Mysketch.Profiles.AddForSolid();
            ExtrudeDefinition Extrudedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(myPro, PartFeatureOperationEnum.kJoinOperation);
            Extrudedef.SetDistanceExtent(UsMM(flanch.H), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrude = Definition.Features.ExtrudeFeatures.Add(Extrudedef);
            Face EF = InventorTool.GetFirstFromIEnumerator<Face>(extrude.EndFaces.GetEnumerator());

            PlanarSketch osketch = Definition.Sketches.Add(EF);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(flanch.D6 / 2));
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(flanch.D1 / 2));
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1) item.AddsMaterial = true;
                else item.AddsMaterial = false;
            }
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(flanch.H), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);
        }
        private void CreateHandle(Face plane, WorkPlane midd)
        {
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            double Y1 = par.Height - par.HandleY1-par.HandleLength;
            double Y2 = par.Height - par.HandleY2-par.HandleLength;
          ExtrudeFeature hole1=  CreateRactHole(plane,edges[2], par.HandleX1, Y1, par.HandleWidth, par.HandleLength, par.HandleDepth);
          ExtrudeFeature hole2=  CreateRactHole(plane,edges[2], par.HandleX2, Y1, par.HandleWidth, par.HandleLength, par.HandleDepth);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(hole1);
            objc.Add(hole2);
            Definition.Features.MirrorFeatures.Add(objc, midd, false, PatternComputeTypeEnum.kAdjustToModelCompute);
        }
        private ExtrudeFeature CreateRactHole(Face  plane,Edge edge,double sideX,double sideY,double width,double length,double depth)
        {
          
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, edge,false,true, edge.StopVertex);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)), InventorTool.CreatePoint2d(UsMM(sideX + width),UsMM( sideY + length)));
             
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(UsMM(depth), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature pit = Definition.Features.ExtrudeFeatures.Add(def);
            return pit;
        }
        #endregion
        #region 创建透风窗口
        private void CreateAir(Face ForFace,Face SideFace,WorkPlane planeY)
        {
          ObjectCollection objc=  CreateAirW(ForFace, UsMM(par.ForX), UsMM(par.ForY), UsMM(par.AirWidth), UsMM(par.AirHeight));
            Definition.Features.MirrorFeatures.Add(objc, planeY,false,PatternComputeTypeEnum.kAdjustToModelCompute);
            CreateAirW(SideFace, UsMM(par.Width - par.AirWidth) / 2, UsMM(par.SideY), UsMM(par.AirWidth), UsMM(par.AirHeight));
        }
        private ObjectCollection CreateAirW(Face plane,double positionX,double positionY,double width,double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);

            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(positionX, positionY), InventorTool.CreatePoint2d(positionX + width, positionY + UsMM(16)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(UsMM(5), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature extrude = Definition.Features.ExtrudeFeatures.Add(def);
            List<Face> SF = InventorTool.GetCollectionFromIEnumerator<Face>(extrude.SideFaces.GetEnumerator());
            List<Edge> edges= InventorTool.GetCollectionFromIEnumerator<Edge>(SF[3].Edges.GetEnumerator());
            EdgeCollection EDC = InventorTool.CreateEdgeCollection();
            EDC.Add(edges[0]);
            ChamferFeature chamfer= Definition.Features.ChamferFeatures.AddUsingDistance(EDC, UsMM(5));
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(extrude);
            objc.Add(chamfer);
            int num = (int)(height / UsMM(22));
          RectangularPatternFeature ractangular=  Definition.Features.RectangularPatternFeatures.Add(objc,Definition.WorkAxes[2],false,num,UsMM(22), PatternSpacingTypeEnum.kDefault, null, null, true, null, null, PatternSpacingTypeEnum.kDefault, null, PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Add(ractangular);
            return objc;
        }
        #endregion
    }
}
