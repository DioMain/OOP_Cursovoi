using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Filters;
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
using System.Windows.Media.Imaging;

namespace PCBuilder.ViewModel
{
    public class TemplateItemVM : BaseViewModel
    {
        private object _owner;

        private Product product;
        public Product Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        private ProductType targetType;
        public ProductType TargetType
        {
            get => targetType;
        }

        public string Name { get => product != null ? product.Name : Application.Current.Resources["Loc_TempEdit_ProdNameUnset"] as string; }
        public string Price { get => product != null ? $"{product.Price}$" : "0$"; }

        public TemplateItemVM(ProductType targetType, object owner)
        {
            this.targetType = targetType;

            product = null;

            _owner = owner;
        }

        private void OnChoised(Product product)
        {
            Product = product;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Price));

            if (_owner is TemplateEditorFrameVM)
            {
                TemplateEditorFrameVM frame = (TemplateEditorFrameVM)_owner;

                frame.OnPropertyChanged(nameof(frame.TotalPrice));
            }
        }

        #region Commands

        #region Init

        private BaseCommand InitCommand;
        public ICommand Init
        {
            get => InitCommand ?? (InitCommand = new BaseCommand(InitExecuted));
        }
        private void InitExecuted(object obj)
        {
            if (_owner is TemplateEditorFrameVM)
            {
                FrameworkElement element = (FrameworkElement)obj;

                FrameworkElement but0 = element.FindName("moreButton") as FrameworkElement;
                FrameworkElement but1 = element.FindName("editButton") as FrameworkElement;
                Image img = element.FindName("img") as Image;

                but0.Style = Application.Current.Resources["CB_DEF"] as Style;
                but1.Style = Application.Current.Resources["CB_DEF"] as Style;

                BitmapImage image = new BitmapImage();

                string url = "";

                switch (TargetType)
                {
                    case ProductType.Unknown:
                        url = "/Resources/no-photos.png";
                        break;
                    case ProductType.CPU:
                        url = "/Resources/cpu.png";
                        break;
                    case ProductType.GPU:
                        url = "/Resources/gpu.png";
                        break;
                    case ProductType.RAM:
                        url = "/Resources/ram.png";
                        break;
                    case ProductType.Memory:
                        url = "/Resources/memory.png";
                        break;
                    case ProductType.Case:
                        url = "/Resources/pccase.png";
                        break;
                    case ProductType.PowerUnit:
                        url = "/Resources/power.png";
                        break;
                    case ProductType.MotherBroad:
                        url = "/Resources/motherboard.png";
                        break;
                    case ProductType.СoolingSystem:
                        url = "/Resources/coller.png";
                        break;
                }

                image.BeginInit();
                image.UriSource = new Uri(url, UriKind.Relative);
                image.EndInit();

                img.Source = image;
            }
        }

        #endregion

        #region Edit

        private BaseCommand EditCommand;
        public ICommand Edit
        {
            get => EditCommand ?? (EditCommand = new BaseCommand(EditExecuted));
        }
        private void EditExecuted(object obj)
        {
            if (_owner is TemplateEditorFrameVM)
            {
                TemplateEditorFrameVM frame = (TemplateEditorFrameVM)_owner;

                MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                CatalogFrame catalog = new CatalogFrame(frame.OwnerWindow, targetType);

                catalog.viewModel.OnProductSellected += OnChoised;

                windowVM.PushSubPage(catalog);
            }
        }

        #endregion

        #region More

        private BaseCommand MoreCommand;
        public ICommand More
        {
            get => MoreCommand ?? (MoreCommand = new BaseCommand(MoreExecuted));
        }
        private void MoreExecuted(object obj)
        {
            if (product != null)
            {
                if (_owner is TemplateEditorFrameVM)
                {
                    TemplateEditorFrameVM frame = (TemplateEditorFrameVM)_owner;

                    MainWindowVM windowVM = frame.OwnerWindow.DataContext as MainWindowVM;

                    windowVM.PushSubPage(new ProductCartFrame(frame.OwnerWindow, Product));
                }
            }
        }

        #endregion

        #endregion
    }
}
