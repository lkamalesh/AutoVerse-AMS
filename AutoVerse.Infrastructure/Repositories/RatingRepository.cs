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
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistingRatingAsync(int vehicleId, string userId)
        {
            return await _context.Ratings
                .AnyAsync(r => r.VehicleId == vehicleId && r.UserId == userId);
        }

        public async Task<double> GetAverageRatingAsync(int vehicleId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.VehicleId == vehicleId)
                .Select(r => (double?)r.Value)// Select rating values as nullable doubles
                .AverageAsync();

            return Math.Round(ratings ?? 0, 2);// Return 0 if no ratings exist, rounded to 2 decimal places
        }
    }
}
