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
using Infranstructure.Behaviors;
using System.ComponentModel.Composition;
using KMP.Interface;
using KMP.Parameterization.InventorMonitor;
using Infranstructure.Commands;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace KMP.Parameterization
{
    [ViewExport(RegionName = RegionNames.MainRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    /// <summary>
    /// MainDockView.xaml 的交互逻辑
    /// </summary>
    public partial class MainDockView : UserControl
    {
        public MainDockView()
        {
            InitializeComponent();

            init_viewCommands();
        }

        [Import]
        MainDockViewModel _viewModel
        {
            set { this.DataContext = value; }
            get { return (MainDockViewModel)DataContext; }
        }
        private ViewCommandProxy viewCommandProxy;


        private void init_viewCommands()
        {
            viewCommandProxy = ServiceLocator.Current.GetInstance<ViewCommandProxy>();
            this.viewCommandProxy.OrientViewCommand = new DelegateCommand<string>(ViewOperation);
        }

        private void ViewOperation(string orient)
        {
            try
            {
                IInvMonitorViewModel v = (IInvMonitorViewModel)this.DocManager.ActiveContent;
                v.ViewOperation(orient);
            }
            catch (Exception e)
            {

                return;
            }
            
            
        }
        private void _moduleTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _viewModel.ShowModule(_moduleTree.SelectedItem as IParamedModule);
        }

        private void DockingManager_DocumentClosed(object sender, Xceed.Wpf.AvalonDock.DocumentClosedEventArgs e)
        {
            
            ((IInvMonitorViewModel)e.Document.Content).CloseDocument();
            _viewModel.Documents.Remove((IInvMonitorViewModel)e.Document.Content);
        }

        private void PropertyGrid_SelectedPropertyItemChanged(object sender, RoutedPropertyChangedEventArgs<Xceed.Wpf.Toolkit.PropertyGrid.PropertyItemBase> e)
        {
            try
            {
                if (e.NewValue.Description.Length > 0)
                {
                    string PreviewImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview", e.NewValue.Description + ".png");
                    this.preview.Source = new BitmapImage(new Uri(PreviewImagePath));
                }
            }
            catch (Exception)
            {

                this.preview.Source = null;
            }
           
            
        }
    }
}
