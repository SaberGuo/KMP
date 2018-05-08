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
                catch (Exception)
                {
                    //throw new ArgumentException("inventor 没有打开");

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
       
     
    }
}
