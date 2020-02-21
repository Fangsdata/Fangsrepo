using System.Threading.Tasks;
using OffloadWebApi.Models.EntityModels;
using Microsoft.AspNetCore.Mvc;
using OffloadWebApi.Repository;

namespace OffloadWebApi.Controllers
{
    [Route("api/[controller]")]
    public class GetQuery1Controller : ControllerBase
    {
        public GetQuery1Controller(AppDb db)
        {
            this.Db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatest()
            {
                await this.Db.Connection.OpenAsync();
                var query = new GetQuery1(this.Db);
                var result = await query.GetAsync();
                return new OkObjectResult(result);
            }

        public AppDb Db { get; }
    }
}
