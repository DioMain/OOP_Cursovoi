using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Repositories
{
    public delegate void DataBaseError(string message, DataBaseErrorType errorType);
    public delegate void DataBaseSuccess(object obj, string tag);

    public class DataBaseManager : IDisposable
    {
        public static DataBaseManager Instance { get; private set; }

        private static object _locktemp0, _locktemp1;

        public event Action OnConnected;
        public event DataBaseError OnError;
        public event DataBaseSuccess OnSuccess;

        private DataBase _dataBase;

        public bool IsConnected { get; private set; }

        public UserRepository Users { get; private set; }
        public OrderRepository Orders { get; private set; }
        public OrderItemRepository OrderItems { get; private set; }
        public ProductRepository Products { get; private set; }
        public PerformanceRepository Performances { get; private set; }
        public TemplateRepository Templates { get; private set; }
        public TemplateItemRepository TemplateItems { get; private set; }
        public CommentRepository Comments { get; private set; }

        public static void CreateInstance()
        {
            _locktemp0 = new object();
            _locktemp1 = new object();

            lock (_locktemp0)
            {
                if (Instance == null)
                {
                    lock (_locktemp1)
                    {
                        Instance = new DataBaseManager();
                    }
                }
            }
        }

        public void Connect()
        {
            try
            {
                _dataBase = new DataBase();

                _dataBase.Database.Initialize(false);

                if (_dataBase.Users.FirstOrDefault(i => i.FirstName == "ADMIN") == null)
                {
                    User user = new User()
                    {
                        Id = -1,
                        FirstName = "ADMIN",
                        LastName = "ZERO_USER",
                        LastAuthDate = DateTime.Now,
                        Email = "ADMIN",
                        Address = "ADMIN",
                        Rights = "ADMIN",
                        Password = PasswordHasher.GetHash("ADMIN")
                    };

                    _dataBase.Users.Add(user);

                    _dataBase.SaveChanges();
                }

                Users = new UserRepository(_dataBase);
                Orders = new OrderRepository(_dataBase);
                OrderItems = new OrderItemRepository(_dataBase);
                Products = new ProductRepository(_dataBase);
                Performances = new PerformanceRepository(_dataBase);
                Templates = new TemplateRepository(_dataBase);
                TemplateItems = new TemplateItemRepository(_dataBase);
                Comments = new CommentRepository(_dataBase);

                IsConnected = true;

                OnConnected?.Invoke();
            }
            catch (SystemException Error)
            {
                OnError?.Invoke(Error.Message, DataBaseErrorType.ConnectionFailed);
            }
        }
        public async void ConnectAsync()
        {
            try
            {
                _dataBase = new DataBase();

                // Initialize of database
                string result = await Task.Run(() =>
                {
                    try
                    {
                        _dataBase.Database.Initialize(false);

                        return "Ok";
                    }
                    catch (SystemException Error)
                    {
                        return Error.Message;
                    }
                });

                if (result != "Ok")
                    throw new SystemException(result);

                if (_dataBase.Users.FirstOrDefault(i => i.FirstName == "ADMIN") == null)
                {
                    User user = new User()
                    {
                        Id = -1,
                        FirstName = "ADMIN",
                        LastName = "ZERO_USER",
                        LastAuthDate = DateTime.Now,
                        Email = "ADMIN",
                        Address = "ADMIN",
                        Rights = "ADMIN",
                        Password = PasswordHasher.GetHash("ADMIN")
                    };

                    _dataBase.Users.Add(user);

                    await _dataBase.SaveChangesAsync();
                }

                Users = new UserRepository(_dataBase);
                Orders = new OrderRepository(_dataBase);
                OrderItems = new OrderItemRepository(_dataBase);
                Products = new ProductRepository(_dataBase);
                Performances = new PerformanceRepository(_dataBase);
                Templates = new TemplateRepository(_dataBase);
                TemplateItems = new TemplateItemRepository(_dataBase);
                Comments = new CommentRepository(_dataBase);

                IsConnected = true;

                OnConnected?.Invoke();
            }
            catch (SystemException Error)
            {
                OnError?.Invoke(Error.Message, DataBaseErrorType.ConnectionFailed);
            }
        }

        public void Dispose()
        {
            _dataBase?.Dispose();
        }

        public void DropError(string message, DataBaseErrorType errorType)
        {
            if (errorType == DataBaseErrorType.ConnectionFailed)
                IsConnected = false;

            OnError?.Invoke(message, errorType);
        }
        public void DropSuccess(object obj, string note = "")
        {
            OnSuccess?.Invoke(obj, note);
        }
    }

    public enum DataBaseErrorType
    {
        ConnectionFailed, UpdateError, CreateError, InsertError, DeleteError, GetError
    }
}
