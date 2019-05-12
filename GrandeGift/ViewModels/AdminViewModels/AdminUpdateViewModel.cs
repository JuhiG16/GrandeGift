using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class AdminUpdateViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public UserViewModel User { get; set; }
    }
}
