﻿using System;
using System.Collections.Generic;

namespace OffloadWebApi.Models.Dtos
{
    public class OffloadDto
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