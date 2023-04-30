using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(User item)
        {
            try
            {
                _dataBase.Users.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "User_Add");
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
                _dataBase.Users.Remove(_dataBase.Users.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "User_Delete");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }

        public override User Get(int id)
        {
            User result = null;

            try
            {
                result = _dataBase.Users.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "User_Get");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }
        public User GetByEmail(string email)
        {
            User result = null;

            try
            {
                result = _dataBase.Users.First(i => i.Email == email);

                DataBaseManager.Instance.DropSuccess(null, "User_GetByEmail");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }

        public override List<User> GetAll()
        {
            List<User> result = null;

            try
            {
                result = _dataBase.Users.ToList();

                DataBaseManager.Instance.DropSuccess(null, "User_GetAll");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }

            return result;
        }

        public override void Update(User item)
        {
            try
            {
                User edit = _dataBase.Users.Find(item.Id);

                edit.Address = item.Address;
                edit.Password = item.Password;
                edit.Email = item.Email;
                edit.FirstName = item.FirstName;
                edit.LastName = item.LastName;
                edit.LastAuthDate = item.LastAuthDate;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "User_Update");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.CreateError);
            }
        }
    }
}
