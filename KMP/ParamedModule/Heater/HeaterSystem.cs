using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using KMP.Interface.Model.Heater;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace ParamedModule.Heater
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class HeaterSystem : AssembleModuleBase
    {
        ParHeaterSystem par = new ParHeaterSystem();
        HeaterBack heaterBack;
        HeaterDoor heaterDoor;
        HeaterCylinder heaterCylinder;
        public HeaterSystem():base()
        {
            this.Parameter = par;
            heaterCylinder = new HeaterCylinder();
            heaterDoor = new HeaterDoor();
            heaterBack = new HeaterBack();
            init();
        }

        void init()
        {
            
        }

        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateModule()
        {
            CreateDoc();
            oPos = InventorTool.TranGeo.CreateMatrix();
            heaterCylinder.CreateModule();
            heaterDoor.CreateModule();
            heaterBack.CreateModule();
            ComponentOccurrence COcylinder = LoadOccurrence((ComponentDefinition)heaterCylinder.Doc.ComponentDefinition);
            ComponentOccurrence COheaterDoor = LoadOccurrence((ComponentDefinition)heaterDoor.Doc.ComponentDefinition);
            ComponentOccurrence COheaterBack = LoadOccurrence((ComponentDefinition)heaterBack.Doc.ComponentDefinition);
            SetiMateResult(COcylinder);
            SetiMateResult(COheaterDoor);
            SetiMateResult(COheaterBack);
        }


   }
}
