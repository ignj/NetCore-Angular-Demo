using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCore_Angular_Demo.Controllers.Resources;
using NetCore_Angular_Demo.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore_Angular_Demo.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {        
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IModelRepository modelRepository;

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IModelRepository modelRepository)
        {            
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.unitOfWork = unitOfWork;
            this.modelRepository = modelRepository;
        }

        public IActionResult Index()
        {
            return Ok("ok");
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            /* Business validation */
            var model = await modelRepository.GetModel(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid model id");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            vehicleRepository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            // Reload entire vehicle
            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);            

            /* Business validation */
            var model = await modelRepository.GetModel(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid model id");
                return BadRequest(ModelState);
            }

            // Reload entire vehicle
            var vehicle = await vehicleRepository.GetVehicle(id);

            if (vehicle == null) return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id, includeRelated: false);

            if (vehicle == null) return NotFound();

            vehicleRepository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id);

            if (vehicle == null) return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
            var vehicles = await vehicleRepository.GetVehicles();

            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        }
    }
}