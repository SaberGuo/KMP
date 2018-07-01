using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Other;
namespace ParamedModule.Other
{
    [Export("CryoLiquidTanks", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CryoLiquidTanks : AssembleModuleBase
    {
        ParCryoLiquidTanks par = new ParCryoLiquidTanks();
        CryoLiquidTank tank;
        [ImportingConstructor]
        public CryoLiquidTanks():base()
        {
            this.Name = "低温液体储槽阵列";
            par.Number = 3;
            par.Offset = 2500;
            this.Parameter = par;
            tank = new CryoLiquidTank();
            this.SubParamedModules.Add(tank);
        }
        public override bool CheckParamete()
        {
            if (par.Number <= 1 || par.Offset == 0) return false;
            return true;
        
        }

        public override void CreateSub()
        {
            tank.CreateModule();
            ComponentOccurrence COTank = LoadOccurrence((ComponentDefinition)tank.Doc.ComponentDefinition);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            objc.Add(COTank);

            WorkAxis axis = InventorTool.GetFirstFromIEnumerator<WorkAxis>(tank.Doc.ComponentDefinition.WorkAxes.GetEnumerator());
            object AxisProxy;
            COTank.CreateGeometryProxy(axis, out AxisProxy);
            Definition.OccurrencePatterns.AddRectangularPattern(objc, AxisProxy, true, par.Offset, par.Number);
        }
    }
}
