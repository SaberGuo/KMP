using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.NitrogenSystem;
using KMP.Interface.ComParam;

namespace ParamedModule.NitrogenSystem
{
    /// <summary>
    /// 氮系统
    /// </summary>
    [Export("Nitrogen", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Nitrogen :ParamedModuleBase
    {
        ParNitrogen _par = new ParNitrogen();
        public ParNitrogen par
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
        CryoLiquidTanks tanks = new CryoLiquidTanks();
        HeatUpArea heatUps = new HeatUpArea();
        PumpArea pumps = new PumpArea();
        public Nitrogen():base()
        {
            
        }
        public override void InitCreatedModule()
        {
            this.Name = "低温系统";
            this.Parameter = par;
            this.SubParamedModules.AddModule(tanks);
            this.SubParamedModules.AddModule(heatUps);
            this.SubParamedModules.AddModule(pumps);
            base.InitCreatedModule();
        }
        private NitrogenParam _cPar = new NitrogenParam();
        public NitrogenParam cPar
        {
            get
            {
                return this._cPar;
            }
        }
        public override void InitModule()
        {
            this.Parameter = par;
            this.SubParamedModules.AddModule(tanks);
            this.SubParamedModules.AddModule(heatUps);
            this.SubParamedModules.AddModule(pumps);
            base.InitModule();
        }
        public override bool CheckParamete()
        {
            foreach (var item in SubParamedModules)
            {
                if(item.CheckParamete() == false)
                {
                    return false;
                }
            }
            return true;
            /*if(tanks.CheckParamete()&&heatUps.CheckParamete()&&pumps.CheckParamete())
            {
                return true;
            }
            else
            {
                return false;
            }*/
        }
        public override void CreateModule()
        {
            GeneratorProgress(this, "开始创建部件" + this.Name);
          
            if (!CheckParamete()) return;
            tanks.CreateModule();
            heatUps.CreateModule();
            pumps.CreateModule();
            GeneratorProgress(this, "完成创建部件" + this.Name);
        }
      
        internal override void CloseSameNameDocment()
        {
           
        }
        //public override void CreateSub()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
