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
 
    public class RailSystem : AssembleModuleBase
    {
      internal  ParRailSystem par = new ParRailSystem();
        internal Rail rail = new Rail();
        internal RailSupport support;
        public RailSystem(PassedParameter InRadius) :base()
        {
            rail = new Rail();
            support = new RailSupport();
            SubParamedModules.Add(rail);
            SubParamedModules.Add(support);
       
            this.Parameter = par;
            init();
            this.rail.Name = "导轨";
            this.support.Name = "导轨支架";
            par.CylinderInRadius = InRadius;
        }
        void init()
        {
            par.SupportNum = 3;
           
            par.Offset = 400;
            par.RailTotalHeight = 555;
            par.HeightOffset = 1;
        }
        public override bool CheckParamete()
        {
            if (!CommonTool.CheckParameterValue(par)) return false;
            if ((!support.CheckParamete())||(!rail.CheckParamete())) return false;
            if (par.Offset >= par.CylinderInRadius.Value) return false;
            double h0 = Math.Pow(Math.Pow(par.CylinderInRadius.Value, 2) - Math.Pow(par.Offset, 2), 0.5);//偏移后圆上点到圆心的垂直高度
            par.HeightOffset = h0 - par.RailTotalHeight; //偏移后导轨中心线定点到圆心的垂直高度
            if (par.HeightOffset < 0) return false;
            double railHeight = rail.par.BraceHeight + rail.par.UpBridgeHeight + rail.par.DownBridgeHeight;//导轨高度
            //高度=总高度+导轨高度+顶板厚度+支撑高度+中间板厚度+底板厚度
            double h1 =  railHeight + support.topBoard.par.Thickness + support.brace.par.Height+support.centerBoard.par.Thickness + support.baseBoard.par.Thickness;
            //中心线底板到大圆线垂直距离
            double h2 = par.RailTotalHeight - h1;
            if (h2 <= 0) return false;
            //大圆到底板和旁板交接线距离
            double w1 = support.baseBoard.par.Width - support.sidePlate.par.Width;
            if (w1 <= 0) return false;
            double h22 = par.HeightOffset + h1;
            double w22 = Math.Pow(Math.Pow(par.CylinderInRadius.Value, 2) - Math.Pow(h22, 2), 0.5);
            double w2 = w22 - w1; //旁板与圆接触面到圆心的水平距离
            double h3 = Math.Pow(Math.Pow(par.CylinderInRadius.Value, 2) - Math.Pow(w2, 2), 0.5); //旁板与圆接触面圆上点到圆心的垂直距离
            support.sidePlate.par.Thickness = h3 - par.HeightOffset - h1;
            if (support.sidePlate.par.Thickness <= 0)
            {
                return false;
            }

            return true;
                
        }
        
        public override void CreateModule()
        {
           // if (!CheckParamete()) return;
            CreateDoc();
            rail.CreateModule();
            support.CreateModule();
            ComponentOccurrence CORail = LoadOccurrence((ComponentDefinition)rail.Doc.ComponentDefinition);
           // iMateDefinition railMate = Getimate(CORail, "mateR1");
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


            SaveDoc();
        }
    }
}
