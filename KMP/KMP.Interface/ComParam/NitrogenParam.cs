using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace KMP.Interface.ComParam
{
    class NitrogenInputParam : NotificationObject, ISubComParam
    {
        private double _Q = 50;
        [Category("系统循环流量")]
        [DisplayName("液氮系统热负荷")]
        [Description("液氮系统热负荷,kw")]
        public double Q
        {
            get
            {
                return this._Q;
            }
            set
            {
                this._Q = value;
                RaisePropertyChanged(() => Q);
            }
        }

        private double _cp = 2.1;
        [Category("系统循环流量")]
        [DisplayName("液氮的定压比热")]
        [Description("液氮的定压比热,kj/kg*k")]
        public double cp
        {
            get
            {
                return this._cp;
            }
            set
            {
                this._cp = value;
                RaisePropertyChanged(() => cp);
            }
        }

        private double _rou = 772;
        [Category("系统循环流量")]
        [DisplayName("液氮的密度")]
        [Description("液氮的密度,kg/m3")]
        public double rou
        {
            get
            {
                return this._rou;
            }
            set
            {
                this._rou = value;
                RaisePropertyChanged(() => rou);
            }
        }

        private double _deltaT = 772;
        [Category("系统循环流量")]
        [DisplayName("液氮过冷度")]
        [Description("液氮过冷度,K")]
        public double deltaT
        {
            get
            {
                return this._deltaT;
            }
            set
            {
                this._deltaT = value;
                RaisePropertyChanged(() => deltaT);
            }
        }

        private double _u = 2;
        [Category("系统主管道直径")]
        [DisplayName("液氮的流速")]
        [Description("液氮的流速,m/s")]
        public double u
        {
            get
            {
                return this._u;
            }
            set
            {
                this._u = value;
                RaisePropertyChanged(() => u);
            }
        }

        private double _Q1 = 25;
        [Category("液氮耗量计算-正常运行")]
        [DisplayName("设备运行期间热负荷")]
        [Description("设备运行期间热负荷,kw")]
        public double Q1
        {
            get
            {
                return this._Q1;
            }
            set
            {
                this._Q1 = value;
                RaisePropertyChanged(() => Q1);
            }
        }


    }
    class NitrogenOutputParam : NotificationObject, ISubComParam
    {
        private double _V = 0;
        [Category("系统循环流量")]
        [DisplayName("液氮的体积流量")]
        [Description("液氮的体积流量,m3/h")]
        public double V
        {
            get
            {
                return this._V;
            }
            set
            {
                this._V = value;
                RaisePropertyChanged(() => V);
            }
        }

        private double _D = 0;
        [Category("系统主管道直径")]
        [DisplayName("液氮循环管道直径")]
        [Description("液氮循环管道直径,m")]
        public double D
        {
            get
            {
                return this._D;
            }
            set
            {
                this._D = value;
                RaisePropertyChanged(() => D);
            }
        }
    }
    public class NitrogenParam : IComParam
    {
        private ICommand _computeCommand;
        private NitrogenInputParam _input;
        private NitrogenOutputParam _output;

        public NitrogenParam()
        {
            _computeCommand = new DelegateCommand(this.computeExecuted);
            _input = new NitrogenInputParam();
            _output = new NitrogenOutputParam();
        }

        private void computeExecuted()
        {
            _output.V = _input.Q / (_input.cp * _input.rou * _input.deltaT);
            _output.D = Math.Sqrt(_output.V / 3600 * 4 / (Math.PI * _input.u));
        }
        public ICommand ComputeCommand
        {
            get
            {
                return _computeCommand;
            }

            set
            {
                this._computeCommand = value;
            }
        }
        private string _DocPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview/氮系统计算.xps");

        public string DocPath
        {
            get
            {
                return _DocPath;
            }
        }

        public ISubComParam inputs
        {
            get
            {
                return _input;
            }

            set
            {
                _input = value as NitrogenInputParam;
            }
        }

        public ISubComParam outputs
        {
            get
            {
                return _output;
            }

            set
            {
                _output = value as NitrogenOutputParam;
            }
        }
    }
}
