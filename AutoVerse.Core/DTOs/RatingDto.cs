using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public double Value { get; set; }
        public string UserId { get; set; } = null!;
    }
}
