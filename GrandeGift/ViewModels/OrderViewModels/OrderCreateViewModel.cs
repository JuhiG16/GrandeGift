using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class OrderCreateViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double GrandTotal { get; set; }
        public int TotalItems { get; set; }
        public int AddressId { get; set; }
    }
}
