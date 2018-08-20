using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using Inventor;
using Infranstructure.Tool;
using System.ComponentModel.Composition;
using KMP.Interface.Model;
namespace ParamedModule.Container
{
 
    public class RailSystem : AssembleModuleBase
    {
        public ParRailSystem par = new ParRailSystem();
        public Rail rail = new Rail();
        public RailSupport support;
        public RailSystem():base()
        {
            this.PreviewImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview", "RailSystem.png");
        }
        public RailSystem(PassedParameter InRadius) :base()
        {
            rail = new Rail();
            support = new RailSupport();
            SubParamedModules.AddModule(rail);
            SubParamedModules.AddModule(support);
       
            this.Parameter = par;
            init();
            this.rail.Name = "导轨";
            this.support.Name = "导轨支架";
            par.CylinderInRadius = InRadius;

            this.Name = "导轨系统";

            this.PreviewImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview", "RailSystem.png");
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
            if (!CheckParZero()) return false;
            if ((!support.CheckParamete())||(!rail.CheckParamete())) return false;
            if (par.Offset >= par.CylinderInRadius.Value)
            {
                ParErrorChanged(this, "罐体中心偏移量大于罐体半径");
                return false;
            }
            double h0 = Math.Pow(Math.Pow(par.CylinderInRadius.Value, 2) - Math.Pow(par.Offset, 2), 0.5);//偏移后圆上点到圆心的垂直高度
            par.HeightOffset = h0 - par.RailTotalHeight; //偏移后导轨中心线定点到圆心的垂直高度
            if (par.HeightOffset < 0)
            {
                ParErrorChanged(this, "导轨组件安装在罐体中高于罐体半径");
                return false;
            }
            double railHeight = rail.par.BraceHeight + rail.par.UpBridgeHeight + rail.par.DownBridgeHeight;//导轨高度
            //除侧板外组件总高度=总高度+导轨高度+顶板厚度+支撑高度+中间板厚度+底板厚度
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
            ////旁板与圆接触面圆上点到圆心的垂直距离
            double h3 = Math.Pow(Math.Pow(par.CylinderInRadius.Value, 2) - Math.Pow(w2, 2), 0.5); 
            support.sidePlate.par.Thickness = h3 - par.HeightOffset - h1;
            if (support.sidePlate.par.Thickness <= 0)
            {
                ParErrorChanged(this, "板材厚度小于零");
                return false;
            }

            return true;
                
        }
         
       
        public override void CreateSub()
        {
            rail.CreateModule();
            support.CreateModule();
            ComponentOccurrence CORail = LoadOccurrence((ComponentDefinition)rail.Doc.ComponentDefinition);
            // iMateDefinition railMate = Getimate(CORail, "mateR1");
            List<Face> railSideFaces = GetSideFacesproxy(CORail, "Rail");
            ExtrudeFeature railFeature = GetFeature<ExtrudeFeature>(CORail, "Rail", ObjectTypeEnum.kExtrudeFeatureObject);
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
               
                object railSideproxy;
                CORail.CreateGeometryProxy(railEndFace, out railSideproxy);
                Definition.Constraints.AddMateConstraint(railSideFaces[5], supportTopFace, 0);
                Definition.Constraints.AddFlushConstraint(supportSF[0], railSideproxy, i * interval + interval / 2);
                Definition.Constraints.AddFlushConstraint(supportSF[1], railSideFaces[6], 0);
             

            }
        }
    }
}
