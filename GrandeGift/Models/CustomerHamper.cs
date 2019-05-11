using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class CustomerHamper
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int HamperId { get; set; }
        public int Quantity { get; set; }
        public double ItemTotal { get; set; }
        public int OrderId { get; set; }
    }
}
