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
    /// <summary>
    /// 导轨支架立柱下钣金
    /// </summary>

   public class RailSupportCenterBoard : PartModulebase
    {
        public ParRailSupportCenterBoard par = new ParRailSupportCenterBoard();
        public RailSupportCenterBoard():base()
        {
            this.Parameter = par;
            init();

            this.Name = "导轨支架立柱下钣金";
        }
        public override void InitModule()
        {
            this.Parameter = par;
            base.InitModule();
        }
        void init()
        {
            par.Width = 180;
            par.Length = 200;
            par.Thickness = 15;
            par.HoleCenterDistance = 10;
            par.HoleDiameter = 13;
            par.HoleSideEdgeDistance = 30;
            par.HoleTopEdgeDistance = 25;
        }

      
        public override void CreateSub()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            ExtrudeFeature box = InventorTool.CreateBoxWithHole(Definition, osketch, UsMM(par.Width), UsMM(par.Length), UsMM(par.Thickness),
                   UsMM(par.HoleCenterDistance), UsMM(par.HoleTopEdgeDistance), UsMM(par.HoleSideEdgeDistance), UsMM(par.HoleDiameter/2));
            box.Name = "CenterBoard";


            List<Face> sideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());

            MateiMateDefinition mateB = Definition.iMateDefinitions.AddMateiMateDefinition(startFace, 0);
            mateB.Name = "mateB";
            MateiMateDefinition mateC = Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0);
            mateC.Name = "mateC";
            Definition.iMateDefinitions.AddFlushiMateDefinition(sideFaces[2], 0).Name = "flushC";

        }
        public override bool CheckParamete()
        {
            if (par.HoleDiameter >= par.Width / 2)
            {
                ParErrorChanged(this, "螺丝孔太大");
                return false;
            }
            if (par.HoleSideEdgeDistance * 2 + par.HoleDiameter * 2 > par.Width)
            {
                ParErrorChanged(this, "螺丝孔与板侧边距离过大");
                return false;
            }
            if (par.HoleTopEdgeDistance * 2 + par.HoleDiameter * 2 > par.Length)
            {
                ParErrorChanged(this, "螺丝孔与板顶边距离过大");
                return false;
            }
            if (!CheckParZero()) return false;
            return true;
        }
    }
}
