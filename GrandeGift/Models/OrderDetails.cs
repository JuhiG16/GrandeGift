using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int HamperId { get; set; }
        public string OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
