using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Persistence
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly AppDbContext context;

        public FeatureRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Feature>> GetFeatures()
        {
            return await context.Features.ToListAsync();
        }
    }
}
