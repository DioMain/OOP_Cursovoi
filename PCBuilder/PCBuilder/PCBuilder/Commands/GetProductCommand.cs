using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Commands
{
    public class GetProductCommand : BaseCommand
    {
        public Product Product { get; set; }

        public GetProductCommand(Action<object> execute, Func<object, bool> canExecute = null) : base(execute, canExecute)
        {
            Product = null;
        }
    }
}
