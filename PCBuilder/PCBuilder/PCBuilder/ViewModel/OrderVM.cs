using PCBuilder.Model;
using PCBuilder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.ViewModel
{
    public class OrderVM : BaseViewModel
    {
        private object _owner;

        private Order _order;
        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public string Status { get => $"Status: {_order.Status}"; }
        public string Note { get => _order.Note; }

        public string OpenDate { get => _order.OpenDate.ToString(); }
        public string CloseDate { get => _order.CloseDate.ToString(); }

        public string ItemsCount
        {
            get
            {
                int count = DataBaseManager.Instance.Orders.GetItems(_order).Count;

                return $"Items count: {count}";
            }
        }

        public string Price
        {
            get
            {
                OrderItem[] orderItems = DataBaseManager.Instance.Orders.GetItems(_order).ToArray();

                int total = 0;
                foreach (var item in orderItems)
                {
                    if (item.Template == null)
                        total += item.Product.Price;
                    else
                    {
                        TemplateItem[] templateItems = DataBaseManager.Instance.Templates.GetItems(item.Template).ToArray();

                        foreach (var templateItem in templateItems)
                        {
                            total += templateItem.Product.Price;
                        }
                    }   
                }

                return $"Total price: {total}$";
            }
        }

        public OrderVM(Order item)
        {
            _order = item;
        }
        public OrderVM(Order item, object owner)
        {
            _order = item;
            _owner = owner;
        }
    }
}
