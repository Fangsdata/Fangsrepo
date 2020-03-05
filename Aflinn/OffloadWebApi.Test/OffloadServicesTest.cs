using System;
using System.Collections.Generic;
using OffloadWebApi.Models.InputModels;
using OffloadWebApi.Repository;
using OffloadWebApi.Services;
using Xunit;

namespace OffloadWebApi.Test
{
    public class OffloadServicesTest
    {
        private IOffloadService _OffloadService;

        public OffloadServicesTest()
        {
            var testRepo = new OffloadRepoTest();
            this._OffloadService = new OffloadService(testRepo);
        }
        [Fact]
        public void CheckIfGetOffloadByIdReturns()
        {
            var result = _OffloadService.GetOffloadById(1);
            Assert.NotNull(result);

            var result2 = _OffloadService.GetOffloadById(33);

            Assert.Null(result2);
        }
        [Fact]
        public void GetOffloadsTestInputCount()
        {
            var queryInput = new QueryParamsForTopList
            {
                count = "10"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(10,result.Count);

            queryInput.count = "501";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(500,result.Count);

            queryInput.count = "0";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(10,result.Count); 

            queryInput.count = "-1";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result); 

            queryInput.count = "Not a number";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result); 

            queryInput.count = "10.00";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);

            queryInput.count = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(10,result.Count); 

        }
        [Fact]
        public void GetOffloadsTestInputFishingGear()
        {
            var queryInput = new QueryParamsForTopList
            {
                fishingGear = "Line"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput.fishingGear = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput = new QueryParamsForTopList
            {
                fishingGear = "Line,troll"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            var fishingGear = new List<string>();
            for (int i = 0; i < result.Count ;i++)
            {
                fishingGear.Add(result[i].BoatFishingGear);
            }
            Assert.Contains("troll",fishingGear);
            Assert.Contains("line",fishingGear);
        }
        [Fact]
        public void GetOffloadsTestInputBoatLength()
        {
            var queryInput = new QueryParamsForTopList
            {
                boatLength = "11m - 14,99m"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.InRange(result[0].BoatLength,11,14.99);

            queryInput.boatLength = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput = new QueryParamsForTopList
            {
                boatLength = "11,14.99"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);
            Assert.InRange(result[0].BoatLength,10,14.99);

        }
        [Fact]
        public void GetOffloadsTestInputLandingTown()
        {
            var queryInput = new QueryParamsForTopList
            {
                landingTown = "Oslo"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);
            Assert.Equal(result[0].boatLandingTown, "oslo");

            queryInput.landingTown = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput = new QueryParamsForTopList
            {
                landingTown = "Oslo,Reykjavík"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            var landingTown = new List<string>();
            for (int i = 0; i < result.Count ;i++)
            {
                landingTown.Add(result[i].boatLandingTown);
            }
            Assert.Contains("oslo",landingTown);
            Assert.Contains("reykjavík",landingTown);
        }
        [Fact]
        public void GetOffloadsTestInputLandingState()
        {
            var queryInput = new QueryParamsForTopList
            {
                landingState = "Ohio"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);
            Assert.Equal(result[0].boatLandingState, "ohio");

            queryInput.landingState = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput = new QueryParamsForTopList
            {
                landingState = "Oslo,Reykjavík"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            var landingState = new List<string>();
            for (int i = 0; i < result.Count ;i++)
            {
                landingState.Add(result[i].boatLandingState);
            }
            Assert.Contains("oslo",landingState);
            Assert.Contains("reykjavík",landingState);
        }
        [Fact]
        public void GetOffloadsTestInputMonth()
        {
            var queryInput = new QueryParamsForTopList
            {
                month = "Januar"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result[0].LandingDate.Month, 1);

            queryInput.month = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput = new QueryParamsForTopList
            {
                month = "1"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result[0].LandingDate.Month, 1);

            queryInput = new QueryParamsForTopList
            {
                month = "1,Februar,3"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            var testMonth = new List<int>();
            for (int i = 0; i < result.Count ;i++)
            {
                testMonth.Add(result[i].LandingDate.Month);
            }
            Assert.Contains(1,testMonth);
            Assert.Contains(2,testMonth); 
            Assert.Contains(3,testMonth); 


            queryInput = new QueryParamsForTopList
            {
                month = "13"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);

            queryInput = new QueryParamsForTopList
            {
                month = "0"
            };

            queryInput = new QueryParamsForTopList
            {
                month = "-1"
            };

            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);
                       queryInput = new QueryParamsForTopList
            {
                month = "Ekki tala"
            };

            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);
        }
        [Fact]
        public void GetOffloadsTestInputYear()
        {
            var queryInput = new QueryParamsForTopList
            {
                year = "2020"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result[0].LandingDate.Year, 2020);

            queryInput.year = "";
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            queryInput = new QueryParamsForTopList
            {
                year = "2020,2019"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.NotNull(result);

            var testYear = new List<int>();
            for (int i = 0; i < result.Count ;i++)
            {
                testYear.Add(result[i].LandingDate.Year);
            }
            Assert.Contains(2019,testYear);
            Assert.Contains(2020,testYear); 

            queryInput = new QueryParamsForTopList
            {
                year = "3000"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);

            queryInput = new QueryParamsForTopList
            {
                year = "1999"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);

            queryInput = new QueryParamsForTopList
            {
                year = "-1"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);

            queryInput = new QueryParamsForTopList
            {
                year = "Ekki tala"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Null(result);
        }
        [Fact]
        public void GetOffloadsTestInputMultipleInput()
        {
            var queryInput = new QueryParamsForTopList
            {
                count = "50",
                fishingGear = "troll"
            };
            var result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result.Count, 50);
            Assert.Equal(result[0].BoatFishingGear, "troll");

            queryInput = new QueryParamsForTopList
            {
                count = "50",
                fishingGear = "troll",
                boatLength = "0,11"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result.Count, 50);
            Assert.Equal(result[0].BoatFishingGear, "troll");
            Assert.InRange(result[0].BoatLength,0,11);

            queryInput = new QueryParamsForTopList
            {
                count = "50",
                fishingGear = "troll",
                boatLength = "0,11",
                landingTown = "oslo"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result.Count, 50);
            Assert.Equal(result[0].BoatFishingGear, "troll");
            Assert.InRange(result[0].BoatLength,0,11);
            Assert.Equal(result[0].boatLandingTown, "oslo");

            queryInput = new QueryParamsForTopList
            {
                count = "50",
                fishingGear = "troll",
                boatLength = "0,11",
                landingTown = "oslo",
                landingState = "ohio"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result.Count, 50);
            Assert.Equal(result[0].BoatFishingGear, "troll");
            Assert.InRange(result[0].BoatLength,0,11);
            Assert.Equal(result[0].boatLandingTown, "oslo");
            Assert.Equal(result[0].boatLandingState, "ohio");

            queryInput = new QueryParamsForTopList
            {
                count = "50",
                fishingGear = "troll",
                boatLength = "0,11",
                landingTown = "oslo",
                landingState = "ohio",
                month = "1"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result.Count, 50);
            Assert.Equal(result[0].BoatFishingGear, "troll");
            Assert.InRange(result[0].BoatLength,0,11);
            Assert.Equal(result[0].boatLandingTown, "oslo");
            Assert.Equal(result[0].boatLandingState, "ohio");
            Assert.Equal(result[0].LandingDate.Month, 1);


            queryInput = new QueryParamsForTopList
            {
                count = "50",
                fishingGear = "troll",
                boatLength = "0,11",
                landingTown = "oslo",
                landingState = "ohio",
                month = "1",
                year = "2020"
            };
            result = _OffloadService.GetOffloads(queryInput);
            Assert.Equal(result.Count, 50);
            Assert.Equal(result[0].BoatFishingGear, "troll");
            Assert.InRange(result[0].BoatLength,0,11);
            Assert.Equal(result[0].boatLandingTown, "oslo");
            Assert.Equal(result[0].boatLandingState, "ohio");
            Assert.Equal(result[0].LandingDate.Month, 1);
            Assert.Equal(result[0].LandingDate.Year, 2020);
        }
    }
}
