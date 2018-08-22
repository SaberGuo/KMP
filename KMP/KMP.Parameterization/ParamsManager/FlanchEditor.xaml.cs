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
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace KMP.Parameterization.ParamsManager
{
    /// <summary>
    /// FlanchEditor.xaml 的交互逻辑
    /// </summary>
    public partial class FlanchEditor : UserControl, ITypeEditor
    {
        public FlanchEditor()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register("Collection", typeof(string), typeof(FlanchEditor),
                                                                                               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Collection
        {
            get { return (string)GetValue(CollectionProperty); }
            set { SetValue(CollectionProperty, value); }
        }


        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            Binding binding = new Binding("Collection");
            binding.Source = propertyItem;
           
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, FlanchEditor.CollectionProperty, binding);
            return this;
        }
    }
}
