using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoVerse.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IBrandRepository _brandRepo;
        public VehicleController(IVehicleService vehicleservice, IBrandRepository brandRepo)
        {
            _vehicleService = vehicleservice;   
            _brandRepo = brandRepo;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var brands = await _vehicleService.GetAllVehiclesAsync();
            return View(brands);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.SearchByIdAsync(id);
            return View(vehicle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Vehicle vehicle, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.AddVehicleAsync(vehicle, imageFile);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View(vehicle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
           var vehicle = await _vehicleService.SearchByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Vehicle vehicle, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.UpdateVehicleAsync(vehicle, imageFile);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View(vehicle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleService.SearchByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
