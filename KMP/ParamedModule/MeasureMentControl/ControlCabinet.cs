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

namespace ParamedModule.MeasureMentControl
{
    [Export("ControlCabinet", typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ControlCabinet : PartModulebase
    {
      public  ParControlCabinet par = new ParControlCabinet();
        public ControlCabinet():base()
        {
            this.Parameter = par;
            par.Height = 2200;
            par.Width = 1000;
            par.Length = 800;
            this.Name = "控制柜";
        }
        public override bool CheckParamete()
        {
            if (par.Height <= 0 || par.Width <= 0 || par.Length <= 0)
            {
                ParErrorChanged(this, "控制柜参数设置值小于等于零");
                return false;
            }
            return true;
        }

        public override void CreateSub()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            ExtrudeFeature box = InventorTool.CreateBox(Definition, osketch, UsMM(par.Length), UsMM(par.Width), UsMM(par.Height));
            box.Name = "ControlCabinet";
          
        }
    }
}
