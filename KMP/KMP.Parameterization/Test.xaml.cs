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
using KMP.Interface;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition;
using Infranstructure.Behaviors;
namespace KMP.Parameterization
{
    /// <summary>
    /// Test.xaml 的交互逻辑
    /// </summary>
    //[ViewExport(RegionName = "Test")]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class Test : UserControl
    {
        IModuleService _moduleService;
        
        public Test()
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
