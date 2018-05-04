using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
using Infranstructure.Tool;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Pedestal : ParamedModuleBase
    {
        ParPedestal parPedestal = new ParPedestal();
        [ImportingConstructor]
        public Pedestal():base()
        {
            this.Parameter = this.parPedestal;
        }
        void init()
        {
            parPedestal.InRadius = 100;
            parPedestal.Thickness = 2;
            parPedestal.PanelThickness = 3;
            parPedestal.UnderBoardingAngle = 120;
            parPedestal.PedestalLength = 200;
            parPedestal.PedestalCenterDistance = 150;
            parPedestal.FootBoardBetween=30;
            parPedestal.FootBoardNum = 5;
            parPedestal.FootBoardThickness = 3;
            parPedestal.UnderBoardWidth = 10;
        }
        PartDocument part;
        PartComponentDefinition partDef;
        public override void CreateModule(ParameterBase Parameter)
        {
            parPedestal = Parameter as ParPedestal;
            if (parPedestal == null) return;
            init();
            part = InventorTool.CreatePart();
            partDef = part.ComponentDefinition;
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            #region 创建圆
            SketchArc outArc, underBoardArc;
            SketchLine sublineCenter;
            CreateCycle(osketch, out outArc, out underBoardArc, out sublineCenter);
            #endregion
            #region 垫板堵头
            SketchLine line1 = osketch.SketchLines.AddByTwoPoints(underBoardArc.StartSketchPoint, GetCyclePoint(
               underBoardArc.StartSketchPoint.Geometry, underBoardArc.CenterSketchPoint.Geometry, underBoardArc.Radius, outArc.Radius));
            SketchLine line2 = osketch.SketchLines.AddByTwoPoints(underBoardArc.EndSketchPoint, GetCyclePoint(
             underBoardArc.EndSketchPoint.Geometry, underBoardArc.CenterSketchPoint.Geometry, underBoardArc.Radius, outArc.Radius));
            osketch.GeometricConstraints.AddCoincident((SketchEntity)outArc, (SketchEntity)line1.EndSketchPoint);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)outArc, (SketchEntity)line2.EndSketchPoint);
            #endregion
            #region 底板
            List<SketchLine> lines = CreateRectangle(osketch, InventorTool.Origin, InventorTool.TranGeo.CreatePoint2d(2, -2));
            TwoPointDistanceDimConstraint TwoPointDim1 = InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, lines[0].EndSketchPoint, false, DimensionOrientationEnum.kAlignedDim);
            TwoPointDim1.Parameter.Value = parPedestal.PedestalLength;
            TwoPointDistanceDimConstraint TwoPointDim2 = InventorTool.AddTwoPointDistance(osketch, lines[1].StartSketchPoint, lines[1].EndSketchPoint, true, DimensionOrientationEnum.kAlignedDim);
            TwoPointDim2.Parameter.Value = parPedestal.PanelThickness;
            //osketch.GeometricConstraints.AddHorizontal((SketchEntity)lines[0]);
            osketch.DimensionConstraints.AddTwoPointDistance(sublineCenter.StartSketchPoint, lines[0].StartSketchPoint, DimensionOrientationEnum.kHorizontalDim, lines[0].StartSketchPoint.Geometry).Parameter.Value = TwoPointDim1.Parameter.Value / 2;
            osketch.DimensionConstraints.AddTwoPointDistance(underBoardArc.CenterSketchPoint, lines[2].StartSketchPoint, DimensionOrientationEnum.kVerticalDim, underBoardArc.CenterSketchPoint.Geometry).Parameter.Value = parPedestal.PedestalCenterDistance;
            #endregion
            DrawingFootLine(osketch, sublineCenter, lines[0], underBoardArc);
            CreateunderBoard(outArc, underBoardArc, line1, line2);
        }
        /// <summary>
        /// 创建垫板
        /// </summary>
        /// <param name="inArc"></param>
        /// <param name="outArc"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        private void CreateunderBoard(SketchArc inArc,SketchArc outArc,SketchLine line1,SketchLine line2)
        {
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            osketch.AddByProjectingEntity(inArc);
            osketch.AddByProjectingEntity(outArc);
            osketch.AddByProjectingEntity(line1);
            osketch.AddByProjectingEntity(line2);
            Profile profile = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            ex.SetDistanceExtent(parPedestal.UnderBoardWidth,PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            partDef.Features.ExtrudeFeatures.Add(ex);
        }
        private void CreatePedestal(List<SketchLine> list)
        {
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            foreach (var item in list)
            {
                osketch.AddByProjectingEntity(item);
            }
            
        }
        /// <summary>
        /// 创建圆
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="outArc">垫板内圆弧</param>
        /// <param name="underBoardArc">垫板外圆弧</param>
        /// <param name="sublineCenter">中心辅助线</param>
        private void CreateCycle(PlanarSketch osketch, out SketchArc outArc, out SketchArc underBoardArc, out SketchLine sublineCenter)
        {
            SketchCircle inCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, parPedestal.InRadius);
            outArc = osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.Origin, parPedestal.InRadius + parPedestal.Thickness,
                  Math.PI, Math.PI);
            underBoardArc = osketch.SketchArcs.AddByCenterStartSweepAngle(InventorTool.Origin, parPedestal.InRadius + parPedestal.Thickness + parPedestal.PanelThickness,
Math.PI * 1.5 - parPedestal.UnderBoardingAngle / 360 * Math.PI, parPedestal.UnderBoardingAngle / 180 * Math.PI);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)inCircle, (SketchEntity)outArc);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)inCircle, (SketchEntity)underBoardArc);
            TangentDistanceDimConstraint inCircleTangentDim = osketch.DimensionConstraints.AddTangentDistance((SketchEntity)inCircle, (SketchEntity)outArc, GetCyclePoint(outArc.EndSketchPoint.Geometry,
                  inCircle.CenterSketchPoint.Geometry, outArc.Radius, inCircle.Radius), outArc.EndSketchPoint.Geometry, outArc.EndSketchPoint.Geometry, true);
            inCircleTangentDim.Parameter.Value = parPedestal.Thickness;
            TangentDistanceDimConstraint outArcTangentDim = osketch.DimensionConstraints.AddTangentDistance((SketchEntity)underBoardArc, (SketchEntity)outArc, GetCyclePoint(underBoardArc.EndSketchPoint.Geometry,
                outArc.CenterSketchPoint.Geometry, underBoardArc.Radius, outArc.Radius), underBoardArc.EndSketchPoint.Geometry, underBoardArc.EndSketchPoint.Geometry, true);
            outArcTangentDim.Parameter.Value = parPedestal.PanelThickness;
            RadiusDimConstraint inCircleRadiusDim = osketch.DimensionConstraints.AddRadius((SketchEntity)inCircle, GetCircleNotePoint(inCircle.Radius, Math.PI / 3));
            inCircleRadiusDim.Parameter.Value = parPedestal.InRadius;
            sublineCenter = osketch.SketchLines.AddByTwoPoints(InventorTool.Origin, InventorTool.TranGeo.CreatePoint2d(0, -200));
            SketchLine subline1 = osketch.SketchLines.AddByTwoPoints(InventorTool.Origin, underBoardArc.StartSketchPoint);
            SketchLine subline2 = osketch.SketchLines.AddByTwoPoints(InventorTool.Origin, underBoardArc.EndSketchPoint);
            //CreateTwoPointCoinCident( osketch,underBoardArc, subline1);
            //CreateTwoPointCoinCident(osketch, underBoardArc, subline2);
            osketch.GeometricConstraints.AddVertical((SketchEntity)sublineCenter);
            InventorTool.CreateTwoPointCoinCident(osketch, subline1, subline2);
            InventorTool.CreateTwoPointCoinCident(osketch, sublineCenter, subline2);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)underBoardArc.CenterSketchPoint, (SketchEntity)sublineCenter);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)underBoardArc.CenterSketchPoint, (SketchEntity)subline1);

            InventorTool.AddTwoLineAngle(osketch, sublineCenter, subline1);
            InventorTool.AddTwoLineAngle(osketch, sublineCenter, subline2);
            // osketch.GeometricConstraints.AddVertical((SketchEntity)sublineCenter);
            sublineCenter.Construction = true;
            subline1.Construction = true;
            subline2.Construction = true;
        }
        /// <summary>
        /// 创建竖板
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="subline">中心辅助线</param>
        /// <param name="line">底线</param>
        /// <param name="Arc1">垫板底圆弧</param>
        private void  DrawingFootLine(PlanarSketch osketch,SketchLine subline,SketchLine line,SketchArc Arc1)
        {
            List<SketchLine> list = new List<SketchLine>();
            Point2d p1 = InventorTool.TranGeo.CreatePoint2d(1, -1);
            Point2d p2 = InventorTool.TranGeo.CreatePoint2d(1, -2);
            for (int i=0;i<parPedestal.FootBoardNum+1; i++)
            {

                SketchLine L= osketch.SketchLines.AddByTwoPoints(p1,p2);
                osketch.GeometricConstraints.AddPerpendicular((SketchEntity)L, (SketchEntity)line);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)L.StartSketchPoint, (SketchEntity)Arc1);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)L.EndSketchPoint, (SketchEntity)line);
                if (parPedestal.FootBoardNum % 2 != 0)
                {
                    if(i==0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint,false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value=parPedestal.FootBoardThickness/2;
                    }
                    else if(i==parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.EndSketchPoint, (SketchEntity)L);
                     
                    }
                    else if(i%2!=0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i + 1) / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardThickness / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i/ 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardThickness / 2;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parPedestal.FootBoardBetween/ 2;
                    }
                    else if (i == parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.EndSketchPoint, (SketchEntity)L);

                    }
                    else if (i % 2 != 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i - 1)/2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardBetween / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i/2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardBetween / 2;
                    }
                }
                list.Add(L);
            }

            Point2d p3 = InventorTool.TranGeo.CreatePoint2d(-1, -1);
            Point2d p4 = InventorTool.TranGeo.CreatePoint2d(-1, -2);
            for (int i = 0; i < parPedestal.FootBoardNum + 1; i++)
            {

                SketchLine L = osketch.SketchLines.AddByTwoPoints(p3, p4);
                osketch.GeometricConstraints.AddPerpendicular((SketchEntity)L, (SketchEntity)line);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)L.StartSketchPoint, (SketchEntity)Arc1);
                osketch.GeometricConstraints.AddCoincident((SketchEntity)L.EndSketchPoint, (SketchEntity)line);
                if (parPedestal.FootBoardNum % 2 != 0)
                {
                    if (i == 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parPedestal.FootBoardThickness / 2;
                    }
                    else if (i == parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.StartSketchPoint, (SketchEntity)L);

                    }
                    else if (i % 2 != 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i + 1) / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardThickness / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardThickness / 2;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parPedestal.FootBoardBetween / 2;
                    }
                    else if (i == parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.StartSketchPoint, (SketchEntity)L);

                    }
                    else if (i % 2 != 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i - 1) / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardBetween / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, false, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardBetween / 2;
                    }
                }
                list.Add(L);
            }

        }
        /// <summary>
        /// 从同心圆的一点获取到另一个圆的垂直点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="orgin"></param>
        /// <param name="r1">圆的半径</param>
        /// <param name="r2">获取点所在圆的半径</param>
        /// <returns></returns>
        Point2d GetCyclePoint(Point2d p,Point2d orgin,double r1,double r2)
        {
            double x=(p.X - orgin.X) / r1 * r2;
            double y = (p.Y - orgin.Y) / r1 * r2;
            return InventorTool.TranGeo.CreatePoint2d(x, y);
        }
        /// <summary>
        /// 获取在圆上指定角的点
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        Point2d GetCircleNotePoint(double radius,double angle)
        {
          return  InventorTool.TranGeo.CreatePoint2d(radius * Math.Sin(angle), radius * Math.Cos(angle));
        }
        /// <summary>
        /// 根据两点获取矩形并返回四个线段
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
      List<SketchLine> CreateRectangle(PlanarSketch osketch,Point2d p1,Point2d p2)
        {
          System.Collections.IEnumerator collection=  osketch.SketchLines.AddAsTwoPointRectangle(p1, p2).GetEnumerator();
           return InventorTool.GetCollectionFromIEnumerator<SketchLine>(collection);

        }
    }
}
