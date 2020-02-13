using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffloadWebApi.Models.Dtos;
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
        [HttpGet]
        public IActionResult Get()
        {
            var result = this._OffloadService.GetOffloads();
            return this.Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this._OffloadService.GetOffloadById(id);
            return this.Ok(result);
        }
    }
}
