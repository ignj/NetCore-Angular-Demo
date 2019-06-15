using Microsoft.AspNetCore.Http;
using NetCore_Angular_Demo.Core.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Core
{    
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //Service layer to work with domain objects
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);

            await unitOfWork.CompleteAsync();

            return photo;
        }
    }
}
