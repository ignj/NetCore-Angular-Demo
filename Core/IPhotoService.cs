using Microsoft.AspNetCore.Http;
using NetCore_Angular_Demo.Core.Models;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath);
    }
}
