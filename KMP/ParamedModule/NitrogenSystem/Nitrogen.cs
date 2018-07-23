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
    /// 氮系统
    /// </summary>
    [Export("Nitrogen", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Nitrogen : AssembleModuleBase
    {
        ParNitrogen par = new ParNitrogen();
        CryoLiquidTanks tanks = new CryoLiquidTanks();
        HeatUpArea heatUps = new HeatUpArea();
        PumpArea pumps = new PumpArea();
        public Nitrogen():base()
        {
            this.Parameter = par;
            this.SubParamedModules.Add(tanks);
            this.SubParamedModules.Add(heatUps);
            this.SubParamedModules.Add(pumps);
        }
        public override bool CheckParamete()
        {
            if(tanks.CheckParamete()&&heatUps.CheckParamete()&&pumps.CheckParamete())
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
            DisPose();
            if (!CheckParamete()) return;
            tanks.CreateModule();
            heatUps.CreateModule();
            pumps.CreateModule();
            GeneratorProgress(this, "完成创建部件" + this.Name);
        }
        public override void CreateSub()
        {
            throw new NotImplementedException();
        }
    }
}
