using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using GrandeGift.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

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
        
        public IActionResult Index()
        {
            List<Hamper> hampers = _hamperServices.GetAll("Category").ToList();

            //var hampers = _hamperServices.QueryGetAll(h => h.IsContinue == true, "Category");
                List<HamperIndexViewModel> vmHampers = hampers.Select(
                hamp => new HamperIndexViewModel()
                {
                    Id = hamp.Id,
                    Name = hamp.Name,
                    Details = hamp.Details,
                    Price = hamp.Price,
                    CategoryName = hamp.Category.Name,
                    Status = hamp.Status

                }).ToList();
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
                    if(vmHamper.Photo != null)
                    {
                      string uploadFolder =  Path.Combine(_hostingEnvironmentServices.WebRootPath + "\\img");
                      uniqueFileName =  Guid.NewGuid().ToString() + "_" + vmHamper.Photo.FileName;
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
            catch(DbUpdateException)
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
            if(id == null)
            {
                return NotFound();
            }
            //Hamper hamp = _hamperServices.GetById(id);
            Hamper hamp = _hamperServices.QueryGetSingle(c => c.Id == id, "Category");
            if(hamp == null)
            {
                return NotFound();
            }
            HamperDetailViewModel vmHamp = new HamperDetailViewModel {
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
            if(id == null)
            {
                return NotFound();
            }
            Hamper hamp = _hamperServices.QueryGetSingle(h => h.Id == id, "Category");
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
            if(vmHamper.Photo != null)
            {
               string uploadFolder =  Path.Combine(_hostingEnvironmentServices.WebRootPath + "/img/");             
               uniqePhoto =  Guid.NewGuid().ToString() +"_" + vmHamper.Photo.FileName;

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
            
            Hamper hamp = _hamperServices.GetById(vmHamper.Id);
            hamp.Name = vmHamper.Name;
            hamp.Details = vmHamper.Details;
            hamp.Price = vmHamper.Price;
            hamp.CategoryId = vmHamper.CategoryId;
            hamp.Status = vmHamper.Status;
            hamp.PhotoPath = uniqePhoto;

            _hamperServices.Update(hamp);
            return RedirectToAction("Details", new { id = hamp.Id});
        }
       
    }
}