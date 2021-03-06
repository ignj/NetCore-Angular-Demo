﻿using NetCore_Angular_Demo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);

        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);

        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}