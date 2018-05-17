﻿using System;
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

        private void DockingManager_DocumentClosed(object sender, Xceed.Wpf.AvalonDock.DocumentClosedEventArgs e)
        {
            ((IInvMonitorViewModel)e.Document.Content).CloseDocument();
            _viewModel.Documents.Remove((IInvMonitorViewModel)e.Document.Content);
        }
    }
}
