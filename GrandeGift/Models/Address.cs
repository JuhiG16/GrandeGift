using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Address
    {
        public int  AddressId { get; set; }
        [Required(ErrorMessage = "Address is required and must include city and post code.")]
        public string Details { get; set; }
        public int UserId { get; set; }
    }
}
