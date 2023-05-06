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

            }
        }

        #endregion

        #endregion
    }
}
