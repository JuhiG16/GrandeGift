using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using GrandeGift.Models;
using Microsoft.AspNetCore.Identity;

namespace GrandeGift.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDataServices<Customer> _customerServices;
        private readonly UserManager<User> _userManagerServices;
        public CustomerController(IDataServices<Customer> dataServices, UserManager<User> userManagerServices)
        {
            _customerServices = dataServices;
            _userManagerServices = userManagerServices;

        }
        public IActionResult Index()
        {
           return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Test()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Profile(string Username)
        {
           
            User user = await _userManagerServices.FindByNameAsync(Username);
            Customer cust = _customerServices.GetById(user.Id);
            CustomerProflieViewModel vmProfile = new CustomerProflieViewModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                DOB = user.DOB,
                Gender = cust.Gender,
                Address = user.Address
            };
            return View(vmProfile);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(string Username, string ReturnUrl = null)
        {
            User user = await _userManagerServices.FindByNameAsync(Username);
            Customer cust = _customerServices.GetById(user.Id);
            CustomerUpdateProfileViewModel vmProfile = new CustomerUpdateProfileViewModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                DOB = user.DOB,
                Gender = cust.Gender,
                Address = user.Address,
                ReturnUrl = ReturnUrl
            };
            return View(vmProfile);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CustomerUpdateProfileViewModel vmProfile)
        {
            User user = new User
            {

                FirstName = vmProfile.FirstName,
                LastName = vmProfile.LastName,
                Email = vmProfile.Email,
                PhoneNumber = vmProfile.Phone,
                DOB = vmProfile.DOB,
                Address = vmProfile.Address
            };
            //IdentityResult result = await _userManagerServices.UpdateUserAsync(user);
            //if(result.Succeeded)
            //{
            //    if(!String.IsNullOrEmpty(vmProfile.ReturnUrl))
            //    {
            //        return Redirect(vmProfile.ReturnUrl);
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index");
            //    }
            //}
            return View(vmProfile);
        }
    }
}