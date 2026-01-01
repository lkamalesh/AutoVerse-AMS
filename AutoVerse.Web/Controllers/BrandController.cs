using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Core.ViewModels;
using AutoVerse.Web.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AutoVerse.Web.Controllers
{
   [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepo;

        public BrandController(IBrandRepository brand)
        {
            _brandRepo = brand;
        }
        public async Task<IActionResult> Index()
        {
            var brand = await _brandRepo.GetAllAsync();
            return View(BrandMappings.ToViewModels(brand));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var Exists = await _brandRepo.BrandExistsAsync(brand.Name);

                if (Exists)
                {
                    ModelState.AddModelError("Name", "Brand name already exists!");
                    return View(brand);
                }

                await _brandRepo.AddAsync(BrandMappings.ToEntity(brand));
                Log.Information($"New brand created: {brand.Name})");
                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(BrandMappings.ToViewModel(brand));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var Exists = await _brandRepo.BrandExistsAsync(brand.Name);

                if (Exists)
                {
                    ModelState.AddModelError("Name", "Brand name already exists!");
                    return View(brand);
                }
                Log.Information($"Brand updated: {brand.Name})");
                await _brandRepo.UpdateAsync(BrandMappings.ToEntity(brand));
                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

    }
}
