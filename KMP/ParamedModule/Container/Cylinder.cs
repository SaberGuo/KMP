using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using KMP.Interface.Model.Container;
using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule.Container
{
  
    public  class Cylinder: PartModulebase
    {
       internal ParCylinder par=new ParCylinder();
        Dictionary<double,WorkPlane> _planes = new Dictionary<double, WorkPlane>();
        
        public Cylinder(PassedParameter InRadius,PassedParameter Thickness) :base()
        {
            this.Parameter = par;
            init();
            this.par.InRadius = InRadius;
            this.par.Thickness = Thickness;
        }
        private void init()
        {
           // par.InRadius.Value = 1400;
            par.CapRadius = 700;
            
            par.Length = 5000;
            par.RibWidth = 4;
            par.RibHeight = 4;
            par.RibBraceHeight = 2;
            par.RibBraceWidth = 2;
            par.RibNumber = 3;
            par.RibFirstDistance = 1000;
            par.FlanchWidth = 40;
            ParFlanch parflanch1 = new ParFlanch() { H = 2,D1=500,D2=450,D=12,D0=480,N=6 };
            ParCylinderHole hole = new ParCylinderHole() { HoleOffset=300,PositionAngle=90,PositionDistance=500,PipeLenght=300,HoleRadius=100};
            ParCylinderHole hole1 = new ParCylinderHole() { HoleOffset =-300, PositionAngle = 90, PositionDistance = 500, PipeLenght = 300, HoleRadius = 100 };
            ParCylinderHole hole2 = new ParCylinderHole() { HoleOffset = 0, PositionAngle = 90, PositionDistance = 1000, PipeLenght = 300, HoleRadius = 200 };
            ParCylinderHole hole3 = new ParCylinderHole() { HoleOffset = 0, PositionAngle = 90, PositionDistance = 2000, PipeLenght = 300, HoleRadius = 150 };
            hole.ParFlanch = parflanch1;
            hole1.ParFlanch = parflanch1;
            hole2.ParFlanch = parflanch1;
            hole3.ParFlanch = parflanch1;
            par.ParHoles.Add(hole);
            par.ParHoles.Add(hole1);
            par.ParHoles.Add(hole2);
            par.ParHoles.Add(hole3);
            
        }
        public override void CreateModule()
        {
           // if (!CheckParamete()) return;
            CreateDoc();
          RevolveFeature cyling= CreateCyling(UsMM( par.CapRadius), UsMM(par.InRadius.Value), UsMM(par.Length ), UsMM(par.Thickness.Value ), UsMM(par.RibFirstDistance ));
            cyling.Name = "Cylinder";
            List<Face> sideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(cyling.SideFaces.GetEnumerator());
            List<Edge> outFaceEdges = InventorTool.GetCollectionFromIEnumerator<Edge>(sideFaces[2].Edges.GetEnumerator());
            //0 外侧面，4.罐口面
            WorkAxis Axis = Definition.WorkAxes.AddByLine(cyling.AxisEntity); //外侧面的轴
            Axis.Name = "CylinderAxis";
            Axis.Visible = false;
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateH";
            Definition.iMateDefinitions.AddMateiMateDefinition(sideFaces[4],0).Name = "mateK";//罐口面
         
            CreatePlanes(sideFaces); //创建孔平面
            foreach (var item in par.ParHoles)  //创建孔、短管、法兰
            {
                WorkPlane plane = _planes[item.PositionAngle];
                if (plane == null) continue;
          
                CreateHole(plane, sideFaces[4], Axis,item, outFaceEdges[1]); 
            }
            ClearResidue(sideFaces[4], sideFaces[0], UsMM(par.Length));
            SaveDoc();
        }
        /// <summary>
        /// 创建罐本体和加强筋
        /// </summary>
        /// <param name="capRadius">封闭门深度</param>
        /// <param name="inRadius">罐内半径</param>
        /// <param name="length">罐体长度</param>
        /// <param name="thickness">罐体厚度</param>
        /// <param name="RibFirstDistance">加强筋第一个距离罐口位置</param>
        /// <returns></returns>
        private RevolveFeature CreateCyling(double capRadius,double inRadius,double length,double thickness,double RibFirstDistance)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchEllipticalArc Arc1, Arc2;
            SketchLine Line1, Line2;
            CreateLines(osketch, out Arc1, out Line1, capRadius, inRadius, length);

       
            CreateLines(osketch, out Arc2, out Line2, capRadius + thickness, inRadius + thickness, length);
            SketchLine Line3 = osketch.SketchLines.AddByTwoPoints(Arc1.StartSketchPoint, Arc2.StartSketchPoint);
            SketchLine Line4 = osketch.SketchLines.AddByTwoPoints(Line1.EndSketchPoint, Line2.EndSketchPoint);

            osketch.GeometricConstraints.AddHorizontalAlign(Arc1.StartSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddVerticalAlign(Arc1.EndSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)Line3);
            osketch.GeometricConstraints.AddVertical((SketchEntity)Line4);
            osketch.GeometricConstraints.AddEqualLength(Line3, Line4);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)Arc1, (SketchEntity)Arc2);
            // osketch.GeometricConstraints.AddCoincident((SketchEntity)InventorTool.Origin, (SketchEntity)Arc1.CenterSketchPoint);
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, true, InventorTool.TranGeo.CreatePoint2d(-capRadius / 2, 0));
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, false, InventorTool.TranGeo.CreatePoint2d(0, -inRadius / 2));
            Point2d p = InventorTool.TranGeo.CreatePoint2d((Line1.StartSketchPoint.Geometry.X + Line1.EndSketchPoint.Geometry.X) / 2, (Line1.StartSketchPoint.Geometry.Y + Line1.EndSketchPoint.Geometry.Y) / 2 + 1);
            osketch.DimensionConstraints.AddTwoPointDistance(Line1.StartSketchPoint, Line1.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);
            p = InventorTool.TranGeo.CreatePoint2d((Line4.StartSketchPoint.Geometry.X + Line4.EndSketchPoint.Geometry.X) / 2 + 1, (Line4.StartSketchPoint.Geometry.Y + Line4.EndSketchPoint.Geometry.Y) / 2);
            osketch.DimensionConstraints.AddTwoPointDistance(Line4.StartSketchPoint, Line4.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);

            CreateRibs(osketch, Line2,length, RibFirstDistance);  //创建加强筋
            SketchEntitiesEnumerator entities = InventorTool.CreateRangle(osketch, thickness, par.FlanchWidth/10);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            osketch.GeometricConstraints.AddCollinear((SketchEntity)lines[3], (SketchEntity)Line4);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            lines.ForEach(a => objc.Add(a));
            osketch.MoveSketchObjects(objc, lines[3].StartSketchPoint.Geometry.VectorTo(Line4.StartSketchPoint.Geometry));
            Profile profile = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in profile)
            {
                foreach (ProfileEntity sub in item)
                {
                    ObjectTypeEnum enum1 = sub.Type;
                    if (sub.SketchEntity == Arc1 || sub.SketchEntity == Arc2||sub.SketchEntity==lines[2] || sub.SketchEntity == lines[1])
                    {
                        item.AddsMaterial = false;
                        break;
                    }
                    else
                    {
                        item.AddsMaterial = true;
                    }
                }
            }
            #region 创建本体
            RevolveFeature revolve = Definition.Features.RevolveFeatures.AddFull(profile, Line3, PartFeatureOperationEnum.kNewBodyOperation);
            PlanarSketch sketch1 = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            sketch1.AddByProjectingEntity(Arc1);
            sketch1.AddByProjectingEntity(Arc2);
            sketch1.AddByProjectingEntity(Line1);
            sketch1.AddByProjectingEntity(Line2);
            sketch1.AddByProjectingEntity(lines[0]);
            sketch1.AddByProjectingEntity(lines[1]);
            sketch1.AddByProjectingEntity(lines[2]);
            sketch1.AddByProjectingEntity(lines[3]);
            SketchEntity ProjectiongLine3 = sketch1.AddByProjectingEntity(Line3);
            sketch1.AddByProjectingEntity(Line4);
            Profile profile1 = sketch1.Profiles.AddForSolid();
         return   Definition.Features.RevolveFeatures.AddFull(profile1, ProjectiongLine3, PartFeatureOperationEnum.kNewBodyOperation);
            #endregion
        }

        /// <summary>
        /// 创建椭圆线段和直线段
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="oEllipticalArc"></param>
        /// <param name="oLine"></param>
        /// <param name="RadiusX"></param>
        /// <param name="RadiusY"></param>
        /// <param name="Length"></param>
        private void CreateLines(PlanarSketch osketch,out SketchEllipticalArc oEllipticalArc, out SketchLine oLine,double RadiusX,double RadiusY,double Length)
        {
             oEllipticalArc = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, RadiusX, RadiusY, 0, Math.PI / 2);
             oLine = osketch.SketchLines.AddByTwoPoints(oEllipticalArc.EndSketchPoint.Geometry, InventorTool.TranGeo.CreatePoint2d(Length, oEllipticalArc.EndSketchPoint.Geometry.Y));
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)oLine);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)oEllipticalArc, (SketchEntity)oLine.StartSketchPoint);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)oEllipticalArc.EndSketchPoint, (SketchEntity)oLine);
            osketch.GeometricConstraints.AddTangent((SketchEntity)oEllipticalArc, (SketchEntity)oLine);

            //osketch.DimensionConstraints.AddArcLength((SketchEntity)oEllipticalArc, InventorTool.Origin);
        }
        /// <summary>
        /// 偏移
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="osketch"></param>
        /// <param name="source"></param>
        /// <param name="distance"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        private T offsetLine<T>(PlanarSketch osketch,T source,double distance,bool Direction)
        {
            ObjectCollection obj = InventorTool.Inventor.TransientObjects.CreateObjectCollection();
            obj.Add(source);
          SketchEntitiesEnumerator Entities=  osketch.OffsetSketchEntitiesUsingDistance(obj, distance, Direction,true,true);
          return  InventorTool.GetFirstFromIEnumerator<T>(Entities.GetEnumerator());
        }
        /// <summary>
        /// 创建多个加强筋
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="line"></param>
        private void CreateRibs(PlanarSketch osketch,SketchLine line,double length,double RibFirstDistance)
        {
            double distance = (length - RibFirstDistance) / par.RibNumber ;
            for (int i = 0; i < par.RibNumber; i++)
            {
                SketchLine L;
                CreateRib(osketch,out L);
                osketch.GeometricConstraints.AddCollinear((SketchEntity)line, (SketchEntity)L);
                CreateTwoPointDistanceConstraint(osketch, line.EndSketchPoint, L.EndSketchPoint, distance*i+ RibFirstDistance);
              
            }
        }
        /// <summary>
        /// 创建单个加强筋
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="L"></param>
        private void CreateRib(PlanarSketch osketch,out SketchLine L)
        {
            List<SketchLine> Lines = new List<SketchLine>();
            #region
            Point2d p1 = InventorTool.TranGeo.CreatePoint2d(3, 3);
            Point2d p2 = InventorTool.TranGeo.CreatePoint2d(3, 2);
            Point2d p3 = InventorTool.TranGeo.CreatePoint2d(2, 2);
            Point2d p4 = InventorTool.TranGeo.CreatePoint2d(2, 1);
            Point2d p5 = InventorTool.TranGeo.CreatePoint2d(3, 1);
            Point2d p6 = InventorTool.TranGeo.CreatePoint2d(3, 0);
            Point2d p7 = InventorTool.TranGeo.CreatePoint2d(0, 0);
            Point2d p8 = InventorTool.TranGeo.CreatePoint2d(0, 1);
            Point2d p9 = InventorTool.TranGeo.CreatePoint2d(1, 1);
            Point2d p10 = InventorTool.TranGeo.CreatePoint2d(1, 2);
            Point2d p11 = InventorTool.TranGeo.CreatePoint2d(0, 2);
            Point2d p12 = InventorTool.TranGeo.CreatePoint2d(0, 3);

            L = osketch.SketchLines.AddByTwoPoints(p12, p1);
            SketchLine L1 = osketch.SketchLines.AddByTwoPoints(p1, p2);
            SketchLine L2 = osketch.SketchLines.AddByTwoPoints(p2, p3);
            SketchLine L3 = osketch.SketchLines.AddByTwoPoints(p3, p4);
            SketchLine L4 = osketch.SketchLines.AddByTwoPoints(p4, p5);
            SketchLine L5 = osketch.SketchLines.AddByTwoPoints(p5, p6);
            SketchLine L6 = osketch.SketchLines.AddByTwoPoints(p6, p7); //总宽
            SketchLine L7 = osketch.SketchLines.AddByTwoPoints(p7, p8);
            SketchLine L8 = osketch.SketchLines.AddByTwoPoints(p8, p9);
            SketchLine L9 = osketch.SketchLines.AddByTwoPoints(p9, p10);
            SketchLine L10 = osketch.SketchLines.AddByTwoPoints(p10, p11);
            SketchLine L11 = osketch.SketchLines.AddByTwoPoints(p11, p12);
            Lines.Add(L);
            Lines.Add(L1);
            Lines.Add(L2);
            Lines.Add(L3);
            Lines.Add(L4);
            Lines.Add(L5);
            Lines.Add(L6);
            Lines.Add(L7);
            Lines.Add(L8);
            Lines.Add(L9);
            Lines.Add(L10);
            Lines.Add(L11);
            #endregion
           InventorTool.CreateTwoPointCoinCident(osketch, L11, L);
            for (int i = 1; i < Lines.Count; i++)
            {
               InventorTool.CreateTwoPointCoinCident(osketch, Lines[i], Lines[i - 1]);
                osketch.GeometricConstraints.AddPerpendicular((SketchEntity)Lines[i], (SketchEntity)Lines[i - 1]);
            }

            osketch.GeometricConstraints.AddEqualLength(L2, L4);
            osketch.GeometricConstraints.AddEqualLength(L1, L5);
            osketch.GeometricConstraints.AddEqualLength(L8, L10);
            osketch.GeometricConstraints.AddEqualLength(L7, L11);
            osketch.GeometricConstraints.AddEqualLength(L2, L8);
            osketch.GeometricConstraints.AddEqualLength(L1, L11);

            CreateTwoPointDistanceConstraint(osketch, L6.StartSketchPoint, L6.EndSketchPoint, UsMM(par.RibWidth));
            CreateTwoPointDistanceConstraint(osketch, L6.EndSketchPoint, L11.EndSketchPoint, UsMM(par.RibHeight));
            CreateTwoPointDistanceConstraint(osketch, L3.StartSketchPoint, L3.EndSketchPoint, UsMM(par.RibBraceHeight));
            CreateTwoPointDistanceConstraint(osketch, L2.EndSketchPoint, L10.StartSketchPoint, UsMM(par.RibBraceWidth));
        }
        /// <summary>
        /// 创建两点间距离约束
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="value"></param>
        void CreateTwoPointDistanceConstraint(PlanarSketch osketch,SketchPoint p1,SketchPoint p2,double value)
        {
               Point2d p = InventorTool.TranGeo.CreatePoint2d((p1.Geometry.X + p2.Geometry.X) / 2+1, (p1.Geometry.Y + p2.Geometry.Y) / 2+1);
            TwoPointDistanceDimConstraint Constraint1 = osketch.DimensionConstraints.AddTwoPointDistance(p1, p2, DimensionOrientationEnum.kAlignedDim, p);
            Constraint1.Parameter.Value = value;
        }
        /// <summary>
        /// 创建开孔平面
        /// </summary>
        /// <param name="sideFaces"></param>
        private void CreatePlanes(List<Face> sideFaces)
        {
            PlanarSketch osketch = Definition.Sketches.Add(sideFaces[4]);
            osketch.Visible = false;
            List<Face> cylindFaces = sideFaces.Where(a => a.SurfaceType == SurfaceTypeEnum.kCylinderSurface).ToList();
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(sideFaces[2].Edges.GetEnumerator());
           SketchCircle cycle= (SketchCircle) osketch.AddByProjectingEntity(edges[1]);
            SketchLine line = osketch.SketchLines.AddByTwoPoints(cycle.CenterSketchPoint, InventorTool.TranGeo.CreatePoint2d(10,0));
            line.Construction = true;
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)line);

            foreach (var item in par.ParHoles)
            {
                if (_planes.ContainsKey(item.PositionAngle)) continue;
               SketchPoint p= osketch.SketchPoints.Add(InventorTool.CreatePoint2d(1,1));
                osketch.GeometricConstraints.AddCoincident((SketchEntity)p, (SketchEntity)cycle);
              ThreePointAngleDimConstraint dim=  osketch.DimensionConstraints.AddThreePointAngle(p, cycle.CenterSketchPoint, line.EndSketchPoint,p.Geometry);
                dim.Parameter.Value = item.PositionAngle / 180 * Math.PI;
                dim.Visible = false;
                
                WorkPlane plane = Definition.WorkPlanes.AddByPointAndTangent(p, cylindFaces[0]);
                _planes.Add(item.PositionAngle, plane);
            }
           
 
        }
        /// <summary>
        /// 开孔
        /// </summary>
        /// <param name="plane">开孔的平面</param>
        /// <param name="DistanceFace">罐口面</param>
        /// <param name="axis">罐中心轴</param>
        /// <param name="cyliderSideFace">罐外侧面</param>
        private void CreateHole(WorkPlane plane,Face DistanceFace,WorkAxis axis,ParCylinderHole parHole,Edge outFaceEdge)
        {
            #region 创建孔
            double x, y;
          if(parHole.PositionAngle >= 0 && parHole.PositionAngle < 180)
            {
                x = 1;
                y = 1;
                if(parHole.HoleOffset<0)
                {
                    y = -1;
                }
            }
            else 
            {
                x = -1;
                y = -1;
                if(parHole.HoleOffset<0)
                {
                    y = 1;
                }
            }

         
            PlanarSketch osketch= Definition.Sketches.Add(plane);
            Edge edge = InventorTool.GetFirstFromIEnumerator<Edge>(DistanceFace.Edges.GetEnumerator());
          SketchLine line= (SketchLine)osketch.AddByProjectingEntity(edge);
            line.Construction = true;
            SketchLine centerLine =(SketchLine) osketch.AddByProjectingEntity(axis);
            centerLine.Construction = true;
            SketchPoint origin = osketch.SketchPoints.Add(InventorTool.Origin); //边缘中心点 辅助点
            osketch.GeometricConstraints.AddCoincident((SketchEntity)line,(SketchEntity)origin);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)centerLine, (SketchEntity)origin);
            SketchPoint holeCenter = osketch.SketchPoints.Add(InventorTool.CreatePoint2d(x,y));
          TwoPointDistanceDimConstraint offSetDim=  osketch.DimensionConstraints.AddTwoPointDistance(origin, holeCenter, DimensionOrientationEnum.kVerticalDim, holeCenter.Geometry);
            offSetDim.Parameter.Value = Math.Abs(parHole.HoleOffset) / 10;
            TwoPointDistanceDimConstraint distanceDim = osketch.DimensionConstraints.AddTwoPointDistance(origin, holeCenter, DimensionOrientationEnum.kHorizontalDim, holeCenter.Geometry);
            distanceDim.Parameter.Value = parHole.PositionDistance / 10;
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(holeCenter);
            SketchHolePlacementDefinition HolePlace= Definition.Features.HoleFeatures.CreateSketchPlacementDefinition(objc);
           HoleFeature hole=  Definition.Features.HoleFeatures.AddDrilledByDistanceExtent(HolePlace, parHole.HoleRadius * 2 +"mm", par.InRadius.Value + par.Thickness.Value +"mm", PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            // Definition.Features.HoleFeatures.AddDrilledByToFaceExtent(HolePlace, parHole.HoleRadius * 2, holeEndFace, true);
            #endregion
            Face holeSideFace = InventorTool.GetFirstFromIEnumerator<Face>(hole.SideFaces.GetEnumerator());
            WorkAxis holeAxis = Definition.WorkAxes.AddByRevolvedFace(holeSideFace,true);
           
          RevolveFeature pipe=  CreatePipe(DistanceFace, outFaceEdge, holeCenter, UsMM(parHole.PipeLenght), UsMM(parHole.HoleRadius), UsMM(parHole.ParFlanch.H), UsMM(parHole.HoleOffset),parHole.PositionAngle,holeAxis);
            SketchCircle flanchInCircle;
          ExtrudeFeature flanch=  CreateFlance(pipe,UsMM(parHole.ParFlanch.D1/2),UsMM(parHole.ParFlanch.H),out flanchInCircle);
            if (flanch == null) return;
            Face flanchEndFace = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
            CreateFlanceGroove(flanchEndFace,flanchInCircle,UsMM(parHole.ParFlanch.D2/2));
            CreateFlanceScrew(flanchEndFace,flanchInCircle,parHole.ParFlanch.N,UsMM(parHole.ParFlanch.D/2),UsMM(parHole.ParFlanch.D0/2),UsMM(parHole.ParFlanch.H));
        }
        /// <summary>
        /// 创建短管
        /// </summary>
        /// <param name="face1">罐界面</param>
        /// <param name="outFaceEdge">罐外圆侧面边</param>
        /// <param name="centerPoint">孔草图中心点</param>
        /// <param name="pipeLength">管长度</param>
        /// <param name="holeRadius">孔半径</param>
        /// <param name="offset">偏移</param>
        /// /// <param name="pipeThickness">管厚度</param>
        private RevolveFeature CreatePipe(Face face1, Edge outFaceEdge, SketchPoint centerPoint, double pipeLength, double holeRadius, double pipeThickness, double offset, double Angle, WorkAxis Axis)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndPoint(face1, centerPoint, true);
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, Axis, true, true, centerPoint);
            // List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(face2.Edges.GetEnumerator());
            SketchCircle circle = (SketchCircle)osketch.AddByProjectingEntity(outFaceEdge);
            double x = circle.CenterSketchPoint.Geometry.X;
            double y = circle.CenterSketchPoint.Geometry.Y;
            circle.Construction = true;
            SketchPoint point = (SketchPoint)osketch.AddByProjectingEntity(centerPoint);
            SketchLine AidedLine = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(-10, -10), point);
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)AidedLine);
            SketchPoint AidedPoint = osketch.SketchPoints.Add(InventorTool.Origin);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)AidedPoint, (SketchEntity)AidedLine);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)AidedPoint, (SketchEntity)circle);
            AidedLine.Construction = true;
            //SketchEntitiesEnumerator entitys= osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(1, 1), InventorTool.CreatePoint2d(2, 2));
            // List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entitys.GetEnumerator());
            // osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[0].StartSketchPoint, (SketchEntity)circle);
            // osketch.GeometricConstraints.AddCoincident((SketchEntity)lines[0].EndSketchPoint, (SketchEntity)circle);
            // osketch.GeometricConstraints.AddParallel((SketchEntity)lines[1], (SketchEntity)AidedLine);
            // osketch.GeometricConstraints.AddParallel((SketchEntity)lines[3], (SketchEntity)AidedLine);

            // InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, lines[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = pipeThickness;
            // InventorTool.AddTwoPointDistance(osketch, lines[2].EndSketchPoint, point, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = holeRadius;
            // InventorTool.AddTwoPointDistance(osketch, lines[2].EndSketchPoint, circle.CenterSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = circle.Radius + pipeLength;
            SketchLine line1, line2, line3;
            if (offset > 0)
            {
                line1 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(1000, 1000), InventorTool.CreatePoint2d(20000, 1000));
                line2 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(20000, 1000), InventorTool.CreatePoint2d(20000, 20000));
                line3 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(1000, 20000), InventorTool.CreatePoint2d(20000, 20000));
            }
            else
            {
                line1 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(10, -20000), InventorTool.CreatePoint2d(20000, -20000));
                line2 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(20000, -20000), InventorTool.CreatePoint2d(20000, -1000));
                line3 = osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(10, -1000), InventorTool.CreatePoint2d(20000, -1000));
            }

            InventorTool.CreateTwoPointCoinCident(osketch, line1, line1.EndSketchPoint, line2, line2.StartSketchPoint);
            InventorTool.CreateTwoPointCoinCident(osketch, line3, line3.EndSketchPoint, line2, line2.EndSketchPoint);
            InventorTool.AddTwoPointDistance(osketch, line2.StartSketchPoint, line2.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = pipeThickness;
            osketch.GeometricConstraints.AddCoincident((SketchEntity)line3.StartSketchPoint, (SketchEntity)circle);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)circle);
            osketch.GeometricConstraints.AddPerpendicular((SketchEntity)line2, (SketchEntity)AidedLine);
            osketch.GeometricConstraints.AddParallel((SketchEntity)line1, (SketchEntity)AidedLine);
            osketch.GeometricConstraints.AddParallel((SketchEntity)line3, (SketchEntity)AidedLine);

            // InventorTool.AddTwoPointDistance(osketch, line2.StartSketchPoint, line2.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = pipeThickness;
            if (offset > 0)
            {
                InventorTool.AddTwoPointDistance(osketch, line1.EndSketchPoint, point, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = holeRadius;
            }
            else
            {
                InventorTool.AddTwoPointDistance(osketch, line3.EndSketchPoint, point, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = holeRadius;
            }
          
            InventorTool.AddTwoPointDistance(osketch, line3.EndSketchPoint, AidedPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = pipeLength;
            SketchArc arc = osketch.SketchArcs.AddByCenterStartEndPoint(circle.CenterSketchPoint, line3.StartSketchPoint, line1.StartSketchPoint,false);
            osketch.UpdateProfiles();
            Profile pro=  osketch.Profiles.AddForSolid();

            foreach (ProfilePath item in pro)
            {
                if (item.Count > 2)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }

            }
          return  Definition.Features.RevolveFeatures.AddFull(pro, AidedLine, PartFeatureOperationEnum.kJoinOperation);
        }
        /// <summary>
        /// 创建法兰本体
        /// </summary>
        /// <param name="pipe"></param>
        private ExtrudeFeature CreateFlance(RevolveFeature pipe,double flachOutRadius,double flachThickness,out SketchCircle inCircle)
        {
          
            List<Face> pipeSideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(pipe.Faces.GetEnumerator());
            inCircle = null;
            List<Edge> edges=null;
            PlanarSketch osketch=null;
            foreach (var item in pipeSideFaces)
            {
                if (item.Edges.Count != 2) continue;
                try
                {
                    osketch = Definition.Sketches.Add(item);
                    edges = InventorTool.GetCollectionFromIEnumerator<Edge>(item.Edges.GetEnumerator());
                    break;
                }
                catch (Exception)
                {

                }
            }
            if (osketch == null||edges==null) return null;
            SketchCircle  circle1, circle2;
          circle1= (SketchCircle) osketch.AddByProjectingEntity(edges[0]);
          circle2= (SketchCircle)osketch.AddByProjectingEntity(edges[1]);
            if(circle1.Radius>circle2.Radius)
            {
                inCircle = circle2;
                circle1.Delete();
            }
            else
            {
                inCircle = circle1;
                circle2.Delete();
            }
            SketchCircle outCircle = osketch.SketchCircles.AddByCenterRadius(inCircle.CenterSketchPoint, flachOutRadius);
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
            ex.SetDistanceExtent(flachThickness, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(ex);

        }
        /// <summary>
        /// 创建法兰凹面
        /// </summary>
        private void CreateFlanceGroove(Face plane,SketchCircle sketchInCircle,double outRadius)
        {
          PlanarSketch osketch=  Definition.Sketches.Add(plane);
            SketchCircle inCircle = (SketchCircle)osketch.AddByProjectingEntity(sketchInCircle);
          SketchCircle outCircle=  osketch.SketchCircles.AddByCenterRadius(inCircle.CenterSketchPoint, outRadius);
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
        private void CreateFlanceScrew(Face plane,SketchCircle inCircle,double screwNumber,double ScrewRadius,double arrangeRadius,double flanchThickness)
        {
            double angle = 360 / screwNumber/180*Math.PI;
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle flanceInCircle = (SketchCircle)osketch.AddByProjectingEntity(inCircle);
            SketchCircle screwCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(0,arrangeRadius), ScrewRadius);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(screwCircle);
            for(int i=1;i<screwNumber;i++)
            {
                osketch.RotateSketchObjects(objc, flanceInCircle.CenterSketchPoint.Geometry, angle*i,true);
            }
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent( flanchThickness, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(ex);
        }
        private void ClearResidue(Face plance ,Face circle,double length)
        {
           List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(plance.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(plance);
            osketch.AddByProjectingEntity(edges[1]);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(ex);
        }
        public override bool CheckParamete()
        {
            if (!CommonTool.CheckParameterValue(par)) return false;
            if (par.FlanchWidth < par.Thickness.Value) return false;
            if (par.RibWidth <= par.RibBraceWidth) return false;
            if (par.RibHeight <= par.RibBraceHeight) return false;
            for(int i=0;i<par.ParHoles.Count;i++)
            {
                var item = par.ParHoles[i];
                if (item.HoleOffset > par.InRadius.Value - item.HoleRadius) return false;
                if (item.PositionDistance > par.Length - item.HoleRadius) return false;
                if (item.PositionAngle < 0 && item.PositionAngle > 360) return false;
               for(int j=i+1;j<par.ParHoles.Count;j++)
                {
                     if(item.HoleRadius==par.ParHoles[j].HoleRadius)
                    {
                        if(item.HoleOffset==par.ParHoles[j].HoleOffset&&item.PositionDistance==par.ParHoles[j].PositionDistance)
                        {
                            return false;
                        }
                    }
                }
                
            }
            return true;
        }
        ///// <summary>
        ///// 创建两条线关联
        ///// </summary>
        ///// <param name="osketch"></param>
        ///// <param name="line1">开始点</param>
        ///// <param name="line2">结束点</param>
        //void CreateTwoPointCoinCident(PlanarSketch osketch,SketchLine line1,SketchLine line2)
        //{
        //    if(line1.StartSketchPoint.Geometry.X==line2.StartSketchPoint.Geometry.X&& line1.StartSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
        //    }
        //    else if(line1.EndSketchPoint.Geometry.X == line2.StartSketchPoint.Geometry.X && line1.EndSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
        //    }
        //    else if(line1.StartSketchPoint.Geometry.X == line2.EndSketchPoint.Geometry.X && line1.StartSketchPoint.Geometry.Y == line2.EndSketchPoint.Geometry.Y)
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
        //    }
        //    else
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
        //    }
        

        //}
    }
}
