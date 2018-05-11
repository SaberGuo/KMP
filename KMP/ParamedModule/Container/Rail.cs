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
   
    public  class Rail: PartModulebase
    {
       internal ParRail par = new ParRail();
        [ImportingConstructor]
        public Rail():base()
        {
            init();
            this.Parameter = par;
        }
        void init()
        {
            par.UpBridgeWidth = 80;
            par.UpBridgeHeight = 30;
            par.BraceWidth = 30;
            par.BraceHeight = 100;
            par.DownBridgeHeight = 20;
            par.DownBridgeWidth = 120;
            par.RailLength = 5000;
        }
        public override void CreateModule()
        {
   
            CreateDoc();
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            CreateRib(osketch);
            Profile profile = osketch.Profiles.AddForSolid();
            ExtrudeDefinition ex= Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            ex.SetDistanceExtent(par.RailLength + "mm", PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
          ExtrudeFeature box=  Definition.Features.ExtrudeFeatures.Add(ex);
            box.Name = "Rail";
            List<Face> sideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            ///顶层是侧面11，底层面是侧面5，依次顺序排列
           // Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0).Name = "mateR1"; //导轨截面

           // Definition.iMateDefinitions.AddMateiMateDefinition(sideFaces[4], 0).Name = "mateR2"; //导轨底梁侧面
           // Definition.iMateDefinitions.AddMateiMateDefinition(sideFaces[5], 0).Name = "mateR1"; //导轨底面
            SaveDoc();


        }

        /// <summary>
        /// 创建单个加强筋
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="L"></param>
        private void CreateRib(PlanarSketch osketch)
        {
            List<SketchLine> Lines = new List<SketchLine>();
            #region
            Point2d p1 = InventorTool.TranGeo.CreatePoint2d(3, 3);
            Point2d p2 = InventorTool.TranGeo.CreatePoint2d(3, 2);
            Point2d p3 = InventorTool.TranGeo.CreatePoint2d(2, 2);
            Point2d p4 = InventorTool.TranGeo.CreatePoint2d(2, 1);
            Point2d p5 = InventorTool.TranGeo.CreatePoint2d(3, 1);
            Point2d p6 = InventorTool.TranGeo.CreatePoint2d(3, 0);
            Point2d p7 = InventorTool.TranGeo.CreatePoint2d(0, 0);
            Point2d p8 = InventorTool.TranGeo.CreatePoint2d(0, 1);
            Point2d p9 = InventorTool.TranGeo.CreatePoint2d(1, 1);
            Point2d p10 = InventorTool.TranGeo.CreatePoint2d(1, 2);
            Point2d p11 = InventorTool.TranGeo.CreatePoint2d(0, 2);
            Point2d p12 = InventorTool.TranGeo.CreatePoint2d(0, 3);

            SketchLine L = osketch.SketchLines.AddByTwoPoints(p12, p1);
            SketchLine L1 = osketch.SketchLines.AddByTwoPoints(p1, p2);
            SketchLine L2 = osketch.SketchLines.AddByTwoPoints(p2, p3);
            SketchLine L3 = osketch.SketchLines.AddByTwoPoints(p3, p4);
            SketchLine L4 = osketch.SketchLines.AddByTwoPoints(p4, p5);
            SketchLine L5 = osketch.SketchLines.AddByTwoPoints(p5, p6);
            SketchLine L6 = osketch.SketchLines.AddByTwoPoints(p6, p7); //总宽
            SketchLine L7 = osketch.SketchLines.AddByTwoPoints(p7, p8);
            SketchLine L8 = osketch.SketchLines.AddByTwoPoints(p8, p9);
            SketchLine L9 = osketch.SketchLines.AddByTwoPoints(p9, p10);
            SketchLine L10 = osketch.SketchLines.AddByTwoPoints(p10, p11);
            SketchLine L11 = osketch.SketchLines.AddByTwoPoints(p11, p12);
            Lines.Add(L);
            Lines.Add(L1);
            Lines.Add(L2);
            Lines.Add(L3);
            Lines.Add(L4);
            Lines.Add(L5);
            Lines.Add(L6);
            Lines.Add(L7);
            Lines.Add(L8);
            Lines.Add(L9);
            Lines.Add(L10);
            Lines.Add(L11);
            #endregion
            InventorTool.CreateTwoPointCoinCident(osketch, L11, L);
            for (int i = 1; i < Lines.Count; i++)
            {
                InventorTool.CreateTwoPointCoinCident(osketch, Lines[i], Lines[i - 1]);
                osketch.GeometricConstraints.AddPerpendicular((SketchEntity)Lines[i], (SketchEntity)Lines[i - 1]);
            }

            osketch.GeometricConstraints.AddEqualLength(L1, L11);
            osketch.GeometricConstraints.AddEqualLength(L2, L10);
            osketch.GeometricConstraints.AddEqualLength(L3, L9);
            osketch.GeometricConstraints.AddEqualLength(L4, L8);
            //osketch.GeometricConstraints.AddEqualLength(L5, L7);
            

           InventorTool.AddTwoPointDistance(osketch, L.StartSketchPoint, L.EndSketchPoint,0,DimensionOrientationEnum.kAlignedDim)
                .Parameter.Value=UsMM(par.UpBridgeWidth);
            InventorTool.AddTwoPointDistance(osketch, L1.StartSketchPoint, L1.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim)
                .Parameter.Value = UsMM(par.UpBridgeHeight);
            InventorTool.AddTwoPointDistance(osketch, L2.EndSketchPoint, L10.StartSketchPoint, 0, DimensionOrientationEnum.kAlignedDim)
                .Parameter.Value = UsMM(par.BraceWidth);
            InventorTool.AddTwoPointDistance(osketch, L3.StartSketchPoint, L3.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim)
                .Parameter.Value = UsMM(par.BraceHeight);
            InventorTool.AddTwoPointDistance(osketch, L5.StartSketchPoint, L5.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim)
                .Parameter.Value = UsMM(par.DownBridgeHeight);
            InventorTool.AddTwoPointDistance(osketch, L6.StartSketchPoint, L6.EndSketchPoint, 0, DimensionOrientationEnum.kAlignedDim)
                .Parameter.Value = UsMM(par.DownBridgeWidth);
        }

        public override bool CheckParamete()
        {
           
            if (par.BraceWidth >= par.DownBridgeWidth || par.BraceWidth >= par.UpBridgeWidth) return false;
            return CommonTool.CheckParameterValue(par);
        }
    }
}
