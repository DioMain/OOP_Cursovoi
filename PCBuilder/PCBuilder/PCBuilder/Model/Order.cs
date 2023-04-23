using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Status { get; set; }
        [NotMapped]
        public OrderStatus StatusType
        {
            get
            {
                OrderStatus result;

                bool isParsed = Enum.TryParse(Status, true, out result);

                return isParsed ? result : OrderStatus.Closed;
            }
        }

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
    }

    public enum OrderStatus
    {
        Wait, Watch, Dilivery, Closed, Canceled
    }
}
