using PCBuilder.Model;
using PCBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Filters
{
    public class PriceFilter : IFilter<ProductVM>
    {
        public object[] Argumests { get; set; }

        public PriceFilter(int a, int b)
        {
            Argumests = new object[] { a, b };
        }

        public List<ProductVM> DoFilter(List<ProductVM> values)
        {
            if (Argumests == null || Argumests.Length != 2)
                throw new FilterException("Nado 2", "Price");

            int a = (int)Argumests[0], b = (int)Argumests[1];

            return values.Where(x => x.Product.Price >= a && x.Product.Price <= b).ToList();
        }
    }
}
