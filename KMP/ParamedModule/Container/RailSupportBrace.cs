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
    /// 导轨支架支撑
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
  public  class RailSupportBrace : ParamedModuleBase
    {
        ParRailSupportBrace parBrace = new ParRailSupportBrace();
        [ImportingConstructor]
        public RailSupportBrace():base()
        {
            this.Parameter = parBrace;
            init();
        }
        void init()
        {
            parBrace.Height = 1000;
            parBrace.InRadius = 50;
            parBrace.Thickness = 2;
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            PartDocument part = InventorTool.CreatePart();
            PartComponentDefinition partDef = part.ComponentDefinition;
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
           SketchCircle cir1= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, parBrace.InRadius);
           SketchCircle cir2= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, parBrace.InRadius + parBrace.Thickness);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)cir1, (SketchEntity)cir2);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if(item.Count==1)
                {
                    item.AddsMaterial = false;
                }
                else
                {
                    item.AddsMaterial = true;
                }
            }
           ExtrudeDefinition extrudedef= partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extrudedef.SetDistanceExtent(parBrace.Height + "mm", PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           ExtrudeFeature cylinder= partDef.Features.ExtrudeFeatures.Add(extrudedef);
            PlanarSketch holeSketch = partDef.Sketches.Add(partDef.WorkPlanes[2]);
            holeSketch.SketchCircles.AddByCenterRadius(InventorTool.TranGeo.CreatePoint2d(0, parBrace.Height / 20), 8);
            Profile holePro = holeSketch.Profiles.AddForSolid();
           ExtrudeDefinition holeDef= partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(holePro, PartFeatureOperationEnum.kCutOperation);
            holeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            partDef.Features.ExtrudeFeatures.Add(holeDef);
            
        }
    }
}
