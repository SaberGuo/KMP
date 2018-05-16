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
        }

        [Import]
        MainDockViewModel _viewModel
        {
            set { this.DataContext = value; }
            get { return (MainDockViewModel)DataContext; }
        }

        private void _moduleTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _viewModel.ShowModule(_moduleTree.SelectedItem as IParamedModule);
        }
    }
}
