using AutoVerse.API.Mappings;
using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            return Ok(VehicleMappings.ToDtos(vehicles));
        }

        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(VehicleMappings.ToDto(vehicle));
        }
        [Authorize]
        [HttpGet("GetByBrand/{brandName}")]
        public async Task<IActionResult> GetByBrand(string brandName)
        {
            var vehicles = await _vehicleService.SearchVehiclesAsync(brandName, null, null, null);

            if (vehicles == null)
            {
                return NotFound();
            }

            return Ok(VehicleMappings.ToDtos(vehicles));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]VehicleDto vehicleDto)
        {
            var vehicle = VehicleMappings.ToEntity(vehicleDto);
            await _vehicleService.AddVehicleAsync(vehicle, null);
            Log.Information($"New vehicle created. Id={vehicleDto.Id}");
            return CreatedAtAction(
                nameof(GetById),
                new { id = vehicle.Id },
                new { Message = "Vehicle added successfully" }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit")]
        public async Task<IActionResult> Update([FromBody] VehicleDto vehicleDto)
        {
            var vehicle = VehicleMappings.ToEntity(vehicleDto);
            await _vehicleService.UpdateVehicleAsync(vehicle, null);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            Log.Information($"Vehicle deleted. Id={id}");
            return NoContent();
        }
    }
}
