using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace KMP.Parameterization.InventorMonitor
{
    /// <summary>
    /// InvMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class InvMonitorView : LayoutDocument
    {
        public InvMonitorView()
        {
            InitializeComponent();
        }
        private void InitAppComponet()
        {
            this.holder.MouseDown += this._viewModel.OnMouseDown;
            this.holder.MouseMove += this._viewModel.OnMouseMove;
            this.holder.MouseUp += this._viewModel.OnMouseUp;
            this.holder.MouseDoubleClick += this._viewModel.OnMouseDoubleClick;

            this.holder.SizeChanged += this._viewModel.OnSizeChanged;

            this._viewModel.HWnd = this.holder.Handle.ToInt32();
            
        }


        private IInvMonitorViewModel _viewModel;
        public IInvMonitorViewModel ViewModel
        {
            get
            {
                return this._viewModel;
            }
            set
            {
                this._viewModel = value;
                InitAppComponet();
            }
        }

    }
}
