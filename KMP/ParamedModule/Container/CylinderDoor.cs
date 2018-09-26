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
    public class CylinderDoor : PartModulebase
    {

        private ParCylinderDoor _par = new ParCylinderDoor();
        public ParCylinderDoor par
        {
            get
            {
                return _par;
            }
            set
            {
                _par = value;
            }
        }
        Dictionary<double, WorkPlane> _sidePlanes = new Dictionary<double, WorkPlane>();
       public CylinderDoor():base()
        {
            this.ProjectType = "CYD";
            this.PreviewImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview", "CylinderDoor.png");
        }
        public CylinderDoor(PassedParameter InRadius, PassedParameter Thickness) : base()
        {
            this.Name = "容器大门";
            init();
            this.Parameter = par;
            par.InRadius = InRadius;
            par.Thickness = Thickness;

            this.PreviewImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview", "CylinderDoor.png");

        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }

        public override void InitCreatedModule()
        {
            init();
            base.InitCreatedModule();
        }
        private void init()
        {

            par.DoorRadius = 850;
            par.FlanchWidth = 80;
            par.FlanchThinkness=60;
            par.StraightLength = 40;
            //ParTopHole hole = new ParTopHole() {  PipeLenght = 300, PipeThickness = 4 };
            //ParFlanch flanch = new ParFlanch() { D6 = 400, D1 = 520, H = 20, D2 = 450, D0 = 480, C = 10, N = 6 };
            //ParFlanch sideFlanch = new ParFlanch() { D6 = 100, D1 = 320, H = 20, D2 = 250, D0 = 280, C = 10, N = 6 };
            ////  par.TopHole = hole;
            //hole.ParFlanch = flanch;
            //par.TopHoles.Add(hole);
           // par.TopHole.ParFlanch = flanch;
           
            //ParSideHole ParSideHole3 = new ParSideHole() {  HoleOffset = 0, PositionAngle = 0, PositionDistance = 300, PipeThickness = 10, PipeLenght = 200 };
            //ParSideHole ParSideHole4 = new ParSideHole() { HoleOffset = -400, PositionAngle = 0, PositionDistance = 300, PipeThickness = 10, PipeLenght = 200 };
            //ParSideHole ParSideHole5 = new ParSideHole() {  HoleOffset = 400, PositionAngle = 1, PositionDistance = 300, PipeThickness = 10, PipeLenght = 200 };
          
            //ParSideHole3.ParFlanch = sideFlanch;
            //ParSideHole4.ParFlanch = sideFlanch;
            //ParSideHole5.ParFlanch = sideFlanch;
          
            //par.SideHoles.Add(ParSideHole3);
            //par.SideHoles.Add(ParSideHole4);
            //par.SideHoles.Add(ParSideHole5);

            this.Name = "容器大门";
        }
        public override void DisPose()
        {
            base.DisPose();
            _sidePlanes.Clear();
        }
      
        public override void CreateSub()
        {
            SketchEllipticalArc OutArc, InArc;
            RevolveFeature revolve = CreateDoor(UsMM(par.Thickness.Value), UsMM(par.InRadius.Value), UsMM(par.DoorRadius), out InArc, out OutArc);
            List<Face> sideFace = InventorTool.GetCollectionFromIEnumerator<Face>(revolve.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(sideFace[4], true);
            
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateH";
            Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[3], 0).Name = "mateK";
            CreateTopHoles(revolve);
            List<Face> faces = InventorTool.GetCollectionFromIEnumerator<Face>(revolve.Faces.GetEnumerator());
            Face PlaneFace = faces.Where(a => a.SurfaceType == SurfaceTypeEnum.kPlaneSurface).FirstOrDefault();
            List<Face> CylinderFace = faces.Where(a => a.SurfaceType == SurfaceTypeEnum.kCylinderSurface).ToList();
            CreatePlanes(PlaneFace, CylinderFace[0]);
            foreach (var item in par.SideHoles)
            {
                WorkPlane plane = _sidePlanes[item.PositionAngle];

                if (plane == null) continue;
                CreateSideHole(plane, PlaneFace, Axis, item);

            }
            if (par.SideHoles.Count == 0 && par.TopHoles.Count == 0) return;
            CreateClear(InArc);
        }
        private RevolveFeature CreateDoor(double thickness, double inRadius, double doorRadius, out SketchEllipticalArc Arc1, out SketchEllipticalArc Arc2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            Arc1 = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, doorRadius, inRadius, 0, Math.PI / 2);
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

            SketchEntitiesEnumerator entities = InventorTool.CreateRangle(osketch, UsMM(par.StraightLength), thickness);
            SketchEntitiesEnumerator entities1 = InventorTool.CreateRangle(osketch, UsMM(par.FlanchThinkness), UsMM(par.FlanchWidth));
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            List<SketchLine> flanchLines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities1.GetEnumerator());

            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[3].EndSketchPoint, (SketchEntity)Line2);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[0], (SketchEntity)Line2.EndSketchPoint);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[1].EndSketchPoint, (SketchEntity)flanchLines[3]);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[2], (SketchEntity)flanchLines[3].StartSketchPoint);

            Profile profile = osketch.Profiles.AddForSolid();
            RevolveFeature revolve = Definition.Features.RevolveFeatures.AddFull(profile, Line1, PartFeatureOperationEnum.kNewBodyOperation);
            return revolve;
        }
        #region 创建孔
        #region 创建顶孔
        private void CreateTopHoles(RevolveFeature door)
        {
            WorkPlane topHolePlane;
            List<Face> faces = InventorTool.GetCollectionFromIEnumerator<Face>(door.Faces.GetEnumerator());
            Face cylinderFace = faces.Where(a => a.SurfaceType == SurfaceTypeEnum.kCylinderSurface).FirstOrDefault();
            Edge cylinderEdge = InventorTool.GetFirstFromIEnumerator<Edge>(cylinderFace.Edges.GetEnumerator()); //大门法兰内边
            topHolePlane = Definition.WorkPlanes.AddByPlaneAndOffset(InventorTool.GetFirstFromIEnumerator<WorkPlane>(Definition.WorkPlanes.GetEnumerator()), -UsMM(par.DoorRadius + par.Thickness.Value), true);
            foreach (var item in par.TopHoles)
            {
                Face topHoleFace;
                SketchCircle topSketchCircle;
                CreateTopHole(UsMM(item.ParFlanch.D6 / 2), topHolePlane, cylinderEdge, out topHoleFace, out topSketchCircle, UsMM(item.HoleOffset),UsMM(item.PositionDistance));
                #region 管长度
                double length;
                if (item.HoleOffset == 0)
                {
                    length = item.PositionDistance;
                }
                else if (item.PositionDistance == 0)
                {
                    length = item.HoleOffset;
                }
                else
                {
                    length = Math.Pow(Math.Pow(item.PositionDistance, 2) + Math.Pow(item.HoleOffset, 2), 0.5);
                }
                double Height = par.DoorRadius - Math.Pow((1 - Math.Pow(length, 2) / Math.Pow(par.InRadius.Value, 2)) * Math.Pow(par.DoorRadius, 2), 0.5);
                #endregion
                ExtrudeFeature topHolePipe = CreateTopHolePipe(topHolePlane, topHoleFace, UsMM(item.PipeLenght), UsMM(item.PipeThickness), UsMM(Height));
                Face topHoleEndFace = InventorTool.GetCollectionFromIEnumerator<Face>(topHolePipe.EndFaces.GetEnumerator()).Where(a => a.SurfaceType == SurfaceTypeEnum.kPlaneSurface).FirstOrDefault();
                ExtrudeFeature flach = CreateTopFlance(topHoleEndFace, topSketchCircle, UsMM(item.ParFlanch.D1 / 2), UsMM(item.ParFlanch.H));
                Face flachEndFace = InventorTool.GetFirstFromIEnumerator<Face>(flach.EndFaces.GetEnumerator());
                CreateFlanceGroove(flachEndFace, topSketchCircle, UsMM(item.ParFlanch.D2 / 2));
                CreateFlanceScrew(flachEndFace, topSketchCircle, item.ParFlanch.N, UsMM(item.ParFlanch.C / 2), UsMM(item.ParFlanch.D0 / 2), UsMM(item.ParFlanch.H));
            }
        }
        /// <summary>
        /// 顶部开孔
        /// </summary>
        /// <param name="door">罐门</param>
        /// <param name="holeRadius">孔半径</param>
        /// <param name="topHolePlane">顶孔面</param>
        /// <param name="topHoleFace">孔的侧边</param>
        /// <param name="topSketchCircle">孔草图</param>
        private void CreateTopHole( double holeRadius, WorkPlane topHolePlane,Edge cylinderEdge, out Face topHoleFace, out SketchCircle topSketchCircle,double positionX,double postionY)
        {
         
            PlanarSketch osketch = Definition.Sketches.Add(topHolePlane);
            SketchCircle circle = (SketchCircle)osketch.AddByProjectingEntity(cylinderEdge);
            Point2d p = circle.CenterSketchPoint.Geometry;

            circle.Construction = true; 
            topSketchCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(p.X+positionX,p.Y+postionY), holeRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            ExtrudeFeature feature = Definition.Features.ExtrudeFeatures.Add(ex);
            topHoleFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.Faces.GetEnumerator());

        }
        private ExtrudeFeature CreateTopHolePipe(WorkPlane topHolePlane, Face topHoleFace, double pipeLenght, double thickness,double height)
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
            ex.SetDistanceExtent(UsMM(par.DoorRadius), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           ExtrudeFeature f1= Definition.Features.ExtrudeFeatures.Add(ex);


            PlanarSketch tsketch = Definition.Sketches.Add(topHolePlane);
            SketchEntity circle1 = tsketch.AddByProjectingEntity(circle);
            objc.Clear();
            objc.Add(circle1);
            tsketch.OffsetSketchEntitiesUsingDistance(objc, thickness, false);
            // SketchCircle circle2 = osketch.SketchCircles.AddByCenterRadius(circle.CenterSketchPoint, outRadius);
            Profile pro1 = tsketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro1)
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

            if (pipeLenght == height)
            {
                return f1;
            }
            if (pipeLenght > height)
            {
                ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro1, PartFeatureOperationEnum.kJoinOperation);
                def.SetDistanceExtent(pipeLenght - height, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                return Definition.Features.ExtrudeFeatures.Add(def);
            }
            else
            {
                ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro1, PartFeatureOperationEnum.kCutOperation);
                def.SetDistanceExtent(height - pipeLenght, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                return Definition.Features.ExtrudeFeatures.Add(def);
            }
        }
        private ExtrudeFeature CreateTopFlance(Face plane, SketchCircle topInCircle, double flachOutRadius, double flachThickness)
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
        #endregion
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
        #region 创建边孔
        /// <summary>
        /// 创建侧边孔工作面
        /// </summary>
        /// <param name="planeFace">罐门口平面</param>
        /// <param name="CylinderFace">罐门垂直圆柱面</param>
        private void CreatePlanes(Face planeFace, Face CylinderFace)
        {

            PlanarSketch osketch = Definition.Sketches.Add(planeFace, true);
            osketch.Visible = false;
            List<SketchCircle> circles = InventorTool.GetCollectionFromIEnumerator<SketchCircle>(osketch.SketchCircles.GetEnumerator());
            SketchLine line = osketch.SketchLines.AddByTwoPoints(circles[1].CenterSketchPoint, InventorTool.TranGeo.CreatePoint2d(10, 0));
            line.Construction = true;
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)line);
            foreach (var item in par.SideHoles)
            {
                if (_sidePlanes.ContainsKey(item.PositionAngle)) continue;
                SketchPoint p = osketch.SketchPoints.Add(InventorTool.CreatePoint2d(1, 1));
                osketch.GeometricConstraints.AddCoincident((SketchEntity)p, (SketchEntity)circles[0]);
                ThreePointAngleDimConstraint dim = osketch.DimensionConstraints.AddThreePointAngle(p, circles[0].CenterSketchPoint, line.EndSketchPoint, p.Geometry);
                dim.Parameter.Value = item.PositionAngle / 180 * Math.PI;
                dim.Visible = false;

                WorkPlane plane = Definition.WorkPlanes.AddByPointAndTangent(p, CylinderFace, true);
                _sidePlanes.Add(item.PositionAngle, plane);
            }
        }

        private void CreateSideHole(WorkPlane plane, Face DistanceFace, WorkAxis axis, ParSideHole parHole)
        {
            #region
            double x, y;
            if (parHole.PositionAngle > 0 && parHole.PositionAngle <= 180)
            {
                x = 1;
                y = 1;
                if (parHole.HoleOffset < 0)
                {
                    y = -1;
                }
            }
            else
            {
                x = -1;
                y = -1;
                if (parHole.HoleOffset < 0)
                {
                    y = 1;
                }
            }
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.Visible = false;
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(DistanceFace.Edges.GetEnumerator());
            SketchLine line = (SketchLine)osketch.AddByProjectingEntity(edges[1]); //罐口的位置
            line.Construction = true;
            SketchLine centerLine = (SketchLine)osketch.AddByProjectingEntity(axis);//中心辅助线
            centerLine.Construction = true;
            SketchPoint origin = osketch.SketchPoints.Add(InventorTool.Origin); //边缘中心点 辅助点
            osketch.GeometricConstraints.AddCoincident((SketchEntity)line, (SketchEntity)origin);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)centerLine, (SketchEntity)origin);
            SketchPoint holeCenter = osketch.SketchPoints.Add(InventorTool.CreatePoint2d(x, y));
            TwoPointDistanceDimConstraint offSetDim = osketch.DimensionConstraints.AddTwoPointDistance(origin, holeCenter, DimensionOrientationEnum.kVerticalDim, holeCenter.Geometry);
            offSetDim.Parameter.Value = Math.Abs(UsMM(parHole.HoleOffset));
            TwoPointDistanceDimConstraint distanceDim = osketch.DimensionConstraints.AddTwoPointDistance(origin, holeCenter, DimensionOrientationEnum.kHorizontalDim, holeCenter.Geometry);
            distanceDim.Parameter.Value = UsMM(parHole.PositionDistance);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(holeCenter);
            SketchHolePlacementDefinition HolePlace = Definition.Features.HoleFeatures.CreateSketchPlacementDefinition(objc);
            HoleFeature hole = Definition.Features.HoleFeatures.AddDrilledByDistanceExtent(HolePlace, parHole.ParFlanch.D6 + "mm", par.InRadius.Value + par.Thickness.Value + "mm", PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            #endregion
            CreateSideHolePipe(plane, holeCenter, parHole);
            //SketchLine positionLine=  osketch.SketchLines.AddByTwoPoints(holeCenter, InventorTool.Origin);
            //  osketch.GeometricConstraints.AddCoincident((SketchEntity)positionLine.EndSketchPoint, (SketchEntity)line);
            //  osketch.GeometricConstraints.AddPerpendicular((SketchEntity)positionLine, (SketchEntity)line);
            //  positionLine.Construction = true;
            //  WorkPlane pipePlane = Definition.WorkPlanes.AddByLinePlaneAndAngle(positionLine, plane, Math.PI / 2);
            //  Face holeSideFace = InventorTool.GetFirstFromIEnumerator<Face>(hole.SideFaces.GetEnumerator());
            //  WorkAxis holeAxis = Definition.WorkAxes.AddByRevolvedFace(holeSideFace, true);
            //  PlanarSketch pipeSketch = Definition.Sketches.AddWithOrientation(pipePlane, positionLine, true, false, holeCenter, true);
            //  CreateSideHolePipe(pipeSketch, edges[1]);
        }
        private void CreateSideHolePipe(WorkPlane plane, SketchPoint holeCenter, ParSideHole parHole)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            osketch.Visible = false;
            SketchPoint center = (SketchPoint)osketch.AddByProjectingEntity(holeCenter);
            SketchCircle circle1 = osketch.SketchCircles.AddByCenterRadius(center, UsMM(parHole.ParFlanch.D6/2));
            SketchCircle circle2 = osketch.SketchCircles.AddByCenterRadius(center, UsMM(parHole.ParFlanch.D6/2 + parHole.PipeThickness));
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
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.InRadius.Value + par.Thickness.Value), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);

            double a = UsMM(par.InRadius.Value + par.Thickness.Value);
            double b = UsMM(par.DoorRadius + par.Thickness.Value);
            double y = UsMM(parHole.PositionDistance);
            double x = Math.Pow(Math.Pow(a, 2) * (1 - Math.Pow(y, 2) / Math.Pow(b, 2)), 0.5);
            double length = (a - x);
            PlanarSketch PipeSketch = Definition.Sketches.Add(plane);
            PipeSketch.Visible = false;
            PipeSketch.AddByProjectingEntity(circle1);
            PipeSketch.AddByProjectingEntity(circle2);
            Profile pro2 = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro2)
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
            ExtrudeDefinition def2;
            if (UsMM(parHole.PipeLenght) > length)
            {

                def2 = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro2, PartFeatureOperationEnum.kJoinOperation);
                def2.SetDistanceExtent(UsMM(parHole.PipeLenght) - length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);

            }
            else
            {
                def2 = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro2, PartFeatureOperationEnum.kCutOperation);
                def2.SetDistanceExtent(length - UsMM(parHole.PipeLenght), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

            }
            ExtrudeFeature pipe = Definition.Features.ExtrudeFeatures.Add(def2);
            Face flachPlane = InventorTool.GetFirstFromIEnumerator<Face>(pipe.EndFaces.GetEnumerator());
            ExtrudeFeature flach = CreateTopFlance(flachPlane, circle1, UsMM(parHole.ParFlanch.D1 / 2), UsMM(parHole.ParFlanch.H));
            Face flachEndFace = InventorTool.GetFirstFromIEnumerator<Face>(flach.EndFaces.GetEnumerator());
            List<Edge> flachEdges = InventorTool.GetCollectionFromIEnumerator<Edge>(flachEndFace.Edges.GetEnumerator());
            if (flachEdges.Count != 2) return;
            CreateFlanceGroove(flachEndFace, circle1, UsMM(parHole.ParFlanch.D2 / 2));
            SurfaceTypeEnum t1 = flachEndFace.SurfaceType;
            CreateFlanceScrew(flachEndFace, circle1, parHole.ParFlanch.N, UsMM(parHole.ParFlanch.C / 2), UsMM(parHole.ParFlanch.D0 / 2), UsMM(parHole.ParFlanch.H));
        }
        #endregion
        #endregion
        private void CreateClear(SketchEllipticalArc arc)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchEllipticalArc arc_P = (SketchEllipticalArc)osketch.AddByProjectingEntity(arc);
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(arc_P.CenterSketchPoint, arc_P.StartSketchPoint);
            SketchLine line2 = osketch.SketchLines.AddByTwoPoints(arc_P.CenterSketchPoint, arc_P.EndSketchPoint);
            Profile profile = osketch.Profiles.AddForSolid();
            RevolveFeature revolve = Definition.Features.RevolveFeatures.AddFull(profile, line1, PartFeatureOperationEnum.kCutOperation);
            List<SurfaceBody> bodys = InventorTool.GetCollectionFromIEnumerator<SurfaceBody>(Definition.SurfaceBodies.GetEnumerator());
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            bodys.ForEach(a => objc.Add(a));
            revolve.SetAffectedBodies(objc);
        }
        public override bool CheckParamete()
        {
            if (!CheckParZero()) return false;
            if (par.FlanchWidth < par.Thickness.Value)
            {
                ParErrorChanged(this, "法兰的宽度小于罐体的厚度");
                return false;
            }

            return true;
        }

    }
}
