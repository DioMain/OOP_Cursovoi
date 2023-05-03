using PCBuilder.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PCBuilder.ViewModel;

namespace PCBuilder.Utilities
{
    public class BasketManager : IDisposable
    {
        public const string BasketsFolder = "Baskets";

        private static object _temp0;
        private static object _temp1;

        public static BasketManager Instance;

        private JsonSerializerOptions _serializer;

        private List<Product> products;
        public List<Product> Products { get { return products; } }

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

            products = new List<Product>();

            _serializer = new JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true
            };

            OwnerName = userName.Replace('.', 'D');

            LoadBasket();
        }

        private void LoadBasket()
        {
            if (!File.Exists(BasketPath))
                return;

            string json = File.ReadAllText(BasketPath);

            products = JsonSerializer.Deserialize<List<Product>>(json, _serializer);
        }
        private void SaveBasket()
        {
            if (File.Exists(BasketPath))
                File.Delete(BasketPath);

            string json = JsonSerializer.Serialize(products, typeof(List<Product>), _serializer);

            File.WriteAllText(BasketPath, json);
        }

        public void AddProduct(Product product) => products.Add(product);
        public void RemoveProduct(Product product) => products.Remove(product);

        public void Dispose()
        {
            SaveBasket();
        }
    }
}
