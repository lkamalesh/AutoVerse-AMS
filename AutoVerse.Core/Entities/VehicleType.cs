using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; } = string.Empty;

        public ICollection<Vehicle>? vehicles { get; set; } = new List<Vehicle>(); //Each type can have multiple vehicles
    }
}
