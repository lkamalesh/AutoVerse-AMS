using AutoVerse.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.ViewModels
{
    public class RatingViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; } 

        [Range(1, 5)]
        public double Value { get; set; }

        [Required]
        public string UserId { get; set; } = null!; 

    }
}
