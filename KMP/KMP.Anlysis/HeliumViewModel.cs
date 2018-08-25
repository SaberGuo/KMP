using Infranstructure.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Anlysis
{
    public class HeliumViewModel : NotificationObject
    {

        private HeliumParam _parameters = new HeliumParam();
        public HeliumParam Parameters
        {
            get { return _parameters; }
            set { this._parameters = value; }
        }

        public HeliumViewModel()
        {
            Analysis1Command = new DelegateCommand(analysis1Executed);
            Analysis2Command = new DelegateCommand(analysis2Executed);
            Analysis3Command = new DelegateCommand(analysis3Executed);
        }
        public DelegateCommand Analysis1Command { get; set; }
        public DelegateCommand Analysis2Command { get; set; }
        public DelegateCommand Analysis3Command { get; set; }

        private void analysis1Executed()
        {
            this.Parameters.V = this.Parameters.Q / (this.Parameters.cp*this.Parameters.rou*this.Parameters.deltaT) * 3600;
        }

        private void analysis2Executed()
        {
            this.Parameters.D = Math.Sqrt(4 * this.Parameters.V / Math.PI / this.Parameters.u);
        }

        private void analysis3Executed()
        {
            this.Parameters.deltaP = this.Parameters.deltaP1 + this.Parameters.deltaP2 + this.Parameters.deltaP3;
        }
    }
}
