using NetCore_Angular_Demo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);        
    }
}
