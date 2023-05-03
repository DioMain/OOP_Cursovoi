using PCBuilder.Commands;
using PCBuilder.CustomUI;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.Utilites;
using PCBuilder.View.Frames;
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

                Border border = obj as Border;

                CircleButton btn;

                if (frame.Mode)
                {
                    btn = border.FindName("totemplate") as CircleButton;
                    btn.Visibility = Visibility.Visible;
                    btn.Style = Application.Current.Resources["CB_DEF"] as Style;
                }
                else
                {
                    btn = border.FindName("tobasket") as CircleButton;
                    btn.Visibility = Visibility.Visible;
                    btn.Style = Application.Current.Resources["CB_DEF"] as Style;
                }
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

                if (!frame.Mode)
                {
                    MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                    windowVM.SetFrame(new ProductCartFrame(frame.OwnerWindow, Product));
                }
                else
                {
                    // Для Template editor
                }             
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
            MessageBox.Show("a");
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
            MessageBox.Show("It works!");
        }

        #endregion

        #endregion
    }
}
