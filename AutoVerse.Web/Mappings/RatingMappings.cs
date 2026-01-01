using AutoVerse.Core.Entities;
using AutoVerse.Core.ViewModels;

namespace AutoVerse.Web.Mappings
{
    public static class RatingMappings
    {
        public static Rating ToEntity(RatingViewModel rating)
        {
            return new Rating
            {
                Id = rating.Id,
                VehicleId = rating.VehicleId,
                UserId = rating.UserId,
                Value = rating.Value
            };
        }
    }
}
