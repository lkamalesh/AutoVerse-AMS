using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; } = null!;// -> Non-Nullable.

        [Required]
        [MaxLength(50)]
        public string BodyType { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseModelPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TopModelPrice { get; set; }

        [MaxLength(50)]
        public string? Engine { get; set; }

        [MaxLength(50)]
        public string? FuelType { get; set; }

        [MaxLength(50)]
        public string? Transmission { get; set; } 

        public int? Mileage { get; set; } 

        public int? TopSpeed { get; set; } 

        public int SeatingCapacity { get; set; }      

        [MaxLength(500)]
        public string? Imageurl { get; set; }   

        [Range(0, 5)]
        public double Rating { get; set; } = 0.0;

        //Foreign Keys
        [Required]
        public int BrandId { get; set; }

        public Brand? Brand { get; set; }// Navigation property to the Brand entity.

    }
}
