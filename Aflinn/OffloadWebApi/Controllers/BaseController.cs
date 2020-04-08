using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OffloadWebApi.Controllers
{
    [ApiController]
    [Route("")]
    public class baseController : Controller
    {
        public ContentResult get()
        {
            string content = System.IO.File.ReadAllText(@"wwwroot/index.html");
            return this.Content(content, "text/html");
        }
    }
}