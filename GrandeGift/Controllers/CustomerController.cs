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
using GrandeGift.Utility;

namespace GrandeGift.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UserManager<User> _userManagerServices;
        private readonly IDataServices<Customer> _customerServices;
        private readonly IDataServices<Hamper> _hamperServices;
        

        public CustomerController(IDataServices<Customer> dataServices, 
                                  UserManager<User> userManagerServices,
                                  IDataServices<Hamper> hamperServices)
        {
            _customerServices = dataServices;
            _userManagerServices = userManagerServices;
            _hamperServices = hamperServices;
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Index()
        {
            return View();
           //List<Hamper> hampers =  _hamperServices.Query(h => (h.Status == Status.Available ||
           //                                                          h.Status == Status.OutOfStock), 
           //                                                          "Category").ToList();
           // List<HamperIndexViewModel> vmHampers = hampers.Select(h => new HamperIndexViewModel
           // {
           //     Id = h.Id,
           //     Name = h.Name,
           //     Details = h.Details,
           //     Price = h.Price,
           //     Status = h.Status,
           //     CategoryName = h.Category.Name,
           //     Category = h.Category,
           //     PhotoPath = h.PhotoPath
           // }).ToList();
           //return View(vmHampers);
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
            Customer cust = _customerServices.GetSingle( c => c.CustomerId ==   user.Id);
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
            Customer cust = _customerServices.GetSingle(c => c.CustomerId == user.Id);
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
            User user = await _userManagerServices.FindByNameAsync(vmProfile.Username);
            user.FirstName = vmProfile.FirstName;
            user.LastName = vmProfile.LastName;
            user.Email = vmProfile.Email;
            user.PhoneNumber = vmProfile.Phone;
            user.DOB = vmProfile.DOB;
            user.Address = vmProfile.Address;
         
            IdentityResult result = await _userManagerServices.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!String.IsNullOrEmpty(vmProfile.ReturnUrl))
                {
                    return Redirect(vmProfile.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(vmProfile);
        }
    }
}