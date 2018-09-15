using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infranstructure.Tool;
using Inventor;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Interface.Model.Container;
using ParamedModule.NitrogenSystem;
using ParamedModule.Other;
using ParamedModule.Container;
using ParamedModule.HeatSinkSystem;
using ParamedModule.MeasureMentControl;
namespace ParamedModule
{
    [Export("ProjectSystem", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectSystem : ParamedModuleBase
    {
        public WareHouseEnvironment _warehouse;
        public Nitrogen _nitrogen;
        public VacuoSystem _vacuoSystem;
        public ContainerSystem _container;
        public HeatSink _heatSink;
        public Cabinets _Cabinets;
        [ImportingConstructor]
        public ProjectSystem():base()
        {
            this.ProjectType = "ProjectSystem";

        }
        public override void InitModule()
        {
            if(_warehouse != null)
            {
                this.SubParamedModules.AddModule(_warehouse);
            }

            if(_nitrogen != null)
            {
                this.SubParamedModules.AddModule(_nitrogen);
            }

            if(_vacuoSystem != null)
            {
                this.SubParamedModules.AddModule(_vacuoSystem);
            }

            if(_container != null)
            {
                this.SubParamedModules.AddModule(_container);
            }

            if(_heatSink != null)
            {
                this.SubParamedModules.AddModule(_heatSink);
            }

            base.InitModule();

        }
        public void InitProject(List<string> ProjectTypes)
        {
          

            if (ProjectTypes.Contains("ContainerSystem")&& ProjectTypes.Contains("HeaterSystem"))
            {
                _warehouse = new WareHouseEnvironment();
                _warehouse.ModelPath = this.ModelPath;
                this.SubParamedModules.AddModule(_warehouse);
            }
            else
            {
                if (ProjectTypes.Contains("ContainerSystem"))
                {
                    _container = new ContainerSystem();
                    _container.ModelPath = this.ModelPath;
                    this.SubParamedModules.AddModule(_container);
                }
                if (ProjectTypes.Contains("HeaterSystem"))
                {
                    _heatSink = new HeatSink();
                    _heatSink.ModelPath = this.ModelPath;
                    this.SubParamedModules.AddModule(_heatSink);
                }
            }
            if (ProjectTypes.Contains("Nitrogen"))
            {
                _nitrogen = new Nitrogen();
                _nitrogen.ModelPath = this.ModelPath;
                this.SubParamedModules.AddModule(_nitrogen);
            }

            if (ProjectTypes.Contains("VacuoSystem"))
            {
                _vacuoSystem = new VacuoSystem();
                _vacuoSystem.ModelPath = this.ModelPath;
                this.SubParamedModules.AddModule(_vacuoSystem);
            }
            if (ProjectTypes.Contains("Cabinets"))
            {
                _Cabinets = new Cabinets();
                _Cabinets.ModelPath = this.ModelPath;
                this.SubParamedModules.AddModule(_Cabinets);
            }
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule()
        {
            foreach (var item in this.SubParamedModules)
            {
                item.CreateModule();
            }
        }

        internal override void CloseSameNameDocment()
        {
            throw new NotImplementedException();
        }
    }
}
