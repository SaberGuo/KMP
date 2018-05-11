using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Container;
namespace ParamedModule.Container
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ContainerSystem : AssembleModuleBase
    {
        ParContainerSystem par = new ParContainerSystem();
        Cylinder _cylinder;
        CylinderDoor _cylinderDoor;
        Pedestal _pedestal;
        RailSystem _railSystem;
        [ImportingConstructor]
        public ContainerSystem():base()
        {
            this.Parameter = par;
            _cylinder = new Cylinder();
            _cylinderDoor = new CylinderDoor();
            _pedestal = new Pedestal();
            _railSystem = new RailSystem();
            SubParamedModules.Add(_cylinder);
            SubParamedModules.Add(_cylinderDoor);
            SubParamedModules.Add(_pedestal);
            SubParamedModules.Add(_railSystem);
            init();
        }
        void init()
        {
            par.PedestalNumber = 3;
        }
        public override bool CheckParamete()
        {
         
            if ((!_cylinder.CheckParamete()) || (!_cylinderDoor.CheckParamete()) || (!_pedestal.CheckParamete()) || (!_railSystem.CheckParamete()))
                return false;
            return CommonTool.CheckParameterValue(par);
        }

        public override void CreateModule()
        {
            if (!CheckParamete()) return;
            CreateDoc();
            oPos = InventorTool.TranGeo.CreateMatrix();
            _cylinder.CreateModule();
            _cylinderDoor.CreateModule();
            _pedestal.CreateModule();
            _railSystem.CreateModule();
            #region 容器罐、罐门、底座组装
            ComponentOccurrence COcylinder = LoadOccurrence((ComponentDefinition)_cylinder.Doc.ComponentDefinition);
            ComponentOccurrence COcylinderDoor = LoadOccurrence((ComponentDefinition)_cylinderDoor.Doc.ComponentDefinition);
            SetiMateResult(COcylinder);
            SetiMateResult(COcylinderDoor);
            List<Face> cylinderSF = GetSideFaces(COcylinder, "Cylinder");
            double distance = _cylinder.par.Length / (par.PedestalNumber + 1);
            iMateDefinition cylinderAxisMate = Getimate(COcylinder, "mateH");//罐体轴
          //  WorkAxis aixs = ((MateiMateDefinition)cylinderAxisMate).Entity;
           List<WorkAxis> cylinderAxes=  InventorTool.GetCollectionFromIEnumerator<WorkAxis>(((PartComponentDefinition)COcylinder.Definition).WorkAxes.GetEnumerator());
            WorkAxis cylinderAxis = cylinderAxes.Where(a => a.Name == "CylinderAxis").FirstOrDefault();
            MateiMateDefinition cylinderOutageMate = (MateiMateDefinition)Getimate(COcylinder, "mateK");
            Face cylinderOutageFace = (Face)cylinderOutageMate.Entity;
            object cylinderAxisProxy, cylinderOutageFaceProxy;//罐体轴代理 罐口面代理
            COcylinder.CreateGeometryProxy(cylinderOutageFace, out cylinderOutageFaceProxy);
            COcylinder.CreateGeometryProxy(cylinderAxis, out cylinderAxisProxy);
         
            for (int i=0;i<par.PedestalNumber;i++)
            {
                ComponentOccurrence COpedestal = LoadOccurrence((ComponentDefinition)_pedestal.Doc.ComponentDefinition);
               
                #region 底座轴配对
              
                iMateDefinition b = Getimate(COpedestal, "mateM");
                Definition.iMateResults.AddByTwoiMates(cylinderAxisMate,b);
                #endregion
                #region 底座平移
               
                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)COpedestal.Definition).Features.GetEnumerator());

                PartFeature feature = features.Where(d => d.Name == "UnderBoard").FirstOrDefault();
                Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(((ExtrudeFeature)feature).StartFaces.GetEnumerator());
             
                ((PartComponentDefinition)COcylinder.Definition).iMateDefinitions.AddFlushiMateDefinition(cylinderOutageFace, ( -i * distance - distance)+"mm").Name = "mateG" + i;
                ((PartComponentDefinition)COpedestal.Definition).iMateDefinitions.AddFlushiMateDefinition(startFace,( -i * distance - distance)+"mm").Name="mateG"+i;
            
                Definition.iMateResults.AddByTwoiMates(Getimate(COcylinder, "mateG" + i), Getimate(COpedestal, "mateG" + i));
                #endregion
            }
            #endregion
            ComponentOccurrence CORai1 = LoadOccurrence((ComponentDefinition)_railSystem.Doc.ComponentDefinition);
            ComponentOccurrence CORai2 = LoadOccurrence((ComponentDefinition)_railSystem.Doc.ComponentDefinition);
           ExtrudeFeature rail1 = GetFeatureproxy<ExtrudeFeature>(CORai1, "Rail",ObjectTypeEnum.kExtrudeFeatureObject);
            ExtrudeFeature rail2 =GetFeatureproxy<ExtrudeFeature>(CORai2, "Rail", ObjectTypeEnum.kExtrudeFeatureObject);
            List<Face> railSF1 = InventorTool.GetCollectionFromIEnumerator<Face>(rail1.SideFaces.GetEnumerator());
            List<Face> railSF2 = InventorTool.GetCollectionFromIEnumerator<Face>(rail2.SideFaces.GetEnumerator());
            Face railEndFace1 = InventorTool.GetFirstFromIEnumerator<Face>(rail1.StartFaces.GetEnumerator());
            Face railStartFace2 = InventorTool.GetFirstFromIEnumerator<Face>(rail2.EndFaces.GetEnumerator());
            //  List<Face> railSF2 = GetSideFacesproxy(CORai2, "Rail");
          
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF1[11], UsMM(_railSystem.par.HeightOffset));
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF2[11], UsMM(_railSystem.par.HeightOffset));
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF1[0], UsMM(-_railSystem.par.Offset));
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF2[10], UsMM(_railSystem.par.Offset));
            Definition.Constraints.AddFlushConstraint(cylinderOutageFaceProxy, railEndFace1, 0);
            Definition.Constraints.AddFlushConstraint(cylinderOutageFaceProxy, railStartFace2, 0);
            SaveDoc();
        }

    }
}
