using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 泵区
    /// </summary>
    [Export("PumpArea", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PumpArea : AssembleModuleBase
    {
        ParPumpArea par = new ParPumpArea();
        List<ComponentOccurrence> PumpCOs = new List<ComponentOccurrence>();
        List<ComponentOccurrence> SurCOs = new List<ComponentOccurrence>();
        public PumpArea():base()
        {
            init();
        }
        void init()
        {
            par.PumpNum = 6;
            par.SubCoolerNum = 2;
            par.Distance = 2000;
            this.Name = "泵区";
            this.Parameter = par;
        }
        public override bool CheckParamete()
        {
            return true;
        }

        public override void CreateSub()
        {
            double offset=0;
          for(int i=0;i<par.SubCoolerNum;i++)
            {
                ComponentOccurrence occ = LoadOccurrence("过冷器.ipt",oPos);
                SurCOs.Add(occ);
            }

            oPos.SetTranslation(InventorTool.TranGeo.CreateVector(UsMM(par.Distance), 0, 0), true);
           // COs[i].Transformation = otransform;
            for (int i = 0; i < par.PumpNum; i++)
            {
                ComponentOccurrence occ = LoadOccurrence("液压泵.ipt",oPos);
                PumpCOs.Add(occ);
            }

          ExtrudeFeature sur0= GetFeatureproxy<ExtrudeFeature>(SurCOs[0], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
            List<Face> SurEF0 = InventorTool.GetCollectionFromIEnumerator<Face>(sur0.SideFaces.GetEnumerator());
            WorkPlane plane0 = GetPlane(SurCOs[0], "Flush");
            WorkPlane SubOffset = GetPlane(SurCOs[0], "Mate");
           // WorkAxis Axis0 = GetAxis(SurCOs[0], "Axis");
            for (int i = 1; i < par.SubCoolerNum; i++)
            {
                ExtrudeFeature suri = GetFeatureproxy<ExtrudeFeature>(SurCOs[i], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
                List<Face> SurEFi = InventorTool.GetCollectionFromIEnumerator<Face>(suri.SideFaces.GetEnumerator());
                Definition.Constraints.AddFlushConstraint(SurEF0[2], SurEFi[2], 0);
                offset += UsMM(par.SubCoolerOffsets[i-1]);
                WorkPlane planei = GetPlane(SurCOs[i], "Flush");
               // WorkPlane planeMate = GetPlane(SurCOs[i], "Mate");
                Definition.Constraints.AddFlushConstraint(plane0, planei, 0);
                WorkPlane SubOffseti = GetPlane(SurCOs[i], "Mate");
                Definition.Constraints.AddFlushConstraint(SubOffset, SubOffseti, offset);
            }
            offset = 0;

            ExtrudeFeature PumpSur = GetFeatureproxy<ExtrudeFeature>(PumpCOs[0], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
            Face pumpEF0 = InventorTool.GetFirstFromIEnumerator<Face>(PumpSur.EndFaces.GetEnumerator());
            List<Face> pumpSF0 = InventorTool.GetCollectionFromIEnumerator<Face>(PumpSur.SideFaces.GetEnumerator());
            Definition.Constraints.AddFlushConstraint(SurEF0[2], pumpEF0, 0);
            Definition.Constraints.AddFlushConstraint(plane0, pumpSF0[3], -UsMM(par.Distance));
            Definition.Constraints.AddFlushConstraint(SubOffset, pumpSF0[2], -100);
            for (int i = 1; i < par.PumpNum; i++)
            {
                offset += UsMM(par.PumpOffsets[i - 1]);
                ExtrudeFeature suri = GetFeatureproxy<ExtrudeFeature>(PumpCOs[i], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
                Face pumpEFi = InventorTool.GetFirstFromIEnumerator<Face>(suri.EndFaces.GetEnumerator());
                List<Face> pumpSFi = InventorTool.GetCollectionFromIEnumerator<Face>(suri.SideFaces.GetEnumerator());
                Definition.Constraints.AddFlushConstraint(pumpEF0, pumpEFi, 0);
                Definition.Constraints.AddFlushConstraint(pumpSF0[1], pumpSFi[1], 0);
                Definition.Constraints.AddFlushConstraint(pumpSF0[2], pumpSFi[2], offset);
            }
            #region 将部件另存为
            PartDocument pump = ((PartComponentDefinition)PumpCOs[0].Definition).Document;
            string FullName = System.IO.Path.Combine(ModelPath, "液压泵.ipt");
            if (System.IO.File.Exists(FullName))
            {
                System.IO.File.Delete(FullName);
            }
            pump.SaveAs(FullName,false);

            PartDocument subCool = ((PartComponentDefinition)PumpCOs[0].Definition).Document;
             FullName = System.IO.Path.Combine(ModelPath, "过冷器.ipt");
            if (System.IO.File.Exists(FullName))
            {
                System.IO.File.Delete(FullName);
            }
            subCool.SaveAs(FullName, false);
            #endregion

            Area area = new Area();
            area.Name = "泵区地面";
            double length1=0, length2=0;
            par.PumpOffsets.ToList().ForEach(a => length1 += a);
            par.SubCoolerOffsets.ToList().ForEach(a => length2 += a);
            if(length1>length2)
            {
                area.Length = UsMM(length1+2000);
            }
            else
            {
                area.Length = UsMM(length2+2000);
            }
            area.width = UsMM(par.Distance + 3000);
            area.CreateModule();
            
            ComponentOccurrence COArea = LoadOccurrence((ComponentDefinition)area.Doc.ComponentDefinition);
            ExtrudeFeature train = GetFeatureproxy<ExtrudeFeature>(COArea, "Area", ObjectTypeEnum.kExtrudeFeatureObject);
            Face TrainEF = InventorTool.GetFirstFromIEnumerator<Face>(train.EndFaces.GetEnumerator());
            List<Face> TrainSF = InventorTool.GetCollectionFromIEnumerator<Face>(train.SideFaces.GetEnumerator());
            Definition.Constraints.AddMateConstraint(SurEF0[2], TrainEF, 0);
            Definition.Constraints.AddFlushConstraint(plane0, TrainSF[1], area.width/2);
            Definition.Constraints.AddMateConstraint(SubOffset, TrainSF[0], -UsMM(par.SubCoolerOffsets[0] / 2));
        }
    }
}
