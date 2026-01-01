using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string BodyType { get; set; } = null!;
        public decimal BaseModelPrice { get; set; } 
        public decimal? TopModelPrice { get; set; }

        public string? Engine { get; set; }
        public string? FuelType { get; set; }
        public string? Transmission { get; set; }

        public int? Mileage { get; set; }
        public int? TopSpeed { get; set; }
        public int SeatingCapacity { get; set; }

        public string? ImageUrl { get; set; }
        public double Rating { get; set; }

        public int BrandId { get; set; }
    }
}
