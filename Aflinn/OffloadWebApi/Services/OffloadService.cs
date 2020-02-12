using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
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

        public List<OffloadDto>? GetOffloads(QueryOffloadsInput filters)
        {
            if(filters.Count > 500)
            {
                return null;
            }

            return this._offloadRepo.GetFilteredResults(filters);
        }
    }
}
