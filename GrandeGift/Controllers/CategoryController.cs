using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.Models;
using Microsoft.EntityFrameworkCore;
using GrandeGift.ViewModels;

namespace GrandeGift.Controllers
{
    public class CategoryController : Controller
    {
       private readonly IDataServices<Category> _categoryServices;

        public CategoryController(IDataServices<Category> categoryServices)
        {
            this._categoryServices = categoryServices;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> cats = _categoryServices.GetAll();
            IEnumerable<CategoryIndexViewModel> vmCats = cats.Select(cat =>
             new CategoryIndexViewModel
             {
                 CategoryId = cat.Id,
                 Name = cat.Name,
                 Description = cat.Description
             }
            ).ToList();
            return View(vmCats);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel vmCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category cat = new Category();

                    cat.Name = vmCategory.Name;
                    cat.Description = vmCategory.Description;

                    _categoryServices.Add(cat);
                    return RedirectToAction("Index");
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
        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Category cat = _categoryServices.GetById(id);
            CategoryUpdateViewModel vmCat = new CategoryUpdateViewModel
            {
                CategoryId = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };
            return View(vmCat);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateViewModel vmCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category cat = new Category();

                    cat.Id = vmCategory.CategoryId;
                    cat.Name = vmCategory.Name;
                    cat.Description = vmCategory.Description;

                    _categoryServices.Update(cat);
                    return RedirectToAction("Index");
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

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Category cat = _categoryServices.GetById(id);
            CategoryDetailViewModel vmCat = new CategoryDetailViewModel
            {
                CategoryId = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };

            return View(vmCat);
        }

    }
}