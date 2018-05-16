using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using KMP.Interface.Model.Container;
using KMP.Interface;
using Inventor;
using Infranstructure.Tool;
namespace ParamedModule.Container
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlaneSystem : AssembleModuleBase
    {
        internal ParPlaneSystem par = new ParPlaneSystem();
        PlaneSupport _planeSup;
        RailSupportSidePlate _plane;
        [ImportingConstructor]
        public PlaneSystem():base()
        {
           this.Parameter = par;
            _planeSup = new PlaneSupport();
            _plane = new RailSupportSidePlate();
            init();
        }
        void init()
        {
            par.PlaneNumber = 4;
            _plane.par.Length = 1200;
            _plane.par.Width = 300;
            _plane.par.Thickness = 20;
        }

        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateModule()
        {
            GeneratorProgress(this, "开始创建容器内平板系统");
            CreateDoc();
            List<OccStruct> COPlanes = new List<OccStruct>();
            List<OccStruct> COPlanceSupS = new List<OccStruct>();
            _plane.CreateModule();
            _planeSup.CreateModule();
           
            for (int i = 0; i < par.PlaneNumber; i++)
            {
                ComponentOccurrence COPlane = LoadOccurrence((ComponentDefinition)_plane.Doc.ComponentDefinition);
                ComponentOccurrence COPlaneSup = LoadOccurrence((ComponentDefinition)_planeSup.Doc.ComponentDefinition);
                ExtrudeFeature p = GetFeatureproxy<ExtrudeFeature>(COPlane, "",ObjectTypeEnum.kExtrudeFeatureObject);
            
                COPlanes.Add(GetOccStruct(COPlane, "RailSidePlate"));
                COPlanceSupS.Add(GetOccStruct(COPlaneSup, "PlaneSupTop"));
            }
            for (int i = 1; i < par.PlaneNumber; i++)
            {
                Definition.Constraints.AddFlushConstraint(COPlanes[i].EndFace, COPlanes[i - 1].EndFace, 0);
                Definition.Constraints.AddMateConstraint(COPlanes[i].SideFaces[0], COPlanes[i - 1].SideFaces[2],0);
                Definition.Constraints.AddFlushConstraint(COPlanes[i].SideFaces[1], COPlanes[i - 1].SideFaces[1], 0);
            }
            for (int i = 0; i < par.PlaneNumber; i++)
            {
                Definition.Constraints.AddMateConstraint(COPlanes[i].StartFace, COPlanceSupS[i].EndFace, 0);
                Definition.Constraints.AddFlushConstraint(COPlanes[i].SideFaces[1], COPlanceSupS[i].SideFaces[0], 0);
            }
            for (int i = 0; i < par.PlaneNumber - 1; i++)
            {
                Definition.Constraints.AddFlushConstraint(COPlanes[i].SideFaces[2], COPlanceSupS[i].SideFaces[1], UsMM(_planeSup.par.TopBoardWidth) / 2);
            }
            Definition.Constraints.AddFlushConstraint(COPlanes[par.PlaneNumber - 1].SideFaces[2], COPlanceSupS[par.PlaneNumber - 1].SideFaces[1],0);
            //  Definition.Constraints.AddMateConstraint(COPlanes[par.PlaneNumber - 1].SideFaces[2], COPlanceSupS[par.PlaneNumber - 1].SideFaces[1], 0);
            ComponentOccurrence COSup = LoadOccurrence((ComponentDefinition)_planeSup.Doc.ComponentDefinition);

           OccStruct OccSub=  GetOccStruct(COSup, "PlaneSupTop");
            Definition.Constraints.AddMateConstraint(COPlanes[0].StartFace, OccSub.EndFace, 0);
            Definition.Constraints.AddFlushConstraint(COPlanes[0].SideFaces[1], OccSub.SideFaces[0], 0);
            Definition.Constraints.AddFlushConstraint(COPlanes[0].SideFaces[0], OccSub.SideFaces[3], 0);
            SaveDoc();
            GeneratorProgress(this, "完成创建容器内平板系统");
        }
    OccStruct GetOccStruct(ComponentOccurrence occ,string name)
        {
            OccStruct planeStruct = new OccStruct();
            planeStruct.Occurrence = occ;
            ExtrudeFeature feature = GetFeatureproxy<ExtrudeFeature>(occ, name, ObjectTypeEnum.kExtrudeFeatureObject);
            planeStruct.SideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(feature.SideFaces.GetEnumerator());
            planeStruct.EndFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.EndFaces.GetEnumerator());
            planeStruct.StartFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.StartFaces.GetEnumerator());
            planeStruct.Part = (PartFeature)feature;
            return planeStruct;
        }
    }
    public struct OccStruct
    {
        public ComponentOccurrence Occurrence;
        public List<Face> SideFaces;
        public Face EndFace;
        public Face StartFace;
        public PartFeature Part;
    }
}
