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
    /// 导轨支架
    /// </summary>
 
   public class RailSupport : AssembleModuleBase
    {
        public ParRailSupport par = new ParRailSupport();
        public RailSupportTopBoard topBoard;
        public RailSupportSidePlate sidePlate;
        public RailSupportCenterBoard centerBoard;
        public RailSupportBrace brace;
        public RailSupportbaseBoard baseBoard;
    
        public RailSupport():base()
        {
            this.Parameter = par;
            topBoard = new RailSupportTopBoard();
            sidePlate = new RailSupportSidePlate();
            centerBoard = new RailSupportCenterBoard();
            brace = new RailSupportBrace();
            baseBoard = new RailSupportbaseBoard();
            SubParamedModules.AddModule(topBoard);
            SubParamedModules.AddModule(sidePlate);
            SubParamedModules.AddModule(centerBoard);
            SubParamedModules.AddModule(brace);
            SubParamedModules.AddModule(baseBoard);

            this.Name = "导轨支架";
        }

        public override bool CheckParamete()
        {
            if ((!topBoard.CheckParamete()) || (!sidePlate.CheckParamete()) ||
                (!centerBoard.CheckParamete()) || (!brace.CheckParamete()) || (!baseBoard.CheckParamete())) return false;
            if (!CheckParZero()) return false;
            return true;
        }

        
        public override void CreateSub()
        {
          //  GeneratorProgress(this, "开始创建容器内导轨支撑");
            centerBoard.CreateModule();
            baseBoard.CreateModule();
            sidePlate.CreateModule();
            brace.CreateModule();
            topBoard.CreateModule();
            ComponentOccurrence CObaseBoad = LoadOccurrence((ComponentDefinition)baseBoard.Doc.ComponentDefinition);
            ComponentOccurrence COcenterBoad = LoadOccurrence((ComponentDefinition)centerBoard.Doc.ComponentDefinition);
            ComponentOccurrence COsidePlate = LoadOccurrence((ComponentDefinition)sidePlate.Doc.ComponentDefinition);
            ComponentOccurrence CObrace = LoadOccurrence((ComponentDefinition)brace.Doc.ComponentDefinition);
            ComponentOccurrence COtopBoard = LoadOccurrence((ComponentDefinition)topBoard.Doc.ComponentDefinition);
            List<Face> baseBoardSideFaces = GetSideFaces(CObaseBoad, "RailBaseBoard");
            List<Face> sidePlateSideFaces = GetSideFaces(COsidePlate, "RailSidePlate");
            List<Face> centerBoardSF = GetSideFaces(COcenterBoad, "CenterBoard");
            List<Face> topBoardSF = GetSideFaces(COtopBoard, "TopBoard");
            List<WorkAxis> braceAxises = InventorTool.GetCollectionFromIEnumerator<WorkAxis>(((PartComponentDefinition)CObrace.Definition).WorkAxes.GetEnumerator());
            WorkAxis braceAxis = braceAxises.Where(a => a.Name == "BraceAxis").FirstOrDefault();
            SetMateiMate(CObrace, braceAxis, COcenterBoad, centerBoardSF[0], "mateE", -centerBoard.par.Width / 2);
            SetMateiMate(CObrace, braceAxis, COcenterBoad, centerBoardSF[1], "mateF", -centerBoard.par.Width / 2);
            SetMateiMate(CObrace, braceAxis, COtopBoard, topBoardSF[0], "mateG", -topBoard.par.Width / 2);
            SetMateiMate(CObrace, braceAxis, COtopBoard, topBoardSF[1], "mateH", -topBoard.par.Width / 2);
            SetFlushiMate(CObaseBoad, baseBoardSideFaces[0], COsidePlate, sidePlateSideFaces[0], "flushA", -(baseBoard.par.Length - sidePlate.par.Length) / 2);
            SetFlushiMate(COcenterBoad, centerBoardSF[3], CObaseBoad, baseBoardSideFaces[0], "flushD", -(baseBoard.par.Length - centerBoard.par.Width) / 2);
            SetiMateResult(CObaseBoad);
            SetiMateResult(COcenterBoad);
            SetiMateResult(COsidePlate);
            SetiMateResult(CObrace);
            SetiMateResult(COtopBoard);
          //  GeneratorProgress(this, "完成创建容器内导轨支撑");
        }
    }
}
