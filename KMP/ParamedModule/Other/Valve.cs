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
       public ParValve par = new ParValve();
        [ImportingConstructor]
        public Valve():base()
        {
            this.Name = "阀门";
            this.Parameter = this.par;
            par.ValveType.A = 110;
            par.ValveType.B = 130;
            par.ValveType.C = 52;
            par.ValveType.D = 173;
            par.ValveType.G = 34;
            par.ValveType.H = 153;
            par.ValveType.I = 56;
            par.ValveType.F = 64;
            par.ValveType.Flanch.DN = 50;
            par.ValveType.Flanch.D1 = 165;
            par.ValveType.Flanch.D6 = 60.3;
            par.ValveType.Flanch.D0 = 125;
            par.ValveType.Flanch.D = 18;
            par.ValveType.Flanch.D2 = 80;
            par.ValveType.Flanch.H = 20;
            par.ValveType.Flanch.N = 10;
            par.ValveType.Flanch.C = 10;
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            //阀体
            ExtrudeFeature ract = CreateRact();
            List<Face> RSFs = InventorTool.GetCollectionFromIEnumerator<Face>(ract.SideFaces.GetEnumerator());
            ExtrudeFeature top = CreateTop(RSFs);
           ShellDefinition shellDef= Definition.Features.ShellFeatures.CreateShellDefinition(InventorTool.Inventor.TransientObjects.CreateFaceCollection(), 1);
            Definition.Features.ShellFeatures.Add(shellDef);
            SketchCircle topCir,CyCir;
            ExtrudeFeature top1 = CreateTop1(top, out topCir);
            Face topFace = InventorTool.GetFirstFromIEnumerator<Face>(top1.EndFaces.GetEnumerator());
            CreateTop2(topCir, topFace);
            Face REndFace = InventorTool.GetFirstFromIEnumerator<Face>(ract.StartFaces.GetEnumerator());
            Face RStartFace = InventorTool.GetFirstFromIEnumerator<Face>(ract.EndFaces.GetEnumerator());
            ExtrudeFeature cy = CreateHole(REndFace, out CyCir);
            // Face CYEF = InventorTool.GetFirstFromIEnumerator<Face>(cy.EndFaces.GetEnumerator());
            ExtrudeFeature flanch = InventorTool.CreateFlance(REndFace,CyCir, UsMM(par.ValveType.Flanch.D1 / 2), UsMM(par.ValveType.Flanch.H), Definition);
            // flanch.Name = "Flanch1";
            // Face SF = InventorTool.GetFirstFromIEnumerator<Face>(flanch.SideFaces.GetEnumerator());
            // WorkAxis axis1 = Definition.WorkAxes.AddByRevolvedFace(SF);
            // axis1.Name = flanch.Name + "A";
             Face FEFace = InventorTool.GetFirstFromIEnumerator<Face>(flanch.EndFaces.GetEnumerator());
            ExtrudeFeature Groove= InventorTool.CreateFlanceGroove(FEFace, CyCir, UsMM(par.ValveType.Flanch.D2 / 2), Definition);
            ExtrudeFeature Screw= InventorTool.CreateFlanceScrew(FEFace, CyCir, par.ValveType.Flanch.N, UsMM(par.ValveType.Flanch.C / 2), UsMM(par.ValveType.Flanch.D0 / 2), UsMM(par.ValveType.Flanch.H), Definition);
             WorkPlane plane = Definition.WorkPlanes.AddByTwoPlanes(REndFace, RStartFace,true);
           ExtrudeFeature  sur= CreateSur(REndFace);

            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(flanch);
            objc.Add(Groove);
            objc.Add(Screw);
            objc.Add(sur);
            Definition.Features.MirrorFeatures.Add(objc, plane,false,PatternComputeTypeEnum.kAdjustToModelCompute);
            //for (int i = 0; i < RSFs.Count; i++)
            //{
            //    Definition.iMateDefinitions.AddMateiMateDefinition(RSFs[i], 0).Name = "h" + i;
            //}

        }
        private ExtrudeFeature CreateSur(Face REndFace)
        {
            PlanarSketch osketch = Definition.Sketches.Add(REndFace);
            double h = (par.ValveType.D-20 - par.ValveType.Flanch.D1 / 2)/5;
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(par.ValveType.Flanch.D1 / 2 + h),UsMM(-par.ValveType.A / 2)),
                InventorTool.CreatePoint2d(UsMM(par.ValveType.Flanch.D1 / 2 + h * 2), UsMM(par.ValveType.A / 2)));
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(par.ValveType.Flanch.D1 / 2 + h * 3), UsMM(-par.ValveType.A / 2)),
               InventorTool.CreatePoint2d(UsMM(par.ValveType.Flanch.D1 / 2 + h * 4),UsMM(par.ValveType.A / 2)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kJoinOperation);
            def.SetDistanceExtent(UsMM(par.ValveType.F - par.ValveType.G) / 2, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }
        private ExtrudeFeature CreateHole( Face REndFace,out SketchCircle cir)
        {
            PlanarSketch osketch = Definition.Sketches.Add(REndFace);
           cir= osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(par.ValveType.Flanch.D6 / 2));
           // osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, UsMM(par.ValveType.Flanch.D6 / 2 + 5));
            Profile pro = osketch.Profiles.AddForSolid();
            //foreach (ProfilePath item in pro)
            //{
            //    if (item.Count > 1) item.AddsMaterial = true;
            //    else item.AddsMaterial = false;
            //}
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kCutOperation);
            // def.SetDistanceExtent(UsMM(1), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            def.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
        }

        private void CreateTop2(SketchCircle cir, Face topFace)
        {
            PlanarSketch osketch = Definition.Sketches.Add(topFace);
            double radius = cir.Radius;
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(-radius, -radius), InventorTool.CreatePoint2d(radius, radius));
            //SketchCircle circle = (SketchCircle)osketch.AddByProjectingEntity(cir);
            //ObjectCollection objc = InventorTool.CreateObjectCollection();
            //objc.Add(circle);
            //osketch.OffsetSketchEntitiesUsingDistance(objc, 20, true);
            //circle.Construction = true;
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(UsMM(10), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(def);
        }
        /// <summary>
        /// 生成板上圆柱体
        /// </summary>
        /// <param name="top"></param>
        /// <param name="cir"></param>
        /// <returns></returns>
        private ExtrudeFeature CreateTop1(ExtrudeFeature top,out SketchCircle cir)
        {
            Face topFace = InventorTool.GetFirstFromIEnumerator<Face>(top.EndFaces.GetEnumerator());
            PlanarSketch osketch = Definition.Sketches.Add(topFace);
            cir= osketch.SketchCircles.AddByCenterRadius(InventorTool.CreatePoint2d(UsMM(par.ValveType.G / 2), UsMM(-par.ValveType.A / 2)), UsMM(par.ValveType.I / 2));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(UsMM(par.ValveType.H ), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
           return  Definition.Features.ExtrudeFeatures.Add(def);
        }
        /// <summary>
        /// 生成阀体上面横板
        /// </summary>
        /// <param name="RSFs"></param>
        /// <returns></returns>
        private ExtrudeFeature CreateTop(List<Face> RSFs)
        {
            PlanarSketch osketch = Definition.Sketches.Add(RSFs[1]);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(-par.ValveType.F / 2 + par.ValveType.G / 2),UsMM( -par.ValveType.B / 2 - par.ValveType.A / 2)), InventorTool.CreatePoint2d(UsMM(par.ValveType.F / 2 + par.ValveType.G / 2),UsMM( par.ValveType.B / 2 - par.ValveType.A / 2)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(UsMM(20), PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            return Definition.Features.ExtrudeFeatures.Add(def);
           
        }
        /// <summary>
        /// 生成阀体
        /// </summary>
        /// <returns></returns>
        private ExtrudeFeature CreateRact()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[1]);
            osketch.SketchLines.AddAsTwoPointRectangle(InventorTool.CreatePoint2d(UsMM(-par.ValveType.A / 2),UsMM( -par.ValveType.C)), InventorTool.CreatePoint2d(UsMM(par.ValveType.A / 2), UsMM(par.ValveType.D-20)));
            Profile pro = osketch.Profiles.AddForSolid();
            ExtrudeDefinition def = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            def.SetDistanceExtent(UsMM(par.ValveType.G), PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
          return  Definition.Features.ExtrudeFeatures.Add(def);
        }

    }
}
