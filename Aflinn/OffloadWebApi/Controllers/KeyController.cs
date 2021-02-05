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
    public class KeyController : ControllerBase
    {
        private IKeyValueService _KeyValueService;

        public KeyController(IKeyValueService KeyValueService)
        {
            this._KeyValueService = KeyValueService;
        }
        [HttpGet("{value}")]
        public IActionResult Get(string value)
        {
            var result = this._KeyValueService.GetValue(value);
            if (result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }
    }
}