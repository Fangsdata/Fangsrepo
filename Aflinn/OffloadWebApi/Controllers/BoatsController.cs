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
    public class BoatsController : ControllerBase
    {
        private IBoatService _IBoatService;

        public BoatsController(IBoatService boatService)
        {
            this._IBoatService = boatService;
        }

        [HttpGet("radio/{BoatRadioSignalId}")]
        public IActionResult GetRatio(string BoatRadioSignalId)
        {
            var result = this._IBoatService.GetBoatByRadio(BoatRadioSignalId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
        [HttpGet("registration/{registrationId}")]
        public IActionResult GetRegistration(string registrationId)
        {
            Console.WriteLine(registrationId);
            var result = this._IBoatService.GetBoatByRegistration(registrationId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}