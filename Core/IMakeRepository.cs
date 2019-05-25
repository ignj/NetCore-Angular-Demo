using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IMakeRepository
    {
        Task<IEnumerable<Make>> GetMakes();

        Task<IEnumerable<Make>> GetMakesWithModels();
    }
}
