using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class PerformanceRepository : RepositoryBase<Performance>
    {
        public PerformanceRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(Performance item)
        {
            try
            {
                _dataBase.Performances.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Performance_Add");
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
                _dataBase.Performances.Remove(_dataBase.Performances.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Performance_Delete");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.DeleteError);
            }
        }

        public override Performance Get(int id)
        {
            Performance result = null;

            try
            {
                result = _dataBase.Performances.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "Performance_Get");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override List<Performance> GetAll()
        {
            List<Performance> result = null;

            try
            {
                result = _dataBase.Performances.ToList();

                DataBaseManager.Instance.DropSuccess(null, "Performance_GetAll");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override void Update(Performance item)
        {
            try
            {
                Performance edit = _dataBase.Performances.Find(item.Id);

                edit.Dependency = item.Dependency;
                edit.Note = item.Note;
                edit.Value = item.Value;
                edit.Name = item.Name;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Performance_Update");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.UpdateError);
            }
        }
    }
}
