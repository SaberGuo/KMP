using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace KMP.Interface.ComParam
{
    class VacuoInputParam : NotificationObject, ISubComParam
    {

    }
    class VacuoOutputParam: NotificationObject, ISubComParam
    {

    }

    
    public class VacuoParam : IComParam
    {
        private VacuoInputParam _input;
        private VacuoOutputParam _output;
        public VacuoParam()
        {
            _input = new VacuoInputParam();
            _output = new VacuoOutputParam();
        }
        private ICommand _computeCommand;
        private void computedExecuted()
        {
            ComputeCommand = new DelegateCommand(this.computedExecuted);
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
