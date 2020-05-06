using System;
using System.Collections.Generic;
using OffloadWebApi.Models.InputModels;
using OffloadWebApi.Repository;
using OffloadWebApi.Services;
using Xunit;

namespace OffloadWebApi.Test
{
    public class BoatServicesTest
    {
        private IBoatService _boatService;

        public BoatServicesTest()
        {
            var testRepo = new OffloadRepoTest();
            var mapService = new MapService();
            this._boatService = new BoatService(testRepo, mapService);
        }
        [Fact]
        public void CheckIfGetBoatReturnsBoatt(){
            
            var result = _boatService.GetBoatByRadio("radio123");
            Assert.Equal("radio123", result.RadioSignalId);

            result = _boatService.GetBoatByRadio("not_radioXXX");
            Assert.Null(result);
        }
    }
}