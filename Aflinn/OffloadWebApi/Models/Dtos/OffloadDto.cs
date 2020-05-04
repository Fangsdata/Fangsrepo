using System;
using System.Collections.Generic;

namespace OffloadWebApi.Models.Dtos
{
    public class OffloadDto
    {
        public string Id { get; set; }

        public string Town { get; set; }

        public string State { get; set; }

        public DateTime? LandingDate { get; set; }

        public float TotalWeight { get; set; }

        public List<FishDto> Fish { get; set; }

        public BoatSimpleDto Boat { get; set; }

        public List<MapDataDto> MapData { get; set; }
    }
}
