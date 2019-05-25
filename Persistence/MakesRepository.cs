using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Persistence
{
    public class MakesRepository : IMakesRepository
    {
        private readonly AppDbContext context;

        public MakesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Make>> GetMakes()
        {
            return await context.Makes.ToListAsync();
        }

        public async Task<IEnumerable<Make>> GetMakesWithModels()
        {
            return await context.Makes.Include(m => m.Models).ToListAsync();
        }
    }
}
