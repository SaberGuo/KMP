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
using KMP.Interface.Model.Container;
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
            par.OffSet = 0;
        }
        public override bool CheckParamete()
        {
            if (!_container.CheckParamete() ||(! _heatSink.CheckParamete()))
                return false;
            return true;
        }

      
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
            MateiMateDefinition cylinderOutageMate = (MateiMateDefinition)Getimate(COcontainer, "mateK");
            Face cylinderOutageFace = (Face)cylinderOutageMate.Entity;
            object  cylinderOutageFaceProxy;//罐体轴代理 罐口面代理
            COcontainer.CreateGeometryProxy(cylinderOutageFace, out cylinderOutageFaceProxy);
           ExtrudeFeature cap= GetFeatureproxy<ExtrudeFeature>(COheatSink, "HeartCap", ObjectTypeEnum.kExtrudeFeatureObject);
            Face capStartFace = InventorTool.GetFirstFromIEnumerator<Face>(cap.StartFaces.GetEnumerator());
            Definition.Constraints.AddFlushConstraint(capStartFace, cylinderOutageFace, UsMM(par.OffSet));
           List<ParCylinderHole> holes=  _container._cylinder.par.ParHoles.Where(a => a.IsHeatSinkHole).ToList();
            _heatSink._nomenon.CreateHoles(holes, UsMM(par.OffSet+_heatSink._cap.par.CapThickness));
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
