using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore_Angular_Demo.Models;

namespace NetCore_Angular_Demo.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            return Ok("ok");
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
        {
            return Ok(vehicle);
        }
    }
}