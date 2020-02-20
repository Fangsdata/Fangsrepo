using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Repository;

namespace OffloadWebApi.Services
{
    public class BoatService : IBoatService
    {
        private IOffloadRepo _offloadRepo;

        public BoatService(IOffloadRepo offloadRepo)
        {
            this._offloadRepo = offloadRepo;
        }
        #nullable enable
        public BoatDto? GetBoat(string BoatRadioSignalId)
        {
            var boat = this._offloadRepo.GetBoatByRadioSignal(BoatRadioSignalId);
            return boat;
        }
    }
}