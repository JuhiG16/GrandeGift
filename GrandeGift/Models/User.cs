using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class User : IdentityUser<int>
    {
       
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        ICollection<UserRole> UserRoles { get; set; }
        //virtual public Admin Admin { get; set; }
        //virtual public Customer Customer { get; set; }


    }
}
