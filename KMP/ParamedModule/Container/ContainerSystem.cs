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
        Cylinder cylinder;
        CylinderDoor cylinderDoor;
        Pedestal pedestal;
        [ImportingConstructor]
        public ContainerSystem():base()
        {
            this.Parameter = par;
            cylinder = new Cylinder();
            cylinderDoor = new CylinderDoor();
            pedestal = new Pedestal();
            init();
        }
        void init()
        {
            par.PedestalNumber = 3;
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateModule()
        {
            CreateDoc();
            oPos = InventorTool.TranGeo.CreateMatrix();
            cylinder.CreateModule();
            cylinderDoor.CreateModule();
            pedestal.CreateModule();
            ComponentOccurrence COcylinder = LoadOccurrence((ComponentDefinition)cylinder.Doc.ComponentDefinition);
            ComponentOccurrence COcylinderDoor = LoadOccurrence((ComponentDefinition)cylinderDoor.Doc.ComponentDefinition);
            SetiMateResult(COcylinder);
            SetiMateResult(COcylinderDoor);
            List<Face> cylinderSF = GetSideFaces(COcylinder, "Cylinder");
            double distance = cylinder.par.Length / (par.PedestalNumber + 1);
            for(int i=0;i<par.PedestalNumber;i++)
            {
                ComponentOccurrence COpedestal = LoadOccurrence((ComponentDefinition)pedestal.Doc.ComponentDefinition);
               
                #region 底座轴配对
                iMateDefinition a = Getimate(COcylinder, "mateH");
                iMateDefinition b = Getimate(COpedestal, "mateM");
                Definition.iMateResults.AddByTwoiMates(a,b);
                #endregion
                #region 底座平移
               
                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)COpedestal.Definition).Features.GetEnumerator());

                PartFeature feature = features.Where(d => d.Name == "UnderBoard").FirstOrDefault();
                Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(((ExtrudeFeature)feature).StartFaces.GetEnumerator());
                MateiMateDefinition mate = (MateiMateDefinition)Getimate(COcylinder, "mateK");
                Face jarFace = (Face)mate.Entity;
                ((PartComponentDefinition)COcylinder.Definition).iMateDefinitions.AddFlushiMateDefinition(jarFace, ( -i * distance - distance)+"mm").Name = "mateG" + i;
                ((PartComponentDefinition)COpedestal.Definition).iMateDefinitions.AddFlushiMateDefinition(startFace,( -i * distance - distance)+"mm").Name="mateG"+i;
            
                Definition.iMateResults.AddByTwoiMates(Getimate(COcylinder, "mateG" + i), Getimate(COpedestal, "mateG" + i));
                #endregion
            }



        }
        iMateDefinition Getimate(ComponentOccurrence occ,string name)
        {
          List<iMateDefinition> list=InventorTool.GetCollectionFromIEnumerator<iMateDefinition>( occ.iMateDefinitions.GetEnumerator());
            return list.Where(a => a.Name == name).FirstOrDefault();
        }
        ComponentOccurrence GetFeature(ComponentOccurrence occ,string name)
        {
            if(occ.Name==name)
            {
                return occ;
            }
            else
            {

                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    return GetFeature(item, name);
                }
                return null;
            }
        }
    }
}
