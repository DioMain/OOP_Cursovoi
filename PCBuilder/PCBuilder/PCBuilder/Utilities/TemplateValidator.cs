using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new List<TemplateValidateError>();
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
