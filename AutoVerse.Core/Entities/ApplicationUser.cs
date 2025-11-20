using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Additional custom properties can be added here as needed
        [MaxLength(50)]
        public string? FullName { get; set; } // -> optional
    }
}
