using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
namespace Infranstructure.Tool
{
    public class InventorTool
    {
       
        #region base unit
        private static Point2d origin;
        private static UnitVector2d right;
        private static UnitVector2d top;
        private static UnitVector2d left;
        private static UnitVector2d down;
        public static Point2d Origin
        {
            get
            {
                if (origin == null)
                {
                    origin = TranGeo.CreatePoint2d(0, 0);
                }
                return origin;
            }

        }

        public static UnitVector2d Right
        {
            get
            {
                if (right == null)
                {
                    right = TranGeo.CreateUnitVector2d(1, 0);
                }
                return right;
            }

        }

        public static UnitVector2d Top
        {
            get
            {
                if (top == null)
                {
                    top = TranGeo.CreateUnitVector2d(0, 1);
                }
                return top;
            }

        }

        public static UnitVector2d Left
        {
            get
            {
                if (left == null)
                {
                    left = TranGeo.CreateUnitVector2d(-1, 0);
                }
                return left;
            }

        }

        public static UnitVector2d Down
        {
            get
            {
                if (down == null)
                {
                    down = TranGeo.CreateUnitVector2d(0, -1);
                }
                return down;
            }

        }
        #endregion
        private static Inventor.Application inventor;
        public static Application Inventor
        {
            get
            {
                if (inventor != null) return inventor;
                try
                {
                    inventor = System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application") as Inventor.Application;
                }
                catch 
                {
                   

                }
                if (inventor == null)
                {
                    Type inventorAppType = System.Type.GetTypeFromProgID("Inventor.Application");
                    inventor = System.Activator.CreateInstance(inventorAppType) as Inventor.Application;

                   
                }
                return inventor;
            }


        }
       
        public static TransientGeometry TranGeo
        {
            get
            {
                return Inventor.TransientGeometry;
            }

       
        }

