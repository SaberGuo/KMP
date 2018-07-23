using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using Infranstructure.Tool;
using System.IO;
namespace ParamedModule
{
  public abstract  class PartModulebase:ParamedModuleBase
    {
      internal  PartComponentDefinition Definition;
      internal  PartDocument Doc;
        public PartModulebase():base()
        {
            
        }
        public override string FullPath
        {
            get
            {
                return System.IO.Path.Combine(ModelPath, this.Name + ".ipt");
            }
        }
        protected void CreateDoc()
        {
             Doc = InventorTool.CreatePart();
            Doc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
            Definition = Doc.ComponentDefinition;
        }
        
        protected void SaveDoc()
        {
            Doc.FullFileName = this.FullPath;
            if (System.IO.File.Exists(Doc.FullFileName))
            {
                System.IO.File.Delete(Doc.FullFileName);
            }
            Doc.Save2();
           
          //  Doc.Close();
          
        }
        public virtual void DisPose()
        {
            Doc = null;
            Definition = null;
        }
        public override void CreateModule()
        {
            GeneratorProgress(this, "开始创建零件" + this.Name);
            DisPose();
            CloseSameNameDocment();
            CreateDoc();
            CreateSub();
            SaveDoc();
            GeneratorProgress(this, "结束创建零件" + this.Name);
        }
        public abstract void CreateSub();
        internal override void CloseSameNameDocment()
        {
            List<Document> list = InventorTool.GetCollectionFromIEnumerator<Document>(InventorTool.Inventor.Documents.GetEnumerator());
            List<Document> select = list.Where(a => a.DisplayName == (this.Name + ".ipt")).ToList();
            select.ForEach(a => a.Close(true));
        }
        internal static void AddDiameter(PlanarSketch osketch,SketchEntity entity,double Diameter)
        {
            Point2d p;
            if (entity.Type==ObjectTypeEnum.kSketchCircleObject)
            {
                p = ((SketchCircle)entity).CenterSketchPoint.Geometry;
                p = InventorTool.CreatePoint2d(p.X + Diameter / 2, p.Y);
            }
            else if(entity.Type==ObjectTypeEnum.kSketchArcObject)
            {
                p = ((SketchArc)entity).CenterSketchPoint.Geometry;
                p = InventorTool.CreatePoint2d(p.X + Diameter / 2, p.Y);
            }
            else
            {
                return;
            }

            osketch.DimensionConstraints.AddDiameter(entity, p).Parameter.Value = Diameter;
        }
    }
}
