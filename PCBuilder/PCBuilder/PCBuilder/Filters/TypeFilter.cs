using PCBuilder.Model;
using PCBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Filters
{
    public class TypeFilter : IFilter<ProductVM>
    {
        public object[] Argumests { get; set; }

        public TypeFilter(string typeName)
        {
            Argumests = new object[] { typeName };
        }

        public List<ProductVM> DoFilter(List<ProductVM> values)
        {
            if (Argumests == null || Argumests.Length != 1)
                throw new FilterException("Ne tot filter!", "Type");

            return values.Where(i => i.Product.Type == (string)Argumests[0]).ToList();
        }
    }
}
