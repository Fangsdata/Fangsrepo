using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
using OffloadWebApi.Services;

namespace OffloadWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private IBoatService _IBoatService;
        public SearchController(IBoatService boatService)
        {
            this._IBoatService = boatService;
        }

        [HttpGet("boats/{boatSearchTerm}")]
        public IActionResult Get(string boatSearchTerm)
        {
            var result = this._IBoatService.SearchForBoat(boatSearchTerm, 10, 1);
            if(result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }
        [HttpGet("boats/{boatSearchTerm}/{count}/{pageNo}")]
        public IActionResult Get(string boatSearchTerm, int count, int pageNo)
        {
            var result = this._IBoatService.SearchForBoat(boatSearchTerm, count, pageNo);
            if(result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }
    }
}