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
    //[Export(typeof(IParamedModule))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlaneSystem : AssembleModuleBase
    {
        internal ParPlaneSystem par = new ParPlaneSystem();
       internal PlaneSupport _planeSup;
      internal  RailSupportSidePlate _plane;
        [ImportingConstructor]
        public PlaneSystem(PassedParameter InRadius) :base()
        {
           this.Parameter = par;
           
            this.par.CylinderInRadius = InRadius;
            _planeSup = new PlaneSupport(InRadius);
            _plane = new RailSupportSidePlate();
            _planeSup.Name = "平台支架";
            _plane.Name = "平台";
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

            if ((!_plane.CheckParamete()) || (!_planeSup.CheckParamete())) return false;
            ///平板总高度是平板组件高度之和
            ///平板偏移高度是平板偏移位置的高度/2-平板组件高度
          par.TotalHeight=  _plane.par.Thickness + _planeSup.par.BrachHeight1 + _planeSup.par.BrachHeight2 + _planeSup.par.TopBoardThickness;
            double offHeight = Math.Pow(Math.Pow(par.CylinderInRadius.Value, 2) - Math.Pow(_planeSup.par.Offset, 2), 0.5);
            if (offHeight <= par.TotalHeight) return false;
            par.HeightOffset = offHeight - par.TotalHeight;
           return CommonTool.CheckParameterValue(par);
        }

        public override void CreateModule()
        {
            DisPose();
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
            
                COPlanes.Add(GetOccStruct(COPlane, "RailSidePlate",0));
                COPlanceSupS.Add(GetOccStruct(COPlaneSup, "PlaneSupTop",0));
            }
            for (int i = 1; i < par.PlaneNumber; i++)
            {
                Definition.Constraints.AddFlushConstraint(COPlanes[i].EndFace, COPlanes[i - 1].EndFace, 0);//平板顶面平行
                Definition.Constraints.AddMateConstraint(COPlanes[i].SideFaces[0], COPlanes[i - 1].SideFaces[2],0);//平板截面相接
                Definition.Constraints.AddFlushConstraint(COPlanes[i].SideFaces[1], COPlanes[i - 1].SideFaces[1], 0);//平板侧面平行
            }
            for (int i = 0; i < par.PlaneNumber; i++)
            {
                Definition.Constraints.AddMateConstraint(COPlanes[i].StartFace, COPlanceSupS[i].EndFace, 0); //平板底面与支架
                Definition.Constraints.AddFlushConstraint(COPlanes[i].SideFaces[1], COPlanceSupS[i].SideFaces[3], 0);//平板侧面与支架侧面平行
            }
            for (int i = 0; i < par.PlaneNumber - 1; i++)
            {
                Definition.Constraints.AddFlushConstraint(COPlanes[i].SideFaces[2], COPlanceSupS[i].SideFaces[0], UsMM(_planeSup.par.TopBoardWidth) / 2);
            }
            Definition.Constraints.AddFlushConstraint(COPlanes[par.PlaneNumber - 1].SideFaces[2], COPlanceSupS[par.PlaneNumber - 1].SideFaces[0],0);
            //  Definition.Constraints.AddMateConstraint(COPlanes[par.PlaneNumber - 1].SideFaces[2], COPlanceSupS[par.PlaneNumber - 1].SideFaces[1], 0);
            ComponentOccurrence COSup = LoadOccurrence((ComponentDefinition)_planeSup.Doc.ComponentDefinition);

           OccStruct OccSub=  GetOccStruct(COSup, "PlaneSupTop",0);
            Definition.Constraints.AddMateConstraint(COPlanes[0].StartFace, OccSub.EndFace, 0);
            Definition.Constraints.AddFlushConstraint(COPlanes[0].SideFaces[1], OccSub.SideFaces[3], 0);
            Definition.Constraints.AddFlushConstraint(COPlanes[0].SideFaces[2], OccSub.SideFaces[0], 0);
            SaveDoc();
        }
  
    }
  
}
