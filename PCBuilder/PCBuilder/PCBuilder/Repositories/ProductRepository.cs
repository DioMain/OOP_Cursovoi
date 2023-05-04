using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(Product item)
        {
            try
            {
                _dataBase.Products.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Product_Add");
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
                foreach (var item in DataBaseManager.Instance.Performances.GetAll().Where(i => i.ProductId == id))
                {
                    DataBaseManager.Instance.Performances.Delete(item.Id);
                }

                foreach (var item in DataBaseManager.Instance.OrderItems.GetAll().Where(i => i.ProductId == id))
                {
                    DataBaseManager.Instance.OrderItems.Delete(item.Id);
                }

                Template[] templates = DataBaseManager.Instance.Templates.GetAll().Where(i =>
                {
                    foreach (var item in DataBaseManager.Instance.Templates.GetItems(i))
                    {
                        if (item.ProductId == id)
                            return true;
                    }

                    return false;
                }).ToArray();

                foreach (var template in templates)
                {
                    DataBaseManager.Instance.Templates.Delete(template.Id);
                }

                _dataBase.Products.Remove(_dataBase.Products.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Product_Delete");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.DeleteError);
            }
        }

        public override Product Get(int id)
        {
            Product result = null;

            try
            {
                result = _dataBase.Products.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "Product_Get");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override List<Product> GetAll()
        {
            List<Product> result = null;

            try
            {
                result = _dataBase.Products.ToList();

                DataBaseManager.Instance.DropSuccess(null, "Product_GetAll");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public List<Performance> GetPerformances(Product item)
        {
            List<Performance> result = null;

            try
            {
                result = _dataBase.Performances.Where(i => i.ProductId == item.Id).ToList();

                DataBaseManager.Instance.DropSuccess(null, "Product_GetPerformances");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override void Update(Product item)
        {
            try
            {
                Product edit = _dataBase.Products.Find(item.Id);

                edit.Manufacturer = item.Manufacturer;
                edit.FullDescription = item.FullDescription;
                edit.ShortDescription = item.ShortDescription;
                edit.Price = item.Price;
                edit.Name = item.Name;
                edit.Type = item.Type;
                edit.ImageUrl = item.ImageUrl;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Product_Update");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.UpdateError);
            }
        }
    }
}
