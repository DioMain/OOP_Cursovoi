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
    /// Логика взаимодействия для OptionFrame.xaml
    /// </summary>
    public partial class OptionFrame : Page
    {
        public OptionFrame(MainWindow mainWindow)
        {
            InitializeComponent();

            DataContext = new OptionFrameVM(this, mainWindow);
        }
    }
}
