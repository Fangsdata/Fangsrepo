using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Services
{
    public interface IOffloadService
    {
        public List<TopListDto> GetOffloads(QueryParamsForTopList filters);

        public OffloadDetailDto GetOffloadById(int id);
    }
}
