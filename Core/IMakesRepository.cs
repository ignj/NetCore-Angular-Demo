using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IMakesRepository
    {
        Task<IEnumerable<Make>> GetMakes();

        Task<IEnumerable<Make>> GetMakesWithModels();
    }
}
