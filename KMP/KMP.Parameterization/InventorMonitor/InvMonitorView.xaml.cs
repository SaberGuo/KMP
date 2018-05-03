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
using Microsoft.Practices.ServiceLocation;
using KMP.Interface;
using System.ComponentModel.Composition;
using Xceed.Wpf.AvalonDock.Layout;

namespace KMP.Parameterization.InventorMonitor
{
    /// <summary>
    /// InvMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class InvMonitorView : UserControl
    {
        //IModuleService _moduleService;
        public InvMonitorView()
        {
            InitializeComponent();
            
        }
        public override void OnApplyTemplate()
        {
            this._viewModel = (IInvMonitorViewModel)this.DataContext;
            InitAppComponet();
            base.OnApplyTemplate();
        }
        private void InitAppComponet()
        {
            this.holder.MouseDown += this._viewModel.OnMouseDown;
            this.holder.MouseMove += this._viewModel.OnMouseMove;
            this.holder.MouseUp += this._viewModel.OnMouseUp;
            this.holder.MouseDoubleClick += this._viewModel.OnMouseDoubleClick;

            //this.holder.SizeChanged += this._viewModel.OnSizeChanged;
            this.holder.Paint += Holder_Paint;
    
            this._viewModel.HWnd = this.holder.Handle.ToInt32();

            this._viewModel.InitVM();


        }

        private void Holder_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            this._viewModel.OnSizeChanged(sender, e);
        }

        private IInvMonitorViewModel _viewModel;
        

    }
}
