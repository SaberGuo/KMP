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
    public class HeatSinkMaterial: NotificationObject
    {
        private string _name;
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                RaisePropertyChanged(() => this.name);
            }
        }

        private string _format;
        public string format
        {
            get
            {
                return _format;
            }
            set
            {
                this._format = value;
                RaisePropertyChanged(() => this.format);
            }
        }

        private double _unitWeight;
        public double unitWeight
        {
            get
            {
                return this._unitWeight;
            }
            set
            {
                this._unitWeight = value;
                RaisePropertyChanged(() => this.unitWeight);
            }
        }

        private double _num;
        public double num
        {
            get
            {
                return this._num;
            }
            set
            {
                this._num = value;
                RaisePropertyChanged(() => this.num);
            }
        }

        private double _weight;
        public double weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                this._weight = value;
                RaisePropertyChanged(()=>this.weight);
            }
        }

    }
    class HeatSinkInputParam : NotificationObject, ISubComParam
    {
        private double _A1 = 143;
        [Category("室温容器壁对热沉的辐射热")]
        [DisplayName("热沉总的外表面积")]
        [Description("热沉总的外表面积,m2")]
        public double A1
        {
            get
            {
                return this._A1;
            }
            set
            {
                this._A1 = value;
                RaisePropertyChanged(() => this.A1);
            }
        }

        private double _A2 = 199;
        [Category("室温容器壁对热沉的辐射热")]
        [DisplayName("容器内表面积")]
        [Description("容器内表面积,m2")]
        public double A2
        {
            get
            {
                return this._A2;
            }
            set
            {
                this._A2 = value;
                RaisePropertyChanged(() => this.A2);
            }
        }

        private double _q = 400;
        [Category("试验时热沉热负荷")]
        [DisplayName("热沉内表面的平均热负荷")]
        [Description("热沉内表面的平均热负荷,w/m2")]
        public double q
        {
            get
            {
                return this._q;
            }
            set
            {
                this._q = value;
                RaisePropertyChanged(()=>this.q);
            }
        }

        private double _A3 = 143.5;
        [Category("试验时热沉热负荷")]
        [DisplayName("热沉内表面积")]
        [Description("热沉内表面积,m2")]
        public double A3
        {
            get
            {
                return this._A3;
            }
            set
            {
                this._A3 = value;
                RaisePropertyChanged(() => this.A3);
            }
        }

        private double _Am = 0.24;
        [Category("漏热热负荷")]
        [DisplayName("支承材料的接触面积")]
        [Description("支承材料的接触面积,m2")]
        public double Am
        {
            get
            {
                return this._Am;
            }
            set
            {
                this._Am = value;
                RaisePropertyChanged(()=>Am);
            }
        }

        private double _L = 0.01;
        [Category("漏热热负荷")]
        [DisplayName("支承材料的长度")]
        [Description("支承材料的长度,m")]
        public double L
        {
            get
            {
                return this._L;
            }
            set
            {
                this._L = value;
                RaisePropertyChanged(() => L);
            }
        }

        private double _n = 5;
        [Category("漏热热负荷")]
        [DisplayName("支承数")]
        [Description("支承数,个")]
        public double n
        {
            get
            {
                return this._n;
            }
            set
            {
                this._n = value;
                RaisePropertyChanged(() => n);
            }
        }

        private double _deltaT = 5;
        [Category("热沉的总热负荷")]
        [DisplayName("液氮出口温度与进口温度之差")]
        [Description("液氮出口温度与进口温度之差,K")]
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



    }
    class HeatSinkOutputParam : NotificationObject, ISubComParam
    {
        private double _Q1 = 0;
        [Category("室温容器壁对热沉的辐射热")]
        [DisplayName("容器壁对热沉的辐射热负荷")]
        [Description("容器壁对热沉的辐射热负荷,kw")]
        public double Q1
        {
            get
            {
                return this._Q1;
            }
            set
            {
                this._Q1 = value;
                this.RaisePropertyChanged(() => this.Q1);
            }
        }

        private double _Q2 = 0;
        [Category("试验时热沉热负荷")]
        [DisplayName("热沉内表面承受的热负荷")]
        [Description("热沉内表面承受的热负荷,kw")]
        public double Q2
        {
            get
            {
                return this._Q2;
            }
            set
            {
                this._Q2 = value;
                RaisePropertyChanged(() => this.Q2);
            }
        }

        private double _Qd = 0;
        [Category("漏热热负荷")]
        [DisplayName("传导漏热")]
        [Description("传导漏热,kw")]
        public double Qd
        {
            get
            {
                return this._Qd;
            }
            set
            {
                this._Qd = value;
                this.RaisePropertyChanged(() => this.Qd);
            }
        }

        private double _Q = 0;
        [Category("热沉的总热负荷")]
        [DisplayName("热沉的总热负荷")]
        [Description("热沉的总热负荷,kw")]
        public double Q
        {
            get
            {
                return this._Q;
            }
            set
            {
                this._Q = value;
                this.RaisePropertyChanged(() => this.Q);
            }
        }

        private double _M = 0;
        [Category("热沉所需液氮流量")]
        [DisplayName("热沉所需液氮流量")]
        [Description("热沉所需液氮流量,m3/h")]
        public double M
        {
            get
            {
                return this._M;
            }
            set
            {
                this._M = value;
                this.RaisePropertyChanged(() => this.M);
            }
        }
    }
    public class HeatSinkParam : IComParam
    {
        private ICommand _computeCommand;
        private HeatSinkInputParam _input;
        private HeatSinkOutputParam _output;

        private List<HeatSinkMaterial> _noumenonMaterials = new List<HeatSinkMaterial>();
        private List<HeatSinkMaterial> _capMaterials = new List<HeatSinkMaterial>();
        private List<HeatSinkMaterial> _frontCapMaterials = new List<HeatSinkMaterial>();
        public HeatSinkParam()
        {
            this._computeCommand = new DelegateCommand(computeExecuted);
            _input = new HeatSinkInputParam();
            _output = new HeatSinkOutputParam();
            //noumeno
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢胀板", format = "厚度3.7mm", unitWeight = 30 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ32x2.5(mm)", unitWeight = 1.82 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ38x3(mm)", unitWeight = 2.59 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ45x2.5(mm)", unitWeight = 2.62 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ57x3.5(mm)", unitWeight = 4.62 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ76X3(mm)", unitWeight = 5.40 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ89X4.5(mm)", unitWeight = 9.38 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ108X4(mm)", unitWeight = 10.26 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢160×80×5(mm)", unitWeight = 12.2 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢100×50×4(mm)", unitWeight = 5.788 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢80×40×4(mm)", unitWeight = 4.532 });
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "聚四氟乙烯", format = "", unitWeight = 2200});
            _noumenonMaterials.Add(new HeatSinkMaterial { name = "防辐射屏", format = "0.8mm", unitWeight = 6.36 });
            //cap
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢胀板", format = "厚度3.7mm", unitWeight = 30 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ32x2.5(mm)", unitWeight = 1.82 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ38x3(mm)", unitWeight = 2.59 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ45x2.5(mm)", unitWeight = 2.62 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ57x3.5(mm)", unitWeight = 4.62 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ76X3(mm)", unitWeight = 5.40 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ89X4.5(mm)", unitWeight = 9.38 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ108X4(mm)", unitWeight = 10.26 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢160×80×5(mm)", unitWeight = 12.2 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢100×50×4(mm)", unitWeight = 5.788 });
            _capMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢80×40×4(mm)", unitWeight = 4.532 });
            _capMaterials.Add(new HeatSinkMaterial { name = "聚四氟乙烯", format = "", unitWeight = 2200 });
            _capMaterials.Add(new HeatSinkMaterial { name = "防辐射屏", format = "0.8mm", unitWeight = 6.36 });
            //front cap
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢胀板", format = "厚度3.7mm", unitWeight = 30 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ32x2.5(mm)", unitWeight = 1.82 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ38x3(mm)", unitWeight = 2.59 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ45x2.5(mm)", unitWeight = 2.62 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ57x3.5(mm)", unitWeight = 4.62 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ76X3(mm)", unitWeight = 5.40 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ89X4.5(mm)", unitWeight = 9.38 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢管", format = "φ108X4(mm)", unitWeight = 10.26 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢160×80×5(mm)", unitWeight = 12.2 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢100×50×4(mm)", unitWeight = 5.788 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "不锈钢骨架", format = "槽钢80×40×4(mm)", unitWeight = 4.532 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "聚四氟乙烯", format = "", unitWeight = 2200 });
            _frontCapMaterials.Add(new HeatSinkMaterial { name = "防辐射屏", format = "0.8mm", unitWeight = 6.36 });
        }

        private void computeExecuted()
        {
            _output.Q1 = 5.67 * _input.A1 / (1 / 0.2 + _input.A1 / _input.A2 * (1 / 0.2 - 1)) * (Math.Pow(3, 4) - Math.Pow(1, 4)) / 2;
            _output.Q2 = _input.q * _input.A3;
            _output.Qd = _input.n * _input.Am * 0.26 / _input.L * (200-100);
            _output.Q = _output.Q1 + _output.Q2 + _output.Qd;
            _output.M = _output.Q / (2024*_input.deltaT*760)*1000*3600;
        }
        public List<HeatSinkMaterial> noumenonMaterials
        {
            get
            {
                return this._noumenonMaterials;
            }
            
        }

        public List<HeatSinkMaterial> capMaterials
        {
            get
            {
                return this._capMaterials;
            }
        }

        public List<HeatSinkMaterial> frontCapMaterials
        {
            get
            {
                return this._frontCapMaterials;
            }
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
        private string _DocPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview/热沉系统计算.xps");

        public string DocPath
        {
            get
            {
                return this._DocPath;
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
                _input = value as HeatSinkInputParam;
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
                _output = value as HeatSinkOutputParam;
            }
        }


    }
}
