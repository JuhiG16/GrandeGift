using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        //public int Id { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
