using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;

namespace AutoVerse.API.Mappings
{
    public static class VehicleMappings
    {
        public static Vehicle ToEntity(this VehicleDto vehicle)
        {
            return new Vehicle
            {
                Id = vehicle.Id,
                Model = vehicle.Model,
                BodyType = vehicle.BodyType,
                BaseModelPrice = vehicle.BaseModelPrice,
                TopModelPrice = vehicle.TopModelPrice,
                Engine = vehicle.Engine,
                FuelType = vehicle.FuelType,
                Transmission = vehicle.Transmission,
                Mileage = vehicle.Mileage,
                TopSpeed = vehicle.TopSpeed,
                SeatingCapacity = vehicle.SeatingCapacity,
                Rating = vehicle.Rating,
                BrandId = vehicle.BrandId
            };
        }
        public static VehicleDto ToDto(this Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                Model = vehicle.Model,
                BrandName = vehicle.Brand?.Name ?? string.Empty,
                BodyType = vehicle.BodyType,
                BaseModelPrice = vehicle.BaseModelPrice,
                TopModelPrice = vehicle.TopModelPrice,
                Engine = vehicle.Engine,
                FuelType = vehicle.FuelType,
                Transmission = vehicle.Transmission,
                Mileage = vehicle.Mileage,
                TopSpeed = vehicle.TopSpeed,
                SeatingCapacity = vehicle.SeatingCapacity,
                ImageUrl = vehicle.Imageurl,
                Rating = vehicle.Rating,
                BrandId = vehicle.BrandId,
            };
        }

        public static IEnumerable<VehicleDto> ToDtos(IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Select(v => v.ToDto());
            // or vehicles.Select(ToViewModel);
        }
    }
}
