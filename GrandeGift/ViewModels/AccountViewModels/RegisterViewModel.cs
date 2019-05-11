using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class RegisterViewModel
    {
        [RegularExpression(@"^[a-zA-Z]{2,}$", ErrorMessage = "First Name can contain only alphabets and should have min 2 letters.")]
        public string testNAme1 { get; set; }
        [Required(ErrorMessage = "Please provide first Name.")]
        [StringLength(256, ErrorMessage = "FirstName is very large.")]
        [RegularExpression(@"^[a-zA-Z]{2,}$", ErrorMessage = "First Name can contain only alphabets and should have min 2 letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last Name.")]
        [StringLength(256, ErrorMessage = "LastName is very large.")]
        [RegularExpression(@"^[a-z/.A-Z]{1,}$", ErrorMessage = "Last Name can contain only alphabets and should have min 2 letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provide email address.")]
        [RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide phone number.")]
        [RegularExpression(@"^(\+61-|0)[0-9]{9,10}$")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please provide your DOB")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DateRange("01/01/1900", ErrorMessage = "Date must be between 01/01/2000 and today's date.")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please provide your Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please provide your UserName")]
        [RegularExpression(@"[a-zA-z0-9]{3,}", ErrorMessage = "User name can only contain alphanumerics")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide password.")]
        //[RegularExpression(@"[^\s][a-zA-Z0-9@,#,$,%,&,_]{8,}[^\s]"
        //[RegularExpression(@"([a-z])+[A-Z]+[0-9]+[@#$%&_]")]
        [RegularExpression(@"^(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&_])(?!.*\s).*$", ErrorMessage = "Password must have:\n at least one uppercase, " +
           "letter.\n at least one lower case letter, \n Any special symbols from @,#,$,%,&,_" +
            " \n And at least one digit.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password diesn't match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "City is mendatory.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Country is mendatory.")]
        public int CountryId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
