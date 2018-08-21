using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
using System.Collections.ObjectModel;
namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 低温液体储槽阵列
    /// </summary>
    [Export("CryoLiquidTanks", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CryoLiquidTanks : AssembleModuleBase
    {
        ParCryoLiquidTanks par = new ParCryoLiquidTanks();
         CryoLiquidTank LiquidTank;
        CryoLiquidTank GasTank;
        Vaporizer vaporizer;
        List<ComponentOccurrence> COs = new List<ComponentOccurrence>();
        [ImportingConstructor]
        public CryoLiquidTanks():base()
        {
            this.Name = "低温液体储槽阵列";

            //par.Number = 3;
            //par.Offset = 2500;
            //this.Parameter = par;
            //tank = new CryoLiquidTank();
            //this.SubParamedModules.Add(tank);
            this.Parameter = par;
            LiquidTank = new CryoLiquidTank();
            GasTank = new CryoLiquidTank();
            vaporizer = new Vaporizer();
            LiquidTank.Name = "液氮储槽";
            GasTank.Name = "氮气储存罐";
            vaporizer.Name = "汽化器";
            this.SubParamedModules.Add(GasTank);
            this.SubParamedModules.Add(LiquidTank);
            this.SubParamedModules.Add(vaporizer);
            init();
        }
        void init()
        {
            par.NitrogenTankNum = 3;
            par.LiquidTankNum = 3;
            par.VaporizerNum = 2;
            GasTank.par.CapacityDN = 1200;
            LiquidTank.par.CapacityDN = 3500;
            for(int i=0;i<par.Offsets.Count;i++)
            {
                par.Offsets[i] = 3500;
            }
        }
        public override bool CheckParamete()
        {
         
            if(GasTank.CheckParamete()&&LiquidTank.CheckParamete()&&vaporizer.CheckParamete())
            {
                return true;
            }
            return false;
        
        }

        public override void CreateSub()
        {
            double offset=0;
            LiquidTank.CreateModule();
            GasTank.CreateModule();
            vaporizer.CreateModule();
            for (int i = 0; i < par.NitrogenTankNum; i++)
            {
                ComponentOccurrence CO = LoadOccurrence((ComponentDefinition)GasTank.Doc.ComponentDefinition);
                COs.Add(CO);
            }
            for (int i=0;i<par.NitrogenTankNum;i++)
            {
                ComponentOccurrence COLiquidTank = LoadOccurrence((ComponentDefinition)LiquidTank.Doc.ComponentDefinition);
                COs.Add(COLiquidTank);
            }

            for (int i = 0; i < par.VaporizerNum; i++)
            {
                ComponentOccurrence CO = LoadOccurrence((ComponentDefinition)vaporizer.Doc.ComponentDefinition);
                COs.Add(CO);
            }
            ExtrudeFeature sur = GetFeatureproxy<ExtrudeFeature>(COs[0], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
            List<Face> SurEF = InventorTool.GetCollectionFromIEnumerator<Face>(sur.SideFaces.GetEnumerator());
            //  WorkAxis Axis = GetAxis(COs[0], "Axis");
            WorkPlane plane = GetPlane(COs[0], "Flush");
            WorkPlane planeMate = GetPlane(COs[0], "Mate");
            for (int i=1;i<COs.Count;i++)
            {
                ExtrudeFeature suri = GetFeatureproxy<ExtrudeFeature>(COs[i], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
                if(i<par.NitrogenTankNum+par.LiquidTankNum)
                {
                    List<Face> SurEFi = InventorTool.GetCollectionFromIEnumerator<Face>(suri.SideFaces.GetEnumerator());
                    Definition.Constraints.AddFlushConstraint(SurEF[2], SurEFi[2], 0);
                }
                else
                {
                     Face SurEFi = InventorTool.GetFirstFromIEnumerator<Face>(suri.EndFaces.GetEnumerator());
                    Definition.Constraints.AddFlushConstraint(SurEF[2], SurEFi, 0);
                }
                offset += UsMM(par.Offsets[i - 1]);
                // WorkAxis Axisi = GetAxis(COs[i], "Axis");
                WorkPlane planei = GetPlane(COs[i], "Flush");
                WorkPlane planeMatei = GetPlane(COs[i], "Mate");
                FlushConstraint constraint=   Definition.Constraints.AddFlushConstraint(plane, planei, 0);
                FlushConstraint constraint1 = Definition.Constraints.AddFlushConstraint(planeMate, planeMatei, offset);
                //constraint.Delete();

                //Matrix otransform = COs[i].Transformation;
                //offset += UsMM(par.Offsets[i - 1]);
                //otransform.SetTranslation(InventorTool.TranGeo.CreateVector(offset, 0, 0));
                //COs[i].Transformation = otransform;
            }

            Area area = new Area();
            area.Name = "液体储槽地面";
            area.Length = offset + UsMM(par.Offsets[0]);
            area.CreateModule();
            ComponentOccurrence COArea = LoadOccurrence((ComponentDefinition)area.Doc.ComponentDefinition);
            ExtrudeFeature train = GetFeatureproxy<ExtrudeFeature>(COArea, "Area", ObjectTypeEnum.kExtrudeFeatureObject);
            Face TrainEF = InventorTool.GetFirstFromIEnumerator<Face>(train.EndFaces.GetEnumerator());
            List<Face> TrainSF = InventorTool.GetCollectionFromIEnumerator<Face>(train.SideFaces.GetEnumerator());
            Definition.Constraints.AddMateConstraint(SurEF[2], TrainEF,0);
            Definition.Constraints.AddFlushConstraint(plane, TrainSF[1], area.Length/4);
            Definition.Constraints.AddMateConstraint(planeMate, TrainSF[0], -UsMM(par.Offsets[0]/2));
            // ComponentOccurrence COLiquidTank1 = LoadOccurrence((ComponentDefinition)LiquidTank.Doc.ComponentDefinition);


            //tank.CreateModule();
            //ComponentOccurrence COTank = LoadOccurrence((ComponentDefinition)tank.Doc.ComponentDefinition);
            //ObjectCollection objc = InventorTool.CreateObjectCollection();
            //objc.Add(COTank);

            //WorkAxis axis = InventorTool.GetFirstFromIEnumerator<WorkAxis>(tank.Doc.ComponentDefinition.WorkAxes.GetEnumerator());
            //object AxisProxy;
            //COTank.CreateGeometryProxy(axis, out AxisProxy);
            //Definition.OccurrencePatterns.AddRectangularPattern(objc, AxisProxy, true, par.Offset, par.Number);

        }
      
    }
}
