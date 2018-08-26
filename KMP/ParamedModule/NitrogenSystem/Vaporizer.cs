using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 汽化器
    /// </summary>
    [Export("Vaporizer", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Vaporizer : PartModulebase

    {
       public  ParVaporizer par = new ParVaporizer();
        public Vaporizer():base()
        {
            this.Parameter = par;
            init();
        }
        void init()
        {
            par.Width = 1900;
            par.Length = 2000;
            par.Height = 4470;
            par.GrooveWidth = 60;
            par.GrooveDepth = 50;
            par.GrooveStartWidth = 80;
            par.WGrooveNum = 10;
            par.HGrooveNum = 11;
            par.SurWidth = 100;
            par.SurHeight = 300;
            par.SurWidth2 = 220;
            par.SurDistanceEdge =220;
            par.GrooveBetween = 180;
        }
        public override bool CheckParamete()
        {
            par.WGrooveNum = Math.Floor((par.Width - par.GrooveStartWidth) / par.GrooveBetween);
            par.HGrooveNum = Math.Floor((par.Length - par.GrooveStartWidth) / par.GrooveBetween);
            if (par.GrooveBetween<=par.GrooveWidth)
            {
                return false;
            }
            if(par.Length<=(par.GrooveStartWidth+par.GrooveBetween*(par.HGrooveNum-1)+par.GrooveWidth))
            {
                return false;
            }
            if (par.Width <= (par.GrooveStartWidth + par.GrooveBetween * (par.WGrooveNum - 1) + par.GrooveWidth))
            {
                return false;
            }
            if(par.SurWidth2<=par.SurWidth)
            {
                return false;
            }
            if(par.SurDistanceEdge<=par.GrooveDepth)
            {
                return false;
            }
            if(par.SurWidth2>=par.Width/2)
            {
                return false;
            }
            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch oskech = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            ExtrudeFeature box = InventorTool.CreateBox(Definition, oskech, UsMM(par.Length), UsMM(par.Width), UsMM(par.Height));
            List<Face> BoxSFs = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            WorkPlane Wplane = Definition.WorkPlanes.AddByTwoPlanes(BoxSFs[0], BoxSFs[2],false);
            WorkPlane Lplane = Definition.WorkPlanes.AddByTwoPlanes(BoxSFs[1], BoxSFs[3],false);
            Wplane.Visible = false;
            Wplane.Name = "Flush";
            Lplane.Visible = false;
            Lplane.Name = "Mate";
            //WorkAxis Axis = Definition.WorkAxes.AddByTwoPlanes(Wplane, Lplane);
            //Axis.Name = "Axis";
            //Axis.Visible = false;
            #region 支架
            Face boxStartFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            ExtrudeFeature sur = CreateSur(boxStartFace, UsMM(par.SurWidth), UsMM(par.SurDistanceEdge), UsMM(par.SurHeight));
            Face EndSur = InventorTool.GetFirstFromIEnumerator<Face>(sur.EndFaces.GetEnumerator());
            ExtrudeFeature SurBottom= CreateSurBootom(EndSur, UsMM(par.SurWidth), UsMM(par.SurWidth2));
            SurBottom.Name = "Sur";
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(SurBottom);
            objc.Add(sur);
            MirrorFeature mirror= Definition.Features.MirrorFeatures.Add(objc, Wplane, false, PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Add(mirror);
            Definition.Features.MirrorFeatures.Add(objc, Lplane, false, PatternComputeTypeEnum.kAdjustToModelCompute);
            #endregion
            #region 一三面槽
            ExtrudeFeature groove =  CreateGroove(BoxSFs[0], UsMM(par.GrooveWidth), UsMM(par.Height), UsMM(par.GrooveDepth), UsMM(par.GrooveStartWidth),  0);
            objc.Clear();
            objc.Add(groove);
           RectangularPatternFeature pattern= Definition.Features.RectangularPatternFeatures.Add(objc, Definition.WorkAxes[3], false, par.WGrooveNum, UsMM(par.GrooveBetween), PatternSpacingTypeEnum.kDefault, null, null,true, null, null, PatternSpacingTypeEnum.kDefault, null, PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Clear();
            objc.Add(groove);
            objc.Add(pattern);
            Definition.Features.MirrorFeatures.Add(objc, Wplane, false, PatternComputeTypeEnum.kAdjustToModelCompute);
            #endregion
            #region 二四面槽
            ExtrudeFeature groove1 = CreateGroove(BoxSFs[1], UsMM(par.GrooveWidth), UsMM(par.Height), UsMM(par.GrooveDepth), UsMM(par.GrooveStartWidth), 0);
            objc.Clear();
           
            objc.Add(groove1);
            RectangularPatternFeature pattern1 = Definition.Features.RectangularPatternFeatures.Add(objc, Definition.WorkAxes[1], true, par.HGrooveNum, UsMM(par.GrooveBetween), PatternSpacingTypeEnum.kDefault, null, null, true, null, null, PatternSpacingTypeEnum.kDefault, null, PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Add(pattern1);
            Definition.Features.MirrorFeatures.Add(objc, Lplane, false, PatternComputeTypeEnum.kAdjustToModelCompute);
            #endregion
           
        }
        ExtrudeFeature CreateGroove(Face plane,double width,double height, double depth,double offset, int Num)
        {
            PlanarSketch oskech = Definition.Sketches.Add(plane,true);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(oskech.SketchEntities.GetEnumerator());
           SketchEntitiesEnumerator ract= oskech.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(offset, 0), InventorTool.CreatePoint2d(offset+width, height));
           // List<SketchLine> racts = InventorTool.GetCollectionFromIEnumerator<SketchLine>(ract.GetEnumerator());
            lines.ForEach(a => a.Construction = true);
            Profile pro = oskech.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(depth, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        ExtrudeFeature CreateSur(Face plane,double width,double offset,double height)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane,true);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(osketch.SketchEntities.GetEnumerator());
            lines.ForEach(a => a.Construction = true);
           SketchEntitiesEnumerator enumerator=  osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(offset, offset), InventorTool.CreatePoint2d(offset + width, offset + width));
            List<SketchLine> ract = InventorTool.GetCollectionFromIEnumerator<SketchLine>(enumerator.GetEnumerator());
               InventorTool.AddTwoPointDistance(osketch, ract[0].StartSketchPoint, ract[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = width;
            InventorTool.AddTwoPointDistance(osketch, ract[1].StartSketchPoint, ract[1].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = width;
            InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, ract[0].StartSketchPoint, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = offset;
            InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, ract[0].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = offset;
         
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        ExtrudeFeature CreateSurBootom(Face plane,double width,double width2)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane, true);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(osketch.SketchEntities.GetEnumerator());

            Point2d p= lines[0].StartSketchPoint.Geometry;
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(p.X - width2/2, p.Y - width2/2), InventorTool.CreatePoint2d(p.X + width2, p.Y + width2));
            lines.ForEach(a => a.Construction = true);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(2.5, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
                
        }
    }
}
