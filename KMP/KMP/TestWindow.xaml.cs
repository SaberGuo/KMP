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
using System.Windows.Shapes;
using System.ComponentModel.Composition;
namespace KMP
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }
    }
}
