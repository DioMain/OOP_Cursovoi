using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Filters
{
    public interface IFilter<T>
    {
        object[] Argumests { get; set; }

        List<T> DoFilter(List<T> values);
    }
}
