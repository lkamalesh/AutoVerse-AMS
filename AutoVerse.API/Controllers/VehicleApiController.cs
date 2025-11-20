using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleApiController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleApiController(IVehicleService vehicleservice)
        {
            _vehicleService = vehicleservice;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleService.SearchByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }
        [Authorize]
        [HttpGet("GetByBrand/{brandName}")]
        public async Task<IActionResult> GetByBrand(string brandName)
        {
            var vehicles = await _vehicleService.SearchByBrandAsync(brandName);

            if (vehicles == null)
            {
                return NotFound();
            }

            return Ok(vehicles);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vehicleService.AddVehicleAsync(vehicle, null);
            return Ok(new {Message = "Vehicle added successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit")]
        public async Task<IActionResult> Update([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vehicleService.UpdateVehicleAsync(vehicle, null);
            return Ok(new { Message = "Vehicle updated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return Ok(new { Message = "Vehicle deleted successfully" });
        }
    }
}
