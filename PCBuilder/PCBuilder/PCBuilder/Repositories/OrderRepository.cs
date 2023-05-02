﻿using PCBuilder.Model;
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
            catch (SystemException Error)
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
            catch (SystemException Error)
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
            catch (SystemException Error)
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
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }
        public List<OrderItem> GetItems(Order item)
        {
            List<OrderItem> result = null;

            try
            {
                result = _dataBase.OrderItems.Where(i => i.OrderId == item.Id).ToList();

                DataBaseManager.Instance.DropSuccess(null, "Order_GetItems");
            }
            catch (SystemException Error)
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
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }
    }
}
