using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Models.EntityModels
{
    public class OffloadEntity
    {
        public int Id { get; set; }

        public string Town { get; set; }

        public string State { get; set; }

        public DateTime LandingDate { get; set; }

        public int TotalWeight { get; set; }

        public List<FishSimpleDto> Fish { get; set; }

        public BoatSimpleDto Boat { get; set; }    
    }
}