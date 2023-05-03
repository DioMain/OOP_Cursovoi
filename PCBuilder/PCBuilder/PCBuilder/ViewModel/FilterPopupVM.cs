using PCBuilder.Commands;
using PCBuilder.Filters;
using PCBuilder.Model;
using PCBuilder.View.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class FilterPopupVM : WindowViewModel<FilterPopup>
    {
        public List<IFilter<ProductVM>> filters { get; private set; }

        public FilterPopupVM(FilterPopup owner) : base(owner)
        {
            filters = new List<IFilter<ProductVM>>();

            foreach (var item in Enum.GetNames(typeof(ProductType)))
            {
                owner.combo.Items.Add(item);
            }

            owner.combo.SelectedIndex = 0;
        }
        public FilterPopupVM(FilterPopup owner, List<IFilter<ProductVM>> current) : base(owner)
        {
            filters = new List<IFilter<ProductVM>>();

            foreach (var item in Enum.GetNames(typeof(ProductType)))
            {
                owner.combo.Items.Add(item);
            }

            owner.combo.SelectedIndex = 0;

            foreach (var item in current)
            {
                if (item is PriceFilter)
                {
                    owner.contFrom.Text = item.Argumests[0].ToString();
                    owner.contTo.Text = item.Argumests[1].ToString();
                }
                if (item is TypeFilter)
                {
                    for (int i = 0; i < owner.combo.Items.Count; i++)
                    {
                        if ((string)owner.combo.Items[i] == (string)item.Argumests[0])
                        {
                            owner.combo.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        #region Commands

        private BaseCommand SubmitCommand;
        public ICommand Submit
        {
            get
            {
                if (SubmitCommand == null)
                    SubmitCommand = new BaseCommand(DSubmitExecuted);

                return SubmitCommand;
            }
        }
        private void DSubmitExecuted(object obj)
        {
            if (!string.IsNullOrEmpty(Owner.contFrom.Text) && !string.IsNullOrEmpty(Owner.contTo.Text))
            {
                filters.Add(new PriceFilter(int.Parse(Owner.contFrom.Text), int.Parse(Owner.contTo.Text)));
            }

            if ((string)Owner.combo.SelectedItem != "Unknown")
            {
                filters.Add(new TypeFilter((string)Owner.combo.SelectedItem));
            }

            Owner.Close();
        }

        #endregion
    }
}
