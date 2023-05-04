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
using System.Windows.Shapes;

namespace PCBuilder.View.Popups
{
    /// <summary>
    /// Логика взаимодействия для PerformanceEditorPopup.xaml
    /// </summary>
    public partial class PerformanceEditorPopup : Window
    {
        private PerformanceEditorPupupVM dataContext;

        public PerformanceEditorPopup(Performance performance = null)
        {
            InitializeComponent();

            dataContext = new PerformanceEditorPupupVM(this, performance);

            DataContext = dataContext;
        }

        public Performance GetResult() => dataContext.Result;
    }
}
