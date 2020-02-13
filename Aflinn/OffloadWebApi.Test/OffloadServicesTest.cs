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
        public void CheckOffloadServiceReturnCorrectAmount()
        {
            var query = new QueryOffloadsInput {
                Count = 0,
            };

            var result = _OffloadService.GetOffloads(query);
            Assert.Equal(5, result.Count);


            query.Count = 10000;
            result = _OffloadService.GetOffloads(query);
            Assert.Null(result);

            query.Count = 20;
            result = _OffloadService.GetOffloads(query);
            Assert.NotNull(result);
            Assert.Equal(20, result.Count);
        }
        [Fact]
        public void CheckIfFilteredDataReturnsCorrect()
        {
            var towns = new List<string>{"krokavik"};

            var query = new QueryOffloadsInput
            {
                LandingTown = towns,
            };

            var result = _OffloadService.GetOffloads(query);
            Assert.Equal("krokavik", result[0].Town);
        }
    }
}
