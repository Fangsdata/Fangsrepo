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
    public class OffloadsController : ControllerBase
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
        
        // GET api/offloads/details/date
        // details on a single offload filter by date and registratino id
        [HttpGet("details/date/{date}/{registrationId}")]
        public IActionResult Get(string date, string registrationId)
        {
            var result = this._OffloadService.getOffloadDetailsByDateAndRegistration(date, registrationId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        // GET api/offloads/:radioSignal/:count
        [HttpGet("{boatRegistrationId}/{count}")]
        public IActionResult Get(string boatRegistrationId, int count)
        {
            var result = this._OffloadService.GetOffloadById(boatRegistrationId, count, 1);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpGet("{boatRegistrationId}/{count}/{pageNo}")]
        public IActionResult Get(string boatRegistrationId, int count, int pageNo)
        {
            var result = this._OffloadService.GetOffloadById(boatRegistrationId, count, pageNo);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
