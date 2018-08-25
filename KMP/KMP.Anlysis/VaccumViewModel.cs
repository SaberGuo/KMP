using Infranstructure.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Anlysis
{
    public class VaccumViewModel: NotificationObject
    {
        private bool _isPressed = false;
        public bool isPressed
        {
            get { return this._isPressed; }
            set
            {
                this._isPressed = value;
                this.RaisePropertyChanged(() => this.isPressed);
            }
        }
        private VacuumParam _parameters = new VacuumParam();
        public VacuumParam Parameters
        {
            get { return _parameters; }
            set { this._parameters = value; }
        }

        public VaccumViewModel()
        {
            this.AnalysisCommand = new DelegateCommand(AnalysisExecuted);
            this.Analysis2Command = new DelegateCommand(AnalysisPart2Executed);
        }

        public DelegateCommand AnalysisCommand { get; set; }
        public DelegateCommand Analysis2Command { get; set; }

        private void AnalysisExecuted()
        {
            //Sp
            double t = (this.Parameters.PressS - this.Parameters.PreUltimatePress) / (this.Parameters.PressT - Parameters.PreUltimatePress);
            double PrePumpingSpeed = 2.3 * this.Parameters.Kq * this.Parameters.Volume / this.Parameters.PumpingTime * Math.Log((t));
            //U
            double PipelineConductance = 1.34 * 1000 * Math.Pow(this.Parameters.PipeDiameter, 4) / this.Parameters.PipeLength * this.Parameters.AvgPress;
            //S
            double PreAvalPumpingSpeed = this.Parameters.PrePumpingSpeed * this.Parameters.PipelineConductance / (this.Parameters.PrePumpingSpeed + this.Parameters.PipelineConductance);
            this.Parameters.ParametersAnalysed(PrePumpingSpeed, PipelineConductance, PreAvalPumpingSpeed);
        }

        private void AnalysisPart2Executed()
        {
            //Q0
            this.Parameters.Q0 = this.Parameters.Q1 + this.Parameters.Q2 + this.Parameters.Q3;
            //Q
            this.Parameters.Q = this.Parameters.Qt + this.Parameters.Q0;
            //Sp
            if (isPressed)
            {
                this.Parameters.Sp = this.Parameters.Q / (this.Parameters.Pg - this.Parameters.P0);
            }
            else
            {
                this.Parameters.Sp = this.Parameters.Q0 / (this.Parameters.Pj - this.Parameters.P0);
            }
            //Sm
            this.Parameters.Sm = this.Parameters.Sp * this.Parameters.U / (this.Parameters.U - this.Parameters.Sp);
            

        }
    }
}
