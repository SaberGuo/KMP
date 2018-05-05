using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using KMP.Interface.Model;
using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using System.ComponentModel.Composition;
using System.Reflection;
namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架顶板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RailSupportTopBoard : PartModulebase
    {
        ParRailSupportTopBoard parTopboard = new ParRailSupportTopBoard();
        public RailSupportTopBoard():base()
        {
            this.Parameter = parTopboard;
            init();
        }
        void init()
        {
            parTopboard.Width = 100;
            parTopboard.Thickness = 10;
            parTopboard.HoleCenterDistance = 15;
            parTopboard.HoleRadius = 5;
            parTopboard.HoleSideEdgeDistance =8;
            parTopboard.HoleTopEdgeDistance = 7;
        }
      
        public override void CreateModule(ParameterBase Parameter)
        {
            PartDocument part = InventorTool.CreatePart();
             partDef = part.ComponentDefinition;
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);      
            ExtrudeFeature block = InventorTool.CreateBox(partDef, osketch, parTopboard.Width, parTopboard.Width, parTopboard.Thickness);
           Face FacePlane= InventorTool.GetFirstFromIEnumerator<Face>(block.StartFaces.GetEnumerator());
            List<Face> SideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(block.SideFaces.GetEnumerator());
            List<Edge> lines = InventorTool.GetCollectionFromIEnumerator<Edge>(FacePlane.Edges.GetEnumerator());
          // PlanarSketch holeSketch= partDef.Sketches.AddWithOrientation(FacePlane,lines[0],false,true,lines[0].StartVertex,true);
            PlanarSketch holeSketch = partDef.Sketches.Add(FacePlane);
            ExtrudeFeature hole=  CreateHole(holeSketch,lines);
            WorkPlane plane = partDef.WorkPlanes.AddByTwoPlanes(SideFaces[0], SideFaces[2]);
            WorkPlane plane1 = partDef.WorkPlanes.AddByTwoPlanes(SideFaces[1], SideFaces[3]);
            ObjectCollection objects = InventorTool.Inventor.TransientObjects.CreateObjectCollection();
            objects.Add(hole);
           MirrorFeatureDefinition mirrorDef= partDef.Features.MirrorFeatures.CreateDefinition(objects, plane);
           MirrorFeature mirror= partDef.Features.MirrorFeatures.AddByDefinition(mirrorDef);
            objects.Add(mirror);
            MirrorFeatureDefinition mirrorDef2 = partDef.Features.MirrorFeatures.CreateDefinition(objects, plane1);
            partDef.Features.MirrorFeatures.AddByDefinition(mirrorDef2);
        }
        ExtrudeFeature CreateHole(PlanarSketch osketch, List<Edge> Edges)
        {
            List<SketchLine> lines = new List<SketchLine>();
            foreach (var item in Edges)
            {
                SketchLine line = osketch.AddByProjectingEntity(item) as SketchLine;
                lines.Add(line);
            }
           
           
           SketchArc arc1= osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.TranGeo.CreatePoint2d(50,50), 5, Math.PI / 2, Math.PI);
           SketchArc arc2= osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.TranGeo.CreatePoint2d(60,50), 5, -Math.PI / 2, Math.PI);
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(arc1.StartSketchPoint, arc2.EndSketchPoint);
            SketchLine line2 = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc2.StartSketchPoint);
            //osketch.GeometricConstraints.AddEqualLength(line1, line2);
            osketch.GeometricConstraints.AddEqualRadius((SketchEntity)arc1, (SketchEntity)arc2);
            osketch.GeometricConstraints.AddParallel((SketchEntity)line1, (SketchEntity)line2);
            osketch.GeometricConstraints.AddTangent((SketchEntity)line1, (SketchEntity)arc2);
            osketch.GeometricConstraints.AddTangent((SketchEntity)line1, (SketchEntity)arc1);
            osketch.GeometricConstraints.AddTangent((SketchEntity)arc1, (SketchEntity)line2);
            osketch.DimensionConstraints.AddRadius((SketchEntity)arc2, arc2.StartSketchPoint.Geometry);
            InventorTool.AddTwoPointDistance(osketch, arc1.CenterSketchPoint, arc2.CenterSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value=parTopboard.HoleCenterDistance;

            InventorTool.AddTwoPointDistance(osketch, arc2.CenterSketchPoint, lines[0].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parTopboard.HoleTopEdgeDistance;
            InventorTool.AddTwoPointDistance(osketch, arc2.CenterSketchPoint, lines[0].StartSketchPoint, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = parTopboard.HoleSideEdgeDistance;
            osketch.UpdateProfiles();
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                List<ProfileEntity> list = InventorTool.GetCollectionFromIEnumerator<ProfileEntity>(item.GetEnumerator());
                int count = list.Where(a => a.SketchEntity == arc1).Count();
                if(count>0)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.Delete();
                }
          
            }
            ExtrudeDefinition ex= partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            ex.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            
           return partDef.Features.ExtrudeFeatures.Add(ex);
        }

        public override bool CheckParamete()
        {
         
            if (!CommonTool.CheckParameterValue(this.Parameter)) return false;
            if (parTopboard.HoleTopEdgeDistance <= parTopboard.HoleRadius) return false;
            if (parTopboard.HoleSideEdgeDistance <= parTopboard.HoleRadius) return false;
            if (parTopboard.HoleCenterDistance + parTopboard.HoleRadius > parTopboard.Width / 2) return false;
            return true;
        }
    }
}
