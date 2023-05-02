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
using PCBuilder.ViewModel;

namespace PCBuilder.View.Frames
{
    /// <summary>
    /// Логика взаимодействия для CatalogFrame.xaml
    /// </summary>
    public partial class CatalogFrame : Page
    {
        private MainWindow mainWindow;

        private bool mode;

        public CatalogFrame(MainWindow owner, bool mode = false)
        {
            InitializeComponent();

            mainWindow = owner;

            DataContext = new CatalogFrameVM(this, owner, mode);
            this.mode = mode;
        }
    }
}
