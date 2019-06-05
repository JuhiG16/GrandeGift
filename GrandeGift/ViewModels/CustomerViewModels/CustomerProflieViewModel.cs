using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.ViewModels;

namespace GrandeGift.ViewModels
{
    public class CustomerProflieViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public List<AddressDetailViewModel> Addresses { get; set; }
    }
}
