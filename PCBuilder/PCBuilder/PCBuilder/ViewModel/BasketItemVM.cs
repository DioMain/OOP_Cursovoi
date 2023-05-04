using PCBuilder.Commands;
using PCBuilder.CustomUI;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.Utilites;
using PCBuilder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using PCBuilder.View.Frames;

namespace PCBuilder.ViewModel
{
    public class BasketItemVM : BaseViewModel
    {
        private BasketItem _basketItem;
        public BasketItem BasketItem
        {
            get => _basketItem;
            set {
                _basketItem = value;
                OnPropertyChanged(nameof(BasketItem));
            }
        }

        private FrameworkElement _container;

        private object _owner;

        public string Name
        {
            get => _basketItem.IsTemplate ? _basketItem.Template.Name : _basketItem.Product.Name;
        }

        public string Description
        {
            get => _basketItem.IsTemplate ? _basketItem.Template.Description : _basketItem.Product.ShortDescription;
        }

        public int PriceInt
        {
            get
            {
                if (!_basketItem.IsTemplate)
                    return _basketItem.Product.Price;
                else
                {
                    int price = 0;

                    foreach (var item in DataBaseManager.Instance.Templates.GetItems(_basketItem.Template))
                    {
                        Product product = DataBaseManager.Instance.Products.Get(item.ProductId);

                        price += product.Price;
                    }

                    return price;
                }
            }
        }
        public string Price
        {
            get => $"{PriceInt}$";
        }

        public ImageSource Image
        {
            get
            {
                if (_basketItem.IsTemplate)
                    return null;
                else
                {
                    ImageSource image = ImageManager.LoadImage(_basketItem.Product.ImageUrl);

                    return image ?? ImageManager.GetNoImage();
                }
            }
        }

        public BasketItemVM(BasketItem item)
        {
            _container = null;

            _basketItem = item;
        }
        public BasketItemVM(BasketItem basketItem, object owner)
        {
            _basketItem = basketItem;
            _owner = owner;
        }

        #region Commands

        #region Init

        private BaseCommand InitCommand;
        public ICommand Init
        {
            get
            {
                if (InitCommand == null)
                    InitCommand = new BaseCommand(InitExecuted);

                return InitCommand;
            }
        }
        private void InitExecuted(object obj)
        {
            _container = (FrameworkElement)obj;

            FrameworkElement img = _container.FindName("imgBorder") as FrameworkElement;
            FrameworkElement but0 = _container.FindName("deleteButton") as FrameworkElement;
            FrameworkElement but1 = _container.FindName("moreButton") as FrameworkElement;

            but0.Style = (Style)Application.Current.Resources["CB_RED"];
            but1.Style = (Style)Application.Current.Resources["CB_DEF"];

            if (_basketItem.IsTemplate) 
                img.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Delete

        private BaseCommand DeleteCommand;
        public ICommand Delete
        {
            get
            {
                if (DeleteCommand == null)
                    DeleteCommand = new BaseCommand(DeleteExecuted);

                return DeleteCommand;
            }
        }
        private void DeleteExecuted(object obj)
        {
            ((BasketFrameVM)_owner).RemoveItem(this);
        }

        #endregion

        #region More

        private BaseCommand MoreCommand;
        public ICommand More
        {
            get
            {
                if (MoreCommand == null)
                    MoreCommand = new BaseCommand(MoreExecuted);

                return MoreCommand;
            }
        }
        private void MoreExecuted(object obj)
        {
            BasketFrameVM frame = (BasketFrameVM)_owner;

            if (_basketItem.IsTemplate)
            {

            }
            else
            {
                MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                windowVM.PushSubPage(new ProductCartFrame(frame.OwnerWindow, _basketItem.Product));
            }
        }

        #endregion

        #endregion
    }
}
