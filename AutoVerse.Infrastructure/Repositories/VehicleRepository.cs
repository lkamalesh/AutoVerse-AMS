using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Infrastructure.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {        
        private readonly AppDbContext _context;
        public VehicleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Vehicle>> GetAllAsync()// Override to include related entities
        {
            return await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.VehicleType)
                .ToListAsync();
        }

        public override async Task<Vehicle?> GetByIdAsync(int id)// Override to include related entities
        {
            return await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task<IEnumerable<Vehicle>> GetByBrandAsync(string brandName)
        {
            return await _context.Vehicles
                .Include(v => v.Brand)// Include brand details
                .Include(v => v.VehicleType)// Include vehicle type details as well
                .Where(v => v.Brand != null && v.Brand.Name == brandName)// Filter by brand name
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByModelAsync(string modelName)
        {
            return await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.VehicleType)
                .Where(v => v.Model!= null && v.Model == modelName)// Filter by model name
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.VehicleType)
                .Where(v => v.BaseModelPrice >= minPrice && v.BaseModelPrice <= maxPrice)// Filter by min & max price
                .ToListAsync();
        }

    }
}
