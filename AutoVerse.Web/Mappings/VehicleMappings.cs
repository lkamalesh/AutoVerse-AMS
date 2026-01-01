using AutoVerse.Core.Entities;
using AutoVerse.Core.ViewModels;

namespace AutoVerse.Web.Mappings
{
    public static class VehicleMappings
    {
        public static Vehicle ToEntity(this VehicleViewModel vehicle)
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
                BrandId = vehicle.BrandId,
            };
        }
        public static VehicleViewModel ToViewModel(this Vehicle vehicle)
        {
            return new VehicleViewModel
            {
                Id = vehicle.Id,
                Model = vehicle.Model,
                BrandName = vehicle.Brand?.Name,
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

        public static IEnumerable<VehicleViewModel> ToViewmodels(IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Select(v => v.ToViewModel());
            // or vehicles.Select(ToViewModel);
        }
    }
}
