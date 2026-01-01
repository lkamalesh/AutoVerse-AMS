using System.Diagnostics;
using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Web.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoVerse.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public HomeController(IVehicleService vehicleservice)
        {
            _vehicleService = vehicleservice;
        }

        public async Task<IActionResult> Index(string? searchBrand, string? searchModel, decimal? minPrice, decimal? maxPrice)
        {
            var vehicles = await _vehicleService.SearchVehiclesAsync(searchBrand, searchModel, minPrice, maxPrice);

            return View(VehicleMappings.ToViewmodels(vehicles));

        }

    }
}
