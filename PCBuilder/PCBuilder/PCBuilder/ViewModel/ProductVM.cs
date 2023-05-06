using PCBuilder.Commands;
using PCBuilder.CustomUI;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.Utilites;
using PCBuilder.Utilities;
using PCBuilder.View.Frames;
using PCBuilder.View.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PCBuilder.ViewModel
{
    public class ProductVM : BaseViewModel
    {
        private Product _product;

        private object _owner;

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
        public string ShortDescription { get => _product.ShortDescription; }
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

        public ProductVM(Product product)
        {
            _product = product;
        }
        public ProductVM(Product product, object owner)
        {
            _product = product;
            _owner = owner;
        }

        #region Commnads

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
            if (_owner is CatalogFrameVM)
            {
                CatalogFrameVM frame = (CatalogFrameVM)_owner;

                FrameworkElement container = obj as FrameworkElement;

                CircleButton btn;

                if (frame.Mode)
                {
                    btn = container.FindName("totemplate") as CircleButton;
                    btn.Visibility = Visibility.Visible;
                    btn.Style = Application.Current.Resources["CB_DEF"] as Style;
                }
                else
                {
                    btn = container.FindName("tobasket") as CircleButton;
                    btn.Visibility = Visibility.Visible;
                    btn.Style = Application.Current.Resources["CB_DEF"] as Style;
                }
            }
            else if (_owner is ProductViewerFrameVM)
            {
                FrameworkElement container = obj as FrameworkElement;

                FrameworkElement but0 = container.FindName("moreButton") as FrameworkElement;
                FrameworkElement but1 = container.FindName("editButton") as FrameworkElement;
                FrameworkElement but2 = container.FindName("deleteButton") as FrameworkElement;

                but0.Style = Application.Current.Resources["CB_DEF"] as Style;
                but1.Style = Application.Current.Resources["CB_DEF"] as Style;
                but2.Style = Application.Current.Resources["CB_RED"] as Style;
            }
        }

        #endregion

        #region OpenCart

        private BaseCommand OpenCartCommand;
        public ICommand OpenCart
        {
            get
            {
                if (OpenCartCommand == null)
                    OpenCartCommand = new BaseCommand(OpenCartExecuted);

                return OpenCartCommand;
            }
        }
        private void OpenCartExecuted(object obj)
        {
            if (_owner is CatalogFrameVM)
            {
                CatalogFrameVM frame = (CatalogFrameVM)_owner;

                MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                windowVM.PushSubPage(new ProductCartFrame(frame.OwnerWindow, Product));
            }
            else if (_owner is ProductViewerFrameVM)
            {
                ProductViewerFrameVM frame = (ProductViewerFrameVM)_owner;

                MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                windowVM.PushSubPage(new ProductCartFrame(frame.OwnerWindow, Product));
            }
        }
        #endregion

        #region BasketClick

        private BaseCommand BasketClickCommand;
        public ICommand BasketClick
        {
            get
            {
                if (BasketClickCommand == null)
                    BasketClickCommand = new BaseCommand(BasketClickExecuted);

                return BasketClickCommand;
            }
        }
        private void BasketClickExecuted(object obj)
        {
            BasketManager.Instance.Add(Product);

            new MessagePopup((string)Application.Current.Resources["Loc_Popup_Global_Success"],
                            (string)Application.Current.Resources["Loc_Popup_ProdCart_Added"], true).ShowDialog();
        }

        #endregion

        #region TemplateClick

        private BaseCommand TemplateClickCommand;
        public ICommand TemplateClick
        {
            get
            {
                if (TemplateClickCommand == null)
                    TemplateClickCommand = new BaseCommand(TemplateClickExecuted);

                return TemplateClickCommand;
            }
        }
        private void TemplateClickExecuted(object obj)
        {
            if (_owner is CatalogFrameVM)
            {
                CatalogFrameVM frame = (CatalogFrameVM)_owner;

                frame.SellectProduct(Product);

                MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                windowVM.PopSubPage();
            }
        }

        #endregion

        #region Edit

        private BaseCommand EditCommand;
        public ICommand Edit
        {
            get
            {
                if (EditCommand == null)
                    EditCommand = new BaseCommand(EditExecuted);

                return EditCommand;
            }
        }
        private void EditExecuted(object obj)
        {
            if (_owner is ProductViewerFrameVM)
            {
                ((ProductViewerFrameVM)_owner).EditProduct(this);
            }
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
            if (_owner is ProductViewerFrameVM)
            {
                ((ProductViewerFrameVM)_owner).RemoveProduct(this);
            }
        }

        #endregion

        #endregion
    }
}
