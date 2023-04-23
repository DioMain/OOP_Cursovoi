using PCBuilder.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected DataBase _dataBase;

        public RepositoryBase(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public abstract void Add(T item);
        public abstract void Delete(int id);
        public abstract T Get(int id);
        public abstract List<T> GetAll();

        public abstract void Update(T item);
    }
}
