﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Entities;
using TechnologyKeeda.Repositories.Interfaces;

namespace TechnologyKeeda.Repositories.Implementations
{
    public class SkillRepo : ISkillRepo
    {
        private ApplicationDbContext _context;

        public SkillRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Edit(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();  
        }

        public async Task<IEnumerable<Skill>> GetAll()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill> GetById(int id)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveData(Skill skill)
        {
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();  
        }

        public async Task Save(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
        }
    }
}
