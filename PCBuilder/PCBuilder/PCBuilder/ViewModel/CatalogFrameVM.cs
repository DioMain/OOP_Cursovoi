using PCBuilder.Repositories;
using PCBuilder.View;
using PCBuilder.View.Frames;
using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PCBuilder.Commands;
using PCBuilder.CustomUI;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;
using PCBuilder.Filters;
using PCBuilder.View.Popups;

namespace PCBuilder.ViewModel
{
    public delegate void ProductSellected(Product selletedProduct);

    public class CatalogFrameVM : FrameViewModel<CatalogFrame, MainWindow>
    {
        private List<ProductVM> _products;

        private List<ProductVM> _textFilteredProducts;

        private List<ProductVM> _resultProducts;

        private ProductType _productType;


        private List<IFilter<ProductVM>> _filters;
        public List<IFilter<ProductVM>> Filters
        {
            get => _filters;
        }

        public List<ProductVM> Products
        {
            get => _resultProducts;
            set
            {
                _resultProducts = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        
        public event ProductSellected OnProductSellected;

        /// <summary>
        /// Режим каталога (false - for basket, true - for template)
        /// </summary>
        public bool Mode { get; private set; }

        public CatalogFrameVM(CatalogFrame owner, MainWindow window, ProductType productType = ProductType.Unknown) : base(owner, window)
        {
            _products = new List<ProductVM>();
            _filters = new List<IFilter<ProductVM>>();

            foreach (var item in DataBaseManager.Instance.Products.GetAll())
            {
                _products.Add(new ProductVM(item, this));
            }

            _textFilteredProducts = _products;
            _resultProducts = _products;

            owner.textFilterBox.Changed += (sender, args) => { FilterByText(); };

            Mode = productType != ProductType.Unknown;
            _productType = productType;

            if (Mode)
            {
                Filters.Add(new TypeFilter(productType.ToString()));

                ApplyFilters();
            }

            AnimateAwake(owner.container);
        }

        private void FilterByText()
        {
            if (!string.IsNullOrWhiteSpace(Owner.textFilterBox.Text))
            {
                _textFilteredProducts = _products.Where(x =>
                {
                    Regex regex = new Regex(@"(\w)*" + Owner.textFilterBox.Text + @"(\w)*");

                    return regex.IsMatch(x.Name);
                }).ToList();
            }
            else
                _textFilteredProducts = _products;


            ApplyFilters();
        }

        public void ApplyFilters()
        {
            List<ProductVM> products = _textFilteredProducts;

            foreach (var item in _filters)
            {
                products = item.DoFilter(products);
            }

            Products = products;
        }

        public void SellectProduct(Product product) => OnProductSellected.Invoke(product);

        #region Commands

        #region SetFilters

        private BaseCommand SetFiltersCommand;
        public ICommand SetFilters
        {
            get
            {
                if (SetFiltersCommand == null)
                    SetFiltersCommand = new BaseCommand(SetFiltersExecuted);

                return SetFiltersCommand;
            }
        }
        private void SetFiltersExecuted(object obj)
        {
            FilterPopup popup = new FilterPopup(_filters, Mode);

            popup.ShowDialog();

            _filters = popup.GetResult();

            ApplyFilters();
        }

        #endregion

        #region DropFilters

        private BaseCommand DropFiltersCommand;
        public ICommand DropFilters
        {
            get
            {
                if (DropFiltersCommand == null)
                    DropFiltersCommand = new BaseCommand(DropFiltersExecuted);

                return DropFiltersCommand;
            }
        }
        private void DropFiltersExecuted(object obj)
        {
            _filters.Clear();

            if (Mode)
                Filters.Add(new TypeFilter(_productType.ToString()));


            ApplyFilters();
        }

        #endregion

        #endregion

        public override void Dispose()
        {
            base.Dispose();

            if (_filters != null)
                _filters.Clear();

            if (_products != null)
                _products.Clear();

            if (_textFilteredProducts != null)
                _textFilteredProducts.Clear();

            if (_resultProducts != null)
                _resultProducts.Clear();
        }
    }
}
