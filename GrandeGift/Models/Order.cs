using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate{ get; set; }
        public double GrandTotal { get; set; }
        public int TotalItems { get; set; }
        public int AddressId { get; set; }
    }
}
