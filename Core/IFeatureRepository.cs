using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetFeatures();
    }
}
