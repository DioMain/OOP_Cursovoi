using PCBuilder.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PCBuilder.ViewModel;
using PCBuilder.Repositories;

namespace PCBuilder.Utilities
{
    public class BasketManager : IDisposable
    {
        public const string BasketsFolder = "Baskets";

        private static object _temp0;
        private static object _temp1;

        public static BasketManager Instance;

        private JsonSerializerOptions _serializer;

        private List<BasketItem> items;
        public List<BasketItem> Items { get { return items; } }

        public string BasketPath { get => Path.Combine(BasketsFolder, $"{OwnerName}.json"); }

        public string OwnerName { get; private set; }
        
        public static void CreateInstance(string userName)
        {
            _temp0 = new object();
            _temp1 = new object();

            lock (_temp0)
            {
                if (Instance == null)
                {
                    lock (_temp1)
                    {
                        Instance = new BasketManager(userName);
                    }
                }
            }
        }

        public BasketManager(string userName)
        {
            if (!Directory.Exists(BasketsFolder))
                Directory.CreateDirectory(BasketsFolder);

            items = new List<BasketItem>();

            _serializer = new JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true,
            };

            OwnerName = userName.Replace('.', 'D');

            LoadBasket();
        }

        private void LoadBasket()
        {
            if (!File.Exists(BasketPath))
                return;

            string json = File.ReadAllText(BasketPath);

            items = JsonSerializer.Deserialize<List<BasketItem>>(json, _serializer);

            CheckItem();
        }
        private void SaveBasket()
        {
            if (File.Exists(BasketPath))
                File.Delete(BasketPath);

            string json = JsonSerializer.Serialize(items, typeof(List<BasketItem>), _serializer);

            File.WriteAllText(BasketPath, json);
        }

        public void CheckItem()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsTemplate)
                {
                    if (DataBaseManager.Instance.Templates.Get(items[i].Template.Id) == null)
                    {
                        items.Remove(items[i]);
                        i--;
                    }             
                }
                else
                {
                    if (DataBaseManager.Instance.Products.Get(items[i].Product.Id) == null)
                    {
                        items.Remove(items[i]);
                        i--;
                    }
                }
            }
        }

        public void Clear() => items.Clear();

        public void Add(Product product) => items.Add(new BasketItem(product));
        public void Add(BasketItem item) => items.Add(item);
        public void Add(Template template) => items.Add(new BasketItem(template));

        public void Remove(Product product) => items.Remove(items.First(i => i.Product == product));
        public void Remove(Template template) => items.Remove(items.First(i => i.Template == template));
        public void Remove(BasketItem item) => items.Remove(item);

        public void Dispose()
        {
            SaveBasket();
        }
    }

    [Serializable]
    public struct BasketItem
    {
        public bool IsTemplate { get; set; }

        public Product Product { get; set; }
        public Template Template { get; set; }

        public BasketItem(Product product)
        {
            Product = product;
            Template = null;

            IsTemplate = false;
        }
        public BasketItem(Template template)
        {
            Product = null;
            Template = template;

            IsTemplate = true;
        }
    }
}
