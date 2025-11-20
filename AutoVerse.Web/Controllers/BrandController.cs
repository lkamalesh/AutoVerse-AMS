using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
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
            return View(brand);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                var Exists = await _brandRepo.BrandExistsAsync(brand.Name);

                if (Exists)
                {
                    ModelState.AddModelError("Name", "Brand name already exists!");
                    return View(brand);
                }

                Log.Information($"New brand created: {brand.Name})");
                await _brandRepo.AddAsync(brand);
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

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand brand)
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
                await _brandRepo.UpdateAsync(brand);
                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

    }
}
