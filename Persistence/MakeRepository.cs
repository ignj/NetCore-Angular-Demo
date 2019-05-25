using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Persistence
{
    public class MakeRepository : IMakeRepository
    {
        private readonly AppDbContext context;

        public MakeRepository(AppDbContext context)
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
