using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
using OffloadWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace OffloadWebApi.Controllers
{
    [Route("api/[controller]")]
    public class OffloadController : Controller
    {
        private IOffloadService _OffloadService;

        public OffloadController(IOffloadService OffloadService)
        {
            this._OffloadService = OffloadService;
        }

        // GET: api/values
        // query params : count fishingGear boatLength landingTown
        // landingState month year
        [HttpGet]
        public IActionResult Get(
            [FromQuery]int count,
            [FromQuery]List<string> fishingGear,
            [FromQuery]List<double> boatLength,
            [FromQuery] List<string> landingTown,
            [FromQuery] List<string> landingState,
            [FromQuery] List<int> month,
            [FromQuery] List<int> year)
        {
            var filters = new QueryOffloadsInput
            {
                Count = count,
                FishingGear = fishingGear,
                BoatLength = boatLength,
                LandingState = landingState,
                LandingTown = landingTown,
                Month = month,
                Year = year,
            };
            var result = this._OffloadService.GetOffloads(filters);
            if (result == null)
            {
                return this.BadRequest();
            }

            if (result.Count == 0)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this._OffloadService.GetOffloadById(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
