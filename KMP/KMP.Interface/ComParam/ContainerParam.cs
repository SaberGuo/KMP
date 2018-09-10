using KMP.Interface.Tools;
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
    [DisplayName("参数输入")]
    class ContainerInputParam: NotificationObject, ISubComParam
    {
        private double _OuterDiameter = 3428;
        [Category("筒体壁厚计算")]
        [DisplayName("圆筒外直径D")]
        [Description("圆筒外直径，mm")]
        public double OuterDiameter { get { return this._OuterDiameter; } set { this._OuterDiameter = value; this.RaisePropertyChanged(()=> OuterDiameter); } }


        private double _deltaE1 = 13;
        [Category("筒体壁厚计算")]
        [DisplayName("圆筒的有效厚度delta_e1")]
        [Description("圆筒的有效厚度，mm")]
        public double deltaE1 { get { return this._deltaE1; } set { this._deltaE1 = value; this.RaisePropertyChanged(()=> deltaE1); } }


        private double _L = 1900;
        [Category("筒体壁厚计算")]
        [DisplayName("筒体计算长度L")]
        [Description("筒体计算长度，mm")]
        public double L { get { return _L; } set { this._L = value; this.RaisePropertyChanged(()=>L ); } }


        private double _OuterRadius = 1713;
        [Category("容器封头壁厚计算")]
        [DisplayName("椭圆封头当量球壳外半径R")]
        [Description("椭圆封头当量球壳外半径，mm")]
        public double OuterRadius { get { return this._OuterRadius; } set { this._OuterRadius = value; this.RaisePropertyChanged(() => OuterRadius); } }


        private double _deltaE2 = 13;
        [Category("容器封头壁厚计算")]
        [DisplayName("椭圆封头有效厚度delta_e2")]
        [Description("椭圆封头有效厚度，mm")]
        public double deltaE2 { get { return this._deltaE2; } set {this._deltaE2 = value;RaisePropertyChanged(()=>deltaE2); } }
    }
    [DisplayName("参数输出")]
    class ContainerOutputParam: NotificationObject, ISubComParam
    {
        private double _A1 = 0;
        [Category("筒体壁厚计算")]
        [DisplayName("系数A")]
        public double A1 { get {return this._A1; } set {this._A1 = value;RaisePropertyChanged(()=>A1); } }

        private double _B1 = 0;
        [Category("筒体壁厚计算")]
        [DisplayName("系数B")]
        public double B1 { get { return this._B1; } set { this._B1 = value; RaisePropertyChanged(()=>B1); } }

        private double _P1 = 0;
        [Category("筒体壁厚计算")]
        [DisplayName("设计许用应力P")]
        [Description("设计许用应力P，MPa")]
        public double P1 { get { return this._P1; } set { this._P1 = value; RaisePropertyChanged(()=>P1); } }

        private double _A2 = 0;
        [Category("容器封头壁厚计算")]
        [DisplayName("系数A")]
        public double A2 { get { return this._A2; } set { this._A2 = value; RaisePropertyChanged(() => A2); } }

        private double _B2 = 0;
        [Category("容器封头壁厚计算")]
        [DisplayName("系数B")]
        public double B2 { get { return this._B2; } set { this._B2 = value; RaisePropertyChanged(() => B2); } }


        private double _P2 = 0;
        [Category("容器封头壁厚计算")]
        [DisplayName("设计许用应力P")]
        [Description("设计许用应力P，MPa")]
        public double P2 { get { return this._P2; } set { this._P2 = value; RaisePropertyChanged(()=>P2); } }
    }
    public class ContainerParam : IComParam
    {
        private ContainerInputParam _input;
        private ContainerOutputParam _output;
        private ICommand _computeCommand;
        private Interpolation _interpolation = new Interpolation();
        public ContainerParam()
        {
            _input = new ContainerInputParam();
            _output = new ContainerOutputParam();
            ComputeCommand = new DelegateCommand(this.computeExecuted);
        }


        private void computeExecuted()
        {
            //筒体壁厚计算
            double t1_1 = _input.OuterDiameter / _input.deltaE1;
            double t1_2 = _input.L / _input.OuterDiameter;
            _output.A1 = _interpolation.executed2D(t1_1, (int)(t1_2*100));
            _output.B1 = _interpolation.executed1d(_output.A1);
            _output.P1 = _output.B1 / t1_1;

            //容器封头壁厚计算
            _output.A2 = 0.125 / (_input.OuterRadius / _input.deltaE2);
            _output.B2 = _interpolation.executed1d(_output.A2);
            _output.P2 = _output.B2 / (_input.OuterRadius / _input.deltaE2);
        }

        public ICommand ComputeCommand
        {
            get
            {
                return this._computeCommand;
            }

            set
            {
                this._computeCommand = value;
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
                this._input = value as ContainerInputParam;
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
                this._output = value as ContainerOutputParam;
            }
        }

        private string _DocPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview/容器系统计算.xps");
        public string DocPath
        {
            get
            {
                return this._DocPath;
            }
        }
    }
}
