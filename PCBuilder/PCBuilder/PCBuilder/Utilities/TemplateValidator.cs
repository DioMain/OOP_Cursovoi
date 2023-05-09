using PCBuilder.Model;
using PCBuilder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace PCBuilder.Utilities
{
    public class TemplateValidator
    {
        private List<Product> products;

        public TemplateValidator(List<Product> products)
        {
            this.products = products;
        }

        /// <summary>
        /// Проверяет комплектующие на совместимость
        /// </summary>
        /// <returns>Список из ошибок проверки, если пустой значить сборка совместима</returns>
        public List<TemplateValidateError> Validate()
        {
            List<Performance> mainPerformances = new List<Performance>();
            List<Performance> childPerformances = new List<Performance>();

            List<TemplateValidateError> errors = new List<TemplateValidateError>();

            foreach (Product product in products)
            {
                foreach (var item in DataBaseManager.Instance.Products.GetPerformances(product))
                {
                    switch (item.DependencyType)
                    {
                        case PerformanceDependency.Main:
                            mainPerformances.Add(item);
                            break;
                        case PerformanceDependency.Child:
                            childPerformances.Add(item);
                            break;
                    }
                }
            }

            foreach (var item in mainPerformances)
            {
                foreach (var item2 in childPerformances)
                {
                    if (item.Tag == item2.Tag)
                    {
                        if (item.Value != item2.Value)
                            errors.Add(
                                new TemplateValidateError($"{Application.Current.Resources["Loc_TempEdit_ValidateErrMesTemp"]} \"{item.Name}\"!", item2.Product.ProductType));
                    }
                }
            }

            return errors;
        }
    }

    public struct TemplateValidateError
    {
        public string Message { get; private set; }

        public ProductType ProductType { get; private set; }

        public TemplateValidateError(string message, ProductType productType)
        {
            Message = message;
            ProductType = productType;
        }
    }
}
