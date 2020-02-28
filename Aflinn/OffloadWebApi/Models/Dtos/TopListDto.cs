using System;
using System.Collections.Generic;

namespace OffloadWebApi.Models.Dtos
{
    public class TopListDto
    {
        public int Id { get; set; }

        public string Town { get; set; }

        public string State { get; set; }

        public DateTime LandingDate { get; set; }

        public double TotalWeight { get; set; }

        public List<FishSimpleDto> Fish { get; set; }

        public string BoatRegistrationId { get; set; }

        public string BoatRadioSignalId { get; set; }

        public string BoatName { get; set; }

        public string BoatNationality { get; set; }
        public string AverageTrips { get; set; }
        public double Average { get; set; }

        public double LargestLanding { get; set; }

        public double Smallest { get; set; }

        public int Trips { get; set; }
        
        // public string FishingGear { get; set; }
        
        // public string BoatLength { get; set; }
        
        // public string FishName { get; set; }
    }
}