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
    /// 干泵DVR
    /// </summary>
    [Export("DRYVAC", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DRYVAC : PartModulebase
    {
        public ParDRYVAC par = new ParDRYVAC();
        [ImportingConstructor]
        public DRYVAC():base()
        {
            this.Parameter = par;
            this.Name = "干泵DVR";
            init();
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        void init()
        {
            par.Lenth = 1330;
            par.Width = 660;
            par.Height = 900;

            par.TopDN = 250;
            par.TopHoleDepth = 47;
            par.TopHoleX = 442;
            par.TopHoleY = 395;

            par.ScreenX = 46;
            par.ScreenY = 25;
            par.ScreenLength = 160;
            par.ScreenHeight1 = 66;
            par.ScreenHeight2 = 82;
            par.ScreenWidth = 240;
            par.BottomHeight = 120;

            par.FanX = 112;
            par.FanY = 25;
            par.FanWidth = 116;

            par.PumpHeight = 107;
            par.PumpLenght = 131;
            par.PumpWidth = 55;
            par.PumpX = 32;
            par.PumpY = 214;

            par.SideFlanchX = 88;
            par.SideFlanchY = 190;
            par.SideDN = 63;

            par.SideHoleDia = 27;
            par.SideHoleThinkness = 2.4;
            par.SideHoleX1 = 165;
            par.SideHoleX2 = 215;
            par.SideHoleY1 = 100;
            par.SideHoleY2 = 100;
           
            par.ValveInDia = 17.5;
            par.ValveThinkness = 2.3;
            par.ValveX = 60;
            par.ValveY = 395;
            

        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch BoxSketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            //ExtrudeFeature box= InventorTool.CreateBox(Definition, BoxSketch, UsMM(par.Lenth), UsMM(par.Width), UsMM(par.Height));
            ExtrudeFeature box = CreateBox(BoxSketch, UsMM(par.Lenth), UsMM(par.Width), UsMM(par.Height));
            Face BoxEF = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            List<Face> BoxSF = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            Face BoxStartF = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            WorkPlane PlaneX = Definition.WorkPlanes.AddByTwoPlanes(BoxSF[0], BoxSF[2],true);
            WorkPlane PlaneY = Definition.WorkPlanes.AddByTwoPlanes(BoxSF[1], BoxSF[3],true);
            CreateTopFlanch(BoxEF);
            CreateScreen(BoxSF[3]);
            CreateBottom(BoxStartF,PlaneX,PlaneY);
            CreateSidePart(BoxSF[2],BoxStartF);
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
        private void CreateTopFlanch(Face plane)
        {
            SketchCircle cir;
            WorkPlane holePlane = Definition.WorkPlanes.AddByPlaneAndOffset(plane, -UsMM(par.TopHoleDepth),true);
            CreateTopHole(holePlane, UsMM(par.TopHoleX), -UsMM(par.TopHoleY), UsMM(par.TopFlanch.D1 + 50),UsMM(par.TopHoleDepth-3),out cir);
        ExtrudeFeature hole= CreateTopHole(holePlane, UsMM(par.TopHoleX), -UsMM(par.TopHoleY), UsMM(par.TopFlanch.D1), UsMM(par.TopHoleDepth),out cir);
          //  Face holeStartF = InventorTool.GetFirstFromIEnumerator<Face>(hole.Faces.GetEnumerator());
           ExtrudeFeature flanchConcave= CreateTopCY(holePlane, cir, UsMM(par.TopFlanch.D6), UsMM(2));
            Face flanchConcaveEF = InventorTool.GetFirstFromIEnumerator<Face>(flanchConcave.EndFaces.GetEnumerator());
           ExtrudeFeature flanch= CreateTopCY(flanchConcaveEF, cir, UsMM(par.TopFlanch.D2), UsMM(par.TopFlanch.H));
            Face flanchEF = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
            CreateFlanceScrew(flanchEF, cir, par.TopFlanch.N, UsMM(par.TopFlanch.C / 2), UsMM(par.TopFlanch.D0/2), UsMM(par.TopFlanch.H));
        }
        private ExtrudeFeature CreateTopHole(WorkPlane plane,double x,double y,double diameter,double depth,out SketchCircle cir)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            cir= osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(x, y), diameter / 2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(depth, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateTopCY(object plane,SketchCircle cir,double diameter,double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane); 
            SketchCircle cir1 = (SketchCircle)osketch.AddByProjectingEntity(cir);
            cir = osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, diameter / 2);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if(item.Count==2)
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
        private ExtrudeFeature CreateFlanceScrew(Face plane, SketchCircle inCircle, double screwNumber, double ScrewRadius, double arrangeRadius, double flanchThickness)
        {
            double angle = 360 / screwNumber / 180 * Math.PI;
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle flanceInCircle = (SketchCircle)osketch.AddByProjectingEntity(inCircle);
            flanceInCircle.Construction = true;
            SketchCircle screwCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(0, arrangeRadius), ScrewRadius);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(screwCircle);
            for (int i = 1; i < screwNumber; i++)
            {
                osketch.RotateSketchObjects(objc, flanceInCircle.CenterSketchPoint.Geometry, angle * i, true);
            }
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if(item.Count==1)
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
       #region 创建触摸屏
        private void CreateScreen(Face plane)
        {
            WorkPlane ScreenPlane = Definition.WorkPlanes.AddByPlaneAndOffset(plane, -UsMM(par.ScreenY),true);
          ExtrudeFeature ScreenSur=  CreateScreenSur(ScreenPlane);
            List<Face> ScreenSurSF = InventorTool.GetCollectionFromIEnumerator<Face>(ScreenSur.SideFaces.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(ScreenSurSF[1]);
            Point2d p1 = InventorTool.CreatePoint2d(-2, 2);
            Point2d p2 = InventorTool.CreatePoint2d(-UsMM(par.ScreenLength )+2, UsMM(par.ScreenWidth )-2);
            osketch.SketchLines.AddAsTwoPointRectangle(p1, p2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(2), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
             Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateScreenSur(object plane)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
           // double Y = UsMM(par.ScreenY + par.Height);
           SketchLine line1= osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(UsMM(par.ScreenX), UsMM(par.Height)), InventorTool.CreatePoint2d(UsMM(par.ScreenX + par.ScreenLength), UsMM(par.Height)));
            Point2d p1 = line1.StartSketchPoint.Geometry;
            Point2d p2 = line1.EndSketchPoint.Geometry;
            Point2d p3 = InventorTool.CreatePoint2d(p1.X - UsMM(par.ScreenHeight1) * Math.Cos(85 * Math.PI / 180), p1.Y + UsMM(par.ScreenHeight1) * Math.Sin(85 * Math.PI / 180));
            Point2d p4= InventorTool.CreatePoint2d(p2.X - UsMM(par.ScreenHeight2) * Math.Cos(85 * Math.PI / 180), p2.Y + UsMM(par.ScreenHeight2) * Math.Sin(85 * Math.PI / 180));
           SketchLine line2=  osketch.SketchLines.AddByTwoPoints(p1, p3);
            SketchLine line3 = osketch.SketchLines.AddByTwoPoints(p2, p4);
            SketchLine line4 = osketch.SketchLines.AddByTwoPoints(p3, p4);
            InventorTool.CreateTwoPointCoinCident(osketch, line1, line2);
            InventorTool.CreateTwoPointCoinCident(osketch, line2, line4);
            InventorTool.CreateTwoPointCoinCident(osketch, line4, line3);
            InventorTool.CreateTwoPointCoinCident(osketch, line3, line1);
          
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.ScreenWidth), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        #endregion
        #region 创建底部支撑
        private void CreateBottom(Face plane,WorkPlane planeX,WorkPlane planeY)
        {
            double wheelSurWidth = 102;//支撑的宽度
            double wheelSurDistance = 36;//滚轮支撑距边距离
           ExtrudeFeature Wheelsur1= CreateWheelSur1(plane, wheelSurWidth,wheelSurDistance);
            Face WheelSur1EF = InventorTool.GetFirstFromIEnumerator<Face>(Wheelsur1.EndFaces.GetEnumerator());
           RevolveFeature Wheel= CreateWheel1(WheelSur1EF, wheelSurWidth);
            ExtrudeFeature WheelSur2 = CreateWheelSur2(plane, wheelSurWidth, wheelSurDistance);
            Face WheelSur2EF = InventorTool.GetFirstFromIEnumerator<Face>(WheelSur2.EndFaces.GetEnumerator());
            ExtrudeFeature WheelSur2CY = CreateWheelSur2Cy(WheelSur2EF, UsMM(wheelSurWidth));
            Face WheelSur2CYEF = InventorTool.GetFirstFromIEnumerator<Face>(WheelSur2CY.EndFaces.GetEnumerator());
           RevolveFeature Wheel2= CreateWheel2(WheelSur2CYEF, wheelSurWidth);

          ExtrudeFeature Beam=  CreateBottomBeam(plane, par.Width - 37 * 2, 58, 34, 190, 37);
            Face BeamEF = InventorTool.GetFirstFromIEnumerator<Face>(Beam.EndFaces.GetEnumerator());
          ExtrudeFeature BeamSur=   CreateBeamSur(BeamEF, 30, 29, 15, 78);
            Face BeamSurEF = InventorTool.GetFirstFromIEnumerator<Face>(BeamSur.EndFaces.GetEnumerator());
           ExtrudeFeature BeamSurPlane= CreateBeamSurPlane(BeamSurEF, 30, 5);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(BeamSur);
            objc.Add(BeamSurPlane);
           MirrorFeature BeamSurMirror= Definition.Features.MirrorFeatures.Add(objc, planeY);
            objc.Add(Beam);
            objc.Add(BeamSurMirror);
            Definition.Features.MirrorFeatures.Add(objc, planeX);
            objc.Clear();
            objc.Add(Wheelsur1);
            objc.Add(Wheel);
            objc.Add(WheelSur2);
            objc.Add(WheelSur2CY);
            objc.Add(Wheel2);
            Definition.Features.MirrorFeatures.Add(objc, planeY);
        }
             #region  支撑1
        private ExtrudeFeature CreateWheelSur1(Face plane,double SurWidth, double distance)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(distance), UsMM(distance)), InventorTool.CreatePoint2d(UsMM(distance + SurWidth), UsMM(distance + SurWidth)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.BottomHeight*2/3), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
         
        }
        private RevolveFeature CreateWheel1(Face plane,double surWidth)
        {
            double radius = par.BottomHeight / 3;
            Edge e = InventorTool.GetFirstFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, e, true, true, e.StartVertex);
            Point2d p1 = InventorTool.CreatePoint2d(UsMM(surWidth / 2 - surWidth/4),-UsMM(surWidth / 2));
            Point2d p2 = InventorTool.CreatePoint2d(UsMM(surWidth / 2 + surWidth/4), -UsMM(surWidth / 2 + radius));
          SketchEntitiesEnumerator Ract=  osketch.SketchLines.AddAsTwoPointRectangle(p1, p2);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(Ract.GetEnumerator());

            Profile pro = osketch.Profiles.AddForSolid();
            return Definition.Features.RevolveFeatures.AddFull(pro, lines[0], PartFeatureOperationEnum.kJoinOperation);
           
        }
            #endregion
             # region 支撑2
        private ExtrudeFeature CreateWheelSur2(Face plane,double SurWidth,double distance)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(par.Lenth-distance-SurWidth), UsMM(distance)), InventorTool.CreatePoint2d(UsMM(par.Lenth-distance), UsMM(distance + SurWidth)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(5), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
       private ExtrudeFeature CreateWheelSur2Cy(Face plane,double SurWidth)
        {
            Edge e = InventorTool.GetFirstFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, e, true, true, e.StartVertex);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(SurWidth / 2,- SurWidth / 2), SurWidth / 2 - 0.2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.BottomHeight * 2 / 3)-0.5, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private RevolveFeature CreateWheel2(Face plane, double surWidth)
        {
            double radius = par.BottomHeight / 3;
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            // Edge e = InventorTool.GetFirstFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            // PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, e, true, true, );
            Point2d p1 = InventorTool.CreatePoint2d(0, -UsMM(surWidth / 4));
            Point2d p2 = InventorTool.CreatePoint2d(UsMM( radius), UsMM(surWidth / 4));
            SketchEntitiesEnumerator Ract = osketch.SketchLines.AddAsTwoPointRectangle(p1, p2);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(Ract.GetEnumerator());

            Profile pro = osketch.Profiles.AddForSolid();
            return Definition.Features.RevolveFeatures.AddFull(pro, lines[1], PartFeatureOperationEnum.kJoinOperation);

        }
        #endregion
            #region 支撑3
        private ExtrudeFeature CreateBottomBeam(Face plane,double length,double width,double height,double sideX,double sideY)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)), InventorTool.CreatePoint2d(UsMM(sideX + width), UsMM(sideY + length)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateBeamSur(Face plane,double sideX,double sideY,double Radius,double height)
        {
            Edge e = InventorTool.GetFirstFromIEnumerator<Edge>(plane.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, e, true, true, e.StartVertex);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX), -UsMM(sideY)), UsMM(Radius));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }  
        private ExtrudeFeature CreateBeamSurPlane(Face plane,double radius,double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin,UsMM(radius));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        #endregion
        #endregion
        #region 创建侧边部件
        private void CreateSidePart(Face plane,Face StartFace)
        {
            CreateFan(plane,par.FanX,par.FanY,par.FanWidth,par.FanWidth,4);
            CreatePump(plane, par.PumpX, par.PumpY, par.PumpLenght, par.PumpWidth, par.PumpHeight);
            //法兰接口
            ExtrudeFeature cyling = CreateHole(plane, par.SideFlanchX, par.SideFlanchY, par.SideFlanch.D6, 5, 47);
            CreateHole(plane, par.SideHoleX1-par.SideFlanchX, par.SideHoleY1-par.SideFlanchY, par.SideHoleDia, par.SideHoleThinkness,19);
            CreateHole(plane, par.SideHoleX2-par.SideFlanchX, par.SideHoleY2-par.SideFlanchY, par.SideHoleDia, par.SideHoleThinkness,19);
            Face CyEF = InventorTool.GetFirstFromIEnumerator<Face>(cyling.EndFaces.GetEnumerator());
            CreateSideFlanch(CyEF, par.SideFlanch);
            CreateValve(StartFace, plane, par.ValveX-par.SideFlanchX, par.ValveY-par.SideFlanchY, par.ValveInDia, par.ValveThinkness, 35, 26);
        }
        private void CreateFan(Face plane,double sideX,double sideY,double length,double width,double depth)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            double y = par.Height - sideY;
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(y)), InventorTool.CreatePoint2d(UsMM(sideX + width), UsMM(y - length)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(UsMM(depth), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature pit= Definition.Features.ExtrudeFeatures.Add(def); 
            List<Face> SFs = InventorTool.GetCollectionFromIEnumerator<Face>(pit.SideFaces.GetEnumerator());
           EdgeCollection objc = InventorTool.CreateEdgeCollection();
            //List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(SFs[0].Edges.GetEnumerator());
            //   objc.Add(edges[1]);
            foreach (Face item in SFs)
            {
                List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(item.Edges.GetEnumerator());
                objc.Add(edges[1]);
            }
            ChamferFeature chamfer = Definition.Features.ChamferFeatures.AddUsingDistance(objc,UsMM(width/3),true,false);
        }
        private void CreatePump(Face plane, double sideX, double sideY, double length, double width, double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            double x = par.Width - sideX;
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(x), UsMM(sideY)), InventorTool.CreatePoint2d(UsMM(x - width), UsMM(sideY +length)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature pit = Definition.Features.ExtrudeFeatures.Add(def);
        }
       private ExtrudeFeature CreateHole(Face plane,double sideX,double sideY,double diameter,double thinkness,double Height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            
            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)),UsMM(diameter/2));
            osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX), UsMM(sideY)), UsMM(diameter / 2+thinkness));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(Height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature pit = Definition.Features.ExtrudeFeatures.Add(def);
            return pit;
        }
        private void CreateSideFlanch(Face plane,  ParFlanch flanch)
        {
            PlanarSketch Mysketch = Definition.Sketches.Add(plane);
           
            Mysketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(flanch.D1 / 2));
            Profile myPro = Mysketch.Profiles.AddForSolid();
            ExtrudeDefinition Extrudedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(myPro, PartFeatureOperationEnum.kJoinOperation);
            Extrudedef.SetDistanceExtent(UsMM(flanch.H), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
          ExtrudeFeature extrude=  Definition.Features.ExtrudeFeatures.Add(Extrudedef);
            Face EF = InventorTool.GetFirstFromIEnumerator<Face>(extrude.EndFaces.GetEnumerator());

            PlanarSketch osketch = Definition.Sketches.Add(EF);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(flanch.D6/2));
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(flanch.D1/2));
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
        private void CreateValve(Object plane1,Face plane,double sideX,double sideY,double dia,double thinckness,double height,double length)
        {
            PlanarSketch Mysketch = Definition.Sketches.Add(plane);

           SketchCircle cir=  Mysketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(sideX),UsMM(sideY)), UsMM(dia / 2+thinckness));
            Profile myPro = Mysketch.Profiles.AddForSolid();
            ExtrudeDefinition Extrudedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(myPro, PartFeatureOperationEnum.kJoinOperation);
            Extrudedef.SetDistanceExtent(UsMM(height), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrude = Definition.Features.ExtrudeFeatures.Add(Extrudedef);
            Face SF = InventorTool.GetFirstFromIEnumerator<Face>(extrude.SideFaces.GetEnumerator());
            WorkAxis axis = Definition.WorkAxes.AddByRevolvedFace(SF,true);
           // Face EF = InventorTool.GetFirstFromIEnumerator<Face>(extrude.EndFaces.GetEnumerator());
            WorkPlane p = Definition.WorkPlanes.AddByPlaneAndPoint(plane1, cir.CenterSketchPoint,true);
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(p,axis,true,true,cir.CenterSketchPoint);
            Point2d center = InventorTool.CreatePoint2d(UsMM(height - dia / 2 - thinckness),0);
            osketch.SketchCircles.AddByCenterRadius(center, UsMM(dia/2));
            osketch.SketchCircles.AddByCenterRadius(center, UsMM(dia/2+thinckness));
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1) item.AddsMaterial = true;
                else item.AddsMaterial = false;
            }
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(length), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);
        }
        #endregion
    }
}
