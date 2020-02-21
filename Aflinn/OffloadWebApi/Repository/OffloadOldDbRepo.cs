using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public class OffloadOldDbRepo : IOffloadRepo
    {
        public BoatDto GetBoatByRadioSignal(string BoatRadioSignalId)
        {
            throw new System.NotImplementedException();
        }

        public List<TopListDto> GetFilteredResults(QueryOffloadsInput filters)
        {
            throw new System.NotImplementedException();
        }

        public OffloadDetailDto GetOffloadById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}