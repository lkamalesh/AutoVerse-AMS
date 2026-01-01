using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;

namespace AutoVerse.API.Mappings
{
    public static class RatingMappings
    {
        public static Rating ToEntity(RatingDto rating)
        {
            return new Rating
            {
                VehicleId = rating.VehicleId,
                Value = rating.Value
            };
        }
    }
}
