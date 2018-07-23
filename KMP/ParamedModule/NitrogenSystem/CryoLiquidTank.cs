using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParamedModule;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 低温液体储槽
    /// </summary>
    [Export("CryoLiquidTank", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CryoLiquidTank : PartModulebase
    {
       public ParCryoLiquidTank par = new ParCryoLiquidTank();
        [ImportingConstructor]
        public CryoLiquidTank():base()
        {
            this.Name = "低温液体储槽";
            init();
            this.Parameter = par;
        }
        void init()
        {
            par.Capacity.Capacity = 3500;
            par.Capacity.Dimension = 2016;
            par.Capacity.Height = 4025;
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            double shortDimen = UsMM(par.Capacity.Dimension) / 2;
            double height = UsMM(par.Capacity.Height) - shortDimen;
         // SketchLine line=  osketch.SketchLines.AddByTwoPoints(InventorTool.CreatePoint2d(shortDimen / 2, height / 2), InventorTool.CreatePoint2d(shortDimen / 2, -height / 2));
          SketchEllipticalArc arc1=  osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, height / 2), InventorTool.Right, UsMM(par.Capacity.Dimension) / 2, shortDimen / 2, 0, Math.PI/2);
          SketchEllipticalArc arc2 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, -height / 2), InventorTool.Right, UsMM(par.Capacity.Dimension) / 2, shortDimen / 2, 1.5*Math.PI, Math.PI/2);
            SketchEllipticalArc arc3 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, height / 2), InventorTool.Right, UsMM(par.Capacity.Dimension) / 2-2, shortDimen / 2-2, 0, Math.PI / 2);
            SketchEllipticalArc arc4 = osketch.SketchEllipticalArcs.Add(InventorTool.CreatePoint2d(0, -height / 2), InventorTool.Right, UsMM(par.Capacity.Dimension) / 2-2, shortDimen / 2-2,1.5* Math.PI, Math.PI / 2);
            // SketchLine CenterLine = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc2.EndSketchPoint);
            //CenterLine.Construction = true;
            SketchLine line = osketch.SketchLines.AddByTwoPoints(arc1.StartSketchPoint, arc2.EndSketchPoint);
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(arc3.StartSketchPoint, arc4.EndSketchPoint);
            SketchLine line2 = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc3.EndSketchPoint);
            SketchLine line3 = osketch.SketchLines.AddByTwoPoints(arc2.StartSketchPoint, arc4.StartSketchPoint);
            Profile pro = osketch.Profiles.AddForSolid();
            Definition.Features.RevolveFeatures.AddFull(pro, line2, PartFeatureOperationEnum.kNewBodyOperation);
            PlanarSketch SupSketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
             SketchEllipticalArc SurArc=(SketchEllipticalArc) SupSketch.AddByProjectingEntity(arc2);
            Point2d ArcEndPoint = SurArc.EndSketchPoint.Geometry;
            SketchLine SurLine1= SupSketch.SketchLines.AddByTwoPoints(SurArc.EndSketchPoint, InventorTool.CreatePoint2d(ArcEndPoint.X, ArcEndPoint.Y - shortDimen));
            InventorTool.AddTwoPointDistance(SupSketch, SurLine1.StartSketchPoint, SurLine1.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value=shortDimen;
            SketchLine SurLine2 = SupSketch.SketchLines.AddByTwoPoints(SurLine1.EndSketchPoint,InventorTool.CreatePoint2d( SurLine1.EndSketchPoint.Geometry.X-shortDimen/10, SurLine1.EndSketchPoint.Geometry.Y));
            SketchLine SurLine3 = SupSketch.SketchLines.AddByTwoPoints(SurLine2.EndSketchPoint, InventorTool.CreatePoint2d(SurLine2.EndSketchPoint.Geometry.X, SurLine2.EndSketchPoint.Geometry.Y+5));
            SupSketch.GeometricConstraints.AddPerpendicular((SketchEntity)SurLine1, (SketchEntity)SurLine2);
            SupSketch.GeometricConstraints.AddParallel((SketchEntity)SurLine1, (SketchEntity)SurLine3);
            SupSketch.GeometricConstraints.AddCoincident((SketchEntity)SurLine3.EndSketchPoint,(SketchEntity) SurArc);
            Profile SurPro = SupSketch.Profiles.AddForSolid();
            ExtrudeDefinition SurExtrude = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(SurPro, PartFeatureOperationEnum.kJoinOperation);
            SurExtrude.SetDistanceExtent(shortDimen / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            ExtrudeFeature SurExtrudeFeature= Definition.Features.ExtrudeFeatures.Add(SurExtrude);
            SurExtrudeFeature.Name = "Sur";
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(SurExtrudeFeature);
            WorkAxis axis = Definition.WorkAxes.AddByLine(line2,true);
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[2], 0);
            plane.Visible = false;
            plane.Name = "Flush";
            WorkPlane plane1 = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[1], 0);
            plane1.Visible = false;
            plane1.Name = "Mate";
            //axis.Visible = false;
            //axis.Name = "Axis";
            Definition.Features.CircularPatternFeatures.Add(objc, axis, false, 3, Math.PI * 2, true, PatternComputeTypeEnum.kAdjustToModelCompute);
            //SketchLine line1 = osketch.SketchLines.AddByTwoPoints(arc1.EndSketchPoint, arc3.EndSketchPoint);
            // Profile pro = osketch.Profiles.AddForSolid();
        }
    }
}
