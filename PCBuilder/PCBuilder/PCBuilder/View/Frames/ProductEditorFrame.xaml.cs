﻿using PCBuilder.Model;
using PCBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCBuilder.View.Frames
{
    /// <summary>
    /// Логика взаимодействия для ProductEditorFrame.xaml
    /// </summary>
    public partial class ProductEditorFrame : Page
    {
        public ProductEditorFrame(MainWindow mainWindow, Product product = null)
        {
            InitializeComponent();

            DataContext = new ProductEditorFrameVM(this, mainWindow, product);
        }
    }
}
