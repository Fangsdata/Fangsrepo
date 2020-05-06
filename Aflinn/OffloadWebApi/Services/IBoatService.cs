using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public interface IBoatService
    {
        #nullable enable
        BoatDto? GetBoatByRadio(string BoatRadioSignalId);
        #nullable enable
        BoatDto? GetBoatByRegistration(string BoatRadioSignalId);
        #nullable enable
        List<BoatSimpleDto>? SearchForBoat(string boatSearchTerm, int count, int pageNo);
    }
}