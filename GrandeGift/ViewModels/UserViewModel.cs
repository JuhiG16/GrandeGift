using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        ICollection<UserRole> UserRoles { get; set; }
        virtual public AdminIndexViewModel Admin { get; set; }
        virtual public CustomerIndexViewModel Customer { get; set; }
    }
}
