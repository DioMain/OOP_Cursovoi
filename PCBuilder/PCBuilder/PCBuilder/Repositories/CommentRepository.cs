using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>
    {
        public CommentRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(Comment item)
        {
            try
            {
                _dataBase.Comments.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Comment_Add");
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
                _dataBase.Comments.Remove(_dataBase.Comments.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Comment_Delete");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.DeleteError);
            }
        }

        public override Comment Get(int id)
        {
            Comment result = null;

            try
            {
                result = _dataBase.Comments.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "Comment_Get");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override List<Comment> GetAll()
        {
            List<Comment> result = null;

            try
            {
                result = _dataBase.Comments.ToList();

                DataBaseManager.Instance.DropSuccess(null, "Comment_GetAll");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override void Update(Comment item)
        {
            try
            {
                Comment edit = _dataBase.Comments.Find(item.Id);

                edit.SendDateTime = item.SendDateTime;
                edit.UserId = item.UserId;
                edit.ProductId = item.ProductId;
                edit.Message = item.Message;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Comment_Update");
            }
            catch (DbException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.UpdateError);
            }
        }
    }
}
