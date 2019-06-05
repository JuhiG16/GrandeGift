using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.Utility;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrandeGift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HamperController : Controller
    {
        private readonly IDataServices<Hamper> _hamperServices;
        private readonly IDataServices<Category> _categoryServices;
        private readonly IHostingEnvironment _hostingEnvironmentServices;

        public HamperController(IDataServices<Hamper> hamperServices,
                                IDataServices<Category> categoryServices,
                                IHostingEnvironment hostingEnvironmentServices)
        {
            this._hamperServices = hamperServices;
            this._categoryServices = categoryServices;
            this._hostingEnvironmentServices = hostingEnvironmentServices;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            PopulateAllCatgories();
            List<Hamper> hampers;
            if (User.IsInRole("Admin"))
            {
                hampers = _hamperServices.GetAll("Category").ToList();
            }
            else
            {
                hampers = _hamperServices.Query(h => (h.Status == Status.Available || h.Status == Status.OutOfStock),
                                                                                         "Category").ToList();
            }
            //    //List<HamperIndexViewModel> vmHampers = hampers.Select(
            //    //hamp => new HamperIndexViewModel()
            //    //{
            //    //    Id = hamp.Id,
            //    //    Name = hamp.Name,
            //    //    Details = hamp.Details,
            //    //    Price = hamp.Price,
            //    //    CategoryName = hamp.Category.Name,
            //    //    Status = hamp.Status,
            //    //    PhotoPath = hamp.PhotoPath

            //    //}).ToList();
            HamperIndexViewModel vmHampers = new HamperIndexViewModel
            {
                Hampers = hampers
            };
            return View(vmHampers);
        }
     
        [HttpGet]
        public IActionResult Create()
        {
            PopulateAllCatgories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(HamperCreateViewModel vmHamper)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (vmHamper.Photo != null)
                    {
                        string uploadFolder = Path.Combine(_hostingEnvironmentServices.WebRootPath + "\\img");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + vmHamper.Photo.FileName;
                        string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                        vmHamper.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
                    }
                    Hamper hamp = new Hamper()
                    {
                        Name = vmHamper.Name,
                        Details = vmHamper.Details,
                        Price = vmHamper.Price,
                        CategoryId = vmHamper.CategoryId,
                        Status = vmHamper.Status,
                        PhotoPath = uniqueFileName
                    };
                    _hamperServices.Add(hamp);

                    //return RedirectToAction("Details", "Hamper");
                    return RedirectToAction("Details", new { id = hamp.Id });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            return View();
        }

        private void PopulateAllCatgories()
        {

            ViewBag.Cats = _categoryServices.GetAll();

        }
        [HttpGet]

        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Hamper hamp = _hamperServices.GetById(id);
            Hamper hamp = _hamperServices.GetSingle(c => c.Id == id, "Category");
            if (hamp == null)
            {
                return NotFound();
            }
            HamperDetailViewModel vmHamp = new HamperDetailViewModel
            {
                Id = hamp.Id,
                Name = hamp.Name,
                Details = hamp.Details,
                Price = hamp.Price,
                CategoryName = hamp.Category.Name,
                Status = hamp.Status,
                PhotoPath = hamp.PhotoPath

            };
            return View(vmHamp);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hamper hamp = _hamperServices.GetSingle(h => h.Id == id, "Category");
            PopulateAllCatgories();
            HamperUpdateViewModel vmHamp = new HamperUpdateViewModel
            {
                Id = hamp.Id,
                Name = hamp.Name,
                Details = hamp.Details,
                Price = hamp.Price,
                CategoryId = hamp.Category.Id,
                Status = hamp.Status,
                PhotoPath = hamp.PhotoPath
            };


            return View(vmHamp);
        }

        public IActionResult Update(HamperUpdateViewModel vmHamper)
        {
            string uniqePhoto = null;
            if (vmHamper.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironmentServices.WebRootPath + "/img/");
                uniqePhoto = Guid.NewGuid().ToString() + "_" + vmHamper.Photo.FileName;

                string FilePath = Path.Combine(uploadFolder, uniqePhoto);
                FileStream f = new FileStream(FilePath, FileMode.Create);

                vmHamper.Photo.CopyTo(f);
                if (vmHamper.PhotoPath != null)
                {
                    //delete existing file 
                    string fileToDelete = Path.Combine(uploadFolder, vmHamper.PhotoPath);
                    System.IO.File.Delete(fileToDelete);
                }
            }

            Hamper hamp = _hamperServices.GetSingle(h => h.Id == vmHamper.Id);
            hamp.Name = vmHamper.Name;
            hamp.Details = vmHamper.Details;
            hamp.Price = vmHamper.Price;
            hamp.CategoryId = vmHamper.CategoryId;
            hamp.Status = vmHamper.Status;
            hamp.PhotoPath = uniqePhoto;

            _hamperServices.Update(hamp);
            return RedirectToAction("Details", new { id = hamp.Id });
        }

        [AllowAnonymous]
        public IActionResult Filter(HamperIndexViewModel vmIndex)
        {
            PopulateAllCatgories();
            SearchViewModel vm = vmIndex.SearchModel;
            List<Hamper> hampers = new List<Hamper>();

            if (vm.searchCatId != null && vm.MinPrice != null && vm.MaxPrice != null)
            {
                hampers = _hamperServices.Query(h => (h.Status == Status.Available || h.Status == Status.OutOfStock)
                                           && h.CategoryId == vm.searchCatId
                                           && h.Price >= vm.MinPrice
                                           && h.Price <= vm.MaxPrice).ToList();
            }
            else if (vm.searchCatId != null)
            {
                hampers = _hamperServices.Query(h => (h.Status == Status.Available || h.Status == Status.OutOfStock)
                                           && h.CategoryId == vm.searchCatId
                                          ).ToList();
            }

            else if (vm.MinPrice != null || vm.MaxPrice != null)
            {
                hampers = _hamperServices.Query(h => (h.Status == Status.Available || h.Status == Status.OutOfStock)
                                         && (h.Price >= vm.MinPrice
                                         || h.Price <= vm.MaxPrice)).ToList();
            }
            HamperIndexViewModel vmHampers = new HamperIndexViewModel()
            {
                Hampers = hampers
            };
            return View(vmHampers);
        }

        //[AllowAnonymous]
        //public IActionResult AddToCart(int hampId)
        //{
        //    try
        //    {
        //        Hamper hamper = _hamperServices.GetSingle(h => h.Id == hampId);
        //        List<Hamper> hampers = HttpContext.Session.Get<List<Hamper>>("cartItemList");
        //        if (hampers == null)
        //            hampers = new List<Hamper>();

        //        hampers.Add(hamper);
        //        HttpContext.Session.Set<List<Hamper>>("cartItemList", hampers);
                
        //    }
        //    catch(Exception)
        //    {
        //        ViewBag.Message = "Error!! Please try again..";
        //    }


        //    return RedirectToAction("Index");
            
        //}

    }
}