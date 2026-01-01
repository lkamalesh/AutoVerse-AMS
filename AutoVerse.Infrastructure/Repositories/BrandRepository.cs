using AutoVerse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoVerse.Infrastructure.Data;
using AutoVerse.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoVerse.Core.ViewModels;

namespace AutoVerse.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> BrandExistsAsync(string name)
        {
            return await _context.Brands.AnyAsync(b => b.Name.ToLower() == name.ToLower());
            //Use AnyAsync if you just want to check for existence  
        }
        
    }
}
