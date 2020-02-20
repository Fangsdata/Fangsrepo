using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public interface IBoatService
    {
        #nullable enable
        BoatDto? GetBoat(string BoatRadioSignalId);
    }
}