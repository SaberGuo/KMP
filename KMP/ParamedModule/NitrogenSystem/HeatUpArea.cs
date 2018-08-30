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
    /// 加热区
    /// </summary>
    [Export("HeatUpArea", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HeatUpArea : AssembleModuleBase
    {
        ParHeatUpArea par = new ParHeatUpArea();
        Compressor _compressor;
        ElectricHeater _heater;
        List<ComponentOccurrence> COs = new List<ComponentOccurrence>();
        public HeatUpArea():base()
        {
            this.Parameter = par;
            par.CompressorNum = 2;
            par.ElectricHeaterNum = 3;
            _compressor = new Compressor();
            _heater = new ElectricHeater();
            this.Name = "室内氮气闭式回温模块";
            this.SubParamedModules.AddModule(_compressor);
            this.SubParamedModules.AddModule(_heater);
        }
        public override void InitModule()
        {
            this.Parameter = par;
            this.SubParamedModules.AddModule(_compressor);
            this.SubParamedModules.AddModule(_heater);
            base.InitModule();
        }
        public override bool CheckParamete()
        {
           if(_compressor.CheckParamete()&&_heater.CheckParamete())
            {
                return true;
            }
            return false;
        }

        public override void CreateSub()
        {
            _compressor.CreateModule();
            _heater.CreateModule();
            double   offset = 0;
            Matrix otransform = InventorTool.TranGeo.CreateMatrix();

            otransform.SetToRotateTo(InventorTool.TranGeo.CreateVector(0, 1, 0), InventorTool.TranGeo.CreateVector(0, 0, 1));
            for (int i = 0; i < par.CompressorNum; i++)
            {
                ComponentOccurrence CO = LoadOccurrence((ComponentDefinition)_compressor.Doc.ComponentDefinition, otransform);
                COs.Add(CO);
               // Matrix otransform = COs[i].Transformation;
               //\
               // otransform.SetTranslation(InventorTool.TranGeo.CreateVector(0, 1, 1),true);
               // COs[i].Transformation = otransform;
                
            }
            for (int i = 0; i < par.ElectricHeaterNum; i++)
            {
                ComponentOccurrence CO = LoadOccurrence((ComponentDefinition)_heater.Doc.ComponentDefinition);
                COs.Add(CO);
            }
            ExtrudeFeature sur = GetFeatureproxy<ExtrudeFeature>(COs[0], "Box", ObjectTypeEnum.kExtrudeFeatureObject);
            List<Face> boxSF = InventorTool.GetCollectionFromIEnumerator<Face>(sur.SideFaces.GetEnumerator());
            Face boxEF = InventorTool.GetFirstFromIEnumerator<Face>(sur.EndFaces.GetEnumerator());
            WorkAxis Axis0= GetAxis(COs[0], "Axis");
            WorkPlane plane0 = GetPlane(COs[0], "Flush");
            for (int i=1;i<COs.Count;i++)
            {
                offset += par.Offsets[i - 1];
                if(i<par.CompressorNum)
                {
                    ExtrudeFeature suri = GetFeatureproxy<ExtrudeFeature>(COs[i], "Box", ObjectTypeEnum.kExtrudeFeatureObject);
                    List<Face> SurEFi = InventorTool.GetCollectionFromIEnumerator<Face>(suri.SideFaces.GetEnumerator());
                    Definition.Constraints.AddFlushConstraint(boxSF[1], SurEFi[1], 0);
                }
                else
                {
                    ExtrudeFeature suri = GetFeatureproxy<ExtrudeFeature>(COs[i], "Sur", ObjectTypeEnum.kExtrudeFeatureObject);
                    List<Face> SurEFi = InventorTool.GetCollectionFromIEnumerator<Face>(suri.SideFaces.GetEnumerator());
                    Definition.Constraints.AddFlushConstraint(boxSF[1], SurEFi[2], 0);
                }
                WorkPlane planei = GetPlane(COs[i], "Flush");
                Definition.Constraints.AddFlushConstraint(plane0, planei, 0);
                WorkAxis Axis = GetAxis(COs[i], "Axis");
                Definition.Constraints.AddMateConstraint(Axis0, Axis, UsMM(offset));
            }


            Area area = new Area();
            area.Name = "回温模块地面";
            area.Length = UsMM(offset) + UsMM(par.Offsets[0]*2);
            area.CreateModule();
            ComponentOccurrence COArea = LoadOccurrence((ComponentDefinition)area.Doc.ComponentDefinition);
            ExtrudeFeature train = GetFeatureproxy<ExtrudeFeature>(COArea, "Area", ObjectTypeEnum.kExtrudeFeatureObject);
            Face TrainEF = InventorTool.GetFirstFromIEnumerator<Face>(train.EndFaces.GetEnumerator());
            List<Face> TrainSF = InventorTool.GetCollectionFromIEnumerator<Face>(train.SideFaces.GetEnumerator());
            Definition.Constraints.AddMateConstraint(boxSF[1], TrainEF, 0);
         
            Definition.Constraints.AddMateConstraint(plane0, TrainSF[1], -area.Length / 4);
            Definition.Constraints.AddMateConstraint(Axis0, TrainSF[0], -UsMM(par.Offsets[0]));
        }
    }
}
