using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public class OffloadRepoTest : IOffloadRepo
    {
        public OffloadRepoTest()
        {
        }
        
        #nullable enable
        public BoatDto? GetBoatByRadioSignal(string BoatRadioSignalId)
        {
            if (BoatRadioSignalId == "not_radioXXX")
            {
                return null;
            }

            return new BoatDto
            {
                Id = 1,
                RegistrationId = "gk-123",
                RadioSignalId = BoatRadioSignalId,
                Name = "bubbi bátur",
                State = "Stebba fylki",
                Town = "Grimdavik",
                Length = 21.2,
                Weight = 2000,
                BuiltYear = 1992,
                EnginePower = 1200,
                FishingGear = "lalaBlabla",
            };
        }

        public OffloadDetailDto? GetOffloadById(int id)
        {
            if (id != 1)
            {
                return null;
            }

            var testFish = new List<FishDto>();

            testFish.Add(new FishDto
            {
                Id = 1,
                Application = "Mel og olje",
                Condition = "Rund",
                Packaging = "tank/båt",
                Quality = "Uspesifisert",
                Preservation = "Rsw",
                Type = "Sei",
                Weight = 500,
            });
            testFish.Add(new FishDto
            {
                Id = 2,
                Application = "Mel og olje",
                Condition = "Rund",
                Packaging = "tank/båt",
                Quality = "Uspesifisert",
                Preservation = "Rsw",
                Type = "Tosk",
                Weight = 1000,
            });
            testFish.Add(new FishDto
            {
                Id = 3,
                Application = "Mel og olje",
                Condition = "Rund",
                Packaging = "tank/båt",
                Quality = "Uspesifisert",
                Preservation = "Rsw",
                Type = "Hyse",
                Weight = 200,
            });
            testFish.Add(new FishDto
            {
                Id = 4,
                Application = "Mel og olje",
                Condition = "Rund",
                Packaging = "tank/båt",
                Quality = "Uspesifisert",
                Preservation = "Rsw",
                Type = "Leppefisk",
                Weight = 300,
            });

            var testMapData = new List<MapDataDto>();
            testMapData.Add(new MapDataDto
            {
                Latitude = 55.5467,
                Longitude = 3.28022,
                Time = new DateTime(2019, 1, 1, 7, 45, 0),
            });
            testMapData.Add(new MapDataDto
            {
                Latitude = 55.5430,
                Longitude = 3.28500,
                Time = new DateTime(2019, 1, 1, 7, 50, 0),
            });
            testMapData.Add(new MapDataDto
            {
                Latitude = 55.5000,
                Longitude = 3.28500,
                Time = new DateTime(2019, 1, 1, 7, 55, 0),
            });
            testMapData.Add(new MapDataDto
            {
                Latitude = 55.4900,
                Longitude = 3.28600,
                Time = new DateTime(2019, 1, 1, 8, 00, 0),
            });

            var testBoat = new BoatDto
            {
                Id = 1,
                Name = "Valdimar H",
                RadioSignalId = "LK5707",
                BuiltYear = 1990,
                EnginePower = 311,
                Image = "https://photos.marinetraffic.com/ais/showphoto.aspx?shipid=293459&size=thumb600",
                FishingGear = "Autoline",
                Length = 14.99,
                RegistrationId = "N 0006SF",
                State = "Rogaland",
                Weight = 4000,
                Town = "ALTA",
                MapData = testMapData,
            };

            var testData = new OffloadDetailDto
            {
                Id = id,
                Town = "Vikna",
                State = "Trøndelag",
                LandingDate = new DateTime(2019, 1, 1, 7, 47, 0),
                TotalWeight = 200000,
                Fish = testFish,
                MapData = testMapData,
                Boat = testBoat,
            };

            return testData;
        }

        List<TopListDto> IOffloadRepo.GetFilteredResults(QueryOffloadsInput filters)
        {
            if (filters.Month == null || filters.Month.Count == 0)
            {
                filters.Month = new List<int>();
                filters.Month.Add(10);
            }

            if (filters.Year == null || filters.Year.Count == 0)
            {
                filters.Year = new List<int>();

                filters.Year.Add(2020);
            }

            if (filters.LandingState == null || filters.LandingState.Count == 0)
            {
                filters.LandingState = new List<string>();
                filters.LandingState.Add("Suðurnes");
            }

            if (filters.LandingTown == null || filters.LandingTown.Count == 0)
            {
                filters.LandingTown = new List<string>();
                filters.LandingTown.Add("Grindavík");
            }

            if (filters.FishingGear == null || filters.FishingGear.Count == 0)
            {
                filters.FishingGear = new List<string>();
                filters.FishingGear.Add("nót");
            }

            if (filters.BoatLength == null || filters.BoatLength.Count == 0)
            {
                filters.BoatLength = new List<double>();
                filters.BoatLength.Add(10);
            }

            var testFish = new List<FishSimpleDto>();

            testFish.Add(new FishSimpleDto { Type = "Ýsa", TotalWeight = 500, Avrage = 300 });
            testFish.Add(new FishSimpleDto { Type = "Urriði", TotalWeight = 500, Avrage = 300 });
            testFish.Add(new FishSimpleDto { Type = "Hafmeyjur", TotalWeight = 500, Avrage = 300 });
            testFish.Add(new FishSimpleDto { Type = "Steinbítur", TotalWeight = 500, Avrage = 300 });

            var dummyItem = new TopListDto
            {
                Avrage = 500,
                TotalWeight = 5000,
                Trips = 10,
                BoatName = "Tommi togari",
                Town = filters.LandingTown[0],
                State = filters.LandingState[0],
                LandingDate = new DateTime(filters.Year[0], filters.Month[0], 1, 7, 47, 0),
                BoatNationality = "Norge",
                BoatRadioSignalId = "TT-123",
                BoatRegistrationId = "gk-123",
                Smallest = 400,
                LargestLanding = 1000,
                Fish = testFish,
                Id = 40,
            };

            var dummyData = new List<TopListDto>();

            for (int i = 0; i < filters.Count; i++)
            {
                dummyData.Add(dummyItem);
            }

            return dummyData;
        }
    }
}