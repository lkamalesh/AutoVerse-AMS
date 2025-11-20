using AutoVerse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Interfaces.Services
{
    public interface IRatingService
    {
        Task AddRatingAsync(Rating rating);

        Task<double> GetAverageRatingAsync(int vehicleId);
    }
}
