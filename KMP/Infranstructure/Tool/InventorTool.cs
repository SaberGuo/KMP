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

      

        public static PartDocument CreatePart()
        {
            
            string TemplatePath = Inventor.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject,SystemOfMeasureEnum.kMetricSystemOfMeasure);
            return (PartDocument)(Inventor.Documents.Add(DocumentTypeEnum.kPartDocumentObject, TemplatePath));
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
