using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Core;
using NetCore_Angular_Demo.Core.Models;
using NetCore_Angular_Demo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObject)
        {
            var query = context.Vehicles
                                .Include(v => v.Model)
                                    .ThenInclude(m => m.Make)
                                .Include(v => v.Features)
                                    .ThenInclude(vf => vf.Feature)
                                .AsQueryable();

            if (queryObject.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObject.MakeId.Value);

            if (queryObject.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queryObject.ModelId);

            //Mapping for different query filters
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName                
            };

            query = query.ApplyOrdering(queryObject, columnsMap);

            //Paging
            query = query.ApplyPaging(queryObject);

            return await query.ToListAsync();            
        }        
    }
}
