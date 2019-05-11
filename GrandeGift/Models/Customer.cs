using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        ICollection<Address> Addresses { get; set; }
    }
}
