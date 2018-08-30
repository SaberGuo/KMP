using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 压缩机
    /// </summary>
    [Export("Compressor", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Compressor : PartModulebase
    {
        ParCompressor par = new ParCompressor();
        public Compressor():base()
        {
            this.Parameter = par;
            init();
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        void init()
        {
            this.Name = "压缩机";
            par.Height = 2000;
            par.Width = 2000;
            par.ThinckNess = 1500;
            par.WinDepth = 20;
            par.WinHeight = 600;
            par.WinWidth = 400;
            par.DistanceSF = 300;
            par.DistanceTop = 200;
        }
        public override bool CheckParamete()
        {
            if (par.WinHeight + par.DistanceTop+par.WinDepth*2 >= par.Height)
            {
                ParErrorChanged(this, "压缩机窗体高度与窗体到顶边距离和不能大于压缩机总高度");
                return false;
            }
            if(par.WinWidth+par.DistanceSF + par.WinDepth * 2 >= par.Width)
            {
                ParErrorChanged(this, "压缩机窗体宽度与窗体窗体侧边距离和不能大于压缩机总宽度");
                return false;
            }
            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
           ExtrudeFeature box= InventorTool.CreateBox(Definition, osketch, UsMM(par.Height), UsMM(par.Width), UsMM(par.ThinckNess));
            List<Face> boxSF = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
          
            Face EndFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            Face　StartFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            box.Name = "Box";
            WorkPlane plane1 = Definition.WorkPlanes.AddByTwoPlanes(boxSF[0], boxSF[2], true);
            WorkPlane plane2 = Definition.WorkPlanes.AddByTwoPlanes(StartFace, EndFace, false);
            plane2.Visible = false;
            plane2.Name = "Flush";
            WorkAxis Axis = Definition.WorkAxes.AddByTwoPlanes(plane1, plane2);
            Axis.Name = "Axis";
            Axis.Visible = false;
            PlanarSketch Wsketch = Definition.Sketches.Add(EndFace,true);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(Wsketch.SketchEntities.GetEnumerator());
            lines.ForEach(a => a.Construction = true);
            Point2d p= lines[2].EndSketchPoint.Geometry;
            Point2d p1 = InventorTool.CreatePoint2d(p.X+UsMM(par.DistanceSF), p.Y-UsMM(par.DistanceTop));
            Point2d p2 = InventorTool.CreatePoint2d(p1.X+UsMM(par.WinWidth), p1.Y-UsMM(par.WinHeight));
           SketchEntitiesEnumerator entities=  Wsketch.SketchLines.AddAsTwoPointRectangle(p1,p2);
            Profile pro = Wsketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            def.SetDistanceExtent(UsMM(par.WinDepth), PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
          ExtrudeFeature win=  Definition.Features.ExtrudeFeatures.Add(def);
            List<Face> WinSFs = InventorTool.GetCollectionFromIEnumerator<Face>(win.SideFaces.GetEnumerator());
            List<Edge> WinEdges=new List<Edge>();
            WinSFs.ForEach(delegate(Face ee){
                List<Edge> temp = InventorTool.GetCollectionFromIEnumerator<Edge>(ee.Edges.GetEnumerator());
                WinEdges.AddRange(temp);
            });
            EdgeCollection edges = InventorTool.CreateEdgeCollection();
            edges.Add(WinEdges[0]);
            edges.Add(WinEdges[4]);
            edges.Add(WinEdges[8]);
            edges.Add(WinEdges[12]);
            ChamferFeature chamfer = Definition.Features.ChamferFeatures.AddUsingDistance(edges, UsMM(par.WinDepth));
            //FilletDefinition filletdef = Definition.Features.FilletFeatures.CreateFilletDefinition();
            //filletdef.AddConstantRadiusEdgeSet(edges, UsMM(par.WinDepth));
            //Definition.Features.FilletFeatures.Add(filletdef);
        }
    }
}
