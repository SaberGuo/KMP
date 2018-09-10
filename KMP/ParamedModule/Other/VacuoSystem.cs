using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Other;
using KMP.Interface.Model.NitrogenSystem;
namespace ParamedModule.Other
{
    /// <summary>
    /// 真空系统
    /// </summary>
    [Export("VacuoSystem", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class VacuoSystem: ParamedModuleBase
    {
        ParVocuoSystem par = new ParVocuoSystem();
        CoolVAC _Cool= new CoolVAC();
        CoolVAC1 _Cool1 = new CoolVAC1();
        DRYVAC _Dry = new DRYVAC();
        GXS _gxs = new GXS();
        MolecularPump _Molecular = new MolecularPump();
        ScrewLine _screwLine = new ScrewLine();
        Valve _valve = new Valve();
        public VacuoSystem():base()
        {
            this.Name = "真空系统";

            InitModule();
         
        }
        public override void InitModule()
        {
            this.Parameter = par;
            this.SubParamedModules.AddModule(_Cool);
            this.SubParamedModules.AddModule(_Cool1);
            this.SubParamedModules.AddModule(_Dry);
            this.SubParamedModules.Add(_gxs);
            this.SubParamedModules.AddModule(_Molecular);
            this.SubParamedModules.AddModule(_screwLine);
            this.SubParamedModules.AddModule(_valve);
          
            base.InitModule();
        }

        public override bool CheckParamete()
        {
            if(_Cool.CheckParamete()&&_Dry.CheckParamete()&&_Molecular.CheckParamete()&&_screwLine.CheckParamete()&&_valve.CheckParamete())
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public override void CreateModule()
        {
            GeneratorProgress(this, "开始创建部件" + this.Name);
          
            if (!CheckParamete()) return;
            _Cool.CreateModule();
            _Cool1.CreateModule();
            _Dry.CreateModule();
            _gxs.CreateModule();
            _Molecular.CreateModule();
            _screwLine.CreateModule();
            _valve.CreateModule();
            GeneratorProgress(this, "完成创建部件" + this.Name);
        }

        //public override void CreateSub()
        //{
        
        //}

        internal override void CloseSameNameDocment()
        {
            throw new NotImplementedException();
        }
    }
}
