using PCBuilder.Model;
using PCBuilder.Repositories;
using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.ViewModel
{
    public class UserFrameVM : FrameViewModel<UserFrame, MainWindow>
    {
        private List<OrderVM> _orders;
        public List<OrderVM> Orders
        {
            get => _orders;
        }

        public string FName { get => User.Current.FirstName; }
        public string LName { get => User.Current.LastName; }
        public string Email { get => User.Current.Email; }
        public string Address { get => User.Current.Address; }
        public string Number { get => User.Current.ContactNumber; }

        public UserFrameVM(UserFrame owner, MainWindow window) : base(owner, window)
        {
            _orders = new List<OrderVM>();

            foreach (var item in DataBaseManager.Instance.Users.GetOrders(User.Current))
            {
                _orders.Add(new OrderVM(item, this));
            }
        }
    }
}
