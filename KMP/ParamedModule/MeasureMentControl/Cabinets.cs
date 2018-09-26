using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParamedModule;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.MeasureMentControl;
using System.Collections.ObjectModel;
using KMP.Interface.Model;

namespace ParamedModule.MeasureMentControl
{
    [Export("Cabinets", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Cabinets : AssembleModuleBase
    {
        ParCabinets _par = new ParCabinets();
        public ParCabinets par
        {
            get
            {
                return this._par;
            }
            set
            {
                this._par = value;
            }
        }
        ControlCabinet _cabinet = new ControlCabinet();
        public Cabinets():base()
        {
            this.Parameter = par;
            par.Num = 3;
            par.Distance = 1200;
            this.Name = "测控系统";
            this.SubParamedModules.AddModule(_cabinet);
        }
        List<OccStruct> COs = new List<OccStruct>();
        public override void InitModule()
        {
            this.Parameter = par;
            this.SubParamedModules.AddModule(_cabinet);
            this.Name = "测控系统";
            base.InitModule();
        }
        public override bool CheckParamete()
        {
            if (_cabinet.CheckParamete() == false) return false;
            if (par.Num < 1||par.Distance<=0)
            {
                ParErrorChanged(this, "控制柜数量或间距小于零");
                return false;
            }
            return true;
        }

        public override void CreateSub()
        {
            _cabinet.CreateModule();
            for (int i = 0; i < par.Num; i++)
            {
                ComponentOccurrence CO = LoadOccurrence((ComponentDefinition)_cabinet.Doc.ComponentDefinition);
                OccStruct OccPlane1 = GetOccStruct(CO, "ControlCabinet", 0);
                COs.Add(OccPlane1);
            }
            for (int i = 1; i < par.Num; i++)
            {
                Definition.Constraints.AddFlushConstraint(COs[i].StartFace, COs[0].StartFace,0);

                Definition.Constraints.AddFlushConstraint(COs[i].SideFaces[0], COs[0].SideFaces[0], 0);
                Definition.Constraints.AddFlushConstraint(COs[i].SideFaces[1], COs[i - 1].SideFaces[1], UsMM(par.Distance));
            }

            Area area = new Area();
            area.Name = "测控系统地面";
            area.ModelPath = this.ModelPath;
            area.Length = UsMM((par.Num+1)*par.Distance);
            area.CreateModule();
            ComponentOccurrence COArea = LoadOccurrence((ComponentDefinition)area.Doc.ComponentDefinition);
            OccStruct train = GetOccStruct(COArea, "Area", 0);
            Definition.Constraints.AddMateConstraint(COs[0].StartFace, train.EndFace, 0);
            Definition.Constraints.AddFlushConstraint(COs[0].SideFaces[0], train.SideFaces[1], area.Length / 4 - UsMM(_cabinet.par.Length/2));
            Definition.Constraints.AddFlushConstraint(COs[0].SideFaces[1], train.SideFaces[2], UsMM(par.Distance / 2));
       
        }
        public override void DisPose()
        {
            COs.Clear();
            base.DisPose();
        }
    }

}
