using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParamedModule;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Other;

namespace ParamedModule.Other
{
    [Export("Valve", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Valve : PartModulebase
    {
        ParValve par = new ParValve();
        [ImportingConstructor]
        public Valve():base()
        {
            this.Name = "阀门";
            par.A = 110;
            par.B = 130;
            par.C = 52;
            par.D = 173;
            par.G = 34;
            par.H = 153;
            par.I = 56;
            par.F = 64;
            par.Flanch.DN = 50;
            par.Flanch.D1 = 165;
            par.Flanch.D6 = 60.3;
            par.Flanch.D0 = 125;
            par.Flanch.D = 18;
            par.Flanch.D2 = 80;
            par.Flanch.H = 20;
            par.Flanch.N = 10;
            par.Flanch.C = 10;
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            ExtrudeFeature ract = CreateRact();
            List<Face> RSFs = InventorTool.GetCollectionFromIEnumerator<Face>(ract.SideFaces.GetEnumerator());
            ExtrudeFeature top = CreateTop(RSFs);
            SketchCircle topCir,CyCir;
            ExtrudeFeature top1 = CreateTop1(top, out topCir);
            Face topFace = InventorTool.GetFirstFromIEnumerator<Face>(top1.EndFaces.GetEnumerator());
            CreateTop2(topCir, topFace);
            Face REndFace = InventorTool.GetFirstFromIEnumerator<Face>(ract.StartFaces.GetEnumerator());
            Face RStartFace = InventorTool.GetFirstFromIEnumerator<Face>(ract.EndFaces.GetEnumerator());
            ExtrudeFeature cy= CreateCy(REndFace,out CyCir);
            Face CYEF = InventorTool.GetFirstFromIEnumerator<Face>(cy.EndFaces.GetEnumerator());
            ExtrudeFeature flanch= InventorTool.CreateFlance(CYEF, CyCir, par.Flanch.D1/2, par.Flanch.H, Definition);
            Face FEFace = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
           ExtrudeFeature Groove= InventorTool.CreateFlanceGroove(FEFace, CyCir, par.Flanch.D2 / 2, Definition);
           ExtrudeFeature Screw= InventorTool.CreateFlanceScrew(FEFace, CyCir, par.Flanch.N, par.Flanch.C / 2, par.Flanch.D0 / 2, par.Flanch.H, Definition);
            WorkPlane plane = Definition.WorkPlanes.AddByTwoPlanes(REndFace, RStartFace,true);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(cy);
            objc.Add(flanch);
            objc.Add(Groove);
            objc.Add(Screw);
            Definition.Features.MirrorFeatures.Add(objc, plane);
            //for (int i = 0; i < RSFs.Count; i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(RSFs[i], 0).Name = "h" + i;
            //}

        }

        private ExtrudeFeature CreateCy(Face REndFace,out SketchCircle cir)
        {
            PlanarSketch osketch = Definition.Sketches.Add(REndFace);
           cir= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, par.Flanch.D6 / 2);
            osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, par.Flanch.D6 / 2 + 5);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count > 1) item.AddsMaterial = true;
                else item.AddsMaterial = false;
            }
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           return Definition.Features.ExtrudeFeatures.Add(def);
        }

        private void CreateTop2(SketchCircle cir, Face topFace)
        {
            PlanarSketch osketch = Definition.Sketches.Add(topFace);
            SketchCircle circle = (SketchCircle)osketch.AddByProjectingEntity(cir);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(circle);
            osketch.OffsetSketchEntitiesUsingDistance(objc, 20, true);
            circle.Construction = true;
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(20, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);
        }

        private ExtrudeFeature CreateTop1(ExtrudeFeature top,out SketchCircle cir)
        {
            Face topFace = InventorTool.GetFirstFromIEnumerator<Face>(top.EndFaces.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(topFace);
            cir= osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(par.G / 2, -par.A / 2), par.I / 2);
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(par.H / 2, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           return  Definition.Features.ExtrudeFeatures.Add(def);
        }

        private ExtrudeFeature CreateTop(List<Face> RSFs)
        {
            PlanarSketch osketch = Definition.Sketches.Add(RSFs[1]);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(-par.F / 2 + par.G / 2, -par.B / 2 - par.A / 2), InventorTool.CreatePoint2d(par.F / 2 + par.G / 2, par.B / 2 - par.A / 2));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(par.G / 2, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
           
        }

        private ExtrudeFeature CreateRact()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(-par.A / 2, -par.C), InventorTool.CreatePoint2d(par.A / 2, par.D));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(par.G, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
          return  Definition.Features.ExtrudeFeatures.Add(def);
        }

    }
}
