using Infranstructure.Models;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Anlysis
{
    public class VaccumViewModel
    {
        private VacuumParam _parameters = new VacuumParam();
        public VacuumParam Parameters
        {
            get { return _parameters; }
            set { this._parameters = value; }
        }

        public VaccumViewModel()
        {
            this.AnalysisCommand = new DelegateCommand(AnalysisExecuted);
        }

        public DelegateCommand AnalysisCommand { get; set; }

        private void AnalysisExecuted()
        {
            //Sp
            double t = (this.Parameters.PressS - this.Parameters.PreUltimatePress) / (this.Parameters.PressT - Parameters.PreUltimatePress);
            this.Parameters.PrePumpingSpeed = 2.3 * this.Parameters.Kq * this.Parameters.Volume / this.Parameters.PumpingTime * Math.Log((t));
            //U
            this.Parameters.PipelineConductance = 1.34 * 1000 * Math.Pow(this.Parameters.PipeDiameter, 4) / this.Parameters.PipeLength * this.Parameters.AvgPress;
            //S
            this.Parameters.PreAvalPumpingSpeed = this.Parameters.PrePumpingSpeed * this.Parameters.PipelineConductance / (this.Parameters.PrePumpingSpeed + this.Parameters.PipelineConductance);
        }
    }
}
