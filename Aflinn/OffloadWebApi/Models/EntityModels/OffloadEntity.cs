using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Models.EntityModels
{
    public class OffloadEntity
    {
        public string Id { get; set; }

        public string Town { get; set; }

        public string State { get; set; }

        public DateTime LandingDate { get; set; }

        public float TotalWeight { get; set; }

        public List<FishDto> Fish { get; set; }

        public BoatSimpleDto Boat { get; set; }    
    }
}