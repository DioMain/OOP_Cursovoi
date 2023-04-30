using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class TemplateItemRepository : RepositoryBase<TemplateItem>
    {
        public TemplateItemRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(TemplateItem item)
        {
            try
            {
                _dataBase.TemplateItems.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "TemplateItem_Add");
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
                _dataBase.TemplateItems.Remove(_dataBase.TemplateItems.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "TemplateItem_Delete");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.DeleteError);
            }
        }

        public override TemplateItem Get(int id)
        {
            TemplateItem result = null;

            try
            {
                result = _dataBase.TemplateItems.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "TemplateItem_Get");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override List<TemplateItem> GetAll()
        {
            List<TemplateItem> result = null;

            try
            {
                result = _dataBase.TemplateItems.ToList();

                DataBaseManager.Instance.DropSuccess(null, "TemplateItem_GetAll");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override void Update(TemplateItem item)
        {
            try
            {
                TemplateItem edit = _dataBase.TemplateItems.Find(item.Id);

                edit.TemplateId = item.TemplateId;
                edit.ProductId = item.ProductId;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "TemplateItem_Update");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.UpdateError);
            }
        }
    }
}
