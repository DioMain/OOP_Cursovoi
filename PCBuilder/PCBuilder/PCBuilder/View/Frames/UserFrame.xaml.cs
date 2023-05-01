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
    /// Логика взаимодействия для UserFrame.xaml
    /// </summary>
    public partial class UserFrame : Page
    {
        public MainWindow Owner { get; private set; }

        public UserFrame(MainWindow mainWindow)
        {
            InitializeComponent();

            Owner = mainWindow;

            DataContext = new UserFrameVM(this, mainWindow);
        }
    }
}
