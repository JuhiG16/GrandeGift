using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GrandeGift.Controllers
{
    public class AddressController : Controller
    {
        private readonly IDataServices<Address> _addressServices;
        private readonly UserManager<User> _userManagerServices;

        public AddressController(UserManager<User> userManagerServices,
                                 IDataServices<Address> addressServices)
        {
            _userManagerServices = userManagerServices;
            _addressServices = addressServices;
        }
        private async Task<IEnumerable<Address>> GetAddress()
        {

            string userName = User.Identity.Name;
            User user = await _userManagerServices.FindByNameAsync(userName);
            return _addressServices.Query(a => a.UserId == user.Id);
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Address> adds = await GetAddress();
            IEnumerable<AddressIndexViewModel> vm = adds.Select(a => new AddressIndexViewModel
            {
                //Id = a.Id,
                Details = a.Details,
                UserId = a.UserId
            });
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        private async Task<int> GetCustomerId()
        {
            string userName = User.Identity.Name;
            User user = await _userManagerServices.FindByNameAsync(userName);

            return user.Id;

        }
        [HttpPost]
        public async Task<IActionResult> Create(AddressCreateViewModel vmCreate)
        {
            int userId = await GetCustomerId();
            Address add = new Address
            {
                Details = vmCreate.Details,
                UserId = userId
            };
            _addressServices.Add(add);
            return RedirectToAction("Index");
        }
    }
}