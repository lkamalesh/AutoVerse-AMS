using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Core.ViewModels;
using AutoVerse.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepo;
        public VehicleService(IVehicleRepository repo)
        {
            _vehicleRepo = repo;
        }


        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _vehicleRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicleRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Vehicle>> SearchVehiclesAsync(
            string? brandName,
            string? modelName,
            decimal? minPrice,
            decimal? maxPrice)
        {
            var vehicles = await _vehicleRepo.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(brandName))
            {
                vehicles = vehicles.Where(v => v.Brand != null && v.Brand.Name.ToLower() == brandName.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(modelName))
            {
                vehicles = vehicles.Where(v => v.Model.ToLower() == modelName.ToLower());
            }
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                vehicles = vehicles.Where(v => v.BaseModelPrice >= minPrice && v.BaseModelPrice <= maxPrice);
            }

            return vehicles;
        }

        public async Task AddVehicleAsync(Vehicle vehicle, IFormFile? imagefile)
        {
            await UploadImageAsync(vehicle, imagefile);
            await _vehicleRepo.AddAsync(vehicle);
            Log.Information($"New vehicle created: {vehicle.Model} (BrandId: {vehicle.BrandId})");
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle, IFormFile? imagefile)
        {
            await UploadImageAsync(vehicle, imagefile);
            await _vehicleRepo.UpdateAsync(vehicle);
            Log.Information($"Vehicle updated: {vehicle.Model} (BrandId: {vehicle.BrandId})");
        }

        public async Task DeleteVehicleAsync(int id)
        {
            await _vehicleRepo.DeleteAsync(id);
            Log.Warning($"Vehicle deletion attempted for Id: {id}");
        }

        public async Task UploadImageAsync(Vehicle vehicle, IFormFile? imagefile)
        {
            if (imagefile != null && imagefile.Length > 0)
            {
                string imageName = $"{Guid.NewGuid()}{Path.GetExtension(imagefile.FileName)}";// Unique image name using Guid
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Vehicles");// Define folder path
                Directory.CreateDirectory(folderPath);// Create directory if not exists
                string filePath = Path.Combine(folderPath, imageName);// Full file path

                using (var stream = new FileStream(filePath, FileMode.Create))// Create/overwrite file
                {
                    await imagefile.CopyToAsync(stream);// Save file
                }

                vehicle.Imageurl = $"/Images/Vehicles/{imageName}";// Set web-accessible URL path for the image.
            }
            Log.Error("Image upload failed for vehicle {Id}", vehicle.Id);
        }
    }
}
