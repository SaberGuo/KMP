﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using KMP.Interface.Model.Container;
using Inventor;
using Infranstructure.Tool;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule.Container
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
            parPedestal.FootBoardWidth = 5;
            parPedestal.UnderBoardWidth = 10;
            parPedestal.BackBoardMoveDistance = 3;
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
            osketch.Visible = false;
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
            List<SketchLine> pedestalLines = CreateRectangle(osketch, InventorTool.Origin, InventorTool.TranGeo.CreatePoint2d(2, -2));
            TwoPointDistanceDimConstraint TwoPointDim1 = InventorTool.AddTwoPointDistance(osketch, pedestalLines[0].StartSketchPoint, pedestalLines[0].EndSketchPoint, -1, DimensionOrientationEnum.kAlignedDim);
            TwoPointDim1.Parameter.Value = parPedestal.PedestalLength;
            TwoPointDistanceDimConstraint TwoPointDim2 = InventorTool.AddTwoPointDistance(osketch, pedestalLines[1].StartSketchPoint, pedestalLines[1].EndSketchPoint, 1, DimensionOrientationEnum.kAlignedDim);
            TwoPointDim2.Parameter.Value = parPedestal.PanelThickness;
            //osketch.GeometricConstraints.AddHorizontal((SketchEntity)lines[0]);
            osketch.DimensionConstraints.AddTwoPointDistance(sublineCenter.StartSketchPoint, pedestalLines[0].StartSketchPoint, DimensionOrientationEnum.kHorizontalDim, pedestalLines[0].StartSketchPoint.Geometry).Parameter.Value = TwoPointDim1.Parameter.Value / 2;
            osketch.DimensionConstraints.AddTwoPointDistance(underBoardArc.CenterSketchPoint, pedestalLines[2].StartSketchPoint, DimensionOrientationEnum.kVerticalDim, underBoardArc.CenterSketchPoint.Geometry).Parameter.Value = parPedestal.PedestalCenterDistance;
            #endregion
            List<SketchLine> leftLines, rightLines;
          DrawingFootLine(osketch, sublineCenter, pedestalLines[0], underBoardArc,out leftLines,out rightLines);
            CreateunderBoard(outArc, underBoardArc, line1, line2);
            CreatePedestalBoard(pedestalLines);
           ExtrudeFeature footboad=  CreateBackBoard(underBoardArc, rightLines.Last(), leftLines.Last(), pedestalLines[0]);
            CreateFootBoard(footboad, leftLines, rightLines, pedestalLines[0], underBoardArc);
        }
        /// <summary>
        /// 创建竖版
        /// </summary>
        /// <param name="lines">竖板线段</param>
        /// <param name="line">底线</param>
        /// <param name="arc">顶弧段</param>
        private void CreateFootBoard(ExtrudeFeature footboard, List<SketchLine> leftLines, List<SketchLine> rightLines, SketchLine line,SketchArc arc)
        {
            List<SketchEntity> leftEntitys = new List<SketchEntity>();
            List<SketchEntity> rightEntitys = new List<SketchEntity>();
            PlanarSketch osketch=  partDef.Sketches.Add(InventorTool.GetFirstFromIEnumerator<Face>(footboard.EndFaces.GetEnumerator()));
            for(int i=0; i<leftLines.Count-1;i++)
            {
              SketchEntity entity=  osketch.AddByProjectingEntity(leftLines[i]);
                leftEntitys.Add(entity);
            }
            for (int i = 0; i < rightLines.Count - 1; i++)
            {
                SketchEntity entity = osketch.AddByProjectingEntity(rightLines[i]);
                rightEntitys.Add(entity);
            }
            osketch.AddByProjectingEntity(line);
            osketch.AddByProjectingEntity(arc);
            Profile profile = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in profile)
            {
             
              List<ProfileEntity> subs=  InventorTool.GetCollectionFromIEnumerator<ProfileEntity>(item.GetEnumerator());
                if(leftEntitys.Count%2!=0)
                {
                    if (Contains(subs, leftEntitys[0]) && Contains(subs, rightEntitys[0]))
                    {
                        item.AddsMaterial = true;
                        continue;
                    }
                    for (int i = 2; i < leftEntitys.Count; i = i + 2)
                    {
                        if ((Contains(subs, leftEntitys[i]) && Contains(subs, leftEntitys[i - 1])) ||
                            (Contains(subs, rightEntitys[i]) && Contains(subs, rightEntitys[i - 1])))
                        {
                            item.AddsMaterial = true;
                           break;
                        }
                        item.AddsMaterial = false;
                    }

                }
                else
                {
                    for (int i = 1; i < leftEntitys.Count; i = i + 2)
                    {
                        if ((Contains(subs,leftEntitys[i]) && Contains(subs, leftEntitys[i-1])) ||
                            (Contains(subs, rightEntitys[i]) && Contains(subs, rightEntitys[i-1])))
                        {
                            item.AddsMaterial = true;
                            break;
                        }
                        item.AddsMaterial = false;
                    }
                }
               // item.AddsMaterial = false;
            }
            ExtrudeDefinition ex = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            ex.SetDistanceExtent(parPedestal.FootBoardWidth, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            partDef.Features.ExtrudeFeatures.Add(ex);
        }
        /// <summary>
        /// 判断草图路径中是否含有草图实体
        /// </summary>
        /// <param name="profileEntitys"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool Contains(List<ProfileEntity> profileEntitys,SketchEntity entity)
        {
            return profileEntitys.Where(a => a.SketchEntity == entity).Count() > 0;
        }
        /// <summary>
        /// 创建背板
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="line3"></param>
        private ExtrudeFeature CreateBackBoard(SketchArc arc,SketchLine line1,SketchLine line2,SketchLine line3)
        {
          WorkPlane pp=  partDef.WorkPlanes.AddByPlaneAndOffset(partDef.WorkPlanes[3], parPedestal.BackBoardMoveDistance + "mm");
            PlanarSketch osketch = partDef.Sketches.Add(pp);
            pp.Visible = false;
            osketch.AddByProjectingEntity(arc);
            osketch.AddByProjectingEntity(line3);
            osketch.AddByProjectingEntity(line1);
            osketch.AddByProjectingEntity(line2);
            Profile profile = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            ex.SetDistanceExtent(parPedestal.PanelThickness, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           return  partDef.Features.ExtrudeFeatures.Add(ex);
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

        /// <summary>
        /// 创建底板
        /// </summary>
        /// <param name="list"></param>
        private void CreatePedestalBoard(List<SketchLine> list)
        {
            PlanarSketch osketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            foreach (var item in list)
            {
                osketch.AddByProjectingEntity(item);
            }
            Profile profile = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            ex.SetDistanceExtent(parPedestal.UnderBoardWidth, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            partDef.Features.ExtrudeFeatures.Add(ex);
        }


        /// <summary>
        /// 创建圆示意图
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
        /// 创建竖板示意图
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="subline">中心辅助线</param>
        /// <param name="line">底线</param>
        /// <param name="Arc1">垫板底圆弧</param>
        private void  DrawingFootLine(PlanarSketch osketch,SketchLine subline,SketchLine line,SketchArc Arc1,out List<SketchLine> leftLines,out List<SketchLine> rightLines)
        {
            rightLines = new List<SketchLine>();
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
                    if(i==0)//第一个竖板
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint,0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value=parPedestal.FootBoardThickness/2;
                    }
                    else if(i==parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.EndSketchPoint, (SketchEntity)L);
                     
                    }
                    else if(i%2!=0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i + 1) / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardThickness / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i/ 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardThickness / 2;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parPedestal.FootBoardBetween/ 2;
                    }
                    else if (i == parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.EndSketchPoint, (SketchEntity)L);

                    }
                    else if (i % 2 != 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i - 1)/2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardBetween / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i/2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardBetween / 2;
                    }
                }
                rightLines.Add(L);
            }


            leftLines = new List<SketchLine>();
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
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parPedestal.FootBoardThickness / 2;
                    }
                    else if (i == parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.StartSketchPoint, (SketchEntity)L);

                    }
                    else if (i % 2 != 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i + 1) / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardThickness / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardThickness / 2;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = parPedestal.FootBoardBetween / 2;
                    }
                    else if (i == parPedestal.FootBoardNum)
                    {
                        osketch.GeometricConstraints.AddCoincident((SketchEntity)Arc1.StartSketchPoint, (SketchEntity)L);

                    }
                    else if (i % 2 != 0)
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = (i - 1) / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) + parPedestal.FootBoardBetween / 2;
                    }
                    else
                    {
                        InventorTool.AddTwoPointDistance(osketch, L.EndSketchPoint, subline.EndSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = i / 2 * (parPedestal.FootBoardBetween + parPedestal.FootBoardThickness) - parPedestal.FootBoardBetween / 2;
                    }
                }
                leftLines.Add(L);
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

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }
    }
}