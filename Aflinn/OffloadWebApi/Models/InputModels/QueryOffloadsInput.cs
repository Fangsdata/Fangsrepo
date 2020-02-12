using System;
using System.Collections.Generic;

namespace OffloadWebApi.Models.InputModels
{
    public class QueryOffloadsInput
    {
        public int Count { get; set; }

        public List<string> FishingGear { get; set; }

        public List<double> BoatLength { get; set; }

        public List<string> LandingTown { get; set; }

        public List<string> LandingState { get; set; }

        public List<int> Month { get; set; }

        public List<int> Year { get; set; }
    }
}
