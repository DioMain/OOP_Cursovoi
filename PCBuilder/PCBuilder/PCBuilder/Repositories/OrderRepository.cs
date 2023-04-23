using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(Order item)
        {
            try
            {
                _dataBase.Orders.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Order_Add");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }

        public override void Delete(int id)
        {
            try
            {
                _dataBase.Orders.Remove(_dataBase.Orders.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Order_Delete");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }

        public override Order Get(int id)
        {
            Order result = null;

            try
            {
                result = _dataBase.Orders.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "Order_Get");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }

        public override List<Order> GetAll()
        {
            List<Order> result = null;

            try
            {
                result = _dataBase.Orders.ToList();

                DataBaseManager.Instance.DropSuccess(null, "Order_GetAll");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }

        public override void Update(Order item)
        {
            try
            {
                Order edit = _dataBase.Orders.Find(item.Id);

                edit.User = item.User;
                edit.UserId = item.UserId;
                edit.Status = item.Status;
                edit.CloseDate = item.CloseDate;
                edit.OpenDate = item.OpenDate;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Order_Update");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }
    }
}
