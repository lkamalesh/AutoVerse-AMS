using AutoVerse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Interfaces.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        // Vehicle specific actions
        Task<IEnumerable<Vehicle>> GetByModelAsync(string modelName);
        Task<IEnumerable<Vehicle>> GetByBrandAsync(string brandName);
        Task<IEnumerable<Vehicle>> GetByPriceAsync(decimal minPrice, decimal maxPrice);
    }
}
