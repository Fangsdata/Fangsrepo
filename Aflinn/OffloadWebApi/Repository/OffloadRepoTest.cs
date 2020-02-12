﻿using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Repository
{
    public class OffloadRepoTest : IOffloadRepo
    {
        public OffloadRepoTest()
        {
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
    }
}
