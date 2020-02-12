using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public interface IOffloadRepo
    {
        public OffloadDetailDto? GetOffloadById(int id);

        List<OffloadDto> GetFilteredResults(QueryOffloadsInput filters);
    }
}