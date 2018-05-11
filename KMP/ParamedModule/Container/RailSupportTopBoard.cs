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
using System.Reflection;
namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架顶板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RailSupportTopBoard : PartModulebase
    {
      internal  ParRailSupportTopBoard par = new ParRailSupportTopBoard();
        public RailSupportTopBoard():base()
        {
            this.Parameter = par;
            init();
        }
        void init()
        {
            par.Width = 120;
            par.Thickness = 20;
            par.HoleCenterDistance = 10;
            par.HoleRadius = 5.5;
            par.HoleSideEdgeDistance =15;
            par.HoleTopEdgeDistance = 15;
        }
      
        public override void CreateModule()
        {
            CreateDoc();
           PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            ExtrudeFeature box = InventorTool.CreateBoxWithHole(Definition,osketch, UsMM(par.Width), UsMM(par.Width ), UsMM(par.Thickness),
                UsMM(par.HoleCenterDistance ), UsMM(par.HoleTopEdgeDistance ), UsMM(par.HoleSideEdgeDistance ), UsMM(par.HoleRadius ));

            #region 支架装配
            Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(box.StartFaces.GetEnumerator());
            box.Name = "TopBoard";
            MateiMateDefinition mateD = Definition.iMateDefinitions.AddMateiMateDefinition(startFace, 0);
            mateD.Name = "mateD";
         
            #endregion
            #region  导轨装配
            List<Face> sideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(box.SideFaces.GetEnumerator());
            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(box.EndFaces.GetEnumerator());
            Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0).Name = "mateR1";//顶面
            //侧面顺序 0，2 是导轨长度方向
           // Definition.iMateDefinitions.AddMateiMateDefinition(sideFaces[0], 0).Name = "mateR3";
           // Definition.iMateDefinitions.AddMateiMateDefinition(sideFaces[1], 0).Name = "mateR2";
          
            #endregion
             SaveDoc();
        }



        public override bool CheckParamete()
        {
            if (!CommonTool.CheckParameterValue(par)) return false;
            if (!CommonTool.CheckParameterValue(this.Parameter)) return false;
            if (par.HoleTopEdgeDistance <= par.HoleRadius) return false;
            if (par.HoleSideEdgeDistance <= par.HoleRadius) return false;
            if (par.HoleCenterDistance + par.HoleRadius > par.Width / 2) return false;
            return true;
        }
    }
}
