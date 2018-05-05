using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using KMP.Interface.Model;
using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架侧板
    /// </summary>
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
  public  class RailSupportSidePlate : PartModulebase
    {
        ParRailSupportSidePlate parSidePlate = new ParRailSupportSidePlate();
        public RailSupportSidePlate():base()
        {
            this.Parameter = parSidePlate;
            init();
        }
        void init()
        {
            parSidePlate.Width = 100;
            parSidePlate.Thickness = 10;
            parSidePlate.Length = 80;
        }
        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            throw new NotImplementedException();
        }
    }
}
