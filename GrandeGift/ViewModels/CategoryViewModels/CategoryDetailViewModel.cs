using GrandeGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CategoryDetailViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
    }
}
