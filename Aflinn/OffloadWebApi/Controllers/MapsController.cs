using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffloadWebApi.Services;

namespace OffloadWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        private IMapService _MapService;

        public MapsController(IMapService MapService)
        {
            this._MapService = MapService;
        }
        [HttpGet("boats/radio/{BoatRadioSignalId}")]
        public IActionResult Get(string BoatRadioSignalId)
        {
            var result = this._MapService.GetMapDataByRadioSignal(BoatRadioSignalId);
            if (result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }
    }
}