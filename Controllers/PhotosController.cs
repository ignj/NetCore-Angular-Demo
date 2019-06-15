using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore_Angular_Demo.Controllers.Resources;
using NetCore_Angular_Demo.Core;
using NetCore_Angular_Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly PhotoSettings photoSettings;
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRepository;        
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;

        public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository, IPhotoService photoService)
        {
            this.photoSettings = options.Value;
            this.host = host;
            this.vehicleRepository = vehicleRepository;            
            this.mapper = mapper;
            this.photoRepository = photoRepository;
            this.photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetVehicle(vehicleId, includeRelated: false);

            if (vehicle == null) return NotFound();
            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            var photo = await photoService.UploadPhoto(vehicle, file, uploadsFolderPath);

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await photoRepository.GetPhotos(vehicleId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

    }
}
