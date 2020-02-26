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

        // GET: api/values
        // query params : count fishingGear boatLength landingTown
        // landingState month year
        [HttpGet]
        public IActionResult Get(
            [FromQuery]string count,
            [FromQuery]string[] fishingGear,
            [FromQuery]string[] boatLength,
            [FromQuery] string[] landingTown,
            [FromQuery] string[] landingState,
            [FromQuery] string[] month,
            [FromQuery] string[] year)
        {
            try
            {
                var boatLengthList = new List<string>();

                if (boatLengthList.Count > 2)
                {
                    return this.BadRequest();
                }
                else
                {
                   // boatLengthList.Append(Convert.ToDouble(boatLength[0]));
                   // boatLengthList.Append(Convert.ToDouble(boatLength[1]));
                }

                var monthList = new List<int>();
                if (month.Length > 12)
                {
                    return this.BadRequest();
                }
                else
                {
                    for (int i = 0; i < month.Length; i++)
                    {
                        monthList.Append(int.Parse(month[i]));
                    }
                }

                var yearList = new List<int>();
                if (month.Length > 100)
                {
                    return this.BadRequest();
                }
                else
                {
                    for (int i = 0; i < year.Length; i++)
                    {
                        int temp = int.Parse(year[i]);
                        if (temp <= DateTime.Now.Year || temp >= 2000)
                        {
                            yearList.Append(temp);
                        }
                        else
                        {
                            return this.BadRequest();
                        }
                    }
                }

                if (count == null)
                {
                    count = "5";
                }
                
                var filters = new QueryOffloadsInput
                {
                    Count = int.Parse(count),
                    FishingGear = new List<string>(fishingGear),
                    BoatLength = new List<string>(boatLength),
                    LandingState = new List<string>(landingState),
                    LandingTown = new List<string>(landingTown),
                    Month = monthList,
                    Year = yearList,
                };
                var result = this._OffloadService.GetOffloads(filters);
                if (result == null)
                {
                    return this.BadRequest();
                }

                return this.Ok(result);
            }
            catch (FormatException)
            {
                return this.BadRequest();
            }
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
