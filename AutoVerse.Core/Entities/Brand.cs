using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]    
        public string Name { get; set; } = null!;

        public ICollection<Vehicle> vehicles { get; set; } = new List<Vehicle>();  // (one brand → many vehicles)
    }
}
