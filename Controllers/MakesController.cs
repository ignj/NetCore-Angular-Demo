using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Controllers.Resources;
using NetCore_Angular_Demo.Core;
using NetCore_Angular_Demo.Persistence;

namespace NetCore_Angular_Demo.Controllers
{
    public class MakesController : Controller
    {        
        private readonly IMapper mapper;
        private readonly IMakesRepository makesRepository;

        public MakesController(IMapper mapper, IMakesRepository makesRepository)
        {            
            this.mapper = mapper;
            this.makesRepository = makesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakesAsync()
        {
            var makes = await makesRepository.GetMakesWithModels();

            return mapper.Map<List<Make>, List<MakeResource>>(makes.ToList());
        }
    }
}