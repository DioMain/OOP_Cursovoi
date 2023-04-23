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
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }

        public override void Delete(int id)
        {
            try
            {
                _dataBase.Products.Remove(_dataBase.Products.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Product_Delete");
            }
            catch (DbException Error)
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
            catch (DbException Error)
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
            catch (DbException Error)
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
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.UpdateError);
            }
        }
    }
}
