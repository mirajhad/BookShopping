using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Entities;
using TechnologyKeeda.Repositories.DTO;
using TechnologyKeeda.Repositories.Interfaces;

namespace TechnologyKeeda.Repositories.Implementations
{
    public class StateRepository : IStateRepo
    {
        private readonly ApplicationDbContext _context;

        public StateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Edit(State state)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            var stateWithCountry =    await _context.States.FromSqlRaw("Exec GetStateWithCountries").AsNoTracking()                            
                .ToListAsync();
            return (IEnumerable<State>)stateWithCountry;
        }

        public async Task<State> GetById(int id)
        {
           var statewithCountries =  await _context.States.FromSqlRaw("Exec GetStateWithCountries").AsNoTracking().ToListAsync();
            return statewithCountries.FirstOrDefault(x => x.Id == id);

        }

        public async Task RemoveData(State state)
        {
            var idParam = new SqlParameter("@StateId", state.Id);
            await _context.Database.ExecuteSqlRawAsync("Exec GetRemoveState @StateId", idParam);
        }

        public Task Save(State state)
        {
            throw new NotImplementedException();
        }
    }
}
