using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using PCBuilder.Utilities;
using PCBuilder.View.Popups;
using PCBuilder.Repositories;

namespace PCBuilder.ViewModel
{
    public class TemplateEditorFrameVM : FrameViewModel<TemplateEditorFrame, MainWindow>
    {
        private List<TemplateItemVM> _items;
        public List<TemplateItemVM> Items
        {
            get => _items;
        }

        public string TotalPrice
        {
            get
            {
                int price = 0;

                foreach (var item in Items)
                {
                    if (item.Product == null)
                        continue;

                    price += item.Product.Price;
                }

                return $"{price}$"; 
            }
        }

        public TemplateEditorFrameVM(TemplateEditorFrame owner, MainWindow window) : base(owner, window)
        {
            _items = new List<TemplateItemVM>
            {
                new TemplateItemVM(Model.ProductType.Case, this),
                new TemplateItemVM(Model.ProductType.MotherBroad, this),
                new TemplateItemVM(Model.ProductType.CPU, this),
                new TemplateItemVM(Model.ProductType.СoolingSystem, this),
                new TemplateItemVM(Model.ProductType.GPU, this),
                new TemplateItemVM(Model.ProductType.RAM, this),
                new TemplateItemVM(Model.ProductType.Memory, this),
                new TemplateItemVM(Model.ProductType.PowerUnit, this),
            };
        }

        private void DispatchErrors(TemplateValidateError[] errors)
        {

        }
        private void DispatchEmpty()
        {

        }

        private void CreateTemplate()
        {
            Template template = new Template()
            {
                Name = Owner.nameBox.Text,
                Description = Owner.descBox.Text,
                CreatorId = User.Current.Id,
            };

            DataBaseManager.Instance.Templates.Add(template);

            foreach (var item in _items)
            {
                TemplateItem titem = new TemplateItem()
                {
                    TemplateId = template.Id,
                    ProductId = item.Product.Id
                };

                DataBaseManager.Instance.TemplateItems.Add(titem);
            }

            new MessagePopup("Success", "Template created!", true).ShowDialog();

            MainWindowVM mainWindow = (MainWindowVM)OwnerWindow.DataContext;

            if (mainWindow.SectionPage.DataContext is TemplateViewerFrameVM) 
                ((TemplateViewerFrameVM)mainWindow.SectionPage.DataContext).SyncData();

            mainWindow.PopSubPage();
        }

        #region Commands

        #region Init

        private BaseCommand SubmitCommand;
        public ICommand Submit
        {
            get => SubmitCommand ?? (SubmitCommand = new BaseCommand(SubmitExecuted));
        }
        private void SubmitExecuted(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Owner.nameBox.Text))
                    throw new ApplicationException("Name field is empty!");


                foreach (var item in _items)
                {
                    if (item.Product == null)
                    {
                        DispatchEmpty();
                        throw new ApplicationException("Have empty slots!");
                    }
                }

                List<Product> products = new List<Product>();

                foreach (var item in _items)
                    products.Add(item.Product);

                TemplateValidator validator = new TemplateValidator(products);

                List<TemplateValidateError> errors = validator.Validate();

                if (errors.Count > 0)
                {
                    DispatchErrors(errors.ToArray());
                    throw new ApplicationException("Incompatible Components!");
                }

                CreateTemplate();
            }
            catch (ApplicationException error)
            {
                new MessagePopup("Validate error", error.Message, true).ShowDialog();
            }
        }

        #endregion

        #endregion
    }
}
