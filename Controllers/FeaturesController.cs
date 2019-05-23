using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Angular_Demo.Controllers.Resources;
using NetCore_Angular_Demo.Models;
using NetCore_Angular_Demo.Persistence;

namespace NetCore_Angular_Demo.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public FeaturesController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeaturesAsync()
        {
            var features = await dbContext.Features.ToListAsync();

            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);


        }

    }
}