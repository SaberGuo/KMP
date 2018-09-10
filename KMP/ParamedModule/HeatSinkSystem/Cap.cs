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
    [Export("Cap", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Cap : PartModulebase
    {
        public ParCap par = new ParCap();
        public Cap():base()
        {

        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        public Cap(PassedParameter inDiameter, PassedParameter thickness) : base()
        {
            this.Parameter = par;
            this.Name = "大门热沉";
            this.ProjectType = "Cap";
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
            par.InSlotDiameter = 50;

            par.LiqPipeTurnDiameter = 114;
            par.LiqPipeThickness = 2.5;
            par.LiqPipeInDiameter = 40;
            par.LiqPipeIsCreate = true;
            par.LiqPipeDirection = true;
            par.LiqPipeHeight = 143;
            par.LiqPipeLength2.Add(286);
            par.LiqPipeLength2.Add(286);
            par.LiqPipeLength2.Add(200);
            par.LiqPipeLength2.Add(600);
            par.LiqPipeLength1.Add(486);
            par.LiqPipeLength1.Add(943);
        }
        public override bool CheckParamete()
        {
            double Radius = par.InDiameter.Value / 2 + par.Thickness.Value;//盖子半径
            if (!CheckParZero()) return false;
            if(par.PipeAngle<0||par.PipeAngle>140)
            {
                ParErrorChanged(this, "管道角度超出范围");
                return false;
            }
            if(par.PipeYOffset<=par.PipeDiameter/2)
            {
                ParErrorChanged(this, "管道直径大于管道与盖面的距离的两倍！");
                return false;
            }
            if(par.PipeXOffset+par.PipeDiameter/2>=par.SlotOffset-par.SlotWide/2)
            {
                ParErrorChanged(this, "管道与圆槽相交");
                return false;
            }
            if (par.PipeSurDiameter >= par.PipeDiameter||par.PipeSurDiameter+par.PipeSurThickness*2>par.PipeDiameter+par.PipeThickness*2)
            {
                ParErrorChanged(this, "管道支架横截面直径大于管道横截面直径！");
                return false;
            }
             if((par.PipeDiameter/2+par.PipeThickness+par.PipeSurLength+par.PipeSurCurveRadius+par.PipeSurDiameter/2+par.PipeSurThickness)> par.PipeYOffset)
            {
                ParErrorChanged(this, "管道支架距离盖中心过远！");
                return false;
            }
             if(par.PipeSurNum<3)
            {
                ParErrorChanged(this, "管道支架数量过小！");
                return false;
            }
             if(((par.PipeAngle-par.PipeSurDiameter*2)/par.PipeSurNum)/180*Math.PI*(Radius-par.PipeYOffset) <=(par.PipeSurThickness*2+par.PipeSurDiameter))
                {
                ParErrorChanged(this, "管道间放不下"+par.PipeSurNum+"个支架！");
                return false;
            }
            if ((par.SlotOffset+par.SlotWide+par.SlotThickness*2)> Radius)
            {
                ParErrorChanged(this, "槽宽度和槽与门边的距离和大于盖半径！");
                return false;
            }
            if (par.InSlotDiameter / 2 + par.SlotThickness * 2 + par.SlotOffset >= par.InDiameter.Value + par.Thickness.Value - 1)
            {
                ParErrorChanged(this, "内部环形骨架与外部环形骨架重叠！");
                return false;
            }
            return true;
        }

     
        public override void CreateSub()
        {
            Face capStartFace;//盖子结束面
            SketchCircle CapCircle;//盖子圆草图
            WorkPlane pipePlane;
            ExtrudeFeature cap = CreateCap(UsMM(par.InDiameter.Value / 2 + par.Thickness.Value), UsMM(par.CapThickness), out capStartFace, out CapCircle);
            cap.Name = "HeartCap";
            List<Face> start = InventorTool.GetCollectionFromIEnumerator<Face>(cap.StartFaces.GetEnumerator());
            for (int i = 0; i < start.Count; i++)
            {
                Definition.iMateDefinitions.AddMateiMateDefinition(start[i], 0).Name = "face" + i;
            }
            Face EndFace = InventorTool.GetFirstFromIEnumerator<Face>(cap.EndFaces.GetEnumerator());
            Definition.iMateDefinitions.AddMateiMateDefinition(EndFace, 0).Name = "Face";
            Face capSideFace = InventorTool.GetFirstFromIEnumerator<Face>(cap.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(capSideFace);
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "Axis";
            Face SlotOutFace = CreateSlots(CapCircle, Axis, UsMM(par.SlotOffset), UsMM(par.SlotThickness), UsMM(par.SlotWide), UsMM(par.SlotHight), UsMM(par.InSlotDiameter / 2));
            RevolveFeature pipe = CreatePipe(Axis, Definition.WorkPlanes[3], CapCircle, par.PipeAngle, UsMM(par.PipeDiameter / 2), UsMM(par.PipeThickness), UsMM(par.PipeXOffset), UsMM(par.PipeYOffset), out pipePlane);
            SweepFeature pipeSur = CreatePipeSup(pipe, Axis, pipePlane, CapCircle, par.PipeSurDistance, UsMM(par.PipeDiameter / 2), UsMM(par.PipeThickness), UsMM(par.PipeXOffset), UsMM(par.PipeYOffset),
                   UsMM(par.PipeSurLength), UsMM(par.PipeSurCurveRadius), UsMM(par.PipeSurDiameter / 2), UsMM(par.PipeSurThickness), capStartFace);
            CreatePipeSurMirror(pipe, pipeSur, Axis, par.PipeAngle, par.PipeSurDistance, par.PipeSurNum);
            CreateTitle(SlotOutFace, CapCircle, Axis, UsMM(par.TitleWidth), UsMM(par.TitleHeigh), UsMM(par.TitleOffset), UsMM(par.TitleLength));
            CreatePlug(SlotOutFace, UsMM(par.PlugWidth), UsMM(par.PlugHeight), UsMM(par.PlugLenght), UsMM(par.PlugOffset / 2), UsMM(par.PlugHoleDiameter / 2), UsMM(par.PlugHoleDistance));

            if (par.LiqPipeIsCreate)
            {
                CreateLiquidPipe(capStartFace, CapCircle,true);
            }
        }
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
        #region 创建槽钢

        /// <summary>
        /// 创建骨架
        /// </summary>
        /// <param name="capCircle">大门圆草图</param>
        /// <param name="Axis">大门中心轴</param>
        /// <param name="SlotOffset">外环槽到边距离</param>
        /// <param name="slotThickness">槽厚度</param>
        /// <param name="slotWidth">槽宽度</param>
        /// <param name="slotHeight">槽高度</param>
        /// <returns></returns>
        Face CreateSlots(SketchCircle capCircle, WorkAxis Axis, double SlotOffset, double slotThickness, double slotWidth, double slotHeight, double inSlotRadius)
        {
            RevolveFeature Slot = CreateCircleSlot(capCircle, Axis, SlotOffset, slotThickness, slotWidth, slotHeight);
            List<Face> slotSF = InventorTool.GetCollectionFromIEnumerator<Face>(Slot.SideFaces.GetEnumerator());

            if (inSlotRadius <= 0)
            {
                CreateCrossSlots(capCircle, slotThickness, slotWidth, slotHeight, slotSF);
            }
            else
            {
                RevolveFeature InSlot = CreateCircleSlot(capCircle, Axis, capCircle.Radius - inSlotRadius - slotWidth, slotThickness, slotWidth, slotHeight);
                List<Face> InSlotSF = InventorTool.GetCollectionFromIEnumerator<Face>(InSlot.SideFaces.GetEnumerator());
                //for (int i = 0; i < InSlotSF.Count; i++)
                //{
                //    Definition.iMateDefinitions.AddMateiMateDefinition(InSlotSF[i], 0).Name = "a" + i;
                //}
                //InSlotSF[6] 外面
                WorkPlane p1 = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[2], inSlotRadius + slotWidth + 1, true);
                WorkPlane p2 = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[2], -inSlotRadius - slotWidth - 1, true);
                WorkPlane p3 = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[3], inSlotRadius + slotWidth + 1, true);
                WorkPlane p4 = Definition.WorkPlanes.AddByPlaneAndOffset(Definition.WorkPlanes[3], -inSlotRadius - slotWidth - 1, true);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, InSlotSF[6], p1, false);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], p1, true);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, InSlotSF[6], p2, true);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], p2, false);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, InSlotSF[6], p3, false);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], p3, true);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, InSlotSF[6], p4, true);
                CreateSubSlot(capCircle, slotThickness, slotWidth, slotHeight, slotSF[6], p4, false);
            }
            return slotSF[6];

        }
        /// <summary>
        /// 创建十字架骨架
        /// </summary>
        /// <param name="capCircle"></param>
        /// <param name="slotThickness"></param>
        /// <param name="slotWidth"></param>
        /// <param name="slotHeight"></param>
        /// <param name="slotSF"></param>
        private void CreateCrossSlots(SketchCircle capCircle, double slotThickness, double slotWidth, double slotHeight, List<Face> slotSF)
        {
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
        }

        /// <summary>
        /// 创建环型骨架
        /// </summary>
        /// <param name="capCircle"></param>
        /// <param name="Axis"></param>
        /// <param name="SlotOffset"></param>
        /// <param name="slotThickness"></param>
        /// <param name="slotWidth"></param>
        /// <param name="slotHeight"></param>
        /// <returns></returns>
        private RevolveFeature CreateCircleSlot(SketchCircle capCircle, WorkAxis Axis, double SlotOffset, double slotThickness, double slotWidth, double slotHeight)
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
            //创建环型骨架
            RevolveFeature Slot = Definition.Features.RevolveFeatures.AddFull(pro, Axis, PartFeatureOperationEnum.kNewBodyOperation);
            return Slot;
        }
        private ExtrudeFeature CreateSubSlot(SketchCircle capCircle, double slotThickness, double slotWidth, double slotHeight, object plane, object fromFace, object toFace)
        {
            PlanarSketch Xsketch = Definition.Sketches.Add(plane);
            List<SketchLine> Xlines = CreateSlot(Xsketch, slotWidth, slotHeight, slotThickness, new XY(0, 0));
            SketchLine Xline = (SketchLine)Xsketch.AddByProjectingEntity(capCircle);
            Xsketch.GeometricConstraints.AddCollinear((SketchEntity)Xline, (SketchEntity)Xlines[0]);
            InventorTool.AddTwoPointDistance(Xsketch, Xline.EndSketchPoint, Xlines[0].EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim).Parameter.Value = Xline.Length / 2 - slotWidth / 2;
            Profile Xpro = Xsketch.Profiles.AddForSolid();
            ExtrudeDefinition Xdef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(Xpro, PartFeatureOperationEnum.kJoinOperation);
            Xdef.SetFromToExtent(fromFace, false, toFace, false);
            return Definition.Features.ExtrudeFeatures.Add(Xdef);
        }
        /// <summary>
        /// 创建单个骨架
        /// </summary>
        /// <param name="capCircle"></param>
        /// <param name="slotThickness"></param>
        /// <param name="slotWidth"></param>
        /// <param name="slotHeight"></param>
        /// <param name="slotSF"></param>
        /// <param name="plane"></param>
        /// <param name="direct"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 创建单个骨架草图
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="thickness"></param>
        /// <param name="point"></param>
        /// <returns></returns>
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
        #region 创建汇总管
        /// <summary>
        /// 创建管道
        /// </summary>
        /// <param name="Axis">盖子中心轴</param>
        /// <param name="plane">管道开始面的基面</param>
        /// <param name="circle">盖子圆草图</param>
        /// <param name="Angle">管道角度大小</param>
        /// <param name="radius">管道内半径</param>
        /// <param name="thickness">管道壁厚</param>
        /// <param name="pipeXOffset">管中心与门面距离</param>
        /// <param name="pipeYOffset">管中心与门边距离</param>
        /// <param name="pipePlane">管道开始面</param>
        /// <returns>管道特征</returns>
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
        /// <summary>
        /// 创建管道支架
        /// </summary>
        /// <param name="pipe">管道特征</param>
        /// <param name="Axis">盖中心轴</param>
        /// <param name="pipePlane">管道开始面</param>
        /// <param name="circle">管道外圆草图</param>
        /// <param name="Angle">管道角度</param>
        /// <param name="radius">管道半径</param>
        /// <param name="thickness">管厚度</param>
        /// <param name="pipeXOffset">管中心与门边距离</param>
        /// <param name="pipeYOffset">管中心与门面距离</param>
        /// <param name="pipeSupLength">管道支架水平长度</param>
        /// <param name="pipeSurHRaidus">管道支架弯转圆半径</param>
        /// <param name="PipeSurRadius">管道支架半径</param>
        /// <param name="pipeSurThickness">管道支架壁厚</param>
        /// <param name="CapEndFace">盖子开始面</param>
        /// <returns></returns>
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
        #region 创建连接板和上吊板
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
        #region 创建进出液管
        private void  CreateLiquidPipe(Face EF,SketchCircle CapCir,bool direct)
        {
            WorkPlane plane = Definition.WorkPlanes.AddByPlaneAndOffset(EF, UsMM(par.PipeXOffset),true);
            SketchPoint EndPoint1, EndPoint2;
          SweepFeature sweep1=  CreateVerticalLiqPipe(CapCir, true, plane,out EndPoint1);
            SweepFeature sweep2 = CreateVerticalLiqPipe(CapCir, false, plane,out EndPoint2);
            Face EF1 = InventorTool.GetFirstFromIEnumerator<Face>(sweep1.EndFaces.GetEnumerator());
            Face EF2 = InventorTool.GetFirstFromIEnumerator<Face>(sweep2.EndFaces.GetEnumerator());
            WorkPlane plane2 = Definition.WorkPlanes.AddByPlaneAndOffset(plane, UsMM(par.LiqPipeHeight + par.LiqPipeTurnDiameter / 2),true);
            CreateLiqPipe1(EF1, plane2,CapCir, EndPoint1,par.LiqPipeDirection);
            CreateLiqPipe2(EF2, plane2, CapCir, EndPoint2, par.LiqPipeDirection);
        }

        private SweepFeature CreateVerticalLiqPipe(SketchCircle CapCir, bool direct, WorkPlane plane,out SketchPoint endPoint)
        {
            PlanarSketch osketch = Definition.Sketches.AddWithOrientation(plane, Definition.WorkAxes[3], true, true, CapCir.CenterSketchPoint);
            SketchCircle cir1;
            if(direct)
            {
                cir1 = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(0, UsMM(par.InDiameter.Value / 2 + par.Thickness.Value - par.PipeYOffset)), UsMM(par.LiqPipeInDiameter / 2));
            }
            else
            {
                cir1 = osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(0, -UsMM(par.InDiameter.Value / 2 + par.Thickness.Value - par.PipeYOffset)), UsMM(par.LiqPipeInDiameter / 2));
            }
            osketch.SketchCircles.AddByCenterRadius(cir1.CenterSketchPoint, UsMM(par.LiqPipeInDiameter / 2 + par.LiqPipeThickness));
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            PlanarSketch tsketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchPoint StartPoint = (SketchPoint)tsketch.AddByProjectingEntity(cir1.CenterSketchPoint);
            Point2d p = InventorTool.CreatePoint2d(StartPoint.Geometry.X - UsMM(par.LiqPipeHeight), StartPoint.Geometry.Y);
            SketchLine line = tsketch.SketchLines.AddByTwoPoints(StartPoint, p);
            Point2d center;
            if (direct)
            {
                center = InventorTool.CreatePoint2d(line.EndSketchPoint.Geometry.X, line.EndSketchPoint.Geometry.Y - UsMM(par.LiqPipeTurnDiameter / 2));

            }
            else
            {
                center = InventorTool.CreatePoint2d(line.EndSketchPoint.Geometry.X, line.EndSketchPoint.Geometry.Y + UsMM(par.LiqPipeTurnDiameter / 2));
            }
            Point2d EndPoint = InventorTool.CreatePoint2d(line.EndSketchPoint.Geometry.X - UsMM(par.LiqPipeTurnDiameter / 2), center.Y);


          SketchArc arc=  tsketch.SketchArcs.AddByCenterStartEndPoint(center, line.EndSketchPoint, EndPoint,direct);
            endPoint =direct==true? arc.EndSketchPoint:arc.StartSketchPoint;
            Path pPath = Definition.Features.CreatePath(line);
          return   Definition.Features.SweepFeatures.AddUsingPath(pro, pPath, PartFeatureOperationEnum.kNewBodyOperation);
        }
        private SweepFeature CreateLiqPipe1(object plane1,object plane2,SketchCircle CapCir,SketchPoint endpoint,bool direct)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane1, true);
            Profile pro = osketch.Profiles.AddForSolid();
        
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            PlanarSketch tsketch = Definition.Sketches.AddWithOrientation(plane2,Definition.WorkAxes[3],true,true,CapCir.CenterSketchPoint);
            SketchPoint start = (SketchPoint)tsketch.AddByProjectingEntity(endpoint);
            SketchLine line1= tsketch.SketchLines.AddByTwoPoints(start, InventorTool.CreatePoint2d(start.Geometry.X, start.Geometry.Y - UsMM(par.LiqPipeLength1[0])));
            if(direct)
            {
             CreateSketchArc(tsketch, line1.EndSketchPoint.Geometry, UsMM(par.LiqPipeTurnDiameter / 2),UsMM(par.LiqPipeLength1[1]), 0);
            }
            else
            {
                CreateSketchArc(tsketch, line1.EndSketchPoint.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength1[1]), 1);
            }
            Path pPath = Definition.Features.CreatePath(line1);
            return Definition.Features.SweepFeatures.AddUsingPath(pro, pPath, PartFeatureOperationEnum.kJoinOperation);

        }
        private SweepFeature CreateLiqPipe2(object plane1, object plane2, SketchCircle CapCir, SketchPoint endpoint, bool direct)
        {
            PlanarSketch osketch = Definition.Sketches.Add(plane1, true);
            Profile pro = osketch.Profiles.AddForSolid();

            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1)
                {
                    item.AddsMaterial = true;
                }
                else
                {
                    item.AddsMaterial = false;
                }
            }
            PlanarSketch tsketch = Definition.Sketches.AddWithOrientation(plane2, Definition.WorkAxes[3], true, true, CapCir.CenterSketchPoint);
            SketchPoint start = (SketchPoint)tsketch.AddByProjectingEntity(endpoint);
            SketchLine line1 = tsketch.SketchLines.AddByTwoPoints(start, InventorTool.CreatePoint2d(start.Geometry.X, start.Geometry.Y + UsMM(par.LiqPipeLength2[0])));
            SketchPoint p;
            if (direct)
            {
             p=   CreateSketchArc(tsketch, line1.EndSketchPoint.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength2[1]), 2);
             p=   CreateSketchArc(tsketch, p.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength2[2]), 4);
                  CreateSketchArc(tsketch,p.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength2[3]), 2);
            }
            else
            {
                p = CreateSketchArc(tsketch, line1.EndSketchPoint.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength2[1]), 3);
                p = CreateSketchArc(tsketch, p.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength2[2]), 5);
                CreateSketchArc(tsketch, p.Geometry, UsMM(par.LiqPipeTurnDiameter / 2), UsMM(par.LiqPipeLength2[3]), 3);
            }
            Path pPath = Definition.Features.CreatePath(line1);
            return Definition.Features.SweepFeatures.AddUsingPath(pro, pPath, PartFeatureOperationEnum.kJoinOperation);

        }
        private SketchPoint CreateSketchArc(PlanarSketch osketch,Point2d startP,double radius,double LineLength,int ract)
        {
            Point2d centerP, endP;
            SketchArc arc = null;
            Point2d lineEnd = null;
            SketchLine line=null;
            switch (ract)
            {
                case 0://右下逆时针 垂直相切
                    centerP = InventorTool.CreatePoint2d(startP.X+radius, startP.Y);
                    endP= InventorTool.CreatePoint2d(centerP.X, centerP.Y-radius);
                    arc = osketch.SketchArcs.AddByCenterStartEndPoint(centerP, startP, endP, true);
                     lineEnd = InventorTool.CreatePoint2d(arc.EndSketchPoint.Geometry.X+LineLength, arc.EndSketchPoint.Geometry.Y);
                    line = osketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, lineEnd);
                    break;
                case 1://左下顺时针 垂直相切
                    centerP = InventorTool.CreatePoint2d(startP.X - radius, startP.Y);
                    endP = InventorTool.CreatePoint2d(centerP.X, centerP.Y - radius);
                    arc = osketch.SketchArcs.AddByCenterStartEndPoint(centerP, startP, endP,false);
                    lineEnd = InventorTool.CreatePoint2d(arc.StartSketchPoint.Geometry.X - LineLength, arc.StartSketchPoint.Geometry.Y);
                    line = osketch.SketchLines.AddByTwoPoints(arc.StartSketchPoint, lineEnd);
                    break;
                case 2://右上顺时针 垂直相切
                    centerP = InventorTool.CreatePoint2d(startP.X + radius, startP.Y);
                    endP = InventorTool.CreatePoint2d(centerP.X, centerP.Y + radius);
                    arc = osketch.SketchArcs.AddByCenterStartEndPoint(centerP, startP, endP, false);
                    lineEnd = InventorTool.CreatePoint2d(arc.StartSketchPoint.Geometry.X + LineLength, arc.StartSketchPoint.Geometry.Y);
                    line = osketch.SketchLines.AddByTwoPoints(arc.StartSketchPoint, lineEnd);
                    break;
                case 3://左上逆时针 垂直相切
                    centerP = InventorTool.CreatePoint2d(startP.X - radius, startP.Y);
                    endP = InventorTool.CreatePoint2d(centerP.X, centerP.Y + radius);
                    arc = osketch.SketchArcs.AddByCenterStartEndPoint(centerP, startP, endP, true);
                    lineEnd = InventorTool.CreatePoint2d(arc.EndSketchPoint.Geometry.X - LineLength, arc.EndSketchPoint.Geometry.Y);
                    line = osketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, lineEnd);
                    break;
                case 4://右上逆时针 水平相切
                    centerP = InventorTool.CreatePoint2d(startP.X , startP.Y + radius);
                    endP = InventorTool.CreatePoint2d(centerP.X + radius, centerP.Y );
                    arc = osketch.SketchArcs.AddByCenterStartEndPoint(centerP, startP, endP, true);
                    lineEnd = InventorTool.CreatePoint2d(arc.EndSketchPoint.Geometry.X, arc.EndSketchPoint.Geometry.Y + LineLength);
                    line = osketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, lineEnd);
                    break;
                case 5://左上顺时针 水平相切
                    centerP = InventorTool.CreatePoint2d(startP.X , startP.Y + radius);
                    endP = InventorTool.CreatePoint2d(centerP.X - radius, centerP.Y );
                    arc = osketch.SketchArcs.AddByCenterStartEndPoint(centerP, startP, endP, false);
                    lineEnd = InventorTool.CreatePoint2d(arc.StartSketchPoint.Geometry.X , arc.StartSketchPoint.Geometry.Y + LineLength);
                    line = osketch.SketchLines.AddByTwoPoints(arc.StartSketchPoint, lineEnd);
                    break;
                default:
                    break;
               
            }
         
            return line.EndSketchPoint;
        }
        #endregion

    }
}
