using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Services
{
    public interface IOffloadService
    {
        List<TopListDto> GetOffloads(QueryParamsForTopList filters);

        List<OffloadDto> GetOffloadById(string radioSignal, int count);
    }
}
