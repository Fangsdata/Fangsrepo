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
    public class BoatsController : Controller
    {
        private IBoatService _IBoatService;

        public BoatsController(IBoatService boatService)
        {
            this._IBoatService = boatService;
        }

        [HttpGet("{BoatRadioSignalId}")]
        public IActionResult Get(string BoatRadioSignalId)
        {
            Console.WriteLine("ctrl");
            var result = this._IBoatService.GetBoat(BoatRadioSignalId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}