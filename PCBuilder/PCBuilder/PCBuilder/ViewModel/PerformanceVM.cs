using PCBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.ViewModel
{
    public class PerformanceVM : BaseViewModel
    {
        private Performance _performance;

        public string Name { get => _performance.Name; }
        public string Value { get => _performance.Value; }
        public string Note { get => string.IsNullOrWhiteSpace(_performance.Note) ? "" : $"({_performance.Note})"; }
        public string Dependecy { get => _performance.Dependency; }

        public PerformanceVM(Performance performance)
        {
            _performance = performance;
        }
    }
}
