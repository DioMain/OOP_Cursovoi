using PCBuilder.Commands;
using PCBuilder.Filters;
using PCBuilder.Repositories;
using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class ProductViewerFrameVM : FrameViewModel<ProductViewerFrame, MainWindow>
    {
        private List<ProductVM> _products;

        private List<ProductVM> _textFilteredProducts;

        public List<ProductVM> Products
        {
            get => _textFilteredProducts;
            set
            {
                _textFilteredProducts = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ProductViewerFrameVM(ProductViewerFrame owner, MainWindow window) : base(owner, window)
        {
            _products = new List<ProductVM>();

            foreach (var item in DataBaseManager.Instance.Products.GetAll())
            {
                _products.Add(new ProductVM(item, this));
            }

            Products = _products;

            owner.textFilterBox.Changed += (sender, args) => { FilterByText(); };
        }

        private void FilterByText()
        {
            if (!string.IsNullOrWhiteSpace(Owner.textFilterBox.Text))
            {
                Products = _products.Where(x =>
                {
                    Regex regex = new Regex(@"(\w)*" + Owner.textFilterBox.Text + @"(\w)*");

                    return regex.IsMatch(x.Name);
                }).ToList();
            }
            else
                Products = _products;
        }

        private void SyncData()
        {
            _products.Clear();
            _textFilteredProducts.Clear();

            foreach (var item in DataBaseManager.Instance.Products.GetAll())
            {
                _products.Add(new ProductVM(item, this));
            }

            Products = new List<ProductVM>(_products);

            FilterByText();

            OnPropertyChanged(nameof(Products));
        }

        public void RemoveProduct(ProductVM product)
        {
            DataBaseManager.Instance.Products.Delete(product.Product.Id);    

            SyncData();
        }

        public void CreateNewProduct()
        {
            MainWindowVM window = OwnerWindow.DataContext as MainWindowVM;

            if (window != null)
            {
                window.PushSubPage(new ProductEditorFrame(OwnerWindow));
            }
        }

        public void EditProduct(ProductVM product)
        {
            MainWindowVM window = OwnerWindow.DataContext as MainWindowVM;

            if (window != null)
            {
                window.PushSubPage(new ProductEditorFrame(OwnerWindow, product.Product));
            }
        }

        #region Commands

        #region Create

        private BaseCommand CreateCommand;
        public ICommand Create
        {
            get => CreateCommand ?? (CreateCommand = new BaseCommand(CreateExecuted));
        }
        private void CreateExecuted(object obj)
        {
            CreateNewProduct();
        }

        #endregion

        #endregion
    }
}
