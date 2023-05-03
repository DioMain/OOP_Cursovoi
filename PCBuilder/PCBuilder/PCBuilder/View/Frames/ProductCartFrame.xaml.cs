using PCBuilder.Model;
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
    /// Логика взаимодействия для ProductCartFrame.xaml
    /// </summary>
    public partial class ProductCartFrame : Page
    {
        public ProductCartFrame(MainWindow mainWindow, Product product)
        {
            InitializeComponent();

            DataContext = new ProductCartFrameVM(this, mainWindow, product);
        }
    }
}
