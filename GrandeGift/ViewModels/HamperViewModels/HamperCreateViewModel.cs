using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GrandeGift.Models;
using GrandeGift.Utility;
using Microsoft.AspNetCore.Http;

namespace GrandeGift.ViewModels
{
 
    public class HamperCreateViewModel
    {
        [Required(ErrorMessage = "Please provide hamper name")]
        [Display(Name = "Hamper Name")]
        [StringLength(256, ErrorMessage = "Hamper Name is too large")]
        [RegularExpression(@"^[a-zA-Z0-9]{2,}$", ErrorMessage = "First can contain only alphanumeric")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter hamper details")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        [RegularExpression(@"^[0-9.]{1,9}")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryDetailViewModel Category { get; set; }
        public bool IsContinue { get; set; }
        public Status Status { get; set; }
        public IFormFile Photo { get; set; }
    }
}
