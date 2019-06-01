using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext context;

        public VehicleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (includeRelated == false)
            {
                return await context.Vehicles.FindAsync(id);
            }

            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
            
        }

        //public async Task<Vehicle> GetVehicleWithMake(int id)
        //{

        //}

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await context.Vehicles
                                .Include(v => v.Model)
                                    .ThenInclude(m => m.Make)
                                .Include(v => v.Features)
                                    .ThenInclude(vf => vf.Feature)
                                .ToListAsync();
        }
    }
}
