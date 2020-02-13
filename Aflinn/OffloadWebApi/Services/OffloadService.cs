using System;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Repository;

namespace OffloadWebApi.Services
{
    public class OffloadService : IOffloadService
    {
        private IOffloadRepo _offloadRepo;

        public OffloadService(IOffloadRepo offloadRepo)
        {
            this._offloadRepo = offloadRepo;
        }

        public OffloadDetailDto GetOffloadById(int id)
        {
            return this._offloadRepo.GetOffloadById(id);
        }

        public OffloadDto GetOffloads()
        {
            // TODO þarf að láta taka inn query param og endur hugsa dto
            throw new NotImplementedException();
        }
    }
}
