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
        [ImportingConstructor]
        public ProjectSystem():base()
        {
            this.ProjectType = "ProjectSystem";

        }
        public override void InitModule()
        {
            if(_warehouse != null)
            {
                this.SubParamedModules.Add(_warehouse);
            }

            if(_nitrogen != null)
            {
                this.SubParamedModules.Add(_nitrogen);
            }

            if(_vacuoSystem != null)
            {
                this.SubParamedModules.Add(_vacuoSystem);
            }

            if(_container != null)
            {
                this.SubParamedModules.Add(_container);
            }

            if(_heatSink != null)
            {
                this.SubParamedModules.Add(_heatSink);
            }

        }
        public void InitProject(List<string> ProjectTypes)
        {
            if (ProjectTypes.Contains("Nitrogen"))
            {
                _nitrogen = new Nitrogen();
                _nitrogen.ModelPath = this.ModelPath;
                this.SubParamedModules.Add(_nitrogen);
            }

            if (ProjectTypes.Contains("VacuoSystem"))
            {
                _vacuoSystem = new VacuoSystem();
                _vacuoSystem.ModelPath = this.ModelPath;
                this.SubParamedModules.Add(_vacuoSystem);
            }

            if (ProjectTypes.Contains("ContainerSystem")&& ProjectTypes.Contains("HeaterSystem"))
            {
                _warehouse = new WareHouseEnvironment();
                _warehouse.ModelPath = this.ModelPath;
                this.SubParamedModules.Add(_warehouse);
            }
            else
            {
                if (ProjectTypes.Contains("ContainerSystem"))
                {
                    _container = new ContainerSystem();
                    _container.ModelPath = this.ModelPath;
                    this.SubParamedModules.Add(_container);
                }
                if (ProjectTypes.Contains("HeaterSystem"))
                {
                    _heatSink = new HeatSink();
                    _heatSink.ModelPath = this.ModelPath;
                    this.SubParamedModules.Add(_heatSink);
                }
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
