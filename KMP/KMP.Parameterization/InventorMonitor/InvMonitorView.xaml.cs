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
namespace KMP.Parameterization.InventorMonitor
{
    /// <summary>
    /// InvMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class InvMonitorView : UserControl
    {
        IModuleService _moduleService;
        public InvMonitorView()
        {
            InitializeComponent();
            _moduleService = ServiceLocator.Current.GetInstance<IModuleService>();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _moduleService.Create();
        }
    }
}
