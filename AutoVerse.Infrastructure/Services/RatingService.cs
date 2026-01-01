using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;
using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Core.Interfaces.Services;
using AutoVerse.Core.ViewModels;
using AutoVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly IVehicleRepository _vehicleRepo;
        public RatingService(IVehicleRepository repo, IRatingRepository ratingRepo)
        {
            _vehicleRepo = repo;
            _ratingRepo = ratingRepo;
        }

        public async Task AddRatingAsync(Rating rating)
        {

            var existingRating = await _ratingRepo.ExistingRatingAsync(rating.VehicleId, rating.UserId);

            if (!existingRating)
            {
                await _ratingRepo.AddAsync(rating);// Add new rating
            }

            var avgRating = await _ratingRepo.GetAverageRatingAsync(rating.VehicleId);

            var vehicle = await _vehicleRepo.GetByIdAsync(rating.VehicleId);// Fetch vehicle to get current rating

            if (vehicle != null)
            {
                vehicle.Rating = avgRating;// Update vehicle with new average rating
            }

            await _vehicleRepo.SaveChangesAsync();
        }

    }
}
