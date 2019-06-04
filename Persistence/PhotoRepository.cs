using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Core;
using NetCore_Angular_Demo.Core.Models;

namespace NetCore_Angular_Demo.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext context;

        public PhotoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }
    }
}
