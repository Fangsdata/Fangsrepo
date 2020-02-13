using System;
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
        public void CheckIfValidIdReturnsId()
        {
            var result = _OffloadService.GetOffloadById(1);
            Assert.Equal("Valdimar H", result.Boat.Name);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.Fish[0].Id);
        }
        [Fact]
        public void CheckIfValidIdReturnsNull()
        {
            var result = _OffloadService.GetOffloadById(33);

            Assert.Null(result);
        }
    }
}
