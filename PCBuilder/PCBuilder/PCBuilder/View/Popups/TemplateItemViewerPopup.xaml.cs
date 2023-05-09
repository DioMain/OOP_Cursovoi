using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для TemplateItemViewerPopup.xaml
    /// </summary>
    public partial class TemplateItemViewerPopup : Window
    {
        private Template template;

        private List<ProductVM> items;
        public List<ProductVM> Items { get { return items; } }

        public string TotalPrice
        {
            get
            {
                int count = 0;

                foreach (ProductVM item in Items)
                {
                    count += item.Product.Price;
                }

                return $"{count}$";
            }
        }

        public string Creator { get { return DataBaseManager.Instance.Users.Get(template.CreatorId).Email; } }
        public string TemplateName { get { return template.Name; } }
        public string Description { get { return template.Description; } }

        public TemplateItemViewerPopup(Template template)
        {
            InitializeComponent();

            this.template = template;

            DataContext = this;
            
            items = new List<ProductVM>();

            foreach (var item in DataBaseManager.Instance.Templates.GetItems(template))
            {
                Product prod = DataBaseManager.Instance.Products.Get(item.ProductId);

                items.Add(new ProductVM(prod));
            }
        }
    }
}
