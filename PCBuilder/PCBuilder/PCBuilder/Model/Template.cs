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
    public class Template
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int CreatorId { get; set; }   
        public User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
