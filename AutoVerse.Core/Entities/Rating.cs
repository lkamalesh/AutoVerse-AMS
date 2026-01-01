using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; } // Foreign key to Vehicle

        [Range(1, 5)]
        public double Value { get; set; }

        [Required]
        public string UserId { get; set; } = null!; // Foreign key to User

        // Navigation
        public Vehicle? Vehicle { get; set; }
        public ApplicationUser? User { get; set; }   
    }
}
