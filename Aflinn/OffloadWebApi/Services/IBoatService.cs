using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public interface IBoatService
    {
        BoatDto? GetBoat(string BoatRadioSignalId);
    }
}