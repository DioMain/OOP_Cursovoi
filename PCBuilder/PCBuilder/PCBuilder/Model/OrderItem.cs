﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }      
        public Order Order { get; set; }

        [ForeignKey("Template")]
        public int? TemplateId { get; set; }      
        public Template Template { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }      
        public Product Product { get; set; }
    }
}
