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
    class VacuoInputParam : NotificationObject, ISubComParam
    {
        private double _Se = 0;
        [Category("粗真空抽气系统")]
        [DisplayName("干泵的有效抽速")]
        [Description("干泵的有效抽速,m3/h")]
        public double Se { get { return this._Se; } set { this._Se = value; RaisePropertyChanged(() => Se); } }

        private double _V = 0;
        [Category("粗真空抽气系统")]
        [DisplayName("容器的容积")]
        [Description("容器的容积,m3")]
        public double V { get { return this._V; } set { this._V = value; RaisePropertyChanged(() => V); } }


        private double _P1 = 0;
        [Category("粗真空抽气系统")]
        [DisplayName("开始抽气时的压力")]
        [Description("开始抽气时的压力，Pa")]
        public double P1 { get { return this._P1; } set { this._P1 = value; RaisePropertyChanged(() => P1); } }

        private double _P2 = 0;
        [Category("粗真空抽气系统")]
        [DisplayName("t时间后所达压力")]
        [Description("t时间后所达压力，Pa")]
        public double P2 { get { return this._P2; } set { this._P2 = value; RaisePropertyChanged(() => P2); } }


        private double _K = 0;
        [Category("粗真空抽气系统")]
        [DisplayName("修正系数")]
        [Description("修正系数")]
        public double K { get { return this._K; } set { this._K = value; RaisePropertyChanged(() => K); } }

        private double _A = 0;
        [Category("低温泵有效抽速计算")]
        [DisplayName("管道的截面积")]
        [Description("管道的截面积，cm2")]
        public double A { get { return this._A; } set { this._A = value; RaisePropertyChanged(() => A); } }

        private double _L = 0;
        [Category("低温泵有效抽速计算")]
        [DisplayName("管道的长度")]
        [Description("管道的长度，cm")]
        public double L { get { return this._L; } set { this._L = value; RaisePropertyChanged(() => L); } }

        private double _D = 0;
        [Category("低温泵有效抽速计算")]
        [DisplayName("管道的内径")]
        [Description("管道的内径，cm")]
        public double D { get { return this._D; } set { this._D = value; RaisePropertyChanged(() => D); } }


        private double _Sp = 0;
        [Category("低温泵有效抽速计算")]
        [DisplayName("单泵额定名义抽速")]
        [Description("单泵额定名义抽速，L/s")]
        public double Sp { get { return this._Sp; } set { this._Sp = value; RaisePropertyChanged(() => Sp); } }


        private double _Qt = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("试验物释放的的气体负载")]
        [Description("试验物释放的的气体负载，PaL/s")]
        public double Qt { get { return this._Qt; } set { this._Qt = value; RaisePropertyChanged(() => Qt); } }


        private double _Qe = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("试验设备内表面释放的气体负载")]
        [Description("试验设备内表面释放的气体负载，PaL/s")]
        public double Qe { get { return this._Qe; } set { this._Qe = value; RaisePropertyChanged(() => Qe); } }


        private double _Ql = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("试验设备泄漏产生的气体负载")]
        [Description("试验设备泄漏产生的气体负载，PaL/s")]
        public double Ql { get { return this._Ql; } set { this._Ql = value; RaisePropertyChanged(() => Ql); } }

        private double _P0 = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("真空泵的极限真空")]
        [Description("真空泵的极限真空，Pa")]
        public double P0 { get { return this._P0; } set { this._P0 = value; RaisePropertyChanged(() => P0); } }

        private double _Q0 = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("空载时，经过一定时间抽气后真空室的气体负荷")]
        [Description("空载时，经过一定时间抽气后真空室的气体负荷，PaL/s")]
        public double Q0 { get { return this._Q0; } set { this._Q0 = value; RaisePropertyChanged(() => Q0); } }

        private double _Seff = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("真空室抽气口附近泵的有效抽速")]
        [Description("真空室抽气口附近泵的有效抽速，L/s")]
        public double Seff { get { return this._Seff; } set { this._Seff = value; RaisePropertyChanged(() => Seff); } }

    }
    class VacuoOutputParam: NotificationObject, ISubComParam
    {
        private double _t = 0;
        [Category("粗真空抽气系统")]
        [DisplayName("抽气时间")]
        [Description("抽气时间,h")]
        public double t { get { return this._t; } set { this._t = value; RaisePropertyChanged(() => t); } }

        private double _U = 0;
        [Category("低温泵有效抽速计算")]
        [DisplayName("分子流20°C空气的短管流导")]
        [Description("分子流20°C空气的短管流导，L/s")]
        public double U { get { return this._U; } set { this._U = value; RaisePropertyChanged(() => U); } }

        private double _Se = 0;
        [Category("低温泵有效抽速计算")]
        [DisplayName("单泵在容器口的有效抽速")]
        [Description("单泵在容器口的有效抽速，L/s")]
        public double Se { get { return this._Se; } set { this._Se = value; RaisePropertyChanged(() => Se); } }

        private double _Q = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("系统的气体负载")]
        [Description("系统的气体负载，PaL/s")]
        public double Q { get { return this._Q; } set { this._Q = value; RaisePropertyChanged(() => Q); } }

        private double _Pj = 0;
        [Category("空载极限真空度计算")]
        [DisplayName("真空室所能达到的极限真空")]
        [Description("真空室所能达到的极限真空，Pa")]
        public double Pj     { get { return this._Pj; } set { this._Pj = value; RaisePropertyChanged(() => Pj); } }

    }


    public class VacuoParam : IComParam
    {
        private VacuoInputParam _input;
        private VacuoOutputParam _output;
        public VacuoParam()
        {
            _input = new VacuoInputParam();
            _output = new VacuoOutputParam();
            ComputeCommand = new DelegateCommand(this.computedExecuted);
        }
        private ICommand _computeCommand;
        private void computedExecuted()
        {
            _output.Pj = _input.P0 + _input.Q0 / _input.Seff;
            _output.Q = _input.Qt + _input.Qe + _input.Ql;
            _output.t = 2.3 * _input.V / _input.Se * _input.K * _input.P1 / _input.P2;
            _output.U = 11.6 * _input.A / (1 + 3 * _input.L / 4 / _input.D);
            _output.Se = _input.Sp * _output.U / _input.Sp + _output.U;
        }
        public ICommand ComputeCommand
        {
            get
            {
                return this._computeCommand;
            }

            set
            {
                _computeCommand = value;
            }
        }

        private string _DocPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview/真空系统计算.xps");

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
                return this._input;
            }

            set
            {
                this._input = value as VacuoInputParam;
            }
        }

        public ISubComParam outputs
        {
            get
            {
                return this._output;
            }

            set
            {
                this._output = value as VacuoOutputParam;
            }
        }
    }
}
