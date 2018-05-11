using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using KMP.Interface.Model.Heater;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace ParamedModule.Heater
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class HeaterCylinder: PartModulebase
    {
        internal ParHeaterCylinder par = new ParHeaterCylinder();
        public HeaterCylinder() : base()
        {
            this.Parameter = par;
            init();
        }
        private void init()
        {
           
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }
        public override void CreateModule()
        {
            CreateDoc();
            RevolveFeature cyling = CreateCyling();
            cyling.Name = "Cylinder";
            List<Face> sideFace = InventorTool.GetCollectionFromIEnumerator<Face>(cyling.SideFaces.GetEnumerator());
            //WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(sideFace[0]);
            //Axis.Visible = false;
            //Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateH";
            //Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateM";
            //Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[4], 0).Name = "mateK";
            //Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[0], 0).Name = "mateI";

        }

        private RevolveFeature CreateCyling()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);

            SketchEntitiesEnumerator entities= InventorTool.CreateRangle(osketch, par.Length, par.Thickness);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());

            
            SketchLine line = osketch.SketchLines.AddByTwoPoints(InventorTool.Origin, InventorTool.TranGeo.CreatePoint2d(1, 0));
            osketch.GeometricConstraints.AddParallel((SketchEntity)lines[2], (SketchEntity)line);
            //osketch.DimensionConstraints.
            //InventorTool.AddTwoPointDistance(osketch,lines[2].StartSketchPoint, line.StartSketchPoint,0,DimensionOrientationEnum.kAlignedDim).Parameter.Value = par.SkeletonInnerDiameter/2;
            Profile profile = osketch.Profiles.AddForSolid();
            return Definition.Features.RevolveFeatures.AddFull(profile, line, PartFeatureOperationEnum.kNewBodyOperation);

        }

        
    }
}