      public static EdgeCollection CreateEdgeCollection()
        {
            return Inventor.TransientObjects.CreateEdgeCollection();
        }
        public static ObjectCollection CreateObjectCollection()
        {
            return Inventor.TransientObjects.CreateObjectCollection();
        }
        public static PartDocument CreatePart()
        {
            
            string TemplatePath = Inventor.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject,SystemOfMeasureEnum.kMetricSystemOfMeasure);
            return (PartDocument)(Inventor.Documents.Add(DocumentTypeEnum.kPartDocumentObject, TemplatePath));
        }
        public static AssemblyDocument CreateAssembly()
        {

            string TemplatePath = Inventor.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure);
            return (AssemblyDocument)(Inventor.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, TemplatePath));
        }
        public static T GetFirstFromIEnumerator<T>(System.Collections.IEnumerator Source)
        {
            while(Source.MoveNext())
            {
                if(Source.Current is T)
                {
                    return (T)Source.Current;
                }
              
            }
            return default(T);
        }
        public static List<T> GetCollectionFromIEnumerator<T>(System.Collections.IEnumerator Source)
        {
            List<T> result = new List<T>();
            while (Source.MoveNext())
            {
                if(Source.Current is T)
                {
                    result.Add((T)Source.Current);
                }
               
            }
            return result;
        }
        /// <summary>
        /// 注释约束，添加两条线间角度约束
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
       public static void AddTwoLineAngle(PlanarSketch osketch,SketchLine line1,SketchLine line2)
        {
            Point2d p1 = line1.StartSketchPoint.Geometry;
            Point2d p2 = line1.EndSketchPoint.Geometry;
            Point2d p3 = line2.StartSketchPoint.Geometry;
            Point2d p4 = line2.EndSketchPoint.Geometry;
            XY p5 = new XY((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            XY p6 = new XY((p3.X + p4.X) / 2, (p3.Y + p4.Y) / 2);
            Point2d p7 = TranGeo.CreatePoint2d((p5.X + p6.X) / 2, (p5.Y + p6.Y) / 2);
            osketch.DimensionConstraints.AddTwoLineAngle(line1, line2, p7);

        }
        /// <summary>
        /// 注释约束，添加两点间距离约束
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        public static TwoPointDistanceDimConstraint AddTwoPointDistance(PlanarSketch osketch, SketchPoint point1, SketchPoint point2,int direction,DimensionOrientationEnum orientation)
        {
            Point2d p;
            switch (direction)
            {
                case 1:
                    p = TranGeo.CreatePoint2d((point1.Geometry.X + point2.Geometry.X) / 2 + 1, (point1.Geometry.Y + point2.Geometry.Y) / 2 + 1);
                    break;
                case -1:
                    p = TranGeo.CreatePoint2d((point1.Geometry.X + point2.Geometry.X) / 2 - 1, (point1.Geometry.Y + point2.Geometry.Y) / 2 - 1);
                    break;
                default:
                    p = TranGeo.CreatePoint2d((point1.Geometry.X + point2.Geometry.X) / 2, (point1.Geometry.Y + point2.Geometry.Y) / 2);
                    break;
            }
          
       
            return osketch.DimensionConstraints.AddTwoPointDistance(point1, point2,orientation, p);

        }
        /// <summary>
        /// 创建两条线定点相交约束
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        public static void CreateTwoPointCoinCident(PlanarSketch osketch, SketchLine line1, SketchLine line2)
        {
            if (line1.StartSketchPoint.Geometry.X == line2.StartSketchPoint.Geometry.X && line1.StartSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
            }
            else if (line1.EndSketchPoint.Geometry.X == line2.StartSketchPoint.Geometry.X && line1.EndSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
            }
            else if (line1.StartSketchPoint.Geometry.X == line2.EndSketchPoint.Geometry.X && line1.StartSketchPoint.Geometry.Y == line2.EndSketchPoint.Geometry.Y)
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
            }
            else
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
            }


        }
        /// <summary>
        /// 创建弧线段和线段顶点关联
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="line1">开始点</param>
        /// <param name="line2">结束点</param>
       public static void CreateTwoPointCoinCident(PlanarSketch osketch, SketchArc line1, SketchLine line2)
        {
            if (line1.StartSketchPoint.Geometry.X == line2.StartSketchPoint.Geometry.X && line1.StartSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
            }
            else if (line1.EndSketchPoint.Geometry.X == line2.StartSketchPoint.Geometry.X && line1.EndSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
            }
            else if (line1.StartSketchPoint.Geometry.X == line2.EndSketchPoint.Geometry.X && line1.StartSketchPoint.Geometry.Y == line2.EndSketchPoint.Geometry.Y)
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
            }
            else
            {
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
            }


        }
        public static void CreateTwoPointCoinCident(PlanarSketch osketch,SketchLine line1,SketchPoint p1,SketchLine line2,SketchPoint p2)
        {
            osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)p2);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)p1, (SketchEntity)line2);
        }
        /// <summary>
        /// 创建一个长方形示意图 下边线：0，右边线：1，逆时针顺序
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static SketchEntitiesEnumerator CreateRangle(PlanarSketch osketch, double length, double width)
        {
            SketchEntitiesEnumerator entities = osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.Origin, InventorTool.TranGeo.CreatePoint2d(2, 2));
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, lines[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = length;
            InventorTool.AddTwoPointDistance(osketch, lines[1].StartSketchPoint, lines[1].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = width;
            return entities;
        }
        /// <summary>
        /// 创建一个长方体
        /// </summary>
        /// <param name="partDef"></param>
        /// <param name="osketch"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ExtrudeFeature CreateBox(PartComponentDefinition partDef,PlanarSketch osketch, double length, double width,double height)
        {
            CreateRangle(osketch, length, width);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition extrudedef = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extrudedef.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return partDef.Features.ExtrudeFeatures.Add(extrudedef);

        }
        /// <summary>
        /// 创建一个带有槽型孔的长方体
        /// </summary>
        /// <param name="partDef"></param>
        /// <param name="osketch"></param>
        /// <param name="width"></param>
        /// <param name="lenght"></param>
        /// <param name="height"></param>
        /// <param name="holeCenterDistance">孔两个圆心距离</param>
        /// <param name="holeTopEdgeDistance">孔圆心到长方体边的最短距离</param>
        /// <param name="holeSideEdgeDistance">孔圆心到长方体边的最短距离</param>
        /// <param name="holeRadius">半圆半径</param>
        public static ExtrudeFeature  CreateBoxWithHole(PartComponentDefinition partDef,PlanarSketch osketch, double width, double lenght, double height,
        double holeCenterDistance, double holeTopEdgeDistance, double holeSideEdgeDistance, double holeRadius)
        {
            ExtrudeFeature block = InventorTool.CreateBox(partDef, osketch, lenght, width, height);
            Face FacePlane = InventorTool.GetFirstFromIEnumerator<Face>(block.StartFaces.GetEnumerator());
            List<Face> SideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(block.SideFaces.GetEnumerator());
            List<Edge> lines = InventorTool.GetCollectionFromIEnumerator<Edge>(FacePlane.Edges.GetEnumerator());
            // PlanarSketch holeSketch= partDef.Sketches.AddWithOrientation(FacePlane,lines[0],false,true,lines[0].StartVertex,true);
            PlanarSketch holeSketch = partDef.Sketches.Add(FacePlane);
            ExtrudeFeature hole = CreateHole(partDef, holeSketch, lines, holeCenterDistance, holeTopEdgeDistance, holeSideEdgeDistance, holeRadius);
            WorkPlane plane = partDef.WorkPlanes.AddByTwoPlanes(SideFaces[0], SideFaces[2]);
            WorkPlane plane1 = partDef.WorkPlanes.AddByTwoPlanes(SideFaces[1], SideFaces[3]);
            plane.Visible = false;
            plane1.Visible = false;
            ObjectCollection objects = InventorTool.Inventor.TransientObjects.CreateObjectCollection();
            objects.Add(hole);
            //MirrorFeatureDefinition mirrorDef = partDef.Features.MirrorFeatures.CreateDefinition(objects, plane);
            //MirrorFeature mirror = partDef.Features.MirrorFeatures.AddByDefinition(mirrorDef);
            //objects.Add(mirror);
            //MirrorFeatureDefinition mirrorDef2 = partDef.Features.MirrorFeatures.CreateDefinition(objects, plane1);
            //partDef.Features.MirrorFeatures.AddByDefinition(mirrorDef2);
            MirrorFeature mirror = partDef.Features.MirrorFeatures.Add(objects, plane);
            objects.Add(mirror);
            partDef.Features.MirrorFeatures.Add(objects, plane1);
            return block;
        }
        /// <summary>
        /// 创建一个槽型孔
        /// </summary>
        /// <param name="partDef"></param>
        /// <param name="osketch"></param>
        /// <param name="Edges"></param>
        /// <param name="holeCenterDistance">草型孔两个圆心距离</param>
        /// <param name="holeTopEdgeDistance">槽型孔的圆心到边距离</param>
        /// <param name="holeSideEdgeDistance">槽型孔的圆心到侧边距离</param>
        /// <param name="holeRadius">孔半圆半径</param>
        /// <returns></returns>
       public static ExtrudeFeature CreateHole(PartComponentDefinition partDef, PlanarSketch osketch, List<Edge> Edges, double holeCenterDistance,
            double holeTopEdgeDistance, double holeSideEdgeDistance, double holeRadius)
        {
            List<SketchLine> lines = new List<SketchLine>();
            foreach (var item in Edges)
            {
                SketchLine line = osketch.AddByProjectingEntity(item) as SketchLine;
                lines.Add(line);
            }


            SketchArc arc1 = osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.TranGeo.CreatePoint2d(-60, 50), 5, Math.PI / 2, Math.PI);
            SketchArc arc2 = osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.TranGeo.CreatePoint2d(-50, 50), 5, -Math.PI / 2, Math.PI);
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(arc1.StartSketchPoint, arc2.EndSketchPoint);
            SketchLine line2 = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc2.StartSketchPoint);
            //osketch.GeometricConstraints.AddEqualLength(line1, line2);
            osketch.GeometricConstraints.AddEqualRadius((SketchEntity)arc1, (SketchEntity)arc2);
            osketch.GeometricConstraints.AddParallel((SketchEntity)line1, (SketchEntity)line2);
            osketch.GeometricConstraints.AddTangent((SketchEntity)line1, (SketchEntity)arc2);
            osketch.GeometricConstraints.AddTangent((SketchEntity)line1, (SketchEntity)arc1);
            osketch.GeometricConstraints.AddTangent((SketchEntity)arc1, (SketchEntity)line2);
            osketch.DimensionConstraints.AddRadius((SketchEntity)arc2, arc2.StartSketchPoint.Geometry).Parameter.Value = holeRadius;
            InventorTool.AddTwoPointDistance(osketch, arc1.CenterSketchPoint, arc2.CenterSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = holeCenterDistance;

            InventorTool.AddTwoPointDistance(osketch, arc2.CenterSketchPoint, lines[0].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = holeTopEdgeDistance;
            InventorTool.AddTwoPointDistance(osketch, arc2.CenterSketchPoint, lines[0].StartSketchPoint, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = holeSideEdgeDistance;
            osketch.UpdateProfiles();
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                List<ProfileEntity> list = InventorTool.GetCollectionFromIEnumerator<ProfileEntity>(item.GetEnumerator());
                int count = list.Where(a => a.SketchEntity == arc1).Count();
                if (count > 0)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.Delete();
                }

            }
            ExtrudeDefinition ex = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

            return partDef.Features.ExtrudeFeatures.Add(ex);
        }
       public static Point2d CreatePoint2d(double x,double y)
        {
          return  TranGeo.CreatePoint2d(x, y);
        }
       public static SketchLine CreateSketchLine(PlanarSketch osketch)
        {
            return osketch.SketchLines.AddByTwoPoints(Origin, CreatePoint2d(0, 1));
        }

        public static ExtrudeFeature CreateFlance(Face plane, SketchCircle topInCircle, double flachOutRadius, double flachThickness,PartComponentDefinition Definition)
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
        public static ExtrudeFeature CreateFlanceGroove(Face plane, SketchCircle sketchInCircle, double outRadius, PartComponentDefinition Definition)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle inCircle = (SketchCircle)osketch.AddByProjectingEntity(sketchInCircle);
            SketchCircle outCircle = osketch.SketchCircles.AddByCenterRadius(inCircle.CenterSketchPoint, outRadius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(1 + "mm", PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
           return  Definition.Features.ExtrudeFeatures.Add(ex);
           
        }
        /// <summary>
        /// 创建法兰螺丝孔
        /// </summary>
        /// <param name="plane">定位面</param>
        /// <param name="inCircle">中心点定位圆</param>
        /// <param name="screwNumber">螺丝数量</param>
        /// <param name="ScrewRadius">孔半径</param>
        /// <param name="arrangeRadius">排版半径</param>
        public static ExtrudeFeature CreateFlanceScrew(Face plane, SketchCircle inCircle, double screwNumber, double ScrewRadius, double arrangeRadius, double flanchThickness, PartComponentDefinition Definition)
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
            ExtrudeDefinition ex = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetDistanceExtent(flanchThickness, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
           return  Definition.Features.ExtrudeFeatures.Add(ex);
        }

    }
    public struct XY
    {
      public  double X;
       public double Y;
        public XY(double x,double y)
        {
            X = x;
            Y = y;
        }
    }
}
