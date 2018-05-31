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
namespace ParamedModule.HeatSinkSystem
{
    //[Export(typeof(IParamedModule))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class Cap : PartModulebase
    {
        public ParCap par = new ParCap();
        public Cap():base()
        {

        }
        public Cap(PassedParameter inDiameter, PassedParameter thickness) : base()
        {
            this.Parameter = par;
            this.Name = "热沉盖";
            par.InDiameter = inDiameter;
            par.Thickness = thickness;
            init();
        }
        void init()
        {
            par.InDiameter.Value = 2000;
            par.Thickness.Value = 24;
            par.CapThickness = 30;
            par.SlotOffset = 200;
            par.SlotHight = 50;
            par.SlotThickness = 6;
            par.SlotWide = 60;
            par.PipeAngle = 120;
            par.PipeDiameter = 60;
            par.PipeThickness = 4;
            par.PipeXOffset = 60;
            par.PipeYOffset = 100;
            par.PipeSurDistance = 15;
            par.PipeSurCurveRadius = 20;
            par.PipeSurLength = 20;
            par.PipeSurDiameter = 10;
            par.PipeSurThickness = 2;
            par.PipeSurNum = 5;
            par.TitleWidth = 50;
            par.TitleHeigh = 10;
            par.TitleLength = 100;
            par.TitleOffset = 30;
            par.PlugOffset = 20;
            par.PlugWidth = 50;
            par.PlugHeight = 10;
            par.PlugLenght=100;
            par.PlugHoleDiameter = 30;
            par.PlugHoleDistance = 30;
        }
        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule()
        {
            CreateDoc();
            Face capStartFace;//盖子结束面
            SketchCircle CapCircle;//盖子圆草图
            WorkPlane pipePlane;
            ExtrudeFeature cap = CreateCap(UsMM(par.InDiameter.Value / 2 + par.Thickness.Value), UsMM(par.CapThickness), out capStartFace, out CapCircle);
            Face EndFace = InventorTool.GetFirstFromIEnumerator<Face>(cap.EndFaces.GetEnumerator());
            Definition.iMateDefinitions.AddMateiMateDefinition(EndFace, 0).Name="Face";
            Face capSideFace = InventorTool.GetFirstFromIEnumerator<Face>(cap.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(capSideFace);
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "Axis";
            Face SlotOutFace = CreateSlots(CapCircle, Axis, UsMM(par.SlotOffset), UsMM(par.SlotThickness), UsMM(par.SlotWide), UsMM(par.SlotHight));
            RevolveFeature pipe = CreatePipe(Axis, Definition.WorkPlanes[3], CapCircle, par.PipeAngle, UsMM(par.PipeDiameter / 2), UsMM(par.PipeThickness), UsMM(par.PipeXOffset), UsMM(par.PipeYOffset), out pipePlane);
            SweepFeature pipeSur = CreatePipeSup(pipe, Axis, pipePlane, CapCircle, par.PipeSurDistance, UsMM(par.PipeDiameter / 2), UsMM(par.PipeThickness), UsMM(par.PipeXOffset), UsMM(par.PipeYOffset),
                   UsMM(par.PipeSurLength), UsMM(par.PipeSurCurveRadius), UsMM(par.PipeSurDiameter / 2), UsMM(par.PipeSurThickness), capStartFace);
            CreatePipeSurMirror(pipe, pipeSur, Axis, par.PipeAngle, par.PipeSurDistance, par.PipeSurNum);
            CreateTitle(SlotOutFace, CapCircle, Axis,UsMM(par.TitleWidth), UsMM(par.TitleHeigh), UsMM(par.TitleOffset), UsMM(par.TitleLength));
            CreatePlug(SlotOutFace, UsMM(par.PlugWidth), UsMM(par.PlugHeight),UsMM(par.PlugLenght), UsMM(par.PlugOffset / 2), UsMM(par.PlugHoleDiameter / 2), UsMM(par.PlugHoleDistance));
            SaveDoc();
        }
        #region 创建槽
        ExtrudeFeature CreateCap(double radius, double thickness, out Face StartFace, out SketchCircle circle)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            circle = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, radius);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            ExtrudeFeature feature = Definition.Features.ExtrudeFeatures.Add(def);
            StartFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.StartFaces.GetEnumerator());
            return feature;

        }
        Face CreateSlots(SketchCircle capCircle, WorkAxis Axis, double SlotOffset, double slotThickness, double slotWidth, double slotHeight)
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            SketchLine line = (SketchLine)osketch.AddByProjectingEntity(capCircle);
            // SketchPoint center = (SketchPoint)osketch.AddByProjectingEntity(Axis);
            List<SketchLine> lines = CreateSlot(osketch, slotWidth, slotHeight, slotThickness, new XY(0, 0));
            osketch.GeometricConstraints.AddCollinear((SketchEntity)lines[0], (SketchEntity)line);
            InventorTool.AddTwoPointDistance(osketch, lines[0].EndSketchPoint, line.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = SlotOffset;
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                item.AddsMaterial = true;
            }
            RevolveFeature Slot = Definition.Features.RevolveFeatures.AddFull(pro, Axis, PartFeatureOperationEnum.kNewBodyOperation);
            List<Face> slotSF = InventorTool.GetCollectionFromIEnumerator<Face>(Slot.SideFaces.GetEnumerator());
            //for (int i = 0; i < slotSF.Count; i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(slotSF[i], 0).Name = "a" + i;
            //}
            ExtrudeFeature Xslot1 = CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], Definition.WorkPlanes[2], true);
            Face XslotFace = InventorTool.GetFirstFromIEnumerator<Face>(Xslot1.StartFaces.GetEnumerator());
            List<Face> XslotSF = InventorTool.GetCollectionFromIEnumerator<Face>(Xslot1.SideFaces.GetEnumerator());

            ExtrudeFeature Xslot2 = CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], XslotFace, true);
            //for (int i = 0; i < XslotSF.Count; i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(XslotSF[i], 0).Name = "a" + i;
            //}
            ExtrudeFeature Yslot1 = CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], XslotSF[4], true);
            ExtrudeFeature Yslot2 = CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], XslotSF[6], true);
            return slotSF[6];
           
        }

        private ExtrudeFeature CreateSubSlot(SketchCircle capCircle, double slotThickness, double slotWidth, double slotHeight, Face slotSF, object plane, bool direct)
        {
            PlanarSketch Xsketch = Definition.Sketches.Add(plane);
            List<SketchLine> Xlines = CreateSlot(Xsketch, slotWidth, slotHeight, slotThickness, new XY(0, 0));
            SketchLine Xline = (SketchLine)Xsketch.AddByProjectingEntity(capCircle);
            Xsketch.GeometricConstraints.AddCollinear((SketchEntity)Xline, (SketchEntity)Xlines[0]);
            InventorTool.AddTwoPointDistance(Xsketch, Xline.EndSketchPoint, Xlines[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = Xline.Length / 2 - slotWidth / 2;
            Profile Xpro = Xsketch.Profiles.AddForSolid();
            ExtrudeDefinition Xdef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(Xpro, PartFeatureOperationEnum.kJoinOperation);
            if (direct)
            {
                Xdef.SetToNextExtent(PartFeatureExtentDirectionEnum.kPositiveExtentDirection, slotSF.SurfaceBody);
            }
            else
            {
                Xdef.SetToNextExtent(PartFeatureExtentDirectionEnum.kNegativeExtentDirection, slotSF.SurfaceBody);
            }

            return Definition.Features.ExtrudeFeatures.Add(Xdef);
        }

        List<SketchLine> CreateSlot(PlanarSketch osketch, double width, double height, double thickness, XY point)
        {
            //SketchEntitiesEnumerator entity1=  InventorTool.CreateRangle(osketch, thickness, width);
            //  SketchEntitiesEnumerator entity2 = InventorTool.CreateRangle(osketch, height-thickness, thickness);
            //  List<SketchLine> lines1 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entity1.GetEnumerator());
            //  List<SketchLine> lines2 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entity2.GetEnumerator());
            //  osketch.GeometricConstraints.AddCollinear((SketchEntity)lines1[1], (SketchEntity)lines2[3]);
            //  osketch.GeometricConstraints.AddCoincident((SketchEntity)lines1[1].StartSketchPoint, (SketchEntity)lines2[0]);


            //  SketchEntitiesEnumerator entity3 = InventorTool.CreateRangle(osketch, height - thickness, thickness);
            //  List<SketchLine> lines3 = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entity3.GetEnumerator());
            //  osketch.GeometricConstraints.AddCollinear((SketchEntity)lines1[1], (SketchEntity)lines3[3]);
            //  osketch.GeometricConstraints.AddCoincident((SketchEntity)lines1[1].EndSketchPoint, (SketchEntity)lines3[2]);
            Point2d p0 = InventorTool.CreatePoint2d(point.X - width / 2, point.Y);
            Point2d p1 = InventorTool.CreatePoint2d(point.X + width / 2, point.Y);
            Point2d p2 = InventorTool.CreatePoint2d(point.X + width / 2, point.Y + height);
            Point2d p3 = InventorTool.CreatePoint2d(point.X + width / 2 - thickness, point.Y + height);
            Point2d p4 = InventorTool.CreatePoint2d(point.X + width / 2 - thickness, point.Y + thickness);
            Point2d p5 = InventorTool.CreatePoint2d(point.X - width / 2 + thickness, point.Y + thickness);
            Point2d p6 = InventorTool.CreatePoint2d(point.X - width / 2 + thickness, point.Y + height);
            Point2d p7 = InventorTool.CreatePoint2d(point.X - width / 2, point.Y + height);
            List<SketchLine> lines = new List<SketchLine>();
            lines.Add(osketch.SketchLines.AddByTwoPoints(p0, p1));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p1, p2));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p2, p3));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p3, p4));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p4, p5));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p5, p6));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p6, p7));
            lines.Add(osketch.SketchLines.AddByTwoPoints(p7, p0));
            for (int i = 1; i < lines.Count; i++)
            {
                InventorTool.CreateTwoPointCoinCident(osketch, lines[i - 1], lines[i]);
            }
            InventorTool.CreateTwoPointCoinCident(osketch, lines[0], lines[7]);
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[0], (SketchEntity)lines[2]);
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[0], (SketchEntity)lines[4]);
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[0], (SketchEntity)lines[6]);
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[1], (SketchEntity)lines[3]);
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[1], (SketchEntity)lines[5]);
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[1], (SketchEntity)lines[7]);
            osketch.GeometricConstraints.AddPerpendicular((SketchEntity)lines[1], (SketchEntity)lines[0]);
            osketch.GeometricConstraints.AddEqualLength(lines[1], lines[7]);
            osketch.GeometricConstraints.AddEqualLength(lines[2], lines[6]);
            InventorTool.AddTwoPointDistance(osketch, lines[0].StartSketchPoint, lines[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = width;
            InventorTool.AddTwoPointDistance(osketch, lines[1].StartSketchPoint, lines[1].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = height;
            InventorTool.AddTwoPointDistance(osketch, lines[2].StartSketchPoint, lines[2].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = thickness;
            return lines;
        }
        #endregion
        #region 创建管
        RevolveFeature CreatePipe(WorkAxis Axis, WorkPlane plane, SketchCircle circle, double Angle, double radius, double thickness, double pipeXOffset, double pipeYOffset, out WorkPlane pipePlane)
        {
            pipePlane = Definition.WorkPlanes.AddByLinePlaneAndAngle(Axis, plane, Angle / 360 * Math.PI, true);
            PlanarSketch osketch = Definition.Sketches.Add(pipePlane);
            SketchLine line = (SketchLine)osketch.AddByProjectingEntity(circle);
            SketchCircle pipeCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(-radius, radius), radius);
            SketchCircle pipeOutCircle = osketch.SketchCircles.AddByCenterRadius(pipeCircle.CenterSketchPoint, radius + thickness);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)pipeCircle, (SketchEntity)pipeOutCircle);
            InventorTool.AddTwoPointDistance(osketch, line.StartSketchPoint, pipeCircle.CenterSketchPoint, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = pipeYOffset;
            InventorTool.AddTwoPointDistance(osketch, line.StartSketchPoint, pipeCircle.CenterSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = pipeXOffset;
            Profile pro = osketch.Profiles.AddForSolid();
            return Definition.Features.RevolveFeatures.AddByAngle(pro, Axis, Angle / 180 * Math.PI, PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kNewBodyOperation);
        }
        SweepFeature CreatePipeSup(RevolveFeature pipe, WorkAxis Axis, WorkPlane pipePlane, SketchCircle circle, double Angle, double radius, double thickness, double pipeXOffset, double pipeYOffset,
            double pipeSupLength, double pipeSurHRaidus, double PipeSurRadius, double pipeSurThickness, Face CapEndFace)
        {
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(pipe.EndFaces.GetEnumerator());
            WorkPlane pipeSupPlane = Definition.WorkPlanes.AddByLinePlaneAndAngle(Axis, pipePlane, -Angle / 180 * Math.PI, true);
            PlanarSketch osketch = Definition.Sketches.Add(pipeSupPlane);
            SketchLine capLine = (SketchLine)osketch.AddByProjectingEntity(circle);
            SketchCircle pipeOutCircle = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(-radius - thickness, radius + thickness), radius + thickness);
            InventorTool.AddTwoPointDistance(osketch, capLine.StartSketchPoint, pipeOutCircle.CenterSketchPoint, 0, DimensionOrientationEnum.kVerticalDim).Parameter.Value = pipeYOffset;
            InventorTool.AddTwoPointDistance(osketch, capLine.StartSketchPoint, pipeOutCircle.CenterSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = pipeXOffset;

            Point2d center = pipeOutCircle.CenterSketchPoint.Geometry;
            //  Point2d lineStart = InventorTool.CreatePoint2d(center.X , center.Y- pipeOutCircle.Radius);
            Point2d lineEnd = InventorTool.CreatePoint2d(center.X, center.Y - pipeSupLength - pipeOutCircle.Radius);
            SketchLine line = osketch.SketchLines.AddByTwoPoints(pipeOutCircle.CenterSketchPoint, lineEnd);

            //osketch.GeometricConstraints.AddCoincident((SketchEntity)line.StartSketchPoint, (SketchEntity)pipeOutCircle);

            Point2d arcCente = InventorTool.CreatePoint2d(0, -circle.Radius);
            SketchArc arc = osketch.SketchArcs.AddByCenterStartSweepAngle(arcCente, pipeSurHRaidus, Math.PI, Math.PI / 2);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)arc.StartSketchPoint, (SketchEntity)line);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)arc, (SketchEntity)line.EndSketchPoint);
            ///设置管支撑路径水平距离=管半径+壁厚+路径水平距离
            InventorTool.AddTwoPointDistance(osketch, line.StartSketchPoint, line.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = pipeSupLength + radius + thickness;
            osketch.DimensionConstraints.AddRadius((SketchEntity)arc, arc.CenterSketchPoint.Geometry).Parameter.Value = pipeSurHRaidus;
            Point2d VlineEnd = InventorTool.CreatePoint2d(arc.EndSketchPoint.Geometry.X, arc.EndSketchPoint.Geometry.Y - 1);
            SketchLine VLine = osketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, VlineEnd);

            osketch.GeometricConstraints.AddHorizontal((SketchEntity)VLine);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)VLine.EndSketchPoint, (SketchEntity)capLine);

            PlanarSketch pipesketch = Definition.Sketches.Add(CapEndFace);
            SketchPoint pipeCenter = (SketchPoint)pipesketch.AddByProjectingEntity(VLine);
            SketchCircle pipeCircle1 = pipesketch.SketchCircles.AddByCenterRadius(pipeCenter, PipeSurRadius);
            SketchCircle pipeCircle2 = pipesketch.SketchCircles.AddByCenterRadius(pipeCenter, PipeSurRadius + pipeSurThickness);
            Profile pipePro = pipesketch.Profiles.AddForSolid();
            Path pPath = Definition.Features.CreatePath(line);
            //SweepDefinition def = Definition.Features.SweepFeatures.CreateSweepDefinition(SweepTypeEnum.kPathSweepType, pipePro, pPath, PartFeatureOperationEnum.kNewBodyOperation);
            //return Definition.Features.SweepFeatures.Add(def);
            return Definition.Features.SweepFeatures.AddUsingPath(pipePro, pPath, PartFeatureOperationEnum.kNewBodyOperation);
        }
        void CreatePipeSurMirror(RevolveFeature pipe, SweepFeature pipeSur, WorkAxis Axis, double PipeAngle, double FirstSurAngle, int PipeSurNumber)
        {
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(pipeSur);
            double angle = (PipeAngle - FirstSurAngle * 2) * Math.PI / 180;
            //CircularPatternFeatureDefinition circular= Definition.Features.CircularPatternFeatures.CreateDefinition(objc, Axis, false, PipeSurNumber, angle, true);
            // circular.Angle = angle;
            // circular.PositioningMethod = PatternPositioningMethodEnum.kFittedPositioningMethod;
            // circular.Operation = PartFeatureOperationEnum.kNewBodyOperation;
            // CircularPatternFeature cir= Definition.Features.CircularPatternFeatures.AddByDefinition(circular);
            CircularPatternFeature cir = Definition.Features.CircularPatternFeatures.Add(objc, Axis, false, PipeSurNumber, angle, true, PatternComputeTypeEnum.kAdjustToModelCompute);
            objc.Add(cir);
            objc.Add(pipe);
            //MirrorFeatureDefinition MirrorDef = Definition.Features.MirrorFeatures.CreateDefinition(objc, Definition.WorkPlanes[2],PatternComputeTypeEnum.kAdjustToModelCompute);
            //Definition.Features.MirrorFeatures.AddByDefinition(MirrorDef);
            Definition.Features.MirrorFeatures.Add(objc, Definition.WorkPlanes[2], false, PatternComputeTypeEnum.kAdjustToModelCompute);
            #region 切割
            //Face pipeFirstFace = InventorTool.GetFirstFromIEnumerator<Face>(pipe.StartFaces.GetEnumerator());
            //Face pipeEndFace = InventorTool.GetFirstFromIEnumerator<Face>(pipe.EndFaces.GetEnumerator());
            //List<Edge> edges = InventorTool.GetCollectionFromIEnumerator<Edge>(pipeFirstFace.Edges.GetEnumerator());
            //PlanarSketch osketh = Definition.Sketches.Add(pipeFirstFace);
            //osketh.AddByProjectingEntity(edges[0]);
            //Profile pro = osketh.Profiles.AddForSolid();
            //SketchLine line = (SketchLine)osketh.AddByProjectingEntity(Axis);
            //RevolveFeature clear = Definition.Features.RevolveFeatures.AddByAngle(pro, line, 120 / 180 * Math.PI, PartFeatureExtentDirectionEnum.kNegativeExtentDirection, PartFeatureOperationEnum.kCutOperation);
            #endregion
        }
        #endregion
        #region 创建两头
        void CreateTitle(Face surFace, SketchCircle BigCircle,WorkAxis Axis, double width, double height, double offset, double length)
        {
            Point p = InventorTool.TranGeo.CreatePoint(0, 10, 0);
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndTangent(Definition.WorkPlanes[2], surFace, p,true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            SketchLine line = (SketchLine)osketch.AddByProjectingEntity(BigCircle);
           SketchEntitiesEnumerator entities= osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d( -height / 2, - width / 2), InventorTool.CreatePoint2d(height / 2,width / 2));
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            InventorTool.AddTwoPointDistance(osketch, line.EndSketchPoint, lines[0].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value = offset;
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           ExtrudeFeature extrude=  Definition.Features.ExtrudeFeatures.Add(def);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(extrude);
            Definition.Features.CircularPatternFeatures.Add(objc, Axis, true, 2, Math.PI / 2, false, PatternComputeTypeEnum.kAdjustToModelCompute);
            Definition.Features.CircularPatternFeatures.Add(objc, Axis, false, 2, Math.PI / 2, false, PatternComputeTypeEnum.kAdjustToModelCompute);
        

        }
        void CreatePlug(Face surFace,double width,double height,double length,double offset,double radius,double CenterPointDistance)
        {
           
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[3], offset,true);
            PlanarSketch osketch = Definition.Sketches.Add(plane);
            Edge edge = InventorTool.GetFirstFromIEnumerator<Edge>(surFace.Edges.GetEnumerator());
            SketchLine line = (SketchLine)osketch.AddByProjectingEntity(edge);
            Point2d p = line.StartSketchPoint.Geometry;
            SketchEntitiesEnumerator entities = osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(p.X - offset, p.Y ), InventorTool.CreatePoint2d(p.X - width - offset, p.Y - length));
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            InventorTool.AddTwoPointDistance(osketch, line.StartSketchPoint, lines[0].StartSketchPoint, 0, DimensionOrientationEnum.kHorizontalDim).Parameter.Value=offset;
            Point2d HoleCenter = InventorTool.CreatePoint2d(lines[0].EndSketchPoint.Geometry.X+width/2, lines[0].EndSketchPoint.Geometry.Y-radius-CenterPointDistance);
            osketch.SketchCircles.AddByCenterRadius(HoleCenter, radius);


            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrude = Definition.Features.ExtrudeFeatures.Add(def);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(extrude);
            Definition.Features.MirrorFeatures.Add(objc, Definition.WorkPlanes[3], false, PatternComputeTypeEnum.kAdjustToModelCompute);
            //for (int i = 0; i < lines.Count; i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(lines[i], 0).Name = "b" + i;
            //}
        }
        #endregion

    }
}
