using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly AppDbContext _context;
        private readonly IVehicleRepository _vehicleRepo;
        public RatingService(AppDbContext context, IVehicleRepository repo)
        {
            _context = context;
            _vehicleRepo = repo;
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var avgRating = await GetAverageRatingAsync(rating.VehicleId);

            var vehicle = await _vehicleRepo.GetByIdAsync(rating.VehicleId);// Fetch vehicle to get current rating

            if (vehicle != null)
            {
                vehicle.Rating = avgRating;// Update vehicle with new average rating
                await _vehicleRepo.UpdateAsync(vehicle);
            }
        }

        public async Task<double> GetAverageRatingAsync(int vehicleId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.VehicleId == vehicleId)// Average rating in this vehicleId
                .AverageAsync(r => r.Value);

            if (ratings == 0)
            {
                return 0;
            }

            return Math.Round(ratings, 2);
        }
    }
}
