using GrandeGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CartIndexViewModel
    {
        public List<Hamper> CartItems { get; set; }
        public int TotalItems { get; set; }
        public double Price { get; set; }
        public double GrossTotal { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
