using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using GrandeGift.Models;
using GrandeGift.Utility;
using Microsoft.AspNetCore.Identity;

namespace GrandeGift.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDataServices<Order> _ordreServices;
        private readonly IDataServices<OrderDetails> _orderDetailServices;
        private readonly UserManager<User> _userManagerServices;
        
        public OrderController(IDataServices<Order> ordreServices,
                               IDataServices<OrderDetails> ordreDetailServices,
                               UserManager<User> userManagerServices)
        {
            _ordreServices = ordreServices;
            _orderDetailServices = ordreDetailServices;
            _userManagerServices = userManagerServices;
        }

        private async Task<int> GetCustomerId()
        {
            string userName = User.Identity.Name;
            User user = await _userManagerServices.FindByNameAsync(userName);

            return user.Id;

        }
        public async Task<IActionResult> Create()
        {
            
            List<Hamper> hampers = HttpContext.Session.Get<List<Hamper>>("cartItemList");
            string OrderId = (DateTime.Now).Ticks.ToString();
            int userId = await GetCustomerId();
            
            
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                GrandTotal = hampers.Sum(h => h.Price),
                TotalItems = hampers.Count(),
                UserId = userId,
                AddressId = 1,
                OrderId = OrderId
            };
            
            List<OrderDetails> odetailList = new List<OrderDetails>();
            foreach (var item in hampers)
            {
                
                OrderDetails odetails = new OrderDetails
                {
                    HamperId = item.Id,
                    OrderId = OrderId,
                    Price = item.Price,
                    Quantity = 1
                };
                odetailList.Add(odetails);
             }
            
            _ordreServices.Add(order);
            _orderDetailServices.AddRange(odetailList);
            return View("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}