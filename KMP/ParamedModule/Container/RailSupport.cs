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
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class RailSupport : AssembleModuleBase
    {
        RailSupportTopBoard topBoard;
        RailSupportSidePlate sidePlate;
        RailSupportCenterBoard centerBoad;
        RailSupportBrace brace;
        RailSupportbaseBoard baseBoard;
        public RailSupport():base()
        {
            topBoard = new RailSupportTopBoard();
            sidePlate = new RailSupportSidePlate();
            centerBoad = new RailSupportCenterBoard();
            brace = new RailSupportBrace();
            baseBoard = new RailSupportbaseBoard();
            SubParameModules.Add(topBoard);
            SubParameModules.Add(sidePlate);
            SubParameModules.Add(centerBoad);
            SubParameModules.Add(brace);
            SubParameModules.Add(baseBoard);
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule(ParameterBase Parameter)
        {
          //  ComponentOccurrence CObaseBoard = assemblyDef.Occurrences.Add(baseBoard.ModelPath, oPos);

        }
    }
}
