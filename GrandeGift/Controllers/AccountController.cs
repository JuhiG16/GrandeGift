using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Identity;
using GrandeGift.Models;
using GrandeGift.Services;
namespace GrandeGift.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManagerServices;
        private readonly SignInManager<User> _signInManagerServices;
        private readonly IDataServices<Customer> _customerServices;
        //private readonly dAta
        
        public AccountController(UserManager<User> userManagerServices, SignInManager<User> signInManagerservices,IDataServices<Customer> customerServices)
        {
            this._userManagerServices = userManagerServices;
            this._signInManagerServices = signInManagerservices;
            this._customerServices = customerServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vmLogin)
        {
            Microsoft.AspNetCore.Identity.SignInResult result =  await _signInManagerServices.PasswordSignInAsync(vmLogin.UserName, vmLogin.Password, vmLogin.RememberMe, false);

            if(result.Succeeded)
            {
                var user = await _userManagerServices.FindByNameAsync(vmLogin.UserName);
                switch (user.Role)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                    case "Customer":
                        return RedirectToAction("Index", "Customer");
                    case "Manager":
                        return RedirectToAction("Index", "Manager");
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(vmLogin.ReturnUrl))
                {
                    return RedirectToAction(vmLogin.ReturnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect credentials");
            }
            return RedirectToAction("Index","Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if(ModelState.IsValid)
            {
                if(null == await _userManagerServices.FindByNameAsync(vm.Username))
                {
                    User user = new User
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        PhoneNumber = vm.Phone,
                        Email = vm.Email,
                        Address = vm.Address,
                        UserName = vm.Username,
                        DOB = vm.DOB,
                        Role = vm.Role
                    };
                    IdentityResult result = await _userManagerServices.CreateAsync(user, vm.Password);
                    if(result.Succeeded)
                    {
                        await _userManagerServices.AddToRoleAsync(user, vm.Role);

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ViewBag.errorMessage = " User already exists ";
                }

            }

            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManagerServices.SignOutAsync();
            return View("LogOut","Account");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

     


    }
}