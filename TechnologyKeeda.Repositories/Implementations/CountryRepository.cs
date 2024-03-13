using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Entities;
using TechnologyKeeda.Repositories.Interfaces;

namespace TechnologyKeeda.Repositories.Implementations
{
    public class CountryRepository : ICountryRepo
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Edit(Country country)
        {
            var countryId = new SqlParameter("@CountryId", country.Id);
            var countryName = new SqlParameter("@Name", country.Name);
            var isDelete = new SqlParameter("@IsDeleted", false);
            await _context.Database.ExecuteSqlRawAsync("Exec UpdateCountry @CountryId,@Name,@IsDeleted", countryId, countryName, isDelete);
            
        }

        public async Task<List<Country>> GetAll()
        {
            var countries = await _context.Countries.FromSqlRaw("Exec GetAllCountry").IgnoreQueryFilters().ToListAsync();
            return countries;
        }

        public async Task<Country> GetById(int id)
        {
            //var idParam = new SqlParameter("@CountryId", id);

            var countries = await _context.Countries.FromSqlRaw("Exec GetAllCountry")
                 .ToListAsync();
            return countries.FirstOrDefault(x=>x.Id==id);
        }

         public async Task RemoveData(Country country)
        {
            var idParam = new SqlParameter("@CountryId", country.Id);
           await  _context.Database.ExecuteSqlRawAsync("Exec SoftDeleteCountry @CountryId", idParam);
            
        }

        public async Task Save(Country country)
        {
            var nameParam = new SqlParameter("@Name", country.Name);
            var isDeletedParam = new SqlParameter("@IsDeleted", false);
           

            await _context.Database.ExecuteSqlRawAsync("EXEC CreateCountry @Name, @IsDeleted",
                nameParam, isDeletedParam);

           
        }
    }
}
