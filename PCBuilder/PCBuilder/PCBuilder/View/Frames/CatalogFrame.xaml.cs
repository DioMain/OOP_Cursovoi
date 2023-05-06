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
using PCBuilder.Model;
using PCBuilder.ViewModel;

namespace PCBuilder.View.Frames
{
    /// <summary>
    /// Логика взаимодействия для CatalogFrame.xaml
    /// </summary>
    public partial class CatalogFrame : Page
    {
        public CatalogFrameVM viewModel { get; private set; }

        public CatalogFrame(MainWindow owner, ProductType productType = ProductType.Unknown)
        {
            InitializeComponent();

            viewModel = new CatalogFrameVM(this, owner, productType);

            DataContext = viewModel;
        }
    }
}
