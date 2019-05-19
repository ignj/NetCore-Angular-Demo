﻿using System;
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
    public class MakesController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public MakesController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakesAsync()
        {
            var makes = await dbContext.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}