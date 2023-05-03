using PCBuilder.Filters;
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
    /// Логика взаимодействия для FilterPopup.xaml
    /// </summary>
    public partial class FilterPopup : Window
    {
        private FilterPopupVM viewModel;

        public FilterPopup()
        {
            InitializeComponent();

            viewModel = new FilterPopupVM(this);

            DataContext = viewModel;
        }
        public FilterPopup(List<IFilter<ProductVM>> current)
        {
            InitializeComponent();

            viewModel = new FilterPopupVM(this, current);

            DataContext = viewModel;
        }

        public List<IFilter<ProductVM>> GetResult() => viewModel.filters;
    }
}
