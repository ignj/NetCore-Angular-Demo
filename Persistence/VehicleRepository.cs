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

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObject)
        {
            var result = new QueryResult<Vehicle>();

            var query = context.Vehicles
                                .Include(v => v.Model)
                                    .ThenInclude(m => m.Make)
                                .Include(v => v.Features)
                                    .ThenInclude(vf => vf.Feature)
                                .AsQueryable();

            query = query.ApplyFiltering(queryObject);

            //Mapping for different query filters
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName                
            };
            query = query.ApplyOrdering(queryObject, columnsMap);

            //Paging
            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObject);

            result.Items = await query.ToListAsync();

            return result;
        }        
    }
}
