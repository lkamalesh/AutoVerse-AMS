using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<Vehicle?> SearchByIdAsync(int id);
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();        
        Task<IEnumerable<Vehicle>> SearchByBrandAsync(string brandName);
        Task<IEnumerable<Vehicle>> SearchByModelAsync(string modelName);
        Task<IEnumerable<Vehicle>> SearchByPriceAsync(decimal minPrice, decimal maxPrice);

        Task AddVehicleAsync(Vehicle vehicle, IFormFile? imagefile);
        Task UpdateVehicleAsync(Vehicle vehicle, IFormFile? imagefile);
        Task DeleteVehicleAsync(int id);
        Task UploadImageAsync(Vehicle vehicle, IFormFile? imagefile);

    }
}
