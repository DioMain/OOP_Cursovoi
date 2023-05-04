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
    public class CatalogFrameVM : FrameViewModel<CatalogFrame, MainWindow>
    {
        private List<ProductVM> _products;

        private List<ProductVM> _textFilteredProducts;

        private List<ProductVM> _resultProducts;

        private List<IFilter<ProductVM>> _filters;

        public List<ProductVM> Products
        {
            get => _resultProducts;
            set
            {
                _resultProducts = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        
        public event Action OnResult;

        /// <summary>
        /// Режим каталога (false - for basket, true - for template)
        /// </summary>
        public bool Mode { get; private set; }

        public CatalogFrameVM(CatalogFrame owner, MainWindow window, bool mode) : base(owner, window)
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

            Mode = mode;
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


            ApplyFilers();
        }

        public void ApplyFilers()
        {
            List<ProductVM> products = _textFilteredProducts;

            foreach (var item in _filters)
            {
                products = item.DoFilter(products);
            }

            Products = products;
        }

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
            FilterPopup popup = new FilterPopup(_filters);

            popup.ShowDialog();

            _filters = popup.GetResult();

            ApplyFilers();
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

            ApplyFilers();
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
