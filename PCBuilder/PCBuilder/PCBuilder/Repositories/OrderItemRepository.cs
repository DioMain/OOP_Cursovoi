using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem>
    {
        public OrderItemRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(OrderItem item)
        {
            try
            {
                _dataBase.OrderItems.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "OrderItem_Add");
            }   
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }

        public override void Delete(int id)
        {
            try
            {
                _dataBase.OrderItems.Remove(_dataBase.OrderItems.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "OrderItem_Delete");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }

        public override OrderItem Get(int id)
        {
            OrderItem result = null;

            try
            {
                result = _dataBase.OrderItems.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "OrderItem_Get");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }

        public override List<OrderItem> GetAll()
        {
            List<OrderItem> result = null;

            try
            {
                result = _dataBase.OrderItems.ToList();

                DataBaseManager.Instance.DropSuccess(null, "OrderItem_GetAll");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }

        public override void Update(OrderItem item)
        {
            try
            {
                OrderItem edit = _dataBase.OrderItems.Find(item.Id);

                edit.OrderId = item.OrderId;
                edit.ProductId = item.ProductId;
                edit.TemplateId = item.TemplateId;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "OrderItem_Update");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }
    }
}
