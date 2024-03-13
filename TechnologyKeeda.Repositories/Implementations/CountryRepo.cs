using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Entities;
using TechnologyKeeda.Repositories.Interfaces;

namespace TechnologyKeeda.Repositories.Implementations
{
    public class CountryRepo : ICountryRepo
    {
        private readonly ApplicationDbContext _context;

        public CountryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Edit(Country country)
        {
            _context.Countries.Update(country);
           await _context.SaveChangesAsync(); 
        }

        public async Task<List<Country>> GetAll()
        {
           return
                await _context.Countries.IgnoreQueryFilters().ToListAsync();
                 
        }

        public async Task<Country> GetById(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            return country;
        }

        public async Task RemoveData(Country country)
        {
            country.IsDeleted = true;
            // _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Country country)
        {
           await _context.Countries.AddAsync(country);    
            await _context.SaveChangesAsync();
        }
    }
}
