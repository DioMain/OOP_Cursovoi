using PCBuilder.Commands;
using PCBuilder.CustomUI;
using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PCBuilder.Repositories;
using PCBuilder.View.Popups;
using PCBuilder.Utilities;

namespace PCBuilder.ViewModel
{
    public class TemplateVM : BaseViewModel
    {
        private Template template;
        public Template Template
        {
            get { return template; }
            set 
            { 
                template = value; 
                OnPropertyChanged(nameof(Template));
            }
        }

        private object _owner;

        public string Name { get => template.Name; }
        public string Description { get => template.Description; }
        public string Creator 
        {
            get
            {
                User user = DataBaseManager.Instance.Users.Get(template.CreatorId);

                return $"{user.FirstName} {user.LastName}, {user.Email}";
            } 
        }

        public string Price
        {
            get
            {
                int price = 0;

                foreach (var item in DataBaseManager.Instance.Templates.GetItems(Template))
                {
                    price += item.Product.Price;
                }

                return $"{price}$";
            }
        }

        public TemplateVM(Template template, object owner = null)
        {
            this.template = template;
            this._owner = owner;
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
            if (_owner is TemplateViewerFrameVM)
            {
                FrameworkElement element = (FrameworkElement)obj;

                FrameworkElement but0 = element.FindName("delete") as FrameworkElement;
                FrameworkElement but1 = element.FindName("tobasket") as FrameworkElement;
                FrameworkElement but2 = element.FindName("more") as FrameworkElement;

                
                but1.Style = Application.Current.Resources["CB_DEF"] as Style;
                but2.Style = Application.Current.Resources["CB_DEF"] as Style;

                if (template.CreatorId == User.Current.Id || User.Current.Rights == "ADMIN")
                {
                    but0.Style = Application.Current.Resources["CB_RED"] as Style;
                    but0.Visibility = Visibility.Visible;
                }
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
            if (_owner is TemplateViewerFrameVM templateViewer)
            {
                templateViewer.Delete(this);

                new MessagePopup((string)Application.Current.Resources["Loc_Popup_Global_Success"],
                            (string)Application.Current.Resources["Loc_MesPopup_DelTemplate"], true).ShowDialog();
            }
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
            if (_owner is TemplateViewerFrameVM templateViewer)
            {
                BasketManager.Instance.Add(template);

                new MessagePopup((string)Application.Current.Resources["Loc_Popup_Global_Success"],
                            (string)Application.Current.Resources["Loc_Popup_ProdCart_Added"], true).ShowDialog();
            }
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
            if (_owner is TemplateViewerFrameVM)
            {
                TemplateItemViewerPopup viewerPopup = new TemplateItemViewerPopup(template);

                viewerPopup.ShowDialog();
            }
        }

        #endregion

        #endregion
    }
}
