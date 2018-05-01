using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
namespace ParamedModule
{
  public  class Cylinder:ParamedModuleBase
    {
        ParCylinder parCylinder=new ParCylinder();
        public Cylinder():base()
        {
            this.Parameter = parCylinder;
        }

        public override void CreateModule(ParameterBase Parameter)
        {
            
        }
    }
}
