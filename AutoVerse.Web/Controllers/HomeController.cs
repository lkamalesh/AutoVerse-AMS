using System.Diagnostics;
using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
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
            var vehicles = Enumerable.Empty<Vehicle>();

            if (searchBrand != null)
            {
                vehicles = await _vehicleService.SearchByBrandAsync(searchBrand);
            }
            else if (searchModel != null)
            {
                vehicles = await _vehicleService.SearchByModelAsync(searchModel);
            }
            else if(minPrice != null && maxPrice != null)
            {
                vehicles = await _vehicleService.SearchByPriceAsync(minPrice.Value, maxPrice.Value);
            }
            else
            {
                vehicles = await _vehicleService.GetAllVehiclesAsync();
            }

                return View(vehicles);

        }

    }
}
