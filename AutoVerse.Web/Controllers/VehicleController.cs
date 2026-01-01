using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Core.ViewModels;
using AutoVerse.Web.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(VehicleMappings.ToViewmodels(vehicles));
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(VehicleMappings.ToViewModel(vehicle));
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
        public async Task<IActionResult> Create(VehicleViewModel vehicleVm, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var vehicle = VehicleMappings.ToEntity(vehicleVm);
                await _vehicleService.AddVehicleAsync(vehicle, imageFile);
                Log.Information($"New vehicle created. Id={vehicleVm.Id}");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View(vehicleVm);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
           var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View(VehicleMappings.ToViewModel(vehicle));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(VehicleViewModel vehicleVm, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var vehicle = VehicleMappings.ToEntity(vehicleVm);
                await _vehicleService.UpdateVehicleAsync(vehicle, imageFile);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = await _brandRepo.GetAllAsync();
            return View(vehicleVm);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(VehicleMappings.ToViewModel(vehicle));
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            Log.Information($"Vehicle deleted. Id={id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
