using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using ParamedModule.Container;
using ParamedModule.HeatSinkSystem;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.HeatSinkSystem;
using System.Xml.Serialization;

namespace ParamedModule
{
    [Export("WareHouseEnvironment", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WareHouseEnvironment:AssembleModuleBase
    {
        public ParWareHouseEnvironment par = new ParWareHouseEnvironment();
        public ContainerSystem _container;
        public HeatSink _heatSink;
        public WareHouseEnvironment():base()
        {
            this.Parameter = par;
            this.Name = "环境仓";
            _container = new ContainerSystem();
            _heatSink = new HeatSink();
            SubParamedModules.Add(_container);
            SubParamedModules.Add(_heatSink);
        }
        public override bool CheckParamete()
        {
            if (!_container.CheckParamete() ||(! _heatSink.CheckParamete()))
                return false;
            return true;
        }

        //public override void CreateModule()
        //{
        //    CreateDoc();
        //    if (!CheckParamete()) return;
        //    oPos = InventorTool.TranGeo.CreateMatrix();
        //    _container.CreateModule();
        //    _heatSink.CreateModule();
        //    ComponentOccurrence COcontainer = LoadOccurrence((ComponentDefinition)_container.Doc.ComponentDefinition);
        //    ComponentOccurrence COheatSink = LoadOccurrence((ComponentDefinition)_heatSink.Doc.ComponentDefinition);
        //    WorkAxis CylinderAxis = GetAxis(COcontainer, "CylinderAxis");
        //    WorkAxis NoumenonAxis = GetAxis(COheatSink, "NoumenonAxis");
        //    Definition.Constraints.AddMateConstraint(CylinderAxis, NoumenonAxis, 0);

        //}
        public override void CreateSub()
        {
            InventorTool.Inventor.Documents.CloseAll();
            CreateDoc();
            _container.CreateModule();
            _heatSink.CreateModule();
            ComponentOccurrence COcontainer = LoadOccurrence((ComponentDefinition)_container.Doc.ComponentDefinition);
            ComponentOccurrence COheatSink = LoadOccurrence((ComponentDefinition)_heatSink.Doc.ComponentDefinition);
            WorkAxis CylinderAxis = GetAxisProxy(COcontainer, "CylinderAxis");
            WorkAxis NoumenonAxis = GetAxisProxy(COheatSink, "NoumenonAxis");
            Definition.Constraints.AddMateConstraint(CylinderAxis, NoumenonAxis, 0);

        }
        public static WorkAxis GetAxisProxy(ComponentOccurrence occ,string name)
        {
            if(occ.DefinitionDocumentType==DocumentTypeEnum.kPartDocumentObject)
            {
                List<WorkAxis> Axises = InventorTool.GetCollectionFromIEnumerator<WorkAxis>(((PartComponentDefinition)occ.Definition).WorkAxes.GetEnumerator());
                WorkAxis Axis = Axises.Where(a => a.Name == name).FirstOrDefault();
                if (Axis != null)
                {
                    object result;
                    occ.CreateGeometryProxy(Axis, out result);
                    return (WorkAxis)result;
                }
                return null;
            }
            else
            {
                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    WorkAxis result = GetAxisProxy(item, name);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
          
        }

    }
}
