using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using GrandeGift.Models;
using GrandeGift.Utility;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GrandeGift.Controllers
{
    public class CartController : Controller
    {
        private readonly IDataServices<Hamper> _hamperService;
        private readonly UserManager<User> _userManagerServices;
        private readonly IDataServices<Address> _addressService;

        public CartController(IDataServices<Hamper> hamperService,
                                UserManager<User> userManagerServices,
                                IDataServices<Address> addressService)
        {
            _hamperService = hamperService;
            _userManagerServices = userManagerServices;
            _addressService = addressService;
        }
        public async Task<IActionResult> Index()
        {
            var hampers = HttpContext.Session.Get<List<Hamper>>("cartItemList");
            if (hampers == null)
            {
                ViewBag.Message = "No Items in the cart.";
                return View();
            }
            else
            {
                CartIndexViewModel vm = new CartIndexViewModel
                {
                    CartItems = hampers,
                    TotalItems = hampers.Count(),
                    GrossTotal = hampers.Sum(h => h.Price),
                    Addresses =  await GetAddress()
                };
                return View (vm);
            }           
        }

        private async Task<List<Address>> GetAddress()
        {
                   
             string userName = User.Identity.Name;
             User user = await _userManagerServices.FindByNameAsync(userName);
            return _addressService.Query(a => a.UserId == user.Id).ToList();
        }

        [AllowAnonymous]
        public IActionResult AddToCart(int hampId)
        {
            try
            {
                Hamper hamper = _hamperService.GetSingle(h => h.Id == hampId);
                List<Hamper> hampers = HttpContext.Session.Get<List<Hamper>>("cartItemList");
                if (hampers == null)
                    hampers = new List<Hamper>();

                hampers.Add(hamper);
                HttpContext.Session.Set<List<Hamper>>("cartItemList", hampers);

            }
            catch (Exception)
            {
                ViewBag.Message = "Error!! Please try again..";
            }


            return RedirectToAction("Index", "Hamper");

        }
    

    }
}