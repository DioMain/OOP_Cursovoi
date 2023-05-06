
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Model
{
    [Serializable]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        public string ImageUrl { get; set; }

        public int Price { get; set; }

        public string Type { get; set; }
        [NotMapped]
        public ProductType ProductType
        {
            get
            {
                ProductType result;

                bool isParsed = Enum.TryParse(Type, true, out result);

                return isParsed ? result : ProductType.Unknown;
            }
        }
    }

    public enum ProductType
    {
        Unknown, CPU, GPU, RAM, Memory, Case, PowerUnit, MotherBroad, СoolingSystem
    }
}
