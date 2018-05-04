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
namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架顶板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RailSupportTopBoard : ParamedModuleBase
    {
        ParRailSupportTopBoard parTopboard = new ParRailSupportTopBoard();
        public RailSupportTopBoard():base()
        {
            this.Parameter = parTopboard;
            init();
        }
        void init()
        {

        }

        public override void CreateModule(ParameterBase Parameter)
        {
            PartDocument part = InventorTool.CreatePart();
            PartComponentDefinition partDef = part.ComponentDefinition;
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
          SketchEntitiesEnumerator entities=  osketch.SketchLines.AddAsTwoPointCenteredRectangle(InventorTool.Origin, InventorTool.TranGeo.CreatePoint2d(2, 2));
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, lines[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = parTopboard.Width;
            InventorTool.AddTwoPointDistance(osketch, lines[1].StartSketchPoint, lines[1].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = parTopboard.Width;
            Profile pro = osketch.Profiles.AddForSolid();
           ExtrudeDefinition extrudedef= partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extrudedef.SetDistanceExtent(parTopboard.Thickness, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature block = partDef.Features.ExtrudeFeatures.Add(extrudedef);
           PlanarSketch holeSketch= partDef.Sketches.Add(InventorTool.GetFirstFromIEnumerator<Face>(block.EndFaces.GetEnumerator()));
           
        }
    }
}
