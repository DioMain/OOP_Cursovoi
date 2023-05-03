using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Filters
{
    public class FilterException : ApplicationException
    {
        public string FilterName { get; private set; }

        public FilterException(string message, string filterName) : base(message)
        {
            FilterName = filterName;
        }
    }
}
