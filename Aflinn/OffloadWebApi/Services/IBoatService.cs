using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public interface IBoatService
    {
        #nullable enable
        BoatDto? GetBoat(string BoatRadioSignalId);
        List<BoatSimpleDto>? SearchForBoat(string boatSearchTerm);
    }
}