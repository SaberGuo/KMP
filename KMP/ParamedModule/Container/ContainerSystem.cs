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
    [Export("ContainerSystem", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ContainerSystem : AssembleModuleBase
    {
        public ParContainerSystem par = new ParContainerSystem();
        public Cylinder _cylinder;
        public CylinderDoor _cylinderDoor;
        public Pedestal _pedestal;
        public RailSystem _railSystem;
        public PlaneSystem _plane;

        
        [ImportingConstructor]
        public ContainerSystem():base()
        {
            init();
            this.Name = "容器系统";
            this.Parameter = par;
            this.ProjectType = "ContainerSystem";
            _plane = new PlaneSystem(par.InRadius);
            _cylinder = new Cylinder(par.InRadius,par.Thickness);
            _cylinderDoor = new CylinderDoor(par.InRadius,par.Thickness);
            _pedestal = new Pedestal(par.InRadius,par.Thickness);
            _railSystem = new RailSystem(par.InRadius);
            SubParamedModules.AddModule(_cylinder);
            SubParamedModules.AddModule(_cylinderDoor);
            SubParamedModules.AddModule(_pedestal);
            SubParamedModules.AddModule(_railSystem);
            SubParamedModules.AddModule(_plane);
            init();
        }
        void init()
        {
            par.PedestalNumber = 3;
            par.InRadius.Value = 1400;
            par.Thickness.Value = 24;
            this.Name = "容器系统";
        }
        public override bool CheckParamete()
        {
         
            if ((!_cylinder.CheckParamete()) || (!_cylinderDoor.CheckParamete()) || 
                (!_pedestal.CheckParamete()) || (!_railSystem.CheckParamete()) || (!_plane.CheckParamete()))
                return false;
            if (!CheckParZero()) return false;
            return true;
        }

      
        public override void CreateSub()
        {
           // GeneratorProgress(this, "开始创建容器系统");
            _cylinder.CreateModule();
            _cylinderDoor.CreateModule();
            _plane.CreateModule();


            _pedestal.CreateModule();
            _railSystem.CreateModule();

            #region 容器罐、罐门、底座组装
            ComponentOccurrence COcylinder = LoadOccurrence((ComponentDefinition)_cylinder.Doc.ComponentDefinition);
            ComponentOccurrence COcylinderDoor = LoadOccurrence((ComponentDefinition)_cylinderDoor.Doc.ComponentDefinition);
            SetiMateResult(COcylinder);
            SetiMateResult(COcylinderDoor);
           // List<Face> cylinderSF = GetSideFaces(COcylinder, "Cylinder");
            double distance = _cylinder.par.Length / (par.PedestalNumber + 1);
            iMateDefinition cylinderAxisMate = Getimate(COcylinder, "mateH");//罐体轴
                                                                             //  WorkAxis aixs = ((MateiMateDefinition)cylinderAxisMate).Entity;
            List<WorkAxis> cylinderAxes = InventorTool.GetCollectionFromIEnumerator<WorkAxis>(((PartComponentDefinition)COcylinder.Definition).WorkAxes.GetEnumerator());
            WorkAxis cylinderAxis = cylinderAxes.Where(a => a.Name == "CylinderAxis").FirstOrDefault();
            MateiMateDefinition cylinderOutageMate = (MateiMateDefinition)Getimate(COcylinder, "mateK");
            Face cylinderOutageFace = (Face)cylinderOutageMate.Entity;
            object cylinderAxisProxy, cylinderOutageFaceProxy;//罐体轴代理 罐口面代理
            COcylinder.CreateGeometryProxy(cylinderOutageFace, out cylinderOutageFaceProxy);
            COcylinder.CreateGeometryProxy(cylinderAxis, out cylinderAxisProxy);

            for (int i = 0; i < par.PedestalNumber; i++)
            {
                ComponentOccurrence COpedestal = LoadOccurrence((ComponentDefinition)_pedestal.Doc.ComponentDefinition);

                #region 底座轴配对

                iMateDefinition b = Getimate(COpedestal, "mateM");
                Definition.iMateResults.AddByTwoiMates(cylinderAxisMate, b);
                #endregion
                #region 底座平移

                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)COpedestal.Definition).Features.GetEnumerator());

                PartFeature feature = features.Where(d => d.Name == "UnderBoard").FirstOrDefault();
                Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(((ExtrudeFeature)feature).StartFaces.GetEnumerator());

                ((PartComponentDefinition)COcylinder.Definition).iMateDefinitions.AddFlushiMateDefinition(cylinderOutageFace, (-i * distance - distance) + "mm").Name = "mateG" + i;
                ((PartComponentDefinition)COpedestal.Definition).iMateDefinitions.AddFlushiMateDefinition(startFace, (-i * distance - distance) + "mm").Name = "mateG" + i;

                Definition.iMateResults.AddByTwoiMates(Getimate(COcylinder, "mateG" + i), Getimate(COpedestal, "mateG" + i));
                #endregion
            }
            #endregion
            #region 导轨组件组装
            ComponentOccurrence CORai1 = LoadOccurrence((ComponentDefinition)_railSystem.Doc.ComponentDefinition);
            ComponentOccurrence CORai2 = LoadOccurrence((ComponentDefinition)_railSystem.Doc.ComponentDefinition);
            ExtrudeFeature rail1 = GetFeatureproxy<ExtrudeFeature>(CORai1, "Rail", ObjectTypeEnum.kExtrudeFeatureObject);
            ExtrudeFeature rail2 = GetFeatureproxy<ExtrudeFeature>(CORai2, "Rail", ObjectTypeEnum.kExtrudeFeatureObject);
            List<Face> railSF1 = InventorTool.GetCollectionFromIEnumerator<Face>(rail1.SideFaces.GetEnumerator());
            List<Face> railSF2 = InventorTool.GetCollectionFromIEnumerator<Face>(rail2.SideFaces.GetEnumerator());
            Face railEndFace1 = InventorTool.GetFirstFromIEnumerator<Face>(rail1.StartFaces.GetEnumerator());
            Face railStartFace2 = InventorTool.GetFirstFromIEnumerator<Face>(rail2.EndFaces.GetEnumerator());
            //  List<Face> railSF2 = GetSideFacesproxy(CORai2, "Rail");

            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF1[11], UsMM(_railSystem.par.HeightOffset));//导轨顶面
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF2[11], UsMM(_railSystem.par.HeightOffset));
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF1[0], UsMM(-_railSystem.par.Offset - _railSystem.rail.par.UpBridgeWidth / 2));//导轨顶侧面
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, railSF2[10], UsMM(_railSystem.par.Offset - _railSystem.rail.par.UpBridgeWidth / 2));//导轨顶侧面
            Definition.Constraints.AddFlushConstraint(cylinderOutageFaceProxy, railEndFace1, 0); //导轨横截面
            Definition.Constraints.AddFlushConstraint(cylinderOutageFaceProxy, railStartFace2, 0);
            #endregion
            #region
            oPos.SetToRotateTo(InventorTool.TranGeo.CreateVector(0, 0, 1), InventorTool.TranGeo.CreateVector(0, 1, 0));

            ComponentOccurrence COPlane1 = LoadOccurrence((ComponentDefinition)_plane.Doc.ComponentDefinition);

            ComponentOccurrence COPlane2 = LoadOccurrence((ComponentDefinition)_plane.Doc.ComponentDefinition);

            OccStruct OccPlane1 = GetOccStruct(COPlane1, "RailSidePlate", _plane.par.PlaneNumber - 1);
            OccStruct OccPlane2 = GetOccStruct(COPlane2, "RailSidePlate", 0);
            Definition.Constraints.AddAngleConstraint(OccPlane1.SideFaces[2], OccPlane2.SideFaces[2], Math.PI);
            //Definition.Constraints.AddAngleConstraint(railSF1[11], OccPlane1.EndFace, 0);
            //Definition.Constraints.AddAngleConstraint(railSF1[11], OccPlane2.EndFace, 0);
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, OccPlane1.EndFace, UsMM(_plane.par.HeightOffset));//导轨顶面
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, OccPlane2.EndFace, UsMM(_plane.par.HeightOffset));
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, OccPlane1.SideFaces[1], UsMM(-_plane._planeSup.par.Offset - _plane._plane.par.Width / 2));//导轨顶侧面
            Definition.Constraints.AddMateConstraint(cylinderAxisProxy, OccPlane2.SideFaces[3], UsMM(_plane._planeSup.par.Offset - _plane._plane.par.Width / 2));//导轨顶侧面
            Definition.Constraints.AddFlushConstraint(cylinderOutageFaceProxy, OccPlane1.SideFaces[2], 0); //导轨横截面
            Definition.Constraints.AddFlushConstraint(cylinderOutageFaceProxy, OccPlane2.SideFaces[0], 0);
            #endregion
           // GeneratorProgress(this, "完成创建容器系统");
        }
    }
}
