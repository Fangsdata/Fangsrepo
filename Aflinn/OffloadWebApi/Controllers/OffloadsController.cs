using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
using OffloadWebApi.Services;

namespace OffloadsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OffloadsController : Controller
    {
        private IOffloadService _OffloadService;

        public OffloadsController(IOffloadService OffloadService)
        {
            this._OffloadService = OffloadService;
        }

        // GET: api/offloads/ queryparamfilters
        // query params : count fishingGear boatLength landingTown
        // landingState month year
        [HttpGet]
        public IActionResult Get([FromQuery]QueryParamsForTopList input)
        {
            var resp = _OffloadService.GetOffloads(input);
            if (resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return this.BadRequest();
            }
        }

        // GET api/offloads/details
        // details on a single offload
        [HttpGet("details/{offloadId}")]
        public IActionResult Get(string offloadId)
        {
            var result = this._OffloadService.getOffloadDetails(offloadId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/offloads/:radioSignal/:count
        [HttpGet("{radioSignal}/{count}")]
        public IActionResult Get(string radioSignal, int count)
        {
            var result = this._OffloadService.GetOffloadById(radioSignal, count);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
