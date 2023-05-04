using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.Utilites;
using PCBuilder.View;
using PCBuilder.View.Frames;
using PCBuilder.View.Popups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class ProductEditorFrameVM : FrameViewModel<ProductEditorFrame, MainWindow>
    {
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

        private Product _product;

        private string imageUrl;
        public string ImageUrl 
        { 
            get => imageUrl;
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(imageUrl));
            }
        }

        public string[] Types
        {
            get => Enum.GetNames(typeof(ProductType));
        }

        public ProductEditorFrameVM(ProductEditorFrame owner, MainWindow window, Product product = null) 
            : base(owner, window)
        {
            _performances = new List<PerformanceVM>();

            Owner.typeCombo.SelectedIndex = 0;

            _product = product;

            ImageUrl = "[NULL]";

            if (product != null)
                UpdateProduct(product);

            SyncData();
        }

        private void SyncData()
        {
            Owner.list.Items.Refresh();
        }

        private void UpdateProduct(Product product)
        {
            Owner.nameBox.Text = product.Name;
            Owner.manufacturerBox.Text = product.Manufacturer;
            Owner.shortBox.Text = product.ShortDescription;
            Owner.fullBox.Text = product.FullDescription;
            Owner.priceBox.Text = product.Price.ToString();
            ImageUrl = product.ImageUrl;

            for (int i = 0; i < Types.Length; i++)
            {
                if (Types[i] == product.Type)
                {
                    Owner.typeCombo.SelectedIndex = i;
                    break;
                }
            }

            foreach (var item in DataBaseManager.Instance.Products.GetPerformances(product))
            {
                _performances.Add(new PerformanceVM(item, this));
            }
        }

        public void DeleteProp(PerformanceVM performance)
        {
            _performances.Remove(performance);

            SyncData();
        }
        public void EditProp(PerformanceVM performance)
        {
            PerformanceEditorPopup editorPopup = new PerformanceEditorPopup(performance.Performance);

            editorPopup.ShowDialog();

            Performance result = editorPopup.GetResult();

            _performances[_performances.IndexOf(performance)] = new PerformanceVM(result, this);

            SyncData();
        }
        public void CreateProp()
        {
            PerformanceEditorPopup editorPopup = new PerformanceEditorPopup();

            editorPopup.ShowDialog();

            Performance result = editorPopup.GetResult();

            if (result != null)
                _performances.Add(new PerformanceVM(result, this));

            SyncData();
        }

        #region Commands

        #region Submit

        private BaseCommand SubmitCommand;
        public ICommand Submit
        {
            get => SubmitCommand ?? (SubmitCommand = new BaseCommand(SubmitExecuted));
        }
        private void SubmitExecuted(object obj)
        {
            try
            {
                if (_product == null)
                {
                    _product = new Product()
                    {
                        Name = Owner.nameBox.Text,
                        ShortDescription = Owner.shortBox.Text,
                        FullDescription = Owner.fullBox.Text,
                        Type = Owner.typeCombo.Text,
                        Price = int.Parse(Owner.priceBox.Text),
                        ImageUrl = Owner.imgText.Text,
                        Manufacturer = Owner.manufacturerBox.Text,
                    };

                    DataBaseManager.Instance.Products.Add(_product);
                }
                else
                {
                    _product.Name = Owner.nameBox.Text;
                    _product.ShortDescription = Owner.shortBox.Text;
                    _product.FullDescription = Owner.fullBox.Text;
                    _product.Type = Owner.typeCombo.Text;
                    _product.Price = int.Parse(Owner.priceBox.Text);
                    _product.ImageUrl = Owner.imgText.Text;
                    _product.Manufacturer = Owner.manufacturerBox.Text;

                    DataBaseManager.Instance.Products.Update(_product);
                }

                foreach (var item in _performances)
                {
                    item.Performance.ProductId = _product.Id;

                    if (item.Performance.Id == 0)
                        DataBaseManager.Instance.Performances.Add(item.Performance);
                    else
                        DataBaseManager.Instance.Performances.Update(item.Performance);
                }

                new MessagePopup("Success", "Product added!").ShowDialog();

                ((MainWindowVM)OwnerWindow.DataContext).ChangeSection(5);
            }
            catch (SystemException error)
            {
                new MessagePopup("Error", error.Message).ShowDialog();
            }
        }

        #endregion

        #region AddProp

        private BaseCommand AddPropCommand;
        public ICommand AddProp
        {
            get => AddPropCommand ?? (AddPropCommand = new BaseCommand(AddPropExecuted));
        }
        private void AddPropExecuted(object obj)
        {
            CreateProp();
        }

        #endregion

        #region SetImage

        private BaseCommand SetImageCommand;
        public ICommand SetImage
        {
            get => SetImageCommand ?? (SetImageCommand = new BaseCommand(SetImageExecuted));
        }
        private void SetImageExecuted(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "PNG (*.png)|*.png| JPEG (*.jpeg)|*.jpeg| JPG (*.jpg)|*.jpg| IMG (*.img)|*.img";

            openFileDialog.ShowDialog();

            if (!(openFileDialog.CheckFileExists 
                && openFileDialog.CheckPathExists
                && openFileDialog.FileName != string.Empty))
            {
                ImageUrl = "[NULL]";

                return;
            }

            ImageUrl = ImageManager.SaveImage(openFileDialog.FileName);
        }

        #endregion

        #endregion
    }
}
