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

namespace PCBuilder.ViewModel
{
    public class CatalogFrameVM : FrameViewModel<CatalogFrame, MainWindow>
    {
        private List<ProductVM> _products;
        public List<ProductVM> Products
        {
            get => _products;
            set
            {
                _products = value;
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

            foreach (var item in DataBaseManager.Instance.Products.GetAll())
            {
                _products.Add(new ProductVM(item, this));
            }

            Mode = mode;
        }

        #region Commands



        #endregion
    }
}
