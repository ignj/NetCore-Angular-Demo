using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCore_Angular_Demo.Controllers.Resources;
using NetCore_Angular_Demo.Core;
using NetCore_Angular_Demo.Persistence;

namespace NetCore_Angular_Demo.Controllers
{
    public class FeaturesController : Controller
    {        
        private readonly IMapper mapper;
        private readonly IFeatureRepository featureRepository;

        public FeaturesController(AppDbContext dbContext, IMapper mapper, IFeatureRepository featureRepository)
        {            
            this.mapper = mapper;
            this.featureRepository = featureRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeaturesAsync()
        {
            var features = await featureRepository.GetFeatures();

            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features.ToList());
        }

    }
}