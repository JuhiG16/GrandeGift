using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GrandeGift.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Display(Name="Category Name")]
        public string Name { get; set; }
        [StringLength(maximumLength:300)]
        public string Description { get; set; }
        public int CategoryID { get; set; }
    }
}
