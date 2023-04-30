﻿using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public class TemplateRepository : RepositoryBase<Template>
    {
        public TemplateRepository(DataBase dataBase) : base(dataBase)
        {
        }

        public override void Add(Template item)
        {
            try
            {
                _dataBase.Templates.Add(item);

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Template_Add");
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
                _dataBase.Templates.Remove(_dataBase.Templates.First(i => i.Id == id));

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Template_Delete");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.DeleteError);
            }
        }

        public override Template Get(int id)
        {
            Template result = null;

            try
            {
                result = _dataBase.Templates.Find(id);

                DataBaseManager.Instance.DropSuccess(null, "Template_Get");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override List<Template> GetAll()
        {
            List<Template> result = null;

            try
            {
                result = _dataBase.Templates.ToList();

                DataBaseManager.Instance.DropSuccess(null, "Template_GetAll");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.GetError);
            }

            return result;
        }

        public override void Update(Template item)
        {
            try
            {
                Template edit = _dataBase.Templates.Find(item.Id);

                edit.CreateId = item.CreateId;
                edit.Description = item.Description;
                edit.Name = item.Name;

                _dataBase.SaveChanges();

                DataBaseManager.Instance.DropSuccess(null, "Template_Update");
            }
            catch (SystemException Error)
            {
                DataBaseManager.Instance.DropError(Error.Message, DataBaseErrorType.UpdateError);
            }
        }
    }
}
