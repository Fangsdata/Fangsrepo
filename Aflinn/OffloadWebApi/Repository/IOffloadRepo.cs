using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public interface IOffloadRepo
    {
        OffloadDetailDto GetOffloadById(int id);
        List<TopListDto> GetFilteredResults(QueryOffloadsInput filters);
        #nullable enable
        BoatDto? GetBoatByRadioSignal(string BoatRadioSignalId);

        List<OffloadDto> GetLastOffloadsFromBoat(string BoatRadioSignalId, int count);

        OffloadDto GetSingleOffload(string offloadId);
    }
}