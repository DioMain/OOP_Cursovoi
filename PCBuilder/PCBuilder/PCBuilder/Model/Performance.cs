using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Model
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }    
        public Product Product { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }

        public string Dependency { get; set; }
        [NotMapped]
        public PerformanceDependency DependencyType
        {
            get
            {
                PerformanceDependency result;

                bool isParsed = Enum.TryParse(Dependency, true, out result);

                return isParsed ? result : PerformanceDependency.None;
            }
        }
    }

    public enum PerformanceDependency
    {
        None, Main, Child
    }
}
