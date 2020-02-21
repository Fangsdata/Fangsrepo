using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public interface IOffloadRepo
    {
        public OffloadDetailDto GetOffloadById(int id);

        public List<TopListDto> GetFilteredResults(QueryOffloadsInput filters);
        #nullable enable
        public BoatDto? GetBoatByRadioSignal(string BoatRadioSignalId);
    }
}