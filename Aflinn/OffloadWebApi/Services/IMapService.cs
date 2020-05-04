using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public interface IMapService
    {
        List<MapDataDto> GetMapDataByRadioSignal(string radioSignal);
    }
}