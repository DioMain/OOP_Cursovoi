using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.Utilites;
using PCBuilder.Utilities;
using PCBuilder.View;
using PCBuilder.View.Frames;
using PCBuilder.View.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PCBuilder.ViewModel
{
    public class ProductCartFrameVM : FrameViewModel<ProductCartFrame, MainWindow>
    {
        private Product _product;

        private List<PerformanceVM> _performances;
        public List<PerformanceVM> Performances
        {
            get => _performances;
            set
            {
                _performances = value;
                OnPropertyChanged(nameof(Performances));
            }
        }

        #region Props

        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public string Name { get => _product.Name; }
        public string FullDescription { get => _product.FullDescription; }
        public string ShortDescription { get => _product.FullDescription; }
        public string Manufacturer { get => _product.Manufacturer; }
        public string Type { get => _product.Type; }
        public string Price { get => $"{_product.Price}$"; }

        public ImageSource Image
        {
            get
            {
                ImageSource image = ImageManager.LoadImage(_product.ImageUrl);

                return image ?? ImageManager.GetNoImage();
            }
        }

        #endregion

        public ProductCartFrameVM(ProductCartFrame owner, MainWindow window, Product product) : base(owner, window)
        {
            _product = product;

            _performances = new List<PerformanceVM>();

            foreach (var item in DataBaseManager.Instance.Products.GetPerformances(product))
                _performances.Add(new PerformanceVM(item));

            if (Performances == null || Performances.Count == 0)
                Owner.list.Visibility = Visibility.Collapsed;

            MainWindowVM mainWindow = window.DataContext as MainWindowVM;
            if (mainWindow.SectionId == 3)
            {
                owner.toBasketButton.Visibility = Visibility.Hidden;
            }

            AnimateAwake(owner.container);
        }

        #region Commands

        #region Back

        private BaseCommand BackCommand;
        public ICommand Back
        {
            get
            {
                if (BackCommand == null)
                    BackCommand = new BaseCommand(BackExecuted);

                return BackCommand;
            }
        }
        private void BackExecuted(object obj)
        {
            MainWindowVM windowVM = OwnerWindow.DataContext as MainWindowVM;

            windowVM.PopSubPage();
        }

        #endregion

        #region ToBasket

        private BaseCommand ToBasketCommand;
        public ICommand ToBasket
        {
            get
            {
                if (ToBasketCommand == null)
                    ToBasketCommand = new BaseCommand(ToBasketExecuted);

                return ToBasketCommand;
            }
        }
        private void ToBasketExecuted(object obj)
        {
            BasketManager.Instance.Add(Product);

            new MessagePopup((string)Application.Current.Resources["Loc_Popup_Global_Success"],
                            (string)Application.Current.Resources["Loc_Popup_ProdCart_Added"], true).ShowDialog();
        }

        #endregion

        #endregion
    }
}
