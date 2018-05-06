﻿using System;
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
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class RailSupport : AssembleModuleBase
    {
        ParRailSupport par = new ParRailSupport();
        RailSupportTopBoard topBoard;
        RailSupportSidePlate sidePlate;
        RailSupportCenterBoard centerBoard;
        RailSupportBrace brace;
        RailSupportbaseBoard baseBoard;
        [ImportingConstructor]
        public RailSupport():base()
        {
            this.Parameter = par;
            topBoard = new RailSupportTopBoard();
            sidePlate = new RailSupportSidePlate();
            centerBoard = new RailSupportCenterBoard();
            brace = new RailSupportBrace();
            baseBoard = new RailSupportbaseBoard();
            SubParameModules.Add(topBoard);
            SubParameModules.Add(sidePlate);
            SubParameModules.Add(centerBoard);
            SubParameModules.Add(brace);
            SubParameModules.Add(baseBoard);
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            centerBoard.CreateModule(new ParRailSupportCenterBoard());
            baseBoard.CreateModule(new ParRailSupportbaseBoard());
            sidePlate.CreateModule(new ParRailSupportSidePlate());
            brace.CreateModule(new ParRailSupportBrace());
            topBoard.CreateModule(new ParRailSupportTopBoard());
            CreateDoc();
            oPos = InventorTool.TranGeo.CreateMatrix();
          ComponentOccurrence CObaseBoad = LoadOccurrence((ComponentDefinition)baseBoard.Doc.ComponentDefinition);
            ComponentOccurrence COcenterBoad = LoadOccurrence((ComponentDefinition)centerBoard.Doc.ComponentDefinition);
            ComponentOccurrence COsidePlate = LoadOccurrence((ComponentDefinition)sidePlate.Doc.ComponentDefinition);
            ComponentOccurrence CObrace = LoadOccurrence((ComponentDefinition)brace.Doc.ComponentDefinition);
            ComponentOccurrence COtopBoard = LoadOccurrence((ComponentDefinition)topBoard.Doc.ComponentDefinition);
            List<Face> baseBoardSideFaces = GetSideFaces("RailBaseBoard");
            List<Face> sidePlateSideFaces = GetSideFaces("RailSidePlate");
            List<Face> centerBoardSF = GetSideFaces("CenterBoard");
            List<Face> topBoardSF = GetSideFaces("TopBoard");
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
        }
        private List<Face> GetSideFaces(string name)
        {
          return  InventorTool.GetCollectionFromIEnumerator<Face>(((ExtrudeFeature)partFeatures[name]).SideFaces.GetEnumerator());
        }
    }
}
