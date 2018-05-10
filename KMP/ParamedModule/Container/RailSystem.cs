using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using Inventor;
using Infranstructure.Tool;
using System.ComponentModel.Composition;
using KMP.Interface;
namespace ParamedModule.Container
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RailSystem : AssembleModuleBase
    {
        ParRailSystem par = new ParRailSystem();
        Rail rail = new Rail();
        RailSupport support;
        public RailSystem():base()
        {
            rail = new Rail();
            support = new RailSupport();
            this.Parameter = par;
            init();
        }
        public override bool CheckParamete()
        {
            return true;
        }
        void init()
        {
            par.SupportNum = 3;
        }
        public override void CreateModule()
        {
            CreateDoc();
            rail.CreateModule();
            support.CreateModule();
            ComponentOccurrence CORail = LoadOccurrence((ComponentDefinition)rail.Doc.ComponentDefinition);
            iMateDefinition railMate = Getimate(CORail, "mateR1");
            List<Face> railSideFaces = GetSideFacesproxy(CORail, "Rail");
            ExtrudeFeature railFeature = GetFeature<ExtrudeFeature>(CORail, "Rail",ObjectTypeEnum.kExtrudeFeatureObject);
            Face railEndFace = InventorTool.GetFirstFromIEnumerator<Face>(railFeature.EndFaces.GetEnumerator());
            if (railSideFaces == null) return;
            double interval = UsMM(rail.par.RailLength) / par.SupportNum;
            double offset = rail.par.DownBridgeWidth - support.topBoard.par.Width;
            for (int i = 0; i < par.SupportNum; i++)
            {
                ComponentOccurrence COSupport = LoadOccurrence((ComponentDefinition)support.Doc.ComponentDefinition);
                iMateDefinition supportMate = Getimate(COSupport, "mateR1");
               
                List<Face> supportSF = GetSideFacesproxy(COSupport, "TopBoard");
                ExtrudeFeature supportFeature = GetFeatureproxy<ExtrudeFeature>(COSupport, "TopBoard", ObjectTypeEnum.kExtrudeFeatureObject);
                Face supportTopFace = InventorTool.GetFirstFromIEnumerator<Face>(supportFeature.EndFaces.GetEnumerator());
                // Definition.iMateResults.AddByiMateAndEntity(railMate, supportSF[0]);
                //SetFlushiMate(CORail, railEndFace, COSupport, supportSF[0], "mateRA" + i, i * interval + interval / 2);
                //SetFlushiMate(CORail, railSideFaces[5], COSupport, supportSF[1], "mateRB" + i, offset);
                //  Definition.Constraints.AddFlushConstraint(railEndFace, supportSF[1], 0);

                //  supportOcc.CreateGeometryProxy(supportSF[0],out sidefaceproxy);
                object railSideproxy;
                CORail.CreateGeometryProxy(railEndFace,out railSideproxy);
                Definition.Constraints.AddMateConstraint(railSideFaces[5], supportTopFace, 0);
                Definition.Constraints.AddFlushConstraint(supportSF[0], railSideproxy, i*interval+interval/2);
                Definition.Constraints.AddFlushConstraint(supportSF[1], railSideFaces[6],0);
                // CORail.CreateGeometryProxy(railEndFace, out sidefaceproxy);
                //supportSF[0].

            }
           


        }
    }
}
