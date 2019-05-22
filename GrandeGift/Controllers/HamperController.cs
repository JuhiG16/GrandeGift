using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using GrandeGift.Models;
using Microsoft.EntityFrameworkCore;

namespace GrandeGift.Controllers
{
    public class HamperController : Controller
    {
        private readonly IDataServices<Hamper> _hamperServices;
        public HamperController(IDataServices<Hamper> hamperServices)
        {
            this._hamperServices = hamperServices;
        }
        public IActionResult Index()
        {
            IEnumerable<Hamper> hampers = _hamperServices.GetAll();
            List<HamperIndexViewModel> vmHampers = hampers.Select(
                hamp => new HamperIndexViewModel()
                {
                    Id = hamp.Id,
                    Name = hamp.Name,
                    Details = hamp.Details,
                    Price = hamp.Price
                }).ToList();
            return View(vmHampers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HamperCreateViewModel vmHamper)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Hamper hamp = new Hamper()
                    {
                        Name = vmHamper.Name,
                        Details = vmHamper.Details,
                        Price = vmHamper.Price
                    };
                    _hamperServices.Add(hamp);
                    return RedirectToAction("Index", "Hamper");
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            return View();
        }

        [HttpGet]

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Hamper hamp = _hamperServices.GetById(id);
            if(hamp == null)
            {
                return NotFound();
            }
            HamperDetailViewModel vmHamp = new HamperDetailViewModel {
                Id = hamp.Id,
                Name = hamp.Name,
                Details = hamp.Details,
                Price = hamp.Price
            };
            return View(vmHamp);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Hamper hamp = _hamperServices.GetById(id);
            HamperUpdateViewModel vmHamp = new HamperUpdateViewModel
            {
                Id = hamp.Id,
                Name = hamp.Name,
                Details = hamp.Details,
                Price = hamp.Price
            };

            
            return View(vmHamp);
        }

        public IActionResult Update(HamperUpdateViewModel vmHamper)
        {
            Hamper hamp = _hamperServices.GetById(vmHamper.Id);
            hamp.Name = vmHamper.Name;
            hamp.Details = vmHamper.Details;
            hamp.Price = vmHamper.Price;
            _hamperServices.Update(hamp);
            return RedirectToAction("Index");
        }
    }
}