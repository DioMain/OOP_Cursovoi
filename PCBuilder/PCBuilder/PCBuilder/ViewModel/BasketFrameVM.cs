using PCBuilder.Commands;
using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.Utilities;
using PCBuilder.View;
using PCBuilder.View.Frames;
using PCBuilder.View.Popups;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PCBuilder.ViewModel
{
    public class BasketFrameVM : FrameViewModel<BasketFrame, MainWindow>
    {
        private List<BasketItemVM> items;
        public List<BasketItemVM> Items { get { return items; } }

        public string TotalPrice
        {
            get
            {
                int total = 0;
                foreach (var item in Items)
                    total += item.PriceInt;

                return $"{total}$";
            }
        }

        public BasketFrameVM(BasketFrame owner, MainWindow window) : base(owner, window)
        {
            SyncData();
        }

        public void SyncData()
        {
            items = new List<BasketItemVM>();

            foreach (var item in BasketManager.Instance.Items)
            {
                items.Add(new BasketItemVM(item, this));
            }

            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void RemoveItem(BasketItemVM item)
        {
            BasketManager.Instance.Remove(item.BasketItem);

            SyncData();
        }

        #region Commands

        #region CreateOrder

        private BaseCommand CreateOrderCommand;
        public ICommand CreateOrder
        {
            get
            {
                if (CreateOrderCommand == null)
                    CreateOrderCommand = new BaseCommand(CreateOrderExecuted);

                return CreateOrderCommand;
            }
        }
        private void CreateOrderExecuted(object obj)
        {
            if (items.Count == 0)
            {
                new MessagePopup("Failure", "Shoping cart is empty!", true).ShowDialog();
                return;
            }

            new MessagePopup("Success", "Order created!", true).ShowDialog();

            Order order = new Order()
            {
                UserId = User.Current.Id,
                Note = Owner.orderNoteBox.Text,
                Status = OrderStatus.Watch.ToString(),
                OpenDate = DateTime.Now,
                CloseDate = DateTime.Now,
            };

            DataBaseManager.Instance.Orders.Add(order);

            order = DataBaseManager.Instance.Users.GetOrders(User.Current).OrderBy(x => x.Id).LastOrDefault();

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in items)
            {
                OrderItem orderItem;
                if (item.BasketItem.IsTemplate)
                {
                    orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        TemplateId = item.BasketItem.Template.Id
                    };
                }
                else
                {
                    orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        ProductId = item.BasketItem.Product.Id
                    };
                }

                DataBaseManager.Instance.OrderItems.Add(orderItem);
            }

            BasketManager.Instance.Clear();

            SyncData();
        }

        #endregion

        #endregion
    }
}
