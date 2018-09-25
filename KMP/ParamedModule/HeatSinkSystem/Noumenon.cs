using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.HeatSinkSystem;
using Inventor;
using Infranstructure.Tool;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model;
using System.Collections.ObjectModel;
namespace ParamedModule.HeatSinkSystem
{
    //[Export("Noumenon", typeof(IParamedModule))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class Noumenon : PartModulebase
    {
        public ParNoumenon par = new ParNoumenon();
        Dictionary<double, WorkPlane> _HolePlanes = new Dictionary<double, WorkPlane>();
        public Noumenon():base()
        {
            this.ProjectType = "NOU";
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        public Noumenon(PassedParameter inDiameter, PassedParameter thickness) : base()
        {
            this.Parameter = par;
            this.Name = "筒体热沉";
            par.InDiameter = inDiameter;
            par.Thickness = thickness;
            init();
        }
        void init()
        {
            par.InDiameter.Value =3000;
            par.Thickness.Value = 24;
            par.Length = 5000;

            par.PipeDistance = 100;
            par.PipeOffset = 1400;
            par.PipeDiameter = 50;
            par.PipeThickness = 3.5;
            par.PipeLength = 3000;

            par.PipeSurLength = 20;
            par.PipeSurDistance = 100;
            par.PipeSurCurveRadius = 40;
            par.PipeSurThickness = 2;
            par.PipeSurDiameter = 25;
            par.PipeSurNum = 10;

            par.TBrachHeight = 60;
            par.TBrachWidth = 10;
            par.TTopHeight = 10;
            par.TTopWidth = 70;
            par.THoopOffset = 100;
            par.THoopNumber = 4;
            par.EndLongAngle = 30;
            par.EndLongNumber = 3;
            par.TAndCYDiatance = 20;
        }
        public override void DisPose()
        {
            base.DisPose();
            _HolePlanes.Clear();
            CStartFace = null;
            CEndFace = null;
            CSideFace = null;
            axis = null;
        }
        Face CStartFace, CEndFace;
        List<Face> CSideFace;
        WorkAxis axis;

        private void Parameter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public override bool CheckParamete()
        {
            if(par.PipeLength+ par.PipeDistance>par.Length)
            {
                ParErrorChanged(this, "管长度与管道开始面道罐口的距离和大于罐长度！");
                return false;
            }
             if(par.PipeOffset+par.PipeDiameter/2+par.PipeThickness>=par.InDiameter.Value/2)
            {
                ParErrorChanged(this, "管部分超出了罐体横截面外！");
                return false;
            }
             if(((par.PipeLength-par.PipeSurDistance*2)/par.PipeSurNum)<(par.PipeSurDiameter+par.PipeSurThickness*2))
            {
                ParErrorChanged(this, "管长度放不下"+par.PipeSurNum+"个支架！");
                return false;
            }
             if(par.TBrachHeight<=par.TTopHeight)
            {
                ParErrorChanged(this, "T字钢支撑高度不能小于T字钢顶部厚度！");
                return false;
            }
            
            return true;
        }
        #region
        public override void CreateSub()
        {
            SketchCircle inCircle, outCircle, pipeOutCircle; ///罐内外草图圆、罐外圆草图
            ExtrudeFeature cylinder = CreateCylinder(UsMM(par.InDiameter.Value / 2), UsMM(par.Thickness.Value), UsMM(par.Length), out inCircle, out outCircle);
             CStartFace = InventorTool.GetFirstFromIEnumerator<Face>(cylinder.StartFaces.GetEnumerator());
             CEndFace = InventorTool.GetFirstFromIEnumerator<Face>(cylinder.EndFaces.GetEnumerator());
            Definition.iMateDefinitions.AddMateiMateDefinition(CStartFace, 0).Name = "StartFace";
            Definition.iMateDefinitions.AddMateiMateDefinition(CEndFace, 0).Name = "EndFace";
            CSideFace = InventorTool.GetCollectionFromIEnumerator<Face>(cylinder.SideFaces.GetEnumerator());
             axis = Definition.WorkAxes.AddByRevolvedFace(CSideFace[0]);
            axis.Name = "NoumenonAxis";
            Definition.iMateDefinitions.AddMateiMateDefinition(axis, 0).Name = "Axis";
            ExtrudeFeature pipe = CreatePipe(CStartFace, inCircle, UsMM(par.PipeLength), UsMM(par.PipeDiameter / 2), UsMM(par.PipeThickness), UsMM(par.PipeDistance), UsMM(par.PipeOffset), out pipeOutCircle);
            Face pipeStartFace = InventorTool.GetFirstFromIEnumerator<Face>(pipe.StartFaces.GetEnumerator());
            SweepFeature pipeSur = CreatePipeSurp(CSideFace[0], pipeStartFace, inCircle, pipeOutCircle, UsMM(par.PipeSurDistance), UsMM(par.PipeSurCurveRadius), UsMM(par.PipeSurLength), UsMM(par.PipeSurDiameter / 2), UsMM(par.PipeSurThickness));
            CreatePipeSurMid(pipe, pipeSur, axis, UsMM(par.PipeLength), par.PipeSurNum, UsMM(par.PipeSurDistance), Definition.WorkPlanes[3], Definition.WorkPlanes[2]);

            RevolveFeature Hoop = CreateHoop(CSideFace[1], axis, UsMM(par.TBrachWidth), UsMM(par.TBrachHeight), UsMM(par.TTopWidth), UsMM(par.TTopHeight), UsMM(par.THoopOffset), par.THoopNumber, UsMM(par.Length));
            double endloopLength = UsMM(par.Length - par.THoopOffset * 2) / (par.THoopNumber - 1) - UsMM(par.TBrachWidth);
            ExtrudeFeature endLong = CreateEndLong(Hoop, outCircle, axis, UsMM(par.TBrachWidth), UsMM(par.TBrachHeight-par.TTopHeight), UsMM(par.TTopWidth),
                UsMM(par.TTopHeight), par.EndLongAngle, UsMM(par.InDiameter.Value / 2 + par.Thickness.Value), endloopLength);
            CreateEndLondMirror(endLong, axis, UsMM(par.TBrachWidth), par.EndLongNumber, par.THoopNumber, endloopLength);

        }
        #region 创建罐体和罐内管道
        /// <summary>
        /// 创建罐体
        /// </summary>
        /// <param name="radius">罐体半径</param>
        /// <param name="thickness">罐体厚度</param>
        /// <param name="length">罐体直径</param>
        /// <param name="inCircle">罐体内圆草图</param>
        /// <param name="outCircle">罐体外圆草图</param>
        /// <returns></returns>
        ExtrudeFeature CreateCylinder(double radius,double thickness,double length,out SketchCircle inCircle,out SketchCircle outCircle)
        {
        
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            inCircle= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, radius);
           AddDiameter(osketch, (SketchEntity)inCircle, radius * 2);
             outCircle = osketch.SketchCircles.AddByCenterRadius(inCircle.CenterSketchPoint, radius + thickness);
            Profile pro = osketch.Profiles.AddForSolid();
       
           ExtrudeDefinition def= Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
          return  Definition.Features.ExtrudeFeatures.Add(def);
        }
        /// <summary>
        /// 罐内管道
        /// </summary>
        /// <param name="CylinderStartFace">罐口面</param>
        /// <param name="inCircle">罐内圆草图</param>
        /// <param name="pipeLength">管长度</param>
        /// <param name="pipeRadius">管半径</param>
        /// <param name="pipeThickness">管厚度</param>
        /// <param name="distance">管与罐口距离</param>
        /// <param name="offset">管心与罐心距离</param>
        /// <param name="pipeOutCirce">管外圆草图</param>
        /// <returns></returns>
        ExtrudeFeature CreatePipe(Face CylinderStartFace,SketchCircle inCircle,double pipeLength,double pipeRadius,double pipeThickness,double distance,double offset,out SketchCircle pipeOutCirce)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(CylinderStartFace, -distance, true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle BigCircle = (SketchCircle)osketch.AddByProjectingEntity(inCircle);
            BigCircle.Construction = true;
            Point2d temp = BigCircle.CenterSketchPoint.Geometry;
            Point2d center = InventorTool.CreatePoint2d(temp.X, temp.Y-offset);
          
          SketchCircle pipeCircle=  osketch.SketchCircles.AddByCenterRadius(center, pipeRadius);
            InventorTool.AddTwoPointDistance(osketch, BigCircle.CenterSketchPoint, pipeCircle.CenterSketchPoint,0,DimensionOrientationEnum.kAlignedDim).Parameter.Value=offset;
            pipeOutCirce = osketch.SketchCircles.AddByCenterRadius(pipeCircle.CenterSketchPoint, pipeRadius + pipeThickness);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {

                foreach (ProfileEntity sub in item)
                {
                    if(sub.SketchEntity!=pipeCircle)
                    {
                        item.AddsMaterial = true;
                        break;
                    }
                    else
                    {
                        item.AddsMaterial = false;
                    }
                }
            }
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(pipeLength, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        /// <summary>
        /// 创建管支架
        /// </summary>
        /// <param name="CSideFace">罐内侧面</param>
        /// <param name="pipeStartFace">管开始截面</param>
        /// <param name="cylinderInCircle">罐内圆草图</param>
        /// <param name="pipeOutCircle">管外圆草图</param>
        /// <param name="pipeSurHDistance">管支架与管开始截面距离</param>
        /// <param name="pipeSurHRaidus">管支架歪曲圆半径</param>
        /// <param name="pipeSurHLength">管长度</param>
        /// <param name="pipeSurRadius">管半径</param>
        /// <param name="pipeSurThickness">管厚度</param>
        /// <returns></returns>
        SweepFeature CreatePipeSurp(Face CSideFace,Face pipeStartFace,SketchCircle cylinderInCircle,SketchCircle pipeOutCircle,double pipeSurHDistance,
            double pipeSurHRaidus,double pipeSurHLength,double pipeSurRadius,double pipeSurThickness)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(pipeStartFace, -pipeSurHDistance, true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchCircle BigCircle = (SketchCircle)osketch.AddByProjectingEntity(cylinderInCircle);
            SketchCircle pipeCircle = (SketchCircle)osketch.AddByProjectingEntity(pipeOutCircle);
            Point2d center = pipeCircle.CenterSketchPoint.Geometry;
            Point2d lineStart = InventorTool.CreatePoint2d(center.X-pipeCircle.Radius, center.Y);
            Point2d lineEnd = InventorTool.CreatePoint2d(lineStart.X - pipeSurHLength, center.Y);

            SketchLine line = osketch.SketchLines.AddByTwoPoints(lineStart, lineEnd);
            Point2d arcCente = InventorTool.CreatePoint2d(lineEnd.X, lineEnd.Y - pipeSurHRaidus);
            SketchArc arc = osketch.SketchArcs.AddByCenterStartSweepAngle(arcCente, pipeSurHRaidus, Math.PI / 2, Math.PI/2);
            Point2d VlineEnd = InventorTool.CreatePoint2d(arc.EndSketchPoint.Geometry.X, arc.EndSketchPoint.Geometry.Y-1);
            SketchLine VLine = osketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, VlineEnd);
            InventorTool.CreateTwoPointCoinCident(osketch, arc, line);
            //InventorTool.CreateTwoPointCoinCident(osketch, arc, VLine);
            osketch.GeometricConstraints.AddVertical((SketchEntity)VLine);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)VLine.EndSketchPoint, (SketchEntity)BigCircle);
            //SketchPoint SurCenter = osketch.SketchPoints.Add(InventorTool.Origin);
            //osketch.GeometricConstraints.AddCoincident((SketchEntity)SurCenter, (SketchEntity)BigCircle);
            //osketch.GeometricConstraints.AddCoincident((SketchEntity)SurCenter, (SketchEntity)BigCircle);
            WorkPlane PipePlane = Definition.WorkPlanes.AddByPointAndTangent(VLine.EndSketchPoint, CSideFace, true);
            PlanarSketch pipeSketch = Definition.Sketches.Add(PipePlane);
            SketchPoint pipeCenter = (SketchPoint)pipeSketch.AddByProjectingEntity(VLine.EndSketchPoint);
            pipeSketch.SketchCircles.AddByCenterRadius(pipeCenter, pipeSurRadius);
            pipeSketch.SketchCircles.AddByCenterRadius(pipeCenter, pipeSurRadius + pipeSurThickness);
            Profile pipePro = pipeSketch.Profiles.AddForSolid();
            //ObjectCollection objc = InventorTool.CreateObjectCollection();
            //objc.Add(line);
            //objc.Add(arc);
            //objc.Add(VLine);
            Path pPath= Definition.Features.CreatePath(line);
            //  SweepDefinition def=  Definition.Features.SweepFeatures.CreateSweepDefinition(SweepTypeEnum.kPathSweepType, pipePro,pPath, PartFeatureOperationEnum.kJoinOperation);
            //return    Definition.Features.SweepFeatures.Add(def);
            return Definition.Features.SweepFeatures.AddUsingPath(pipePro, pPath, PartFeatureOperationEnum.kJoinOperation);
        }
        /// <summary>
        /// 创建管和管支架阵列、镜像
        /// </summary>
        /// <param name="pipe">管特征</param>
        /// <param name="pipeSur">管支架特征</param>
        /// <param name="axis">罐中心轴</param>
        /// <param name="pipeLength">管长度</param>
        /// <param name="pipeSurNum">单排管支架数量</param>
        /// <param name="pipeSurDistance">管支架距离管截面距离</param>
        /// <param name="pipeSurMirPlane">管支架镜像面</param>
        /// <param name="pipeMirPlane">管镜像面</param>
        void CreatePipeSurMid(ExtrudeFeature pipe,SweepFeature pipeSur,WorkAxis axis,double pipeLength,int pipeSurNum,double pipeSurDistance,WorkPlane pipeSurMirPlane,WorkPlane pipeMirPlane)
        {
            double temp = (pipeLength - pipeSurDistance * 2) / (pipeSurNum - 1);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(pipeSur);
            //RectangularPatternFeatureDefinition def=  Definition.Features.RectangularPatternFeatures.CreateDefinition(objc, axis, true, pipeSurNum, temp);
            //RectangularPatternFeature feature=  Definition.Features.RectangularPatternFeatures.AddByDefinition(def);
            RectangularPatternFeature feature = Definition.Features.RectangularPatternFeatures.Add(objc, axis, true, pipeSurNum, temp,PatternSpacingTypeEnum.kDefault,null,null,true,null,null,PatternSpacingTypeEnum.kDefault,null,PatternComputeTypeEnum.kAdjustToModelCompute);
            ObjectCollection MirObj = InventorTool.CreateObjectCollection();
            MirObj.Add(pipeSur);
            MirObj.Add(feature);
            //MirrorFeatureDefinition mirr=  Definition.Features.MirrorFeatures.CreateDefinition(MirObj, pipeSurMirPlane,PatternComputeTypeEnum.kAdjustToModelCompute);
            //MirrorFeature pipeSurs=  Definition.Features.MirrorFeatures.AddByDefinition(mirr);
            MirrorFeature pipeSurs = Definition.Features.MirrorFeatures.Add(MirObj, pipeSurMirPlane, false, PatternComputeTypeEnum.kAdjustToModelCompute);
            ObjectCollection pipeMirObj = InventorTool.CreateObjectCollection();
            pipeMirObj.Add(pipeSur);
            pipeMirObj.Add(feature);
            pipeMirObj.Add(pipeSurs);
            pipeMirObj.Add(pipe);

            //MirrorFeatureDefinition PipeDef = Definition.Features.MirrorFeatures.CreateDefinition(pipeMirObj, pipeMirPlane,PatternComputeTypeEnum.kAdjustToModelCompute);
            //Definition.Features.MirrorFeatures.AddByDefinition(PipeDef);
            Definition.Features.MirrorFeatures.Add(pipeMirObj, pipeMirPlane, false, PatternComputeTypeEnum.kAdjustToModelCompute);
        }
        #endregion
        /// <summary>
        /// 创建罐箍
        /// </summary>
        /// <param name="COutSF">罐外侧面</param>
        /// <param name="axis">罐中心轴</param>
        /// <param name="braceWidth">T型钢支柱宽度</param>
        /// <param name="braceHeight">T型钢</param>
        /// <param name="topWidth">T型钢头部宽度</param>
        /// <param name="topHeight">T型钢头部高度</param>
        /// <param name="TOffset">T型钢与罐口距离</param>
        /// <param name="THoopNum">箍数量</param>
        /// <param name="CLength">罐长度</param>
        RevolveFeature CreateHoop(Face COutSF, WorkAxis axis, double braceWidth, double braceHeight, double topWidth, double topHeight,
            double TOffset, int THoopNum, double CLength)
        {
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(COutSF.Edges.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2],true);
          SketchLine line1=(SketchLine)  osketch.AddByProjectingEntity(edges[0]);
            SketchLine line2 = (SketchLine)osketch.AddByProjectingEntity(edges[1]);
            SketchLine line3 = osketch.SketchLines.AddByTwoPoints(line1.StartSketchPoint, line2.StartSketchPoint);
          List<SketchLine> lines=  CreateTSteel(osketch, braceWidth, braceHeight, topWidth, topHeight);
            // osketch.GeometricConstraints.AddCollinear((SketchEntity)line3, (SketchEntity)lines[2]);
          
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            foreach (var item in lines)
            {
                objc.Add(item);
            }
            osketch.MoveSketchObjects(objc, InventorTool.TranGeo.CreateVector2d(-100, -100));
            InventorTool.AddTwoPointDistance(osketch, lines[2].StartSketchPoint, line3.EndSketchPoint, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = UsMM(par.TAndCYDiatance);
            InventorTool.AddTwoPointDistance(osketch, line3.EndSketchPoint, lines[2].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = TOffset;
            Profile pro = osketch.Profiles.AddForSolid();
            RevolveFeature T = Definition.Features.RevolveFeatures.AddFull(pro, axis, PartFeatureOperationEnum.kNewBodyOperation);
            ObjectCollection RectObjc = InventorTool.CreateObjectCollection();
            RectObjc.Add(T);
            /*RectangularPatternFeatureDefinition def=*/
            Definition.Features.RectangularPatternFeatures.Add(RectObjc, axis, true, THoopNum,(CLength - TOffset * 2) / (THoopNum - 1));
           // Definition.Features.RectangularPatternFeatures.AddByDefinition(def);
            return T;
        }
        /// <summary>
        /// 创建T型钢
        /// </summary>
        /// <param name="osketch">T型钢草图</param>
        /// <param name="braceWidth">T型钢支架宽度</param>
        /// <param name="braceHeight">T型钢支柱高度</param>
        /// <param name="topWidth">T型钢截面宽度</param>
        /// <param name="topHeight">截面高度</param>
        /// <returns></returns>
        List<SketchLine> CreateTSteel(PlanarSketch osketch,double braceWidth,double braceHeight,double topWidth,double topHeight)
        {
          SketchEntitiesEnumerator entitys1=  InventorTool.CreateRangle(osketch, braceWidth, braceHeight);
            SketchEntitiesEnumerator entitys2 = InventorTool.CreateRangle(osketch, topWidth, topHeight);
            List<SketchLine> lines1 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entitys1.GetEnumerator());
            List<SketchLine> lines2 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entitys2.GetEnumerator());
            osketch.GeometricConstraints.AddCollinear((SketchEntity)lines1[0], (SketchEntity)lines2[2]);
            InventorTool.AddTwoPointDistance(osketch, lines1[0].StartSketchPoint, lines2[2].EndSketchPoint,0,DimensionOrientationEnum.kAlignedDim ).Parameter.Value= (topWidth - braceWidth) / 2;
          lines1.AddRange(lines2);
            return lines1;
        }
        ExtrudeFeature  CreateEndLong(RevolveFeature Hoop,SketchCircle COutCircle,WorkAxis axis,double braceWidth, double braceHeight, double topWidth, double topHeight,double Angle,double CRadius,double EndLongLength)
        {
            List<Face> HoopFaces = InventorTool.GetCollectionFromIEnumerator<Face>(Hoop.SideFaces.GetEnumerator());
            //for (int i = 0; i < HoopFaces.Count; i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(HoopFaces[i], 0).Name = "a" + i;
            //}
            PlanarSketch osketch = Definition.Sketches.Add(HoopFaces[5]);
            SketchCircle outCircle = (SketchCircle)osketch.AddByProjectingEntity(COutCircle);
            outCircle.Construction = true;
            SketchCircle BigCir = osketch.SketchCircles.AddByCenterRadius(outCircle.CenterSketchPoint, outCircle.Radius + UsMM(par.TAndCYDiatance));
            BigCir.Construction = true;
           List<SketchLine> lines= CreateTSteel(osketch, braceWidth, braceHeight, topWidth, topHeight);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            foreach (var item in lines)
            {
                objc.Add(item);
            }
            Point2d p1 = InventorTool.CreatePoint2d(topWidth / 2, CRadius+UsMM(par.TAndCYDiatance));
            Point2d p2 = lines[4].EndSketchPoint.Geometry;
           // Vector2d V = InventorTool.TranGeo.CreateVector2d(p1.X - p2.X, p1.Y - p2.Y);
            // osketch.MoveSketchObjects(objc,V);
            lines[4].EndSketchPoint.MoveTo(p1);
             osketch.GeometricConstraints.AddTangent((SketchEntity)BigCir, (SketchEntity)lines[4]);
            osketch.RotateSketchObjects(objc, outCircle.CenterSketchPoint.Geometry, Angle/180*Math.PI-Math.PI/2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(EndLongLength, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
          return  Definition.Features.ExtrudeFeatures.Add(def);
        }
        void CreateEndLondMirror(ExtrudeFeature EndLong,WorkAxis Axis, double braceWidth,int EndLongNumber,int HoopNumber,double endloopLength)
        {
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(EndLong);
            //RectangularPatternFeatureDefinition def = Definition.Features.RectangularPatternFeatures.CreateDefinition(objc, Axis, true, HoopNumber - 1, braceWidth);
            //RectangularPatternFeature feature = Definition.Features.RectangularPatternFeatures.AddByDefinition(def);
            RectangularPatternFeature feature = Definition.Features.RectangularPatternFeatures.Add(objc, Axis, true, HoopNumber - 1, braceWidth+ endloopLength, PatternSpacingTypeEnum.kDefault,null,null,true,null,null,PatternSpacingTypeEnum.kDefault,null,PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Add(feature);
            //CircularPatternFeatureDefinition CirCularDef=  Definition.Features.CircularPatternFeatures.CreateDefinition(objc, Axis, true, EndLongNumber, Math.PI*2,true);
            //  CirCularDef.Angle = Math.PI * 2;
            //  Definition.Features.CircularPatternFeatures.AddByDefinition(CirCularDef);
            Definition.Features.CircularPatternFeatures.Add(objc, Axis, true, EndLongNumber, Math.PI * 2,true,PatternComputeTypeEnum.kAdjustToModelCompute);
        }
        #endregion
        public void CreateHoles(List<ParCylinderHole> holes,double offset)
        {
            List<Edge> outFaceEdges = InventorTool.GetCollectionFromIEnumerator<Edge>(CSideFace[1].Edges.GetEnumerator());
            CreatePlanes(CStartFace, CSideFace[1], holes);
            foreach (var item in holes)  //创建孔、短管、法兰
            {
                WorkPlane plane = _HolePlanes[item.PositionAngle];
                if (plane == null) continue;

                CreateHole(plane, CStartFace, axis, item, outFaceEdges[1],CSideFace[0],offset);
            }
        }
        /// <summary>
        /// 罐体开孔
        /// </summary>
        /// <param name="plane">开孔的平面</param>
        /// <param name="DistanceFace">罐口面</param>
        /// <param name="axis">罐中心轴</param>
        /// <param name="parHole">孔参数</param>
        /// <param name="outFaceEdge">罐外侧面边</param>
        private void CreateHole(WorkPlane plane, Face DistanceFace, WorkAxis axis, ParCylinderHole parHole, Edge outFaceEdge,Face InFace,double offset)
        {
            #region 创建罐体孔
            double x, y;
            if (parHole.PositionAngle >180 && parHole.PositionAngle < 360)
            {
                x = 1;
                y = 1;
                if (parHole.HoleOffset < 0)
                {
                    y = -1;
                }
            }
            else
            {
                x = -1;
                y = -1;
                if (parHole.HoleOffset < 0)
                {
                    y = 1;
                }
                if(parHole.PositionAngle==0)
                {
                    x = 1;
                    y = 1;
                }
            }
           
           // PlanarSketch osketch=Definition.Sketches.AddWithOrientation()
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            Edge edge = InventorTool.GetFirstFromIEnumerator<Edge>(DistanceFace.Edges.GetEnumerator());
            SketchLine line = (SketchLine)osketch.AddByProjectingEntity(edge);
            line.Construction = true;
            SketchLine centerLine = (SketchLine)osketch.AddByProjectingEntity(axis);
            centerLine.Construction = true;
            SketchPoint origin = osketch.SketchPoints.Add(InventorTool.Origin); //边缘中心点 辅助点
            osketch.GeometricConstraints.AddCoincident((SketchEntity)line, (SketchEntity)origin);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)centerLine, (SketchEntity)origin);
            SketchPoint holeCenter = osketch.SketchPoints.Add(InventorTool.CreatePoint2d(x, y));
            TwoPointDistanceDimConstraint offSetDim = osketch.DimensionConstraints.AddTwoPointDistance(origin, holeCenter, DimensionOrientationEnum.kVerticalDim, holeCenter.Geometry);
            offSetDim.Parameter.Value = Math.Abs(UsMM(parHole.HoleOffset));
            TwoPointDistanceDimConstraint distanceDim = osketch.DimensionConstraints.AddTwoPointDistance(origin, holeCenter, DimensionOrientationEnum.kHorizontalDim, holeCenter.Geometry);
            distanceDim.Parameter.Value = UsMM(parHole.PositionDistance)-offset;
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(holeCenter);
            SketchHolePlacementDefinition HolePlace = Definition.Features.HoleFeatures.CreateSketchPlacementDefinition(objc);
              HoleFeature hole = Definition.Features.HoleFeatures.AddDrilledByDistanceExtent(HolePlace, UsMM(parHole.ParFlanch.D6), UsMM(par.InDiameter.Value/2 + par.Thickness.Value), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           // hole.Name = parHole.Name;
            // Definition.Features.HoleFeatures.AddDrilledByToFaceExtent(HolePlace, parHole.ParFlanch.D6,InFace,true);
            // Definition.Features.HoleFeatures.AddDrilledByToFaceExtent(HolePlace, parHole.HoleRadius * 2, holeEndFace, true);
            #endregion
          
        }
        /// <summary>
        /// 创建开孔平面
        /// </summary>
        /// <param name="outTageFace"></param>
        /// <param name="outFace"></param>
        /// <param name="parHoles"></param>
        private void CreatePlanes(Face outTageFace, Face outFace,List<ParCylinderHole> parHoles)
        {
            PlanarSketch osketch = Definition.Sketches.Add(outTageFace);
            osketch.Visible = false;
            List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(outFace.Edges.GetEnumerator());
            SketchCircle cycle = (SketchCircle)osketch.AddByProjectingEntity(edges[0]);
            SketchLine line = osketch.SketchLines.AddByTwoPoints(cycle.CenterSketchPoint, InventorTool.TranGeo.CreatePoint2d(10, 0));
            line.Construction = true;
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)line);

            foreach (var item in parHoles)
            {
                if (_HolePlanes.ContainsKey(item.PositionAngle)) continue;
                SketchPoint p = osketch.SketchPoints.Add(InventorTool.CreatePoint2d(1, 1));
                osketch.GeometricConstraints.AddCoincident((SketchEntity)p, (SketchEntity)cycle);
                ThreePointAngleDimConstraint dim = osketch.DimensionConstraints.AddThreePointAngle(p, cycle.CenterSketchPoint, line.EndSketchPoint, p.Geometry);
                dim.Parameter.Value = item.PositionAngle / 180 * Math.PI;
                dim.Visible = false;

                WorkPlane plane = Definition.WorkPlanes.AddByPointAndTangent(p, outFace);
                _HolePlanes.Add(item.PositionAngle, plane);
            }


        }
    }
}
