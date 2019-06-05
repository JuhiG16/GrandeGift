using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GrandeGift.Models;
using GrandeGift.Utility;

namespace GrandeGift.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var hampers = HttpContext.Session.Get<List<Hamper>>("cartItemList");
           return Content(String.Join(Environment.NewLine, 
                          hampers.Select(h => $"HamperName : { h.Name},HamperDetails: {h.Details}")));
        }
    }
}