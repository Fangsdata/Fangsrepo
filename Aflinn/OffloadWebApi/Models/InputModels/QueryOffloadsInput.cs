using System;
using System.Collections.Generic;

namespace OffloadWebApi.Models.InputModels
{
    public class QueryOffloadsInput
    {
        #nullable enable
        public int Count { get; set; }
        #nullable enable
        public List<string>? FishingGear { get; set; }
        #nullable enable
        public List<double>? BoatLength { get; set; }
        #nullable enable
        public List<string>? LandingTown { get; set; }
        #nullable enable
        public List<string>? LandingState { get; set; }
        #nullable enable
        public List<int>? Month { get; set; }
        #nullable enable
        public List<int>? Year { get; set; }
    }
}
