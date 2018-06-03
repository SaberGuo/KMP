﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.HeatSinkSystem;
using System.Xml.Serialization;
namespace ParamedModule.HeatSinkSystem
{
    [Export("HeaterSystem", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HeatSink : AssembleModuleBase
    {
        [XmlElement]
      public  ParHeatSink par = new ParHeatSink();
        public Cap _cap;
        public Noumenon _nomenon;
        [ImportingConstructor]
        public HeatSink():base()
        {
            this.Parameter = par;
            this.Name = "热沉系统";
            _cap = new Cap(par.InDiameter,par.Thickness);
            _nomenon = new Noumenon(par.InDiameter, par.Thickness);
            SubParamedModules.Add(_cap);
            SubParamedModules.Add(_nomenon);
        }
        public override bool CheckParamete()
        {
            if (!_cap.CheckParamete() || (!_nomenon.CheckParamete()))
                return false;
            return true;
        }

      
        public override void CreateSub()
        {
            _cap.CreateModule();
            _nomenon.CreateModule();
            ComponentOccurrence COnomenon = LoadOccurrence((ComponentDefinition)_nomenon.Doc.ComponentDefinition);
            ComponentOccurrence COcap1 = LoadOccurrence((ComponentDefinition)_cap.Doc.ComponentDefinition);
            ComponentOccurrence COcap2 = LoadOccurrence((ComponentDefinition)_cap.Doc.ComponentDefinition);
            List<iMateDefinition> NomenoniMates = InventorTool.GetCollectionFromIEnumerator<iMateDefinition>(COnomenon.iMateDefinitions.GetEnumerator());
            iMateDefinition nomenonAxis = NomenoniMates.Where(a => a.Name == "Axis").FirstOrDefault();
            iMateDefinition StartFace = NomenoniMates.Where(a => a.Name == "StartFace").FirstOrDefault();
            iMateDefinition endFace = NomenoniMates.Where(a => a.Name == "EndFace").FirstOrDefault();

            List<iMateDefinition> capiMates1 = InventorTool.GetCollectionFromIEnumerator<iMateDefinition>(COcap1.iMateDefinitions.GetEnumerator());
            iMateDefinition capAxis1 = capiMates1.Where(a => a.Name == "Axis").FirstOrDefault();
            iMateDefinition capFace1 = capiMates1.Where(a => a.Name == "Face").FirstOrDefault();
            List<iMateDefinition> capiMates2 = InventorTool.GetCollectionFromIEnumerator<iMateDefinition>(COcap2.iMateDefinitions.GetEnumerator());
            iMateDefinition capAxis2 = capiMates2.Where(a => a.Name == "Axis").FirstOrDefault();
            iMateDefinition capFace2 = capiMates2.Where(a => a.Name == "Face").FirstOrDefault();
            Definition.iMateResults.AddByTwoiMates(capAxis1, nomenonAxis);
            Definition.iMateResults.AddByTwoiMates(capAxis2, nomenonAxis);
            Definition.iMateResults.AddByTwoiMates(StartFace, capFace1);
            Definition.iMateResults.AddByTwoiMates(endFace, capFace2);
        }
    }
}
